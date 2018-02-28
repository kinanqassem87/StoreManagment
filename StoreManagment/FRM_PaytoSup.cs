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
    public partial class FRM_PaytoSup : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_PaytoSup()
        {
            InitializeComponent();
            //fullcmb();
        }
        void fullcmb() 
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select Sup_Name from Supplier", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbSupName.Items.Add(dt.Rows[i][0].ToString());
                }
                cmbSupName.Text = dt.Rows[0][0].ToString();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter da1 = new OleDbDataAdapter("select Sup_Name from Supplier", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            int found = 0;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i][0].ToString().Equals(cmbSupName.Text))
                {
                    found = 1;
                }
            }
            try
            {
                if (cmbSupName.Text.Equals("") || Amount.Text.Equals("") || rtDiscreption.Text.Equals("")||found!=1)
                {
                    MessageBox.Show("الرجاء ملىء كل الحقول و التأكد من اسم العميل انه موجود بالاصل");
                    return;
                }
                else
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("insert into Sup_Pay (Sup_Pay,Sup_Name,S_P_Date,Discreption)" +
                        " values ('" + Amount.Text + "','" + cmbSupName.Text + "','" + DateTime.Now.Date.ToShortDateString() + "','" + rtDiscreption.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    FRM_PaytoSup pts=new FRM_PaytoSup();
                    con.Open();
                    OleDbCommand cmd1 = new OleDbCommand("insert into BoxInfo (Proc_type,Proc_Date,Deposit,Withdraw,Discreption)" +
                        " values (' دفع الى " + cmbSupName.Text + "','" + DateTime.Now.Date.ToShortDateString() + "','0','" + Amount.Text + "','" + rtDiscreption.Text + "')", con);
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("تمت العملية بنجاح");
                    cmbSupName.Items.Clear();
                    cmbSupName.Text = "";
                    Amount.Text = rtDiscreption.Text = "";
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("هناك خطأ في البيانات المدخلة");
            }
        }

        private void cmbSupName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbSupName.Items.Clear();
                    OleDbDataAdapter da = new OleDbDataAdapter("select Sup_Name from Supplier where Sup_Name like '%" + cmbSupName.Text + "%'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmbSupName.Items.Add(dt.Rows[i][0].ToString());
                    }
                    if (dt.Rows.Count <= 0)
                    {
                        cmbSupName.Text = "";
                    }
                    else
                    {
                        cmbSupName.Text = dt.Rows[0][0].ToString();
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
