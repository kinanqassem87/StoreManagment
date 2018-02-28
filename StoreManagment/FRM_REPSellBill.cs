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
    public partial class FRM_REPSellBill : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_REPSellBill()
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
                OleDbDataAdapter da = new OleDbDataAdapter("select S_Bill_ID as 'رقم المعرف',Date_B_Bill as 'تاريخ العملية',Total" +
                " as 'قيمة اجمالي الفاتورة',Description as 'وصف الفاتورة',Cus_Name as 'اسم العميل',"+
                "Discount as 'الحسم على الفاتورة',Seller_N as 'اسم البائع',S_AmountPay as 'المبلغ المدفوع',S_AmountRem as 'المبلغ المتبقي'"+
                    " from Sell_Bill order by S_Bill_ID desc", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvSellBills.DataSource = dt;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("لا يوجد فواتير");
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
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
                MessageBox.Show("يجب ادخال العملاء اولا");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCusName.Text.Equals(""))
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select S_Bill_ID as 'رقم المعرف',Date_B_Bill as 'تاريخ العملية',Total" +
                    " as 'قيمة اجمالي الفاتورة',Description as 'وصف الفاتورة',Cus_Name as 'اسم العميل'," +
                    "Discount as 'الحسم على الفاتورة',Seller_N as 'اسم البائع',S_AmountPay as 'المبلغ المدفوع',S_AmountRem as 'المبلغ المتبقي'" +
                        " from Sell_Bill where Date_B_Bill between '" + dtpFrom.Value.Date.ToShortDateString() + "' and '" + dtpTo.Value.Date.ToShortDateString() + "' order by S_Bill_ID desc ", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvSellBills.DataSource = dt;
                }
                else 
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select S_Bill_ID as 'رقم المعرف',Date_B_Bill as 'تاريخ العملية',Total" +
                    " as 'قيمة اجمالي الفاتورة',Description as 'وصف الفاتورة',Cus_Name as 'اسم العميل'," +
                    "Discount as 'الحسم على الفاتورة',Seller_N as 'اسم البائع',S_AmountPay as 'المبلغ المدفوع',S_AmountRem as 'المبلغ المتبقي'" +
                        " from Sell_Bill where (Date_B_Bill >= '" + dtpFrom.Value.Date.ToShortDateString() + "') and (Date_B_Bill <= '" + dtpTo.Value.Date.ToShortDateString() + "')"+
                        " and Cus_Name = '" + cmbCusName.Text + "' order by S_Bill_ID desc", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvSellBills.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد فواتير");
            }
        }

        private void dgvSellBills_DoubleClick(object sender, EventArgs e)
        {
            FRM_REPSellDBill rsd = new FRM_REPSellDBill(dgvSellBills.CurrentRow.Cells[0].Value.ToString());
            rsd.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
           
            //try
            //{
            //    FRM_PrintReports pr = new FRM_PrintReports();
            //    OleDbDataAdapter da = new OleDbDataAdapter("select * from S_D_Bill where S_Bill_ID='" + dgvSellBills.CurrentRow.Cells[0].Value + "'", con);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    da.Fill(pr.StoreDataSet.S_D_Bill);
            //    pr.reportViewer1.RefreshReport();
            //    pr.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("الرجاء التأكد من الاتصال بقاعدة البيانات");
            //}
        }
    }
}
