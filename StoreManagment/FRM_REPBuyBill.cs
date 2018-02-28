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
    public partial class FRM_REPBuyBill : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_REPBuyBill()
        {
            InitializeComponent();
            dtpFrom.Value = DateTime.Now.Date;
            dtpTo.Value = DateTime.Now.Date;
            display();
        }
        void display()
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select B_Bill_ID as 'رقم المعرف',Date_B_Bill as 'تاريخ الفاتورة',"+
                    "Total as 'اجمالي قيمة الفاتورة',Sup_Name as 'اسم المورد',Discount as 'الحسم',"+
                    " Description as 'وصف الفاتورة',Buyer_N as 'اسم المشتري',B_AmountPay as 'المبلغ المدفوع',"+
                    " B_AmountRem as 'البلغ المتبقي' from Buy_Bill order by B_Bill_ID desc", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvBuyBills.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد فواتير");
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
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSupName.Text.Equals(""))
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select B_Bill_ID as 'رقم المعرف',Date_B_Bill as 'تاريخ الفاتورة'," +
                    "Total as 'اجمالي قيمة الفاتورة',Sup_Name as 'اسم المورد',Discount as 'الحسم'," +
                    " Description as 'وصف الفاتورة',Buyer_N as 'اسم المشتري',B_AmountPay as 'المبلغ المدفوع'," +
                    " B_AmountRem as 'البلغ المتبقي' from Buy_Bill where Date_B_Bill between '" + dtpFrom.Value.Date.ToShortDateString() + "' and '" + dtpTo.Value.Date.ToShortDateString() + "' order by B_Bill_ID desc ", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvBuyBills.DataSource = dt;
                }
                else
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select B_Bill_ID as 'رقم المعرف',Date_B_Bill as 'تاريخ الفاتورة'," +
                    "Total as 'اجمالي قيمة الفاتورة',Sup_Name as 'اسم المورد',Discount as 'الحسم'," +
                    " Description as 'وصف الفاتورة',Buyer_N as 'اسم المشتري',B_AmountPay as 'المبلغ المدفوع'," +
                    " B_AmountRem as 'البلغ المتبقي' from Buy_Bill where (Date_B_Bill >= '" + dtpFrom.Value.Date.ToShortDateString() + "') and (Date_B_Bill <= '" + dtpTo.Value.Date.ToShortDateString() + "')" +
                        " and Sup_Name = '" + cmbSupName.Text + "' order by B_Bill_ID desc", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvBuyBills.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد فواتير");
            }
        }

        private void dgvBuyBills_DoubleClick(object sender, EventArgs e)
        {
            FRM_REPBuyDBill rbd = new FRM_REPBuyDBill(dgvBuyBills.CurrentRow.Cells[0].Value.ToString());
            rbd.ShowDialog();
        }
    }
}
