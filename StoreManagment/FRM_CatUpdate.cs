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
    public partial class FRM_CatUpdate : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        OleDbDataAdapter da;
        public FRM_CatUpdate()
        {
            InitializeComponent();
            display();
            
        }
        void display() 
        {
            DataTable dt = new DataTable();
            try
            {
                da = new OleDbDataAdapter("select Cat_ID as 'رقم الصنف',Cat_Name as 'اسم الصنف' from Category", con);
                da.Fill(dt);
                dgvCat.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                da = new OleDbDataAdapter("select Cat_ID as 'رقم الصنف',Cat_Name as 'اسم الصنف' from Category where Cat_Name like '%" + txtSearch.Text + "%'", con);
                da.Fill(dt);
                dgvCat.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }

        private void dgvCat_DoubleClick(object sender, EventArgs e)
        {
                //add record to dgv
                //dgvCat.Rows.Add(txtCatID.Text, txtCatName.Text);
           
            txtCatID.Text = dgvCat.CurrentRow.Cells[0].Value.ToString();
            txtCatName.Text = dgvCat.CurrentRow.Cells[1].Value.ToString();
            
            //delete record from dgv
            //dgvCat.Rows.RemoveAt(this.dgvCat.SelectedRows[0].Index);
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCauUpdate_Click(object sender, EventArgs e)
        {
            if (txtCatName.Text.Equals("")) 
            {
                MessageBox.Show("الرجاء تحديد صنف ");
            }
            else
            {
                try
                {
                    OleDbDataAdapter da1 = new OleDbDataAdapter("select * from Category where Cat_Name='" + txtCatName.Text + "' and Cat_ID <> " + txtCatID.Text + "", con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        MessageBox.Show("لا يمكن التعديل هذا الاسم موجود مسبقا");
                    }
                    else
                    {
                        OleDbCommand cmd;
                        con.Open();
                        cmd = new OleDbCommand("update Category set Cat_Name ='" + txtCatName.Text + "'where Cat_ID=" + txtCatID.Text + "", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        display();
                        MessageBox.Show("تمت عملية التعديل بنجاح");
                        txtCatID.Text = txtCatName.Text = "";
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("الرجاء التأكد البيانات المدخلة");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try 
            {
                if (txtCatID.Text.Equals(""))
                {
                    MessageBox.Show("الرجاء تحديد منتج بالضغط عليه مرتين ليتم الحذف");
                }
                else 
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select * from Product where Cat_ID = "+txtCatID.Text+"", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0) 
                    {
                        MessageBox.Show("لا يمكن حذف هذا الصنف  ..... هناك منتجات ضمنه يجب حذفها أولا");
                    }
                    else
                    {
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("delete from Category where Cat_ID=" + txtCatID.Text + "", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        display();
                        MessageBox.Show("تمت عملية الحذف بنجاح");
                        txtCatID.Text = txtCatName.Text = "";

                    }
                }

            }
            catch (Exception ex) 
            {
                MessageBox.Show("لا يمكن اتمام العملية");
            }
        }

        
    }
}
