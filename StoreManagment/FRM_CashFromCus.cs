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
    public partial class FRM_CashFromCus : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_CashFromCus()
        {
            InitializeComponent();
          //  fullcmb();
        }
        void fullcmb() 
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select Cus_Name from Customer where Cus_Name like '%" + cmbCusName.Text + "%'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbCusName.Items.Add(dt.Rows[i][0].ToString());
                }
                cmbCusName.Text = dt.Rows[0][0].ToString();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("الرجاء تحديد اسم العميل");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter da1 = new OleDbDataAdapter("select Cus_Name from Customer", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            int found = 0;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i][0].ToString().Equals(cmbCusName.Text))
                {
                    found = 1;
                }
            }
            try
            {
                if (cmbCusName.Text.Equals("") || Amount.Text.Equals("") || rtDiscreption.Text.Equals("")||found!=1)
                {
                    MessageBox.Show("الرجاء ملىء كل الحقول و التأكد من أن اسم العميل مدخل بالفعل");
                    return;
                }
                else
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("insert into Cus_Pay (C_A_Pay,Cus_Name,C_P_Date,Discreption)" +
                        " values ('" + Amount.Text + "','" + cmbCusName.Text + "','" + DateTime.Now.Date.ToShortDateString() + "','" + rtDiscreption.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    FRM_CashFromCus cfc = new FRM_CashFromCus();
                    con.Open();
                    OleDbCommand cmd1 = new OleDbCommand("insert into BoxInfo (Proc_type,Proc_Date,Deposit,Withdraw,Discreption)" +
                        " values (' قبض من " + cmbCusName.Text + "','" + DateTime.Now.Date.ToShortDateString() + "','" + Amount.Text + "','0','" + rtDiscreption.Text + "')", con);
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("تمت العملية بنجاح");
                    cmbCusName.Items.Clear();
                    cmbCusName.Text = "";
                    Amount.Text = rtDiscreption.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("هناك خطأ في البيانات المدخلة");
            }
        }

        private void Amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char d = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;

            }
        }

        private void cmbCusName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbCusName.Items.Clear();
                    OleDbDataAdapter da = new OleDbDataAdapter("select Cus_Name from Customer where Cus_Name like '%" + cmbCusName.Text + "%'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmbCusName.Items.Add(dt.Rows[i][0].ToString());
                    }
                    if (dt.Rows.Count <= 0)
                    {
                        cmbCusName.Text = "";
                    }
                    else
                    {
                        cmbCusName.Text = dt.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد أسماء عملاء مدخلة ");
            }
        }
        }
    }

