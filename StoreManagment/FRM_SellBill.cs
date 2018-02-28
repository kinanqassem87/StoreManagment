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
    public partial class FRM_SellBill : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_SellBill()
        {
            InitializeComponent();
             txtSellerN.Text = FRM_Menu.FullName;
            txtDate.Text = DateTime.Now.Date.ToShortDateString();
        }

        private void btnChoCus_Click(object sender, EventArgs e)
        {
            FRM_ShowSup ss = new FRM_ShowSup();
            ss.ShowDialog();
            try
            {
                txtCusName.Text = ss.dgvSup.CurrentRow.Cells[0].Value.ToString();
                txtCusPhone.Text = ss.dgvSup.CurrentRow.Cells[1].Value.ToString();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("الرجاء تحديد عميل");
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char d = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;

            }
        }

        private void txtAmountPay_KeyPress(object sender, KeyPressEventArgs e)
        {
            char d = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 )
            {
                e.Handled = true;

            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char d = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 )
            {
                e.Handled = true;

            }
        }

        private void btnChoPro_Click(object sender, EventArgs e)
        {
            FRM_ShowPro sp = new FRM_ShowPro();
            sp.ShowDialog();
            try
            {
                txtProID.Text = sp.dgvPro.CurrentRow.Cells[0].Value.ToString();
                txtProName.Text = sp.dgvPro.CurrentRow.Cells[1].Value.ToString();
                txtCatName.Text = sp.dgvPro.CurrentRow.Cells[2].Value.ToString();
                txtSellPrice.Text = sp.dgvPro.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("الرجاء تحديد منتج معين");
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtProID.Text == "")
                {
                    MessageBox.Show("يجب اختيار منتج");
                    return;
                }
                if (txtQty.Text == "" || double.Parse(txtQty.Text) <= 0)
                {
                    MessageBox.Show("يجب ادخال كمية المنتج و تكون بالموجب");
                    return;
                }
                for (int i = 0; i < dgvBill.Rows.Count; i++)
                {
                    if (txtProID.Text == dgvBill.Rows[i].Cells[0].Value.ToString())
                    {
                        MessageBox.Show("هذا المنتج موجود");
                        return;

                    }
                }
                try
                {
                    OleDbDataAdapter daa = new OleDbDataAdapter("select * from Product where Pro_ID = " + txtProID.Text + "", con);
                    DataTable dttt = new DataTable();
                    daa.Fill(dttt);
                    if (int.Parse(txtQty.Text) > int.Parse(dttt.Rows[0][2].ToString()))
                    {
                        MessageBox.Show("الكمية المحددة أكبر من الكمية الموجودة في المتجر");
                        return;
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("يجب ادخال المنتجات اولا");
                }

                //
                dgvBill.Rows.Add(txtProID.Text, txtProName.Text,txtCatName.Text,txtSellPrice.Text, txtQty.Text);
                txtProID.Text = txtProName.Text = txtCatName.Text = txtSellPrice.Text = txtQty.Text = "";
                btnUpdate.Enabled = btnDelete.Enabled = btnSave.Enabled = true;
                caltotal();
                

            }
        }
        
        void caltotal()
        {
            double total = 0;
            for (int i = 0; i < dgvBill.Rows.Count; i++)
            {
                try
                {
                    total += double.Parse(dgvBill.Rows[i].Cells[3].Value.ToString()) * double.Parse(dgvBill.Rows[i].Cells[4].Value.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("البيانات المدخلة غير صحيحة قد تكون الاسعار ليست بصيغة رقمية");
                    return;
                }

            }
            txtTotal.Text = total.ToString();
            txtAmountPay.Text = txtTotal.Text;
            txtDiscount.Text = txtAmountRem.Text = "0";

        }

        private void txtAmountPay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAmountPay.Text.Equals("") || txtDiscount.Text.Equals("") || (int.Parse(txtAmountPay.Text) + int.Parse(txtDiscount.Text)) > int.Parse(txtTotal.Text))
                {
                    txtDiscount.Text = "0";
                    txtAmountPay.Text = txtTotal.Text;
                    txtAmountPay.Text = (int.Parse(txtTotal.Text) - int.Parse(txtDiscount.Text)).ToString();

                }
                txtAmountRem.Text = (int.Parse(txtTotal.Text) - int.Parse(txtDiscount.Text) - int.Parse(txtAmountPay.Text)).ToString();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("يجب حساب قيمة الفاتورة اولا");
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAmountPay.Text.Equals("") || txtDiscount.Text.Equals("") || (int.Parse(txtDiscount.Text) + int.Parse(txtAmountPay.Text)) > int.Parse(txtTotal.Text))
                {
                    txtDiscount.Text = "0";
                    txtAmountPay.Text = txtTotal.Text;
                    txtAmountPay.Text = (int.Parse(txtTotal.Text) - int.Parse(txtDiscount.Text)).ToString();

                }
                txtAmountRem.Text = (int.Parse(txtTotal.Text) - int.Parse(txtDiscount.Text) - int.Parse(txtAmountPay.Text)).ToString();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("يجب حساب قيمة الفاتورة اولا");
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dgvBill.Rows.Remove(dgvBill.CurrentRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد صفوف يمكن حذفها");
            }
            caltotal();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                txtProID.Text = dgvBill.CurrentRow.Cells[0].Value.ToString();
                txtProName.Text = dgvBill.CurrentRow.Cells[1].Value.ToString();
                txtCatName.Text = dgvBill.CurrentRow.Cells[2].Value.ToString();
                txtSellPrice.Text = dgvBill.CurrentRow.Cells[3].Value.ToString();
                txtQty.Text = dgvBill.CurrentRow.Cells[4].Value.ToString();
                dgvBill.Rows.Remove(dgvBill.CurrentRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد صفوف يمكن تعدليها");
            }
            caltotal();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvBill.Rows.Count > 0)
            {
                if (int.Parse(txtAmountRem.Text) != 0 && txtCusName.Text.Equals(""))
                {
                    MessageBox.Show("يجب ادخال اسم العميل لأن المبلغ المدفوع لا يساوي قيمة الفاتورة");
                    return;

                }
                else
                {
                    try
                    {
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into Sell_Bill (Date_B_Bill,Total,Description,Cus_Name,Discount,Seller_N,S_AmountPay,S_AmountRem)" +
                            "values ('" + txtDate.Text + "','" + txtTotal.Text + "','" + txtDiscription.Text + "','" + txtCusName.Text + "','" + txtDiscount.Text + "'," +
                        "'" + txtSellerN.Text + "','" + txtAmountPay.Text + "','" + txtAmountRem.Text + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();


                        string LastIDBill = "";
                        OleDbDataAdapter da = new OleDbDataAdapter("select TOP 1 S_Bill_ID from Sell_Bill ORDER BY S_Bill_ID DESC", con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        LastIDBill = dt.Rows[0][0].ToString();


                        DataTable dt1 = new DataTable();
                        dt1.Columns.Add("P_ID");
                        dt1.Columns.Add("Pro_name");
                        dt1.Columns.Add("Cat_Name");
                        dt1.Columns.Add("Sell_Price");
                        dt1.Columns.Add("Qty");
                        for (int ii = 0; ii < dgvBill.Rows.Count; ii++)
                        {
                            dt1.Rows.Add(dgvBill.Rows[ii].Cells[0].Value.ToString(), dgvBill.Rows[ii].Cells[1].Value.ToString(), dgvBill.Rows[ii].Cells[2].Value.ToString(), dgvBill.Rows[ii].Cells[3].Value.ToString(), dgvBill.Rows[ii].Cells[4].Value.ToString());
                        }

                        
                        for (int i = 0; i < dgvBill.Rows.Count; i++)
                        {

                            OleDbDataAdapter daq = new OleDbDataAdapter("select Qty from Product where Pro_ID = " + dt1.Rows[i][0].ToString() + "", con);
                            DataTable dtq = new DataTable();
                            daq.Fill(dtq);
                            string qty = Convert.ToString(int.Parse(dtq.Rows[0][0].ToString()) - int.Parse(dt1.Rows[i][4].ToString()));
                            con.Open();
                            OleDbCommand cmd4 = new OleDbCommand("update Product set Qty= " + qty + " where Pro_ID = " + dt1.Rows[i][0].ToString() + "", con);
                            cmd4.ExecuteNonQuery();
                            con.Close();

                            con.Open();
                            OleDbCommand cmd1 = new OleDbCommand("insert into S_D_Bill (Pro_Name,Qty,S_Price,S_Bill_ID)" +
                                "values ('" + dt1.Rows[i][1].ToString() + "','" + dt1.Rows[i][4].ToString() + "','" + dt1.Rows[i][3].ToString() + "'," + LastIDBill + ")", con);
                            cmd1.ExecuteNonQuery();
                            con.Close();
                        }


                        if (int.Parse(txtAmountRem.Text) > 0)
                        {
                            con.Open();
                            OleDbCommand cmd2 = new OleDbCommand("insert into Cus_Account (Cus_Am_Rem,S_Bill_ID,Cus_Name)" +
                                "values ('" + txtAmountRem.Text + "'," + LastIDBill + ",'" + txtCusName.Text + "')", con);
                            cmd2.ExecuteNonQuery();
                            con.Close();

                        }

                        FRM_SellBill sb = new FRM_SellBill();
                        con.Open();
                        OleDbCommand cmd3 = new OleDbCommand("insert into BoxInfo (Proc_type,Proc_Date,Deposit,Withdraw,Discreption)" +
                            "values ('فاتورة بيع','" + txtDate.Text + "','" + txtAmountPay.Text + "','0','" + sb.Text + "')", con);
                        cmd3.ExecuteNonQuery();
                        con.Close();

                        btnSave.Enabled = btnDelete.Enabled = btnUpdate.Enabled = btnCancel.Enabled = btnChoPro.Enabled = btnChoCus.Enabled = false;
                        txtDiscount.ReadOnly = txtAmountPay.ReadOnly = txtQty.ReadOnly = txtDiscription.ReadOnly = true;

                       

                        MessageBox.Show("تمت عملية حفظ بيانات الفاتورة بنجاح");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("هناك خطأ في بيانات الفاتورة المدخلة");
                    }

                }
            }
            else 
            {

                MessageBox.Show("لا يوجد أي منتج في الفاتورة");
            }
        }

        

    }
}
