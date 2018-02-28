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
    public partial class FRM_SupUpd : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        OleDbDataAdapter da;
        public FRM_SupUpd()
        {
            InitializeComponent();
            display();
        }
        void display() 
        {
            try
            {
                DataTable dt = new DataTable();
                da = new OleDbDataAdapter("select Sup_ID as 'رقم المورد',Sup_Name as 'اسم المورد',Phone as 'رقم الهاتف' from Supplier", con);
                da.Fill(dt);
                dgvSup.DataSource = dt;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                da = new OleDbDataAdapter("select Sup_ID as 'رقم المورد',Sup_Name as 'اسم المورد',"
                + "Phone as 'رقم الهاتف' from Supplier where Sup_Name like '%"+txtsearch.Text+"%'", con);
                da.Fill(dt);
                dgvSup.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }

        private void dgvSup_DoubleClick(object sender, EventArgs e)
        {
            txtSupID.Text = dgvSup.CurrentRow.Cells[0].Value.ToString();
            txtSupName.Text = dgvSup.CurrentRow.Cells[1].Value.ToString();
            txtSupPhone.Text = dgvSup.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            if (txtSupName.Text.Equals("")||txtSupID.Text.Equals(""))
            {
                MessageBox.Show("الرجاء ادخال اسم المورد و تحديد رقمه");
            }
            else
            {
                try
                {
                    OleDbDataAdapter da1 = new OleDbDataAdapter("select * from Supplier where Sup_Name='" + txtSupName.Text + "' and Sup_ID <> " + txtSupID.Text + "", con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        MessageBox.Show("لا يمكن التعديل هذا الاسم موجود مسبقا");
                    }
                    else
                    {
                        double Sup_A_Am = 0;
                        double Sup_Pay = 0;
                        OleDbDataAdapter daam = new OleDbDataAdapter("select Sup_A_Am from Sup_Account where Sup_Name='" + txtSupName.Text + "'", con);
                        DataTable dtam = new DataTable();
                        daam.Fill(dtam);
                        OleDbDataAdapter dapa = new OleDbDataAdapter("select Sup_Pay from Sup_Pay where Sup_Name='" + txtSupName.Text + "'", con);
                        DataTable dtpa = new DataTable();
                        dapa.Fill(dtpa);
                        for (int i = 0; i < dtam.Rows.Count; i++)
                        {
                            Sup_A_Am += double.Parse(dtam.Rows[i][0].ToString());
                        }
                        for (int i1 = 0; i1 < dtpa.Rows.Count; i1++)
                        {
                            Sup_Pay += double.Parse(dtpa.Rows[i1][0].ToString());
                        }
                        if ((Sup_A_Am - Sup_Pay) != 0)
                        {
                            MessageBox.Show("لا يمكن تعديل هذا المورد  ..... هناك حسابات غير مغلقة مرتبطة به");
                        }
                        else
                        {


                            con.Open();
                            OleDbCommand cmd = new OleDbCommand("update Supplier set Sup_Name='" + txtSupName.Text + "'"
                            + ",Phone='" + txtSupPhone.Text + "' where Sup_ID=" + txtSupID.Text + "", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("تمت عملية التعديل بنجاح");
                            txtSupID.Text = txtSupName.Text = txtSupPhone.Text = "";
                            display();
                        }
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("هناك خطأ في البيانات المدخلة");
                }
               
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSupID.Text.Equals(""))
                {
                    MessageBox.Show("الرجاء تحديد مورد بالضغط عليه مرتين ليتم الحذف");
                }
                else
                {
                    double Sup_A_Am = 0;
                    double Sup_Pay = 0;
                    OleDbDataAdapter da = new OleDbDataAdapter("select Sup_A_Am from Sup_Account where Sup_Name='" + txtSupName.Text + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    OleDbDataAdapter da1 = new OleDbDataAdapter("select Sup_Pay from Sup_Pay where Sup_Name='" + txtSupName.Text + "'", con);
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
                    if ((Sup_A_Am - Sup_Pay) != 0)
                    {
                        MessageBox.Show("لا يمكن حذف هذا المورد  ..... هناك حسابات غير مغلقة مرتبطة به");
                    }
                    else
                    {
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("delete from Supplier where Sup_ID=" + txtSupID.Text + "", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        display();
                        MessageBox.Show("تمت عملية الحذف بنجاح");
                        txtSupID.Text = txtSupName.Text = txtSupPhone.Text = "";

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
