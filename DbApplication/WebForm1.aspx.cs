using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace DbApplication
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            OracleConnection conn = null;

            try
            {

                conn = createOracleConnection();

                conn.Open();

                Label1.Text = "成功建立";
            }
            catch(Exception ex1)
            {
                Label1.Text = ex1.Message;
            }
            finally
            {
                try
                {
                    conn.Close();
                }
                catch(Exception ex)
                {
                    Label1.Text += ex.Message;
                }
                
            }
            


        }

        private  OracleConnection createOracleConnection()
        {
            OracleConnection conn = null;
            string connString = "Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.2.60)(PORT = 1521)))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = ORCL))); User ID = COA2; Password = COA2; Persist Security Info = false; pooling = true; Min Pool Size = 1; Max Pool Size = 10; Connection Lifetime = 0";

            throw new Exception("連線字串有問題");

            conn = new OracleConnection(connString);

            return conn;
        }
    }
}