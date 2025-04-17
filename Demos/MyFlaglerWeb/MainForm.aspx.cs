using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            else if (rblPersonType.SelectedValue== "Student")
            {
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
            }
            else if (rblPersonType.SelectedValue == "Staff")
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
    }
}