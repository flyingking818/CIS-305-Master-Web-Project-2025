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
            if (!IsPostBack)
            {
                LoadSummary();
            }
        }

        private void LoadSummary()
        {
            string connStr = ConfigurationManager.ConnectionStrings["PersonApp"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
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

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);

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