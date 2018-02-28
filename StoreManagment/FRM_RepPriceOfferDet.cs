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
    public partial class FRM_RepPriceOfferDet : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_RepPriceOfferDet(string id)
        {
            InitializeComponent();
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select Pro_Name as 'اسم المنتج',Qty as 'الكمية',S_Price as 'السعر الافرادي' from S_D_BillOrder where S_Bill_ID= '" + id + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDetails.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد تفاصيل");
            }
        }
    }
}
