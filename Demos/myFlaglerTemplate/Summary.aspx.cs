using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication_Test2025.Demos.MyFlagler
{
    public partial class Summary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
             IsPostBack is a property in ASP.NET Web Forms that tells you whether 
            the page is loading for the first time or being reloaded 
            (like after a button click, form submit, etc.).

            !IsPostBack means "if this is NOT a postback" — in other words, 
            if the page is loading for the very first time.
             */
            if (!IsPostBack)
            {
                LoadSummary();
            }
        }

        private void LoadSummary()
        {
            string connStr = ConfigurationManager.ConnectionStrings["PersonApp"].ConnectionString;

            /*
             using (...) is a special C# statement that automatically manages 
            resources for you by ensuring that objects like database connections, 
            file streams, or network sockets are properly closed and disposed of 
            after use. This helps prevent resource leaks, improves application 
            performance, and ensures that critical system resources are not left 
            hanging even if an error occurs.
             */


            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // SQL query to retrieve a combined view of Persons, including related info for Students,
                // Professors, and Staff.

                // Uses LEFT JOINs to include all persons even if they are not associated
                // with a particular role.
                string query = @"
                    SELECT 
                        p.PersonID,
                        p.Name,
                        p.ID AS CampusID,
                        p.Email,
                        p.PersonType,
                        s.Major, s.GPA, s.IsFullTime, s.EnrollmentDate,
                        pr.Department, pr.ResearchArea, pr.IsTerminalDegree,
                        st.Position, st.Division, st.IsAdministrative
                    FROM Persons p
                    LEFT JOIN Students s ON p.PersonID = s.PersonID
                    LEFT JOIN Professors pr ON p.PersonID = pr.PersonID
                    LEFT JOIN Staff st ON p.PersonID = st.PersonID
                    ORDER BY p.PersonID ASC;";

                // Create a data adapter to execute the query and fill a DataTable with the results.
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                // Create an in-memory table to hold the query results.
                DataTable table = new DataTable();

                // Fill the DataTable with data from the SQL query.
                adapter.Fill(table);

                // Bind the populated DataTable to the GridView control for display.
                gvSummary.DataSource = table;
                gvSummary.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSummary(txtSearch.Text.Trim());
        }


        private void LoadSummary(string keyword = "")
        {
            string connStr = ConfigurationManager.ConnectionStrings["PersonApp"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //Uses LEFT JOINs so that each person appears once,
                //even if they're only a member of one role (Student/Professor/Staff).
                //is a verbatim string literal in C#. It tells the compiler to treat the
                //string exactly as it's written, without interpreting escape sequences like \n, \t, or \\.
                string query = @"
            SELECT 
                p.PersonID,
                p.Name,
                p.ID AS CampusID,
                p.Email,
                p.PersonType,
                s.Major, s.GPA, s.IsFullTime, s.EnrollmentDate,
                pr.Department, pr.ResearchArea, pr.IsTerminalDegree,
                st.Position, st.Division, st.IsAdministrative
            FROM Persons p
            LEFT JOIN Students s ON p.PersonID = s.PersonID
            LEFT JOIN Professors pr ON p.PersonID = pr.PersonID
            LEFT JOIN Staff st ON p.PersonID = st.PersonID
            WHERE (@Keyword = '' OR p.Name LIKE '%' + @Keyword + '%' OR p.Email LIKE '%' + @Keyword + '%')
            ORDER BY p.PersonID ASC;";

                /*If no search keyword is provided (@Keyword = ''), it shows all records.
                  If a keyword is provided:
                        It searches Name and Email using LIKE, allowing partial matches.
                */


                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@Keyword", keyword);

                DataTable table = new DataTable();
                adapter.Fill(table);

                gvSummary.DataSource = table;
                gvSummary.DataBind();
            }
        }

    }
}