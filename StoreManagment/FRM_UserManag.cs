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
    public partial class FRM_UserManag : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_UserManag()
        {
            InitializeComponent();
            display();
            cmbPer.Items.Add("admin");
            cmbPer.Items.Add("user");
        }

        void display() 
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select User_ID as 'رقم المعرف',User_Name as 'اسم المستخدم',U_Password as 'كلمة المرور',Per_Name as 'السماحية',Full_Name as 'الاسم الكامل' from Users", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUsers.DataSource = dt;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select User_ID as 'رقم المعرف',User_Name as 'اسم المستخدم',U_Password as"+
                    "'كلمة المرور',Per_Name as 'السماحية',Full_Name as 'الاسم الكامل' from Users where User_Name+Full_Name like '%"+txtSearch.Text+"%'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUsers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }

        private void dgvUsers_DoubleClick(object sender, EventArgs e)
        {
            txtUserID.Text = dgvUsers.CurrentRow.Cells[0].Value.ToString();
            txtUserName.Text = dgvUsers.CurrentRow.Cells[1].Value.ToString();
            txtPass.Text = dgvUsers.CurrentRow.Cells[2].Value.ToString();
            txtPassConf.Text = txtPass.Text;
            txtFullName.Text = dgvUsers.CurrentRow.Cells[4].Value.ToString();
            cmbPer.Text = dgvUsers.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserID.Text.Equals("") || txtUserName.Text.Equals(""))
                {
                    MessageBox.Show("الرجاء تحديد مستخدم لحذفه");
                }
                else 
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("delete from Users where User_ID = "+txtUserID.Text+"", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تمت عملية الحذف بنجاح");
                    txtFullName.Text = txtPass.Text = txtPassConf.Text = txtUserID.Text = txtUserName.Text = cmbPer.Text = "";
                    display();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("هناك خطأ في البيانات المدخلة");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserID.Text.Equals("") || txtUserName.Text.Equals("") || txtPass.Text.Equals("") || txtFullName.Text.Equals(""))
                {
                    MessageBox.Show("الرجاء تعبئة كامل البيانات ليتم التعديل");
                    return;

                }
                else 
                {
                    if (!txtPass.Text.Equals(txtPassConf.Text))
                    {
                        MessageBox.Show("الرجاء التأكد من مطابقة كلمة المرور");
                        return;
                    }
                    else
                    {
                    OleDbDataAdapter da1 = new OleDbDataAdapter("select * from Users where (User_Name='" + txtUserName.Text + "' and User_ID <> " + txtUserID.Text + ") or (Full_Name='" + txtFullName.Text + "' and User_ID <> " + txtUserID.Text + ")", con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        MessageBox.Show("لا يمكن التعديل هذا الاسم موجود مسبقا");
                    }
                    else
                    {

                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("update Users set " +
                            "User_Name='" + txtUserName.Text + "'," +
                            "U_Password='" + txtPass.Text + "'," +
                            "Per_Name='" + cmbPer.Text + "'," +
                            "Full_Name='" + txtFullName.Text + "' where User_ID = " + txtUserID.Text + "", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("تمت عملية التعديل بنجاح");
                        txtFullName.Text = txtPass.Text = txtPassConf.Text = txtUserID.Text = txtUserName.Text = cmbPer.Text = "";
                        display();
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
