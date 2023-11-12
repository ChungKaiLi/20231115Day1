using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Runtime.Remoting.Messaging;
using System.Collections.ObjectModel;

namespace DbApplication
{
    public partial class AdoNetWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void getDataByDataReader()
        {

        }

        /// <summary>
        /// 建立資料庫連線
        /// </summary>
        /// <returns></returns>
        protected OracleConnection CreateConnection()
        {
            OracleConnection connection = new OracleConnection();
            //connection.ConnectionString = "Data Source=XE;User ID=COA2;Password=COA2;Persist Security Info=false;pooling=true;Min Pool Size=1;Max Pool Size=40;Connection Lifetime=0";
            connection.ConnectionString = "Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.2.60)(PORT = 1521)))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = ORCL))); User ID = COA2; Password = COA2; Persist Security Info = false; pooling = true; Min Pool Size = 1; Max Pool Size = 10; Connection Lifetime = 0";
            return connection;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            OracleConnection connection = CreateConnection();
            try
            {
                connection.Open();
                Label1.Text = "連線成功";
            }
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }
            finally
            {
                connection.Close();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            using (OracleConnection conn = CreateConnection())
            {
                conn.Open();

                //===========================================================
                // ExecuteNonQuery
                //===========================================================
                //OracleCommand command1 = conn.CreateCommand();
                //command1.CommandType = CommandType.Text;
                //command1.CommandText = "Insert INTO T_TEST(COL1,COL2,COL3) Values ('1','2','3')";
                //command1.ExecuteNonQuery();

                //===========================================================
                // ExecuteScalar
                //===========================================================
                OracleCommand command2 = conn.CreateCommand();
                command2.CommandType = CommandType.Text;
                command2.CommandText = "SELECT COL1 FROM T_TEST";
                string col1 = command2.ExecuteScalar().ToString();
                Label2.Text = col1;

            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            using (OracleConnection conn = CreateConnection())
            {
                conn.Open();
                //===========================================================
                // ExecuteReader
                //===========================================================
                OracleCommand command3 = conn.CreateCommand();
                command3.CommandType = CommandType.Text;
                command3.CommandText = "SELECT * From T_TEST";

                DbDataReader oReader = command3.ExecuteReader();
                //將 DataReader 轉成 DataTable 
                DataTable dTable = new DataTable();
                dTable.Load(oReader);
                Label3.Text = dTable.Rows.Count.ToString();

                //int count = 0;
                //順向逐筆讀取
                //while (oReader.Read())
                //{
                //    count++;

                //    Label4.Text += oReader["Col1"].ToString() + " ";

                //}
                //Label3.Text = count.ToString();



                GridView1.DataSource = dTable;
                GridView1.DataBind();
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {

            using (OracleConnection conn = CreateConnection())
            {
                conn.Open();
                OracleTransaction transaction;
                transaction = conn.BeginTransaction();

                try
                {
                    OracleCommand cmd = new OracleCommand();

                    //必須將 Connection 和 Transaction 二個物件都指定給 Command 物件
                    cmd.Connection = conn;
                    cmd.Transaction = transaction;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Insert INTO T_TEST(COL1,COL2,COL3) Values ('7','8','9')";
                    cmd.ExecuteNonQuery();

                    throw new Exception("交易錯誤");
                    cmd.CommandText = "Insert INTO T_TEST(COL1,COL2,COL3) Values ('4','5','6')";
                    cmd.ExecuteNonQuery();

                    transaction.Commit();       //commit the transaction
                }
                catch(Exception ex)
                {
                    transaction.Rollback();     //rollback the transaction

                    Label5.Text = ex.Message;
                }
            }
        }
    }
}