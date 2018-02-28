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
    public partial class FRM_WithdrawBox : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_WithdrawBox()
        {
            InitializeComponent();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char d = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;

            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtAmount.Text.Equals("") || rtDiscreption.Text.Equals(""))
                {
                    MessageBox.Show("الرجاء ملىء كلا الحقلين");
                }
                else
                {
                    if (int.Parse(txtAmount.Text) > 0)
                    {
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into BoxInfo (Proc_type,Proc_Date,Deposit,Withdraw,Discreption)" +
                            " values ('عملية سحب','" + DateTime.Now.Date.ToShortDateString() + "','0','" + txtAmount.Text + "','" + rtDiscreption.Text + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("تمت عملية السحب بنجاح");
                        txtAmount.Text = rtDiscreption.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("الرجاء ادخال مبلغ اكبر من القيمة الصفرية");
                        return;
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
