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
    public partial class FRM_ShowProS : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_ShowProS()
        {
            InitializeComponent();
            display();
        }
        void display() 
        {
            try
            {
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select p.Pro_ID as 'رقم المنتج', p.Pro_Name as 'اسم المنتج',c.Cat_Name as 'اسم الصنف',"
                   + "p.Buy_Price as 'سعر الشراء' from (Product p inner join Category c ON p.Cat_ID=c.Cat_ID)", con);
                da.Fill(dt);
                dgvPro.DataSource = dt;
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
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select p.Pro_ID as 'رقم المنتج', p.Pro_Name as 'اسم المنتج',c.Cat_Name as 'اسم الصنف',"
                   + "p.Buy_Price as 'سعر الشراء' from (Product p inner join Category c ON p.Cat_ID=c.Cat_ID)"
                + " where p.Pro_Name+c.Cat_Name like '%" + txtSearch.Text + "%' ", con);
                da.Fill(dt);
                dgvPro.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }

        private void dgvPro_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
