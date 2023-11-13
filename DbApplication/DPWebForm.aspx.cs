using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DbApplication
{
    public partial class DPWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strConn = "Data Source=SQL8004.site4now.net;Initial Catalog=db_a8a4ae_atunas;User Id=db_a8a4ae_atunas_admin;Password=atunas123";
            using (var db = new SqlConnection(strConn))
            {
                try
                {
                    db.Open();

                    // 單一參數
                    //string strSql = "SELECT * FROM STUDENT WHERE STUDENTNO = @STUDENTNO";

                    //var students = db.Query<Student>(strSql, new { STUDENTNO = "A9201" });

                    // DBParameter
                    string strSql = "SELECT * FROM STUDENT WHERE STUDENTNO = @STUDENTNO";

                    var parm = new DynamicParameters();
                    parm.Add(name: "@STUDENTNO", value: "A9201", dbType: DbType.String, direction: ParameterDirection.Input);

                    var students = db.Query<Student>(strSql, parm);



                    // WHERE IN
                    //string strSql = "SELECT * FROM STUDENT WHERE STUDENTNO IN @STUDENTNO";

                    //var parms = new
                    //{
                    //    STUDENTNO = new[] { "A9201", "A9202" }
                    //};
                    //var students = db.Query<Student>(strSql, parms);




                    GridView1.DataSource = students;
                    GridView1.DataBind();
                }
                catch(Exception ex)
                {
                    Label1.Text = ex.Message;
                }
                finally
                {
                    if(db != null && db.State == ConnectionState.Open)
                        db.Close();
                }
               

            }
        }
    }
}