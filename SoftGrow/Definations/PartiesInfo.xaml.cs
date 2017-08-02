using System;
using System.Windows;
using System.Configuration;
using System.Data;


namespace SoftGrow.Definations
{
    /// <summary>
    /// Interaction logic for PartiesInfo.xaml
    /// </summary>
    public partial class PartiesInfo : Window
    {
        DAL.Parties objDAL = new DAL.Parties();
        //MyMessages Message = new MyMessages();
        bool vOpenMode = false;

        public PartiesInfo()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            objDAL.connectionstring = ConfigurationManager.ConnectionStrings["MyString"].ConnectionString;
            //lblTitle.Parent = this.pictureBox1;
            //lblTitle.BackColor = Color.Transparent;
            //LoadGrid();
            //ClearFields();
            //MessageBox.Show("Saqib");

        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please Enter Party Name.");
                    txtName.Focus();
                    return;
                }


                Objects.Parties obj = new Objects.Parties();
                obj.PartyID = Int64.Parse(txtId.Text);
                obj.PartyName = txtName.Text.Trim();
                obj.ContactNo = txtContactNo.Text.Trim();
                obj.CNICNo = txtCNIC.Text.Trim();
                obj.City = txtCity.Text.Trim();
                obj.Address = txtAddress.Text.Trim();
                obj.Email = txtEmail.Text.Trim();
                obj.Web = txtWeb.Text.Trim();
                obj.NTN = txtNTN.Text.Trim();
                obj.AccountID = txtId.Tag.ToString();
                //obj.IsSupplier = chkSupplier;
                //obj.IsCustomer = chkCustomer.Checked;
                obj.IsSupplier = true;
                obj.IsCustomer = true;

                //decimal vOpDebit = 0;
                //decimal vOpCredit = 0;

                //decimal.TryParse(this.txtOpDebit.Text, out vOpDebit);
                //decimal.TryParse(this.txtOpCredit.Text, out vOpCredit);

                //Insert Account
                //var AccDAL = new DAL.AccountChart();
                //AccDAL.connectionstring = objDAL.connectionstring;
                //Objects.AccountChart objAcc = new Objects.AccountChart();
                //if (!string.IsNullOrEmpty(txtId.Tag.ToString()))
                //    objAcc.AccountNo = txtId.Tag.ToString();
                //objAcc.AccountTitle = obj.PartyName;
                //objAcc.AccountType = "ASSET";
                //objAcc.AccountSubType = "Parties";
                //objAcc.IsParty = true;
                //objAcc.IsBank = false;
                //objAcc.OpeningDebit = vOpDebit;
                //objAcc.OpeningCredit = vOpCredit;

                if (!vOpenMode)
                {
                    //objAcc.AccountNo = AccDAL.getNextNo("ASSET").ToString();
                    //AccDAL.InsertRecord(objAcc);

                    ////Insert Party
                    //obj.PartyID = objDAL.getNextNo();
                    //obj.AccountID = objAcc.AccountNo;

                    objDAL.InsertRecord(obj);
                }
                else
                {
                    // UPdate Opeinig in Account
                    //AccDAL.UpdateRecord(objAcc);
                    objDAL.UpdateRecord(obj);
                }

                MessageBox.Show("SaveRecord");
                //LoadGrid();
                //btnClear_Click(sender, e);


            }
            catch (Exception exc)
            {
                MessageBox.Show("Error", exc.Message);
            }
        }

        private void cmdSelect_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = "32";
            DataTable dt = objDAL.getRecord(" AND PartyID=" + txtId.Text);
            txtId.Tag = dt.Rows[0]["AccountID"].ToString();
            txtName.Text = dt.Rows[0]["PartyName"].ToString();
            txtCNIC.Text = dt.Rows[0]["CNICNo"].ToString();
            txtContactNo.Text = dt.Rows[0]["ContactNo"].ToString();
            txtCity.Text = dt.Rows[0]["City"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            txtWeb.Text = dt.Rows[0]["Web"].ToString();
            txtNTN.Text = dt.Rows[0]["NTN"].ToString();
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
            txtId.Tag = dt.Rows[0]["AccountID"].ToString();
            //chkSupplier.Checked = Convert.ToBoolean(dt.Rows[0]["IsSupplier"].ToString());
            //chkCustomer.Checked = Convert.ToBoolean(dt.Rows[0]["IsCustomer"].ToString());

            txtOpDebit.Text = decimal.Parse(dt.Rows[0]["OpeningDebit"].ToString()).ToString("G29");
            txtOpCredit.Text = decimal.Parse(dt.Rows[0]["OpeningCredit"].ToString()).ToString("G29");
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!vOpenMode) return;
                //if (!MessageBox.ConfrmDelMsg()) return;

                objDAL.DeleteRecord(Int64.Parse(txtId.Text));

                //Delete Account
                var AccDAL = new DAL.AccountChart();
                AccDAL.connectionstring = objDAL.connectionstring;
                AccDAL.DeleteRecord(txtId.Tag.ToString());

                MessageBox.Show("Record Deleted");
                //LoadGrid();
                cmdClear_Click(sender, e);

            }
            catch (Exception exc)
            {
                MessageBox.Show("Error", exc.Message);
            }
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }
        #region // Control Operations
        private void ClearFields()
        {
            try
            {
                txtId.Text = objDAL.getNextNo().ToString();
                txtId.Tag = string.Empty;
                txtName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtCNIC.Text = string.Empty;
                txtContactNo.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtWeb.Text = string.Empty;
                txtNTN.Text = string.Empty;
                this.txtOpDebit.Text = "0";
                this.txtOpCredit.Text = "0";
                vOpenMode = false;
                //Grid.Enabled = false;
                //txtFilter.Text = string.Empty;
                //txtFilter.Enabled = false;
                //chkCustomer = 0;
                //chkSupplier.Checked += 0;
                txtName.Focus();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error", exc.Message);
            }
        }
        #endregion
    }
}
