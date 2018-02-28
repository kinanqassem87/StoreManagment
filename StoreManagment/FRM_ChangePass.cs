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
    public partial class FRM_ChangePass : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_ChangePass()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCurrPass.Text.Equals("") || txtnew.Text.Equals("") || txtconf.Text.Equals(""))
                {
                    MessageBox.Show("الرجاء ملىء جميع الحقول");
                }
                else
                {
                    if (txtCurrPass.Text == FRM_Menu.Password && txtnew.Text == txtconf.Text)
                    {
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("update Users set U_Password='" + txtnew.Text + "' where User_ID=" + FRM_Menu.User_ID + "", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        FRM_Menu.Password = txtnew.Text;
                        MessageBox.Show("تم تغيير كلمة المرور بنجاح");
                        txtconf.Text = txtCurrPass.Text = txtnew.Text = "";
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("المعلومات غير متطابقة");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("هناك خطأ في البيانات المدخلة");
            }
        }
    }
}
