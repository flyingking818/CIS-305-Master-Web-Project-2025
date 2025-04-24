using System;
using System.Configuration;
using System.Data.SqlClient;


namespace WebApplication_Test2025.Demos.MyFlagler
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlProfessor.Visible = false;
            pnlStudent.Visible = false;
            pnlStaff.Visible = false;
        }

        protected void rblPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblPerson.SelectedValue == "Professor")
                pnlProfessor.Visible = true;
            else if (rblPerson.SelectedValue == "Student")
                pnlStudent.Visible = true;
            else if (rblPerson.SelectedValue == "Staff")
                pnlStaff.Visible = true;
        }

        protected void btnDisplayProfile_Click(object sender, EventArgs e)
        {
            try
            {

                //Polymorphism: The Person variable can hold any subclass instance.
                //The CreatePerson() method acts like a factory,
                //creating the appropriate derived class based on some condition(radio button selection).
                Person person = CreatePerson();  //create a person object on demand!

                if (person == null) return;  //early exit if a lot of processes happen after this.

                lblResult.Text = person.GetDetails();

            }
            catch (Exception ex)
            {

                lblResult.Text = $"Error: {ex.Message}";
            }

            RefreshSelection();
        }

        private Person CreatePerson()
        {
            Person person = null;
            if (rblPerson.SelectedValue == "Professor")
            {
                person = new Professor
                {
                    Name = txtName.Text,
                    ID = txtID.Text,
                    Email = txtEmail.Text,
                    Department = ddlDepartment.Text,
                    ResearchArea = txtResearchArea.Text,
                    IsTerminalDegree = chkTerminalDegree.Checked,
                };
            }
            else if (rblPerson.SelectedValue == "Student")
            {
                person = new Student
                {
                    Name = txtName.Text,
                    ID = txtID.Text,
                    Email = txtEmail.Text,
                    Major = ddlMajor.Text,
                    GPA = double.Parse(txtGPA.Text), //this is safe because we already did the validation.
                    IsFullTime = chkFullTime.Checked,
                    EnrollmentDate = Convert.ToDateTime(txtEnrollmentDate.Text)
                };
            }
            else if (rblPerson.SelectedValue == "Staff")
            {
                person = new Staff
                {
                    Name = txtName.Text,
                    ID = txtID.Text,
                    Email = txtEmail.Text,
                    Position = txtPosition.Text,
                    Division = txtDivision.Text,
                    IsAdministrative = chkAdministrative.Checked
                };
            }

            return person;

        }

        private void RefreshSelection()
        {
            if (rblPerson.SelectedValue == "Professor")
                pnlProfessor.Visible = true;
            else if (rblPerson.SelectedValue == "Student")
                pnlStudent.Visible = true;
            else if (rblPerson.SelectedValue == "Staff")
                pnlStaff.Visible = true;
        }

        protected void btnAddProfile_Click(object sender, EventArgs e)
        {
            try
            {

                //Polymorphism: The Person variable can hold any subclass instance.
                //The CreatePerson() method acts like a factory,
                //creating the appropriate derived class based on some condition(radio button selection).
                Person person = CreatePerson();  //create a person object on demand!

                if (person == null) return;  //early exit if a lot of processes happen after this.

                InsertToDatabase(person);

                PanelForm.Visible = false;

            }
            catch (Exception ex)
            {

                lblResult.Text = $"Error: {ex.Message}";
            }
        }

        private void InsertToDatabase(Person person)
        {
            string connString = ConfigurationManager.ConnectionStrings["PersonApp"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // Insert into Persons table
                    SqlCommand cmdPerson = new SqlCommand(@"
                INSERT INTO Persons (Name, ID, Email, PersonType, CreatedAt)
                OUTPUT INSERTED.PersonID
                VALUES (@Name, @ID, @Email, @PersonType, @CreatedAt);", conn, transaction);

                    cmdPerson.Parameters.AddWithValue("@Name", person.Name);
                    cmdPerson.Parameters.AddWithValue("@ID", person.ID);
                    cmdPerson.Parameters.AddWithValue("@Email", person.Email);
                    cmdPerson.Parameters.AddWithValue("@PersonType", person.GetType().Name);
                    cmdPerson.Parameters.AddWithValue("@CreatedAt", DateTime.Now);  // current timestamp

                    int personID = (int)cmdPerson.ExecuteScalar();

                    // Insert into subtype table
                    if (person is Professor professor)
                    {
                        SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Professors (PersonID, Department, ResearchArea, IsTerminalDegree)
                    VALUES (@PersonID, @Department, @ResearchArea, @IsTerminalDegree);", conn, transaction);

                        cmd.Parameters.AddWithValue("@PersonID", personID);
                        cmd.Parameters.AddWithValue("@Department", professor.Department);
                        cmd.Parameters.AddWithValue("@ResearchArea", professor.ResearchArea);
                        cmd.Parameters.AddWithValue("@IsTerminalDegree", professor.IsTerminalDegree);

                        cmd.ExecuteNonQuery();
                    }
                    else if (person is Student student)
                    {
                        SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Students (PersonID, Major, GPA, IsFullTime, EnrollmentDate)
                    VALUES (@PersonID, @Major, @GPA, @IsFullTime, @EnrollmentDate);", conn, transaction);

                        cmd.Parameters.AddWithValue("@PersonID", personID);
                        cmd.Parameters.AddWithValue("@Major", student.Major);
                        cmd.Parameters.AddWithValue("@GPA", student.GPA);
                        cmd.Parameters.AddWithValue("@IsFullTime", student.IsFullTime);
                        cmd.Parameters.AddWithValue("@EnrollmentDate", student.EnrollmentDate);

                        cmd.ExecuteNonQuery();
                    }
                    else if (person is Staff staff)
                    {
                        SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Staff (PersonID, Position, Division, IsAdministrative)
                    VALUES (@PersonID, @Position, @Division, @IsAdministrative);", conn, transaction);

                        cmd.Parameters.AddWithValue("@PersonID", personID);
                        cmd.Parameters.AddWithValue("@Position", staff.Position);
                        cmd.Parameters.AddWithValue("@Division", staff.Division);
                        cmd.Parameters.AddWithValue("@IsAdministrative", staff.IsAdministrative);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    lblResult.Text += "<br/>Profile saved to database. <a href=\"Summary.aspx\">View Summary</a>";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblResult.Text += $"<br/>Database error: {ex.Message}";
                }
            }
        }

    }
}