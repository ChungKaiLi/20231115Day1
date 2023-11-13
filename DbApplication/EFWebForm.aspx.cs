using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace DbApplication
{
    public partial class EFCodeFirstWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (var db = new db_a8a4ae_atunasEntities())
            {
                // 儲存物件
                var student = new Student { StudentNo = "A920", StudentName = "CK.LEE" };
                db.Student.Add(student);
                db.SaveChanges();

                // 查詢物件
                var query = from b in db.Student orderby b.StudentNo select b;

                GridView1.DataSource = query.ToList();
                GridView1.DataBind();
  

            }
        }
    }
}