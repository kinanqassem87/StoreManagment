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
    public partial class FRM_CusUpd : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        OleDbDataAdapter da;
        public FRM_CusUpd()
        {
            InitializeComponent();
            display();
        }
        void display() 
        {
            try
            {
                DataTable dt = new DataTable();
                da = new OleDbDataAdapter("select Cus_ID as 'رقم العميل',Cus_Name as 'اسم العميل',Phone as 'رقم الهاتف' from Customer", con);
                da.Fill(dt);
                dgvCus.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                da = new OleDbDataAdapter("select Cus_ID as 'رقم العميل',Cus_Name as 'اسم العميل',"
                + "Phone as 'رقم الهاتف' from Customer where Cus_Name like '%"+txtsearch.Text+"%'", con);
                da.Fill(dt);
                dgvCus.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }

        private void dgvCus_DoubleClick(object sender, EventArgs e)
        {
            txtCusID.Text = dgvCus.CurrentRow.Cells[0].Value.ToString();
            txtCusName.Text = dgvCus.CurrentRow.Cells[1].Value.ToString();
            txtCusPhone.Text = dgvCus.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCusName.Text.Equals("") || txtCusID.Text.Equals(""))
                {
                    MessageBox.Show("الرجاء ادخال اسم العميل و تحديد رقمه");
                }
                else
                {
                    OleDbDataAdapter da1 = new OleDbDataAdapter("select * from Customer where Cus_Name='" + txtCusName.Text + "' and Cus_ID <> " + txtCusID.Text + "", con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        MessageBox.Show("لا يمكن التعديل هذا الاسم موجود مسبقا");
                    }
                    else
                    {
                    double Cus_Am_Rem = 0;
                    double C_A_Pay = 0;
                    OleDbDataAdapter daam = new OleDbDataAdapter("select Cus_Am_Rem from Cus_Account where Cus_Name='" + txtCusName.Text + "'", con);
                    DataTable dtam = new DataTable();
                    daam.Fill(dtam);
                    OleDbDataAdapter dapa = new OleDbDataAdapter("select C_A_Pay from Cus_Pay where Cus_Name='" + txtCusName.Text + "'", con);
                    DataTable dtpa = new DataTable();
                    dapa.Fill(dtpa);
                    for (int i = 0; i < dtam.Rows.Count; i++)
                    {
                        Cus_Am_Rem += double.Parse(dtam.Rows[i][0].ToString());
                    }
                    for (int i1 = 0; i1 < dtpa.Rows.Count; i1++)
                    {
                        C_A_Pay += double.Parse(dtpa.Rows[i1][0].ToString());
                    }
                    if ((Cus_Am_Rem - C_A_Pay) != 0)
                    {
                        MessageBox.Show("لا يمكن تعديل هذا العميل  ..... هناك حسابات غير مغلقة مرتبطة به");
                    }
                    else
                    {

                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("update Customer set Cus_Name='" + txtCusName.Text + "'"
                        + ",Phone = '" + txtCusPhone.Text + "' where Cus_ID = " + txtCusID.Text + "", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("تمت عملية التعديل بنجاح");
                        txtCusName.Text = txtCusID.Text = txtCusPhone.Text = "";
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCusID.Text.Equals(""))
                {
                    MessageBox.Show("الرجاء تحديد عميل بالضغط عليه مرتين ليتم الحذف");
                }
                else
                {
                    double Cus_Am_Rem = 0;
                    double C_A_Pay = 0;
                    OleDbDataAdapter da = new OleDbDataAdapter("select Cus_Am_Rem from Cus_Account where Cus_Name='" + txtCusName.Text + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    OleDbDataAdapter da1 = new OleDbDataAdapter("select C_A_Pay from Cus_Pay where Cus_Name='" + txtCusName.Text + "'", con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Cus_Am_Rem += double.Parse(dt.Rows[i][0].ToString());
                    }
                    for (int i1 = 0; i1 < dt1.Rows.Count; i1++)
                    {
                        C_A_Pay += double.Parse(dt1.Rows[i1][0].ToString());
                    }
                    if ((Cus_Am_Rem - C_A_Pay)!=0)
                    {
                        MessageBox.Show("لا يمكن حذف هذا العميل  ..... هناك حسابات غير مغلقة مرتبطة به");
                    }
                    else
                    {
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("delete from Customer where Cus_ID=" + txtCusID.Text + "", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        display();
                        MessageBox.Show("تمت عملية الحذف بنجاح");
                        txtCusID.Text = txtCusName.Text =txtCusPhone.Text= "";

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يمكن اتمام العملية");
            }
        }
    }
}
