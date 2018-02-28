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
 
    public partial class FRM_AddNewUser : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_AddNewUser()
        {
            InitializeComponent();
            fullcmb();

        }
        void fullcmb() 
        {
            try
            {
                    cmbPer.Items.Add("admin");
                    cmbPer.Items.Add("user");
                    cmbPer.Text = "user";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFullName.Text.Equals("") || txtPass.Text.Equals("") || txtPassConf.Text.Equals("") || txtUserName.Text.Equals(""))
                {
                    MessageBox.Show("الرجاء ملىء كامل الحقول");
                }
                else
                {
                    if (!txtPass.Text.Equals(txtPassConf.Text))
                    {
                        MessageBox.Show("كلمة المرور و تأكيد كلمة المرور غير متطابقين");
                    }
                    else
                    {
                        try
                        {
                            OleDbDataAdapter da = new OleDbDataAdapter("select User_Name from Users where User_Name = '" + txtUserName.Text + "'", con);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            string s = dt.Rows[0][0].ToString();
                            if (s.Equals(txtUserName.Text))
                            {
                                MessageBox.Show("هذا المستخدم موجود مسبقا");
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            con.Open();
                            OleDbCommand cmd = new OleDbCommand("insert into Users (User_Name,U_Password,Per_Name,Full_Name)" +
                                " values ('" + txtUserName.Text + "','" + txtPass.Text + "','" + cmbPer.Text + "','" + txtFullName.Text + "')", con);
                            cmd.ExecuteNonQuery();
                            con.Close();

                            MessageBox.Show("تمت عملية انشاء المستخدم الجديد بنجاح");
                            txtFullName.Text = txtPass.Text = txtPassConf.Text = txtUserName.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("هناك خطأ في البيانات المدخلة");
            }
        }
    }
}
