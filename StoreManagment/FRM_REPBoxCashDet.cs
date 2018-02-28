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
    public partial class FRM_REPBoxCashDet : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_REPBoxCashDet(string s)
        {
            InitializeComponent();
            textBox2.Text = s;
            
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select Proc_type as 'نوع العملية',Proc_Date as 'تاريخ العملية',Deposit as 'مقبوض',Withdraw as 'مسحوب',Discreption as 'وصف العملية' from BoxInfo order by Box_ID desc", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDet.DataSource = dt;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("لا يوجد قيود لعرضها");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select Proc_type as 'نوع العملية',"+
                    "Proc_Date as 'تاريخ العملية',Deposit as 'مدخل',Withdraw as 'مسحوب',"+
                    "Discreption as 'وصف العملية' from BoxInfo where Proc_type+Discreption like '%"+textBox1.Text+"%' order by Box_ID desc", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDet.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد قيود لعرضها");
            }
        }
    }
}
