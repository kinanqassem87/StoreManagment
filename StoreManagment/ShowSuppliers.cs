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
    public partial class ShowSuppliers : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public ShowSuppliers()
        {
            InitializeComponent();
            display();

        }
        void display() 
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select Sup_ID as 'رقم المورد',Sup_Name as 'اسم المورد',Phone as 'رقم الهاتف' from Supplier", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvSuppliers.DataSource = dt;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select Sup_ID as 'رقم المورد',Sup_Name as 'اسم المورد',Phone as 'رقم الهاتف' from Supplier"+
                    " where Sup_Name like '%"+txtSearch.Text+"%'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvSuppliers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }

        private void dgvSuppliers_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
