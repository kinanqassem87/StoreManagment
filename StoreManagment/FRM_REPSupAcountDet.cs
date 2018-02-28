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
    public partial class FRM_REPSupAcountDet : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_REPSupAcountDet(string s)
        {
            InitializeComponent();
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select B_Bill_ID as 'رقم المعرف',Date_B_Bill as 'تاريخ الفاتورة'," +
                    "Total as 'اجمالي قيمة الفاتورة',Sup_Name as 'اسم المورد',Discount as 'الحسم'," +
                    " Description as 'وصف الفاتورة',Buyer_N as 'اسم المشتري',B_AmountPay as 'المبلغ المدفوع'," +
                    " B_AmountRem as 'البلغ المتبقي' from Buy_Bill where Sup_Name = '" + s + "' and B_AmountRem <> '0'" +
                    "order by B_Bill_ID desc", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvBillDet.DataSource = dt;
            }
             catch (Exception ex)
            {
                MessageBox.Show("لا يوجد فواتير لعرضها");
            }
            try
            {
                OleDbDataAdapter da1 = new OleDbDataAdapter("select Sup_Pay as 'قيمة الدفعة',S_P_Date as 'تاريخ الدفعة',"+
                    "Discreption as 'الوصف' from Sup_Pay where Sup_Name ='" + s + "' order by S_P_Date desc", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                dgvAcount.DataSource = dt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد دفعات لعرضها ");
            }
        }

        private void dgvBillDet_DoubleClick(object sender, EventArgs e)
        {
            FRM_REPBuyDBill rbd = new FRM_REPBuyDBill(dgvBillDet.CurrentRow.Cells[0].Value.ToString());
            rbd.ShowDialog();
        }
    }
}
