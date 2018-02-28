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
    public partial class FRM_AddSup : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        OleDbCommand cmd;
        public FRM_AddSup()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtSupName.Text.Equals(""))
            {
                MessageBox.Show("الرجاء ادخال اسم المورد");
            }
            else
            {
                try
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select Sup_Name from Supplier where Sup_Name='" + txtSupName.Text + "'", con);
                    DataTable dt = new DataTable();
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
                    cmd = new OleDbCommand("insert into Supplier (Sup_Name,Phone) values ('" + txtSupName.Text + "','" + txtSupPhone.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تمت عملية الاضافة بنجاح");
                    txtSupName.Text = txtSupPhone.Text = "";
                    //MessageBox.Show("الرجاء التأكد من الاتصال بقاعدة البيانات");
                }
               
            }
        }
    }
}
