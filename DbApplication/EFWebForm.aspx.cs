using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text.RegularExpressions;
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
                //var student = new Student { StudentNo = "A920", StudentName = "CK.LEE" };

                //var student = new Student { StudentNo = "A920", StudentName = "CK.LEE" };
                Student student = new Student();
                student.StudentNo = "A920";
                student.StudentName = "CK.LEE";


                db.Student.Add(student);
                db.SaveChanges();

                // 查詢物件
                var query = from b in db.Student select b;
                // SELECT  * FROM STUDENT ORDER BY STUDENT

                //var resultSet = query.ToList().Where(w => w.StudentNo == "A920").Sum(s=> s.Id);
                //int sum = query.ToList().Where(w => w.StudentNo == "A920").Sum(s => s.Id);

                string [] a = { "0910000000", "a23456","0911111111","0910111111A" };
                var a1 = a.Where(w => isValidMobileNo(w)).Select(w => w.ToString());

                Label1.Text = a1.ToList().ToString();

                //Label1.Text= sum.ToString();
                //List<Student> list = new List<Student>();

                //foreach (var b in query)
                //{
                //    if(b.StudentNo == "A920")
                //        list.Add(b);
                //}


                //GridView1.DataSource = resultSet;
                //GridView1.DataBind();
  

            }

           
        }

        private bool isValidMobileNo(string mobileNo)
        {
            // 正則表達式
            string pattern = @"^09\d{8}$";

            // 使用 Regex 類別進行比對
            if (Regex.IsMatch(mobileNo, pattern))
            {
                return true;
        
            }
            else
            {
                return false;
            }
        }
    }
}