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
    public partial class FRM_REPSupAcount : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_REPSupAcount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSupName.Text.Equals(""))
                {
                    MessageBox.Show("يجب تحديد اسم المورد");
                }
                else 
                {
                    double Sup_A_Am = 0;
                    double Sup_Pay = 0;
                    OleDbDataAdapter da = new OleDbDataAdapter("select Sup_A_Am from Sup_Account where Sup_Name='"+cmbSupName.Text+"'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    OleDbDataAdapter da1 = new OleDbDataAdapter("select Sup_Pay from Sup_Pay where Sup_Name='" + cmbSupName.Text + "'", con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    for (int i = 0; i < dt.Rows.Count; i++) 
                    {
                        Sup_A_Am += double.Parse(dt.Rows[i][0].ToString());
                    }
                    for (int i1 = 0; i1 < dt1.Rows.Count; i1++)
                    {
                        Sup_Pay += double.Parse(dt1.Rows[i1][0].ToString());
                    }
                    txtCash.Text = (Sup_A_Am - Sup_Pay).ToString();

                }
            }
            catch 
            {
                MessageBox.Show("يجب أن نحدد اسم المورد و أن يكون موجود في قائمة الموردون");
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
                MessageBox.Show("يجب ادخال اسماء الموردين اولا");
            }
        }

        private void btnDet_Click(object sender, EventArgs e)
        {
            if (cmbSupName.Text.Equals("")) 
            {
                MessageBox.Show("يجب تحديد اسم المورد");
            }
            else
            {
                FRM_REPSupAcountDet rad1 = new FRM_REPSupAcountDet(cmbSupName.Text);
                rad1.ShowDialog();
            }
        }

        
    }
}
