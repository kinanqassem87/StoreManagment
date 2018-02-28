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
    public partial class FRM_REPCusAcountDet : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_REPCusAcountDet(string s)
        {
            InitializeComponent();
                {
                    try
                    {
                        OleDbDataAdapter da = new OleDbDataAdapter("select S_Bill_ID as 'رقم المعرف',Date_B_Bill as 'تاريخ العملية',Total" +
                        " as 'قيمة اجمالي الفاتورة',Description as 'وصف الفاتورة',Cus_Name as 'اسم العميل'," +
                        "Discount as 'الحسم على الفاتورة',Seller_N as 'اسم البائع',S_AmountPay as 'المبلغ المدفوع',S_AmountRem as 'المبلغ المتبقي'" +
                            " from Sell_Bill where Cus_Name = '" + s + "' and S_AmountRem <> '0'  order by S_Bill_ID desc", con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvBillDet.DataSource = dt;

                        OleDbDataAdapter da1 = new OleDbDataAdapter("select C_A_Pay as 'قيمة الدفعة',C_P_Date as 'تاريخ الدفعة',Discreption as 'الوصف' from Cus_Pay where Cus_Name ='" + s + "' order by C_P_Date desc", con);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        dgvAcount.DataSource = dt1;
                    } 
                    catch (Exception ex)
                    {
                        MessageBox.Show("لا يوجد معلومات لعرضها ");
                    }
            }
        }

        private void dgvBillDet_DoubleClick(object sender, EventArgs e)
        {
            FRM_REPSellDBill rsd = new FRM_REPSellDBill(dgvBillDet.CurrentRow.Cells[0].Value.ToString());
            rsd.ShowDialog();
        }
    }
}
