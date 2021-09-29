using Bunifu.Framework.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace Sahil
{
    class Sahil
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Sahil"].ToString());
        static public Boolean IsNull(BunifuMaterialTextbox TextboxName)
        {
            if (TextboxName.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public void EnterKeyWithoutNull(KeyEventArgs e, BunifuMaterialTextbox CurrentTextboxName, Control NextControlName)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!IsNull(CurrentTextboxName))
                    NextControlName.Focus();
                else
                    MessageBox.Show("Plese enter value","Alert",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        static public Boolean IsNull(TextBox TextboxName)
        {
            if (TextboxName.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public void EnterKeyWithoutNull(KeyEventArgs e, TextBox CurrentTextboxName, Control NextControlName)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!IsNull(CurrentTextboxName))
                    NextControlName.Focus();
                else
                    MessageBox.Show("Plese enter value", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        static public void EnterKey(KeyEventArgs e, Control NextTextboxName)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextTextboxName.Focus();
            }
        }
        static public void EnterKeyCombo(KeyEventArgs e,ComboBox Name ,Control NextTextboxName)
        {
            if (Name.Text!="--Select--")
            {
                NextTextboxName.Focus();
            }
            else
            {
                MessageBox.Show("Plese Select value", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        static public void MobileNumber(KeyPressEventArgs e, TextBox ControlName)
        {
            ControlName.MaxLength = 10;
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Only Number","Alert",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        static public void IsNumber(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Only Number","Alert",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        static public void IsName(KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Only Character","Alert",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        static public void IsAmount(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Only Character","Alert",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        static public void ExecuteNonQuery(String Query)
        {
            try
            { 
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data Saved Successfully...!","Success");
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        static public void ExecuteNonQueryWithoutMsg(String Query)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        static public void Delete(String Query)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data Deleted Successfully...!", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                con.Close();
            }
        }
        static public void ClearTemp(String TableName)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from "+TableName+"", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                con.Close();
            }
        }
        static public void BindGrid(string Query,BunifuCustomDataGrid GridviewName)
        {
            try
            {
                con.Open();
                SqlCommand sc = new SqlCommand(Query, con);
                SqlDataAdapter adp = new SqlDataAdapter(sc);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                GridviewName.DataSource = dt;
                GridviewName.Columns[0].Visible = false;
                con.Close();
            }catch(Exception)
            {
                MessageBox.Show("Something Wrong..!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }
        }
        static public DataTable ExecuteDataTable(string Query)
        {
            DataTable dt=null;
            try
            {
                con.Open();
                SqlCommand sc = new SqlCommand(Query, con);
                SqlDataAdapter adp = new SqlDataAdapter(sc);
                dt = new DataTable();
                adp.Fill(dt);
                con.Close();   
            }
            catch (Exception)
            {
                MessageBox.Show("Something Wrong..!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }
            return dt;
        }
        static public void BindCombo(string ColumnName,string TableName,ComboBox ComboBoxName)
        {
            try
            {
                con.Open();
                SqlCommand sc = new SqlCommand("select Id,"+ColumnName+" from "+TableName+"", con);
                SqlDataAdapter adp = new SqlDataAdapter(sc);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                DataRow dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "--Select--" };
                dt.Rows.InsertAt(dr, 0);
                ComboBoxName.ValueMember = "Id";
                ComboBoxName.DisplayMember = ColumnName;
                ComboBoxName.DataSource = dt;
                con.Close();
            }
            catch(Exception )
            {
                MessageBox.Show("Something Wrong..!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }
        }
        static public void AutoComplete(string ColumnName, string TableName,TextBox TextBoxName)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select " + ColumnName + " from " + TableName + "", con);
                SqlDataReader dr = cmd.ExecuteReader();
                AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
                while(dr.Read())
                {
                    try
                    {
                        ac.Add(dr.GetString(0));
                    }catch(Exception )
                    {
                        break;
                    }
                }
                TextBoxName.AutoCompleteCustomSource = ac;
                TextBoxName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                TextBoxName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                con.Close();
            }catch(Exception )
            {
                MessageBox.Show("Something Wrong..!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }
        }
        static public void Clear(Control cn)
        {
            foreach(Control c in cn.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Text = "";
                if (c is BunifuMaterialTextbox)
                    ((BunifuMaterialTextbox)c).Text = "";
                if (c is ComboBox)
                    ((ComboBox)c).Text = "--Select--";
            }
        }
        static public string InvoiceNumber(string TableName,string Date)
        {
            DataTable dt = null;
            string Invoice=string.Empty,Postfix=string.Empty;
            try
            {
                dt = Sahil.ExecuteDataTable("select Prefix from Prefix where '" + Date + "' between StartDate and EndDate");
                if(dt.Rows.Count > 0)
                {
                    Postfix = dt.Rows[0]["Prefix"].ToString();
                }
                DataTable Number = ExecuteDataTable("select count(*)+1 as Count from " + TableName + " where InvoiceNo like '%" + Postfix + "'");
                int Count = Convert.ToInt32(Number.Rows[0]["Count"]);
                if (Count < 10)
                    Invoice = "000" + Count + Postfix;
                else if (Count >= 10 && Count < 100)
                    Invoice = "00" + Count + Postfix;
                else if (Count >= 100 && Count < 1000)
                    Invoice = "0" + Count + Postfix;
                else
                    Invoice = Count + Postfix;
            }
            catch (Exception)
            {
                MessageBox.Show("Something Wrong..!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }
            return Invoice;
        }
        static public int IdNumber(string TableName)
        {
            DataTable dt = null;
            int Invoice = 0;
            try
            {
                con.Open();
                SqlCommand sc = new SqlCommand("select Id from " + TableName + " order by Id desc", con);
                SqlDataAdapter adp = new SqlDataAdapter(sc);
                dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Invoice = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Something Wrong..!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }
            return (Invoice);
        }
        public static String ChangeToWords(String numb)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = ("Only.");
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        //int p = points.Length;
                        //char[] TPot = points.ToCharArray();
                        andStr = (" and ");// just to separate whole numbers from points/Rupees                  
                        //for(int i=0;i<p;i++)
                        //{
                        //    andStr += ones(Convert.ToString(TPot[i]))+" ";
                        //}
                        andStr += translateWholeNumber(points).Trim() + " Paise";

                    }

                }
                else
                {
                    andStr += translateWholeNumber(points).Trim() + "Rupees";
                }
                val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
            catch
            {
                ;
            }
            return val;
        }
        static public String translateWholeNumber(String number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX
                bool isDone = false;//test if already translated
                double dblAmt = (Convert.ToDouble(number));
                //if ((dblAmt > 0) && number.StartsWith("0"))

                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric
                    beginsZero = number.StartsWith("0");
                    int numDigits = number.Length;
                    int pos = 0;//store digit grouping
                    String place = "";//digit grouping name:hundres,thousand,etc...
                    switch (numDigits)
                    {
                        case 1://ones' range
                            word = ones(number);
                            isDone = true;
                            break;
                        case 2://tens' range
                            word = tens(number);
                            isDone = true;
                            break;
                        case 3://hundreds' range
                            pos = (numDigits % 3) + 1;
                            place = " Hundred ";
                            break;
                        case 4://thousands' range
                        case 5:
                            pos = (numDigits % 4) + 1;
                            place = " Thousand ";
                            break;
                        case 6:

                        case 7://millions' range
                            pos = (numDigits % 6) + 1;
                            // place = " Million ";
                            place = " Lakh ";
                            break;
                        case 8:
                        case 9:

                        case 10://Billions's range
                            pos = (numDigits % 8) + 1;
                            place = " Core ";
                            break;
                        //add extra case options for anything above Billion...
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)
                        if (beginsZero) place = "";
                        word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                        //check for trailing zeros
                        if (beginsZero) word = " and " + word.Trim();
                    }
                    //ignore digit grouping names
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch
            {
                ;
            }
            return word.Trim();
        }
        private static String tens(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = null;
            switch (digt)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "Fifteen";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty";
                    break;
                case 30:
                    name = "Thirty";
                    break;
                case 40:
                    name = "Fourty";
                    break;
                case 50:
                    name = "Fifty";
                    break;
                case 60:
                    name = "Sixty";
                    break;
                case 70:
                    name = "Seventy";
                    break;
                case 80:
                    name = "Eighty";
                    break;
                case 90:
                    name = "Ninety";
                    break;
                default:
                    if (digt > 0)
                    {
                        name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));
                    }
                    break;
            }
            return name;
        }
        private static String ones(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = "";
            switch (digt)
            {
                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
        }
        public static string GetSqlString(string Value)  // for avoid sql injection 
        {
            return Value.Replace("'", "''");
        }
        static public int NextID(string TableName)
        {
            DataTable dt = null;
            int Invoice = 0;
            try
            {
                con.Open();
                SqlCommand sc = new SqlCommand("select count(Id)+1 as Id from " + TableName + "", con);
                SqlDataAdapter adp = new SqlDataAdapter(sc);
                dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Invoice = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Something Wrong..!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }
            return (Invoice);
        }
    }
}
