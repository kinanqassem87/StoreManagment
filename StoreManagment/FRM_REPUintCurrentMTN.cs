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
    public partial class FRM_REPUintCurrentMTN : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_REPUintCurrentMTN()
        {
            InitializeComponent();
            if (FRM_Menu.per == "user")
            {
                btnCalc.Enabled = btnPull.Enabled =btnDelete.Enabled = false;
            }
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select ID as 'رقم العملية',U_Number as 'الرقم',U_Name as 'الاسم',U_Date as 'تاريخ العملية',U_Seller as 'اسم البائع',U_Value as 'القيمة المحولة',U_Price as 'السعر',U_Type as 'الشركة' from UnitCurrentMTN", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUnit.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد عمليات لعرضها");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select ID as 'رقم العملية',U_Number as 'الرقم',U_Name as 'الاسم',U_Date as" +
                    "'تاريخ العملية',U_Seller as 'اسم البائع',U_Value as 'القيمة المحولة',U_Price as" +
                    "'السعر',U_Type as 'الشركة' from UnitCurrentMTN where U_Number+U_Name+U_Date like '%" + txtSearch.Text + "%'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUnit.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد عمليات لعرضها");
            }
        }

        private void txtSVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            char d = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != d)
            {
                e.Handled = true;

            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSVal.Text.Equals(""))
                {
                    MessageBox.Show("يجب ادخال عمولة المورد بشكل مشابه لمايلي : 1.06");
                }
                else
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select sum(U_Price),sum(U_Value) from UnitCurrentMTN", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    double calc = double.Parse(dt.Rows[0][0].ToString()) - (double.Parse(dt.Rows[0][1].ToString()) * double.Parse(txtSVal.Text));
                    txtProm.Text = calc.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("هناك خطأ في البيانات المدخلة أو أن العمولة ليست بصيغة رقمية صحيحة");
            }
        }

        private void btnPull_Click(object sender, EventArgs e)
        {
            if (dgvUnit.Rows.Count == 0)
            {
                MessageBox.Show("لا يوجد بيانات من أجل سحب الربح");
            }
            else
            {
                try
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select U_Number,U_Name,U_Date,U_Value,U_Price,U_Rem,U_Seller,U_Type from UnitCurrentMTN", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into AllUnit (U_Number,U_Name,U_Date,U_Value,U_Price,U_Rem,U_Seller,U_Type)" +
                            "values ('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "'," +
                        "'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][5].ToString() + "'," +
                        "'" + dt.Rows[i][6].ToString() + "','" + dt.Rows[i][7].ToString() + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    con.Open();
                    OleDbCommand cmd1 = new OleDbCommand("delete from UnitCurrentMTN", con);
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("تمت العملية بنجاح");

                    try
                    {
                        OleDbDataAdapter da2 = new OleDbDataAdapter("select U_Number as 'الرقم',U_Name as 'الاسم',U_Date as 'تاريخ العملية',U_Seller as 'اسم البائع',U_Value as 'القيمة المحولة',U_Price as 'السعر',U_Type as 'الشركة' from UnitCurrentMTN", con);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        dgvUnit.DataSource = dt2;
                        txtProm.Text = txtSVal.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("لا يوجد عمليات لعرضها");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("لا يوجد بيانات من أجل سحب الربح");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUnit.Rows.Count == 0)
            {
                MessageBox.Show("القائمة لا تحوي اي عنصر للحذف");
            }
            else
            {
                DialogResult result = MessageBox.Show("هل أنت متأكد من عملية الحذف", "تنبيه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("delete from UnitCurrentMTN where ID = " + dgvUnit.CurrentRow.Cells[0].Value.ToString() + "", con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        OleDbDataAdapter da = new OleDbDataAdapter("select ID as 'رقم العملية',U_Number as 'الرقم',U_Name as 'الاسم',U_Date as 'تاريخ العملية',U_Seller as 'اسم البائع',U_Value as 'القيمة المحولة',U_Price as 'السعر',U_Type as 'الشركة' from UnitCurrentMTN", con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvUnit.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("لا يوجد بيانات لحذفها");
                    }
                }
            }

        }
    }
}
