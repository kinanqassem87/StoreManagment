using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace StoreManagment
{
    public partial class FRM_Menu : Form
    {
        public static string per = "";
        public static string FullName = "";
        public static string UserName = "";
        public static string Password = "";
        public static string User_ID = "";
        public FRM_Menu()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            ManagementObjectCollection moc = mos.Get();
            string motherBoard = "";
            foreach (ManagementObject mo in moc)
            {
                motherBoard = (string)mo["SerialNumber"];
            }
            if (motherBoard == "PCSGD028J3U3RK")
            {
                InitializeComponent();
            }
            else 
            {
                MessageBox.Show("هذا المنتج غير مرخص .....للتفعيل الاتصال : المهندس كنان قاسم :: 0933372016");
            }
            //ليأخذ الفورم شكل الويندوز الذي يعمل عليه
            //for add skin get skin tool from net and then add to refrence then add to toolbox
            //then drag this tool to your form and give it the path of skin you choose
            //shincraft
            //VisualStylers
            //Application.EnableVisualStyles();
            ///Activations

           
        }

        private void TM_SignIn_Click(object sender, EventArgs e)
        {
            FRM_SignIn si = new FRM_SignIn();
            si.ShowDialog();
        }

        private void FRM_Menu_Activated(object sender, EventArgs e)
        {
            if (per.Equals("admin"))
            {
                TM_About.Enabled = TM_Accounts.Enabled = TM_Product.Enabled = TM_SB.Enabled = TM_Reports.Enabled = TM_SupAndCus.Enabled = TM_BBill.Enabled=TM_UserMang.Enabled = true;
                TM_SignOut.Enabled = TM_ChangePass.Enabled = TM_SellRem.Enabled = TM_BuyRem.Enabled = TM_SellPrice.Enabled = true;
                TM_SignIn.Enabled = false;
            }
            else if (per.Equals("out")) 
            {
                TM_About.Enabled = TM_Accounts.Enabled = TM_Product.Enabled = TM_SB.Enabled = TM_Reports.Enabled = TM_SupAndCus.Enabled = TM_UserMang.Enabled = false;
                TM_SignIn.Enabled = true;
                TM_SignOut.Enabled = TM_ChangePass.Enabled = false;
            }
            else  if (per.Equals("user"))
            {
                TM_About.Enabled = TM_Accounts.Enabled = TM_Product.Enabled = TM_Reports.Enabled = TM_SupAndCus.Enabled = TM_BBill.Enabled = TM_UserMang.Enabled=TM_SellRem.Enabled=TM_BuyRem.Enabled=TM_SellPrice.Enabled = false;
                TM_SignOut.Enabled = TM_SB.Enabled = TM_ChangePass.Enabled =TM_About.Enabled=TM_QTYQuery.Enabled= true;
                TM_SignIn.Enabled = false;
            }
        }

        private void TM_SignOut_Click(object sender, EventArgs e)
        {
            per = "out";
            TM_About.Enabled = TM_Accounts.Enabled = TM_Product.Enabled = TM_SB.Enabled = TM_Reports.Enabled = TM_SupAndCus.Enabled = TM_UserMang.Enabled = false;
            FRM_SignIn fs = new FRM_SignIn();
            fs.ShowDialog();
        }

        private void TM_AddNewCat_Click(object sender, EventArgs e)
        {
            FRM_AddNewCat ac = new FRM_AddNewCat();
            ac.ShowDialog();
        }

        private void TM_ProManag_Click(object sender, EventArgs e)
        {
            FRM_ProUpdate pu = new FRM_ProUpdate();
            pu.ShowDialog();
        }

        private void TM_AddNewPro_Click(object sender, EventArgs e)
        {
            FRM_AddNewPro ap = new FRM_AddNewPro();
            ap.ShowDialog();
        }

        private void TM_FirstAddPro_Click(object sender, EventArgs e)
        {
            FRM_AddQty aq = new FRM_AddQty();
            aq.ShowDialog();
        }

        private void TM_CatManag_Click(object sender, EventArgs e)
        {
            FRM_CatUpdate cu = new FRM_CatUpdate();
            cu.ShowDialog();
        }

        private void TM_AddSup_Click(object sender, EventArgs e)
        {
            FRM_AddSup adds = new FRM_AddSup();
            adds.ShowDialog();
        }

        private void TM_SupUpd_Click(object sender, EventArgs e)
        {
            FRM_SupUpd su = new FRM_SupUpd();
            su.ShowDialog();
        }

        private void TM_AddCus_Click(object sender, EventArgs e)
        {
            AddCus ad = new AddCus();
            ad.ShowDialog();
        }

        private void TM_CusUpd_Click(object sender, EventArgs e)
        {
            FRM_CusUpd cu = new FRM_CusUpd();
            cu.ShowDialog();
        }

        private void TM_SBill_Click(object sender, EventArgs e)
        {
            FRM_SellBill sb = new FRM_SellBill();
            sb.ShowDialog();
        }

        private void TM_BBill_Click(object sender, EventArgs e)
        {
            FRM_BuyBill bb = new FRM_BuyBill();
            bb.ShowDialog();
        }

        private void TM_ChangePass_Click(object sender, EventArgs e)
        {
            FRM_ChangePass cg = new FRM_ChangePass();
            cg.ShowDialog();
        }

        private void TM_AmountOfPro_Click(object sender, EventArgs e)
        {
            FRM_ProAmount pa = new FRM_ProAmount();
            pa.ShowDialog();
        }

        private void TM_Dis_Click(object sender, EventArgs e)
        {
            FRM_DisposeBox dpb = new FRM_DisposeBox();
            dpb.ShowDialog();
        }

        private void TM_With_Click(object sender, EventArgs e)
        {
            FRM_WithdrawBox wd = new FRM_WithdrawBox();
            wd.ShowDialog();
        }

        private void TM_ToSup_Click(object sender, EventArgs e)
        {
            FRM_PaytoSup ps = new FRM_PaytoSup();
            ps.ShowDialog();
        }

        private void TM_FromCus_Click(object sender, EventArgs e)
        {
            FRM_CashFromCus cfc = new FRM_CashFromCus();
            cfc.ShowDialog();
        }

        private void TM_AddNewUser_Click(object sender, EventArgs e)
        {
            FRM_AddNewUser anu = new FRM_AddNewUser();
            anu.ShowDialog();
        }

        private void TM_UserManag_Click(object sender, EventArgs e)
        {
            FRM_UserManag um = new FRM_UserManag();
            um.ShowDialog();
        }

        private void TM_BoxAcc_Click(object sender, EventArgs e)
        {
            FRM_REPBoxCash rpc = new FRM_REPBoxCash();
            rpc.ShowDialog();
        }

        private void TM_SupAcc_Click(object sender, EventArgs e)
        {
            FRM_REPSupAcount rsa = new FRM_REPSupAcount();
            rsa.ShowDialog();
        }

        private void TM_CusAcc_Click(object sender, EventArgs e)
        {
            FRM_REPCusAcount rca = new FRM_REPCusAcount();
            rca.ShowDialog();
        }

        private void TM_ProValue_Click(object sender, EventArgs e)
        {
            FRM_REPProValue rv = new FRM_REPProValue();
            rv.ShowDialog();
        }

        private void TM_AllMoney_Click(object sender, EventArgs e)
        {
            FRM_REPAllMoney alm = new FRM_REPAllMoney();
            alm.ShowDialog();
        }

        private void TM_SellBills_Click(object sender, EventArgs e)
        {
            FRM_REPSellBill rsb = new FRM_REPSellBill();
            rsb.ShowDialog();
        }

        private void TM_BuyBills_Click(object sender, EventArgs e)
        {
            FRM_REPBuyBill fbb = new FRM_REPBuyBill();
            fbb.ShowDialog();
        }

        private void TM_About_Click(object sender, EventArgs e)
        {
            FRM_About fa = new FRM_About();
            fa.ShowDialog();
        }

        private void TM_QTYQuery_Click(object sender, EventArgs e)
        {
            FRM_QTYCount qt = new FRM_QTYCount();
            qt.ShowDialog();
        }

        private void TM_CusRem_Click(object sender, EventArgs e)
        {
            FRM_AllCusRem allr = new FRM_AllCusRem();
            allr.ShowDialog();
        }

        private void TM_SupRem_Click(object sender, EventArgs e)
        {
            FRM_AllSupRem sr = new FRM_AllSupRem();
            sr.ShowDialog();
        }

        private void TM_NewUnit_Click(object sender, EventArgs e)
        {
            FRM_AddUnitTrans ut = new FRM_AddUnitTrans();
            ut.ShowDialog();
        }

        private void TM_CurrentUnit_Click(object sender, EventArgs e)
        {
            
        }

        private void TM_AllUnit_Click(object sender, EventArgs e)
        {
            FRM_AllUnitsPro pro = new FRM_AllUnitsPro();
            pro.ShowDialog();
        }

        private void TM_Syriatel_Click(object sender, EventArgs e)
        {
            FRM_REPUintCurrent uni = new FRM_REPUintCurrent();
            uni.ShowDialog();
        }

        private void TM_MTN_Click(object sender, EventArgs e)
        {
            FRM_REPUintCurrentMTN uniMTN = new FRM_REPUintCurrentMTN();
            uniMTN.ShowDialog();
        }

        private void TM_DelPeo_Click(object sender, EventArgs e)
        {
            FRM_RemovePro rp = new FRM_RemovePro();
            rp.ShowDialog();
        }

        private void TM_SellRem_Click(object sender, EventArgs e)
        {
            FRM_SellBillRem sbr = new FRM_SellBillRem();
            sbr.ShowDialog();
        }

        private void TM_BuyRem_Click(object sender, EventArgs e)
        {
            FRM_BuyBillRem bbr = new FRM_BuyBillRem();
            bbr.ShowDialog();
        }

        private void TM_SellPrice_Click(object sender, EventArgs e)
        {
            FRM_SellPrices spe = new FRM_SellPrices();
            spe.ShowDialog();
        }

        private void TM_PriceOffers_Click(object sender, EventArgs e)
        {
            FRM_RepPriceOffer offer = new FRM_RepPriceOffer();
            offer.ShowDialog();
        }

        

        
    }
}
