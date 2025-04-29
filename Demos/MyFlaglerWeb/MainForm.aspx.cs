using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace CIS_305_Master_Web_Project.Demos.MyFlaglerWeb
{
    public partial class MainForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Initialize the states of the panels.
            pnlProfessor.Visible = false;
            pnlStudent.Visible = false;
            pnlStaff.Visible = false;
        }

        protected void rblPersonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblPersonType.SelectedValue == "Professor")
                pnlProfessor.Visible = true;
            else if (rblPersonType.SelectedValue == "Student")
                pnlStudent.Visible = true;
            else if (rblPersonType.SelectedValue == "Staff")
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

            if (rblPersonType.SelectedValue == "Professor")
                pnlProfessor.Visible = true;
            else if (rblPersonType.SelectedValue == "Student")
                pnlStudent.Visible = true;
            else if (rblPersonType.SelectedValue == "Staff")
                pnlStaff.Visible = true;
        }

        private Person CreatePerson()
        {
            Person person = null;
            if (rblPersonType.SelectedValue=="Professor")
            {
                //Option1: Using the default constructor for professor
                /*
                person = new Professor                
                {
                    Name = txtName.Text,
                    ID = txtID.Text,
                    Email = txtEmail.Text,
                    Department = ddlDepartment.Text,
                    ResearchArea = txtResearchArea.Text,
                    IsTerminalDegree = chkTerminalDegree.Checked,
                };    
                */
                
                //Option 2: Using custom constructor for Professor
                person = new Professor(
                    txtName.Text,
                    txtID.Text,
                    txtEmail.Text,
                    ddlDepartment.Text,
                    txtResearchArea.Text,
                    chkTerminalDegree.Checked
                );
            }
            else if (rblPersonType.SelectedValue== "Student")
            {
                //Option1: Using the default constructor for student
                /*
                person = new Student
                {
                    Name = txtName.Text,
                    ID = txtID.Text,
                    Email = txtEmail.Text,
                    Major = txtMajor.Text,
                    GPA = double.Parse(txtGPA.Text), //this is safe because we already did the validation.
                    IsFullTime = chkFullTime.Checked,
                    EnrollmentDate = Convert.ToDateTime(txtEnrollmentDate.Text)
                };
                */

                //Option 2: Using custom constructor for Professor
                person = new Student(
                     txtName.Text,
                     txtID.Text,
                     txtEmail.Text,
                     txtMajor.Text,
                     double.Parse(txtGPA.Text), 
                     chkFullTime.Checked,
                     Convert.ToDateTime(txtEnrollmentDate.Text)
                 );

            }
            else if (rblPersonType.SelectedValue == "Staff")
            {
                //Option1: Using the default constructor for staff
                /*
                person = new Staff
                {
                    Name = txtName.Text,
                    ID = txtID.Text,
                    Email = txtEmail.Text,
                    Position = txtPosition.Text,
                    Division = txtDivision.Text,
                    IsAdministrative = chkAdministrative.Checked
                };
                */

                //Option 2: Using custom constructor for staff
                person = new Staff(
                    txtName.Text,
                    txtID.Text,
                    txtEmail.Text,
                    txtPosition.Text,
                    txtDivision.Text,
                    chkAdministrative.Checked
                );
            }

            return person;

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

           // string connString = "Server=misapps.flagler.edu;Database=jwang;User ID=jwang;Password=CIS305;Trusted_Connection=False;MultipleActiveResultSets=True;";
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

                    int personID = (int)cmdPerson.ExecuteScalar(); //this returns the PersonID

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

                        cmd.ExecuteNonQuery();//Because we don't require any output back from SQL
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