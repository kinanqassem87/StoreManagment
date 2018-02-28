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
    public partial class FRM_SignIn : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        OleDbDataAdapter da;
        string stateEnter = "0";
        public FRM_SignIn()
        {
            InitializeComponent();   
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            
            try{
                
                da = new OleDbDataAdapter("select * from Users ", con);
                da.Fill(dt);
                if (txtUserN.Text.Equals("") || txtPassword.Text.Equals(""))
                {
                    MessageBox.Show("الرجاء ادخال اسم المستخدم و كلمة المرور");
                }
                else {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][1].Equals(txtUserN.Text))
                        {
                            if (dt.Rows[i][2].Equals(txtPassword.Text))
                            {
                                Close();
                                stateEnter = "1";
                                FRM_Menu.User_ID = dt.Rows[i][0].ToString();
                                FRM_Menu.FullName = dt.Rows[i][4].ToString();
                                FRM_Menu.UserName = dt.Rows[i][1].ToString();
                                FRM_Menu.Password = dt.Rows[i][2].ToString();

                                if (dt.Rows[i][3].Equals("admin"))
                                {
                                    FRM_Menu.per = "admin";
                                   
                                }
                                if (dt.Rows[i][3].Equals("user"))
                                {
                                    FRM_Menu.per = "user";
                                }

                            }
                            //else
                            //{
                            //    MessageBox.Show("كلمة السر غير صحيحة");
                            //    txtPassword.Text = "";
                            //    return;
                            //}

                        }
                        //else
                        //{
                        //    MessageBox.Show("اسم المستخدم غير صحيح");
                        //    txtUserN.Text = "";
                        //    return;
                        //}
                    }
                    if (!stateEnter.Equals("1"))
                    {
                        MessageBox.Show("اسم المستخدم أو كلمة السر غير صحيحة");
                        txtUserN.Text = txtPassword.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("الرجاء التأكد من الاتصال بقاعدة البيانات");
            }
                
            
        }
    }
}
