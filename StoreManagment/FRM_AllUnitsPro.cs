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
    public partial class FRM_AllUnitsPro : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_AllUnitsPro()
        {
            InitializeComponent();
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select U_Number as 'الرقم',U_Name as 'الاسم',U_Date as 'تاريخ العملية',U_Seller as 'اسم البائع',U_Value as 'القيمة المحولة',U_Price as 'السعر',U_Type as 'الشركة' from AllUnit", con);
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
                OleDbDataAdapter da = new OleDbDataAdapter("select U_Number as 'الرقم',U_Name as 'الاسم',U_Date as" +
                    "'تاريخ العملية',U_Seller as 'اسم البائع',U_Value as 'القيمة المحولة',U_Price as" +
                    "'السعر' from U_Type as 'الشركة' AllUnit where U_Number+U_Name+U_Date like '%" + txtSearch.Text + "%'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUnit.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد عمليات لعرضها");
            }
        }
    }
}
