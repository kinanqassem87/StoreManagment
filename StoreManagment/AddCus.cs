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
    
    public partial class AddCus : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        OleDbCommand cmd;
        public AddCus()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtCusName.Text.Equals(""))
            {
                MessageBox.Show("الرجاء ادخال اسم العميل");
            }
            else 
            {
                try
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select Cus_Name from Customer where Cus_Name='"+txtCusName.Text+"'", con);
                    DataTable dt=new DataTable();
                    da.Fill(dt);
                    string s = dt.Rows[0][0].ToString();
                    if (dt.Rows.Count > 0) 
                    {
                        MessageBox.Show("هذا الاسم موجود مسبقا");
                        return;
                    }

                }
                catch (Exception ex)
                {
                    con.Open();
                    cmd = new OleDbCommand("insert into Customer (Cus_Name,Phone) values ('" + txtCusName.Text + "','" + txtCusPhone.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تمت عملية الاضافة بنجاح");
                    txtCusName.Text = txtCusPhone.Text = "";
                    //MessageBox.Show("الرجاء التأكد من الاتصال بقاعدة البيانات");
                }
                
                
            }
        }
    }
}
