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
    public partial class FRM_ShowSup : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        OleDbDataAdapter da;
        public FRM_ShowSup()
        {
            InitializeComponent();
            try
            {
                DataTable dt = new DataTable();
                da = new OleDbDataAdapter("select Cus_Name as 'اسم العميل',Phone as 'رقم الهاتف' from Customer", con);
                da.Fill(dt);
                dgvSup.DataSource = dt;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("لا يوجد بيانات لعرضها ");
            }

        }

        private void dgvSup_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                da = new OleDbDataAdapter("select Cus_Name as 'اسم العميل',Phone as 'رقم الهاتف' "
                + "from Customer where Cus_Name like '%"+txtsearch.Text+"%'", con);
                da.Fill(dt);
                dgvSup.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد بيانات لعرضها ");
            }
        }

        
    }
}
