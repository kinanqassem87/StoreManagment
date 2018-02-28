using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace StoreManagment
{
    public partial class FRM_AddNewCat : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        OleDbCommand cmd;
        public FRM_AddNewCat()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (txtCatName.Text.Equals(""))
            {
                MessageBox.Show("الرجاء ادخال اسم الصنف");
            }
            else 
            {
                try
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select Cat_Name from Category where Cat_Name='"+txtCatName.Text+"'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    string s = dt.Rows[0][0].ToString();
                    if (dt.Rows.Count > 0) 
                    {
                        MessageBox.Show("هذا موجود مسبقا");
                        return;
                    }
                }
                catch (Exception ex) 
                {
                    con.Open();
                    cmd = new OleDbCommand("insert into Category (Cat_Name) values ('" + txtCatName.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تمت العملية بنجاح");
                    txtCatName.Text = "";
                    //MessageBox.Show("الرجاء التأكد من الاتصال بقاعدة البيانات");
                }
                
            }
        }
    }
}
