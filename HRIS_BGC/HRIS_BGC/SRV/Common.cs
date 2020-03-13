using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Configuration;
using System.Net;
using System.IO;
using System.Web.UI.WebControls;

namespace SRV
{
    public class Common
    {
        string sMessage;
        public string sCon = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString; //@"Data Source=DESKTOP-991KFNO\SQLEXPRESS;Initial Catalog=RHBPDB;Integrated Security=True";
        public string pacifictime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("HH:mm");
        public string pacificdate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("yyyy-MM-dd");
        public string pacificmonth = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("yyyy-MM-dd");
        public string pacificyear = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("yyyy");
        public int PageSize = 10;
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sSQL;

        public object LogInPersist(string email, string pass, DataTable dt)
        {
            try
            {
                con = new SqlConnection(sCon);
                con.Open();
                sSQL = "SELECT * FROM db_owner.Employee WHERE Emp_email='" + email + "' AND Emp_pass='" + pass + "'";
                cmd = new SqlCommand(sSQL, con);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void InsertTimeInOut(int employeeid, string date, string time, string type)
        {

        }

        public string GetLocation(string ip)
        {
            var res = "";
            WebRequest request = WebRequest.Create("http://ipinfo.io/" + ip);
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    res += line;
                }
            }
            return res;
        }

        public object LoadData(DataSet ds)
        {
            try
            {
                string sSql = "Select * from db_owner.Payroll";
                con = new SqlConnection(sCon);
                con.Open();
                cmd = new SqlCommand(sSql, con);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                con.Close();
                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public object LoadDataTable(string sSql, DataTable dt)
        {
            try
            {
                dt.Clear();
                con = new SqlConnection(sCon);
                con.Open();
                cmd = new SqlCommand(sSql, con);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public object LoadDataTableView(int empid, string datefrom, string dateto, DataTable dt)
        {
            try
            {
                string sSql ="SELECT B.Emp_ID, convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT FROM (select A.Emp_ID, A.Timelogs_date, MIN(A.Timelogs_time) AS TIMEIN, CASE WHEN COUNT(A.Timelogs_time) > 1 THEN MAX(A.Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE Emp_ID ="+ empid +" AND (A.Timelogs_date >= '" + datefrom + "' AND '" + dateto + "' >= A.Timelogs_date) GROUP BY EMP_iD, Timelogs_date) B";
                dt.Clear();
                con = new SqlConnection(sCon);
                con.Open();
                cmd = new SqlCommand(sSql, con);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Dropdown for Department
        public void DropdownDepartment(DropDownList ddlUpline, string sSQL, string DataTextField, string DataValuefield)
        {

            DataTable dtUpline = new DataTable();

            using (SqlConnection con = new SqlConnection(sCon))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sSQL, con);
                    adapter.Fill(dtUpline);

                    ddlUpline.DataSource = dtUpline;
                    ddlUpline.DataTextField = DataTextField;
                    ddlUpline.DataValueField = DataValuefield;
                    ddlUpline.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            ddlUpline.Items.Insert(0, new ListItem("Select Department", "0"));

        }

        //Dropdown for Mass Compute Department
        public void DropdownDepartmentMass(DropDownList ddlUpline, string sSQL, string DataTextField, string DataValuefield)
        {

            DataTable dtUpline = new DataTable();

            using (SqlConnection con = new SqlConnection(sCon))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sSQL, con);
                    adapter.Fill(dtUpline);

                    ddlUpline.DataSource = dtUpline;
                    ddlUpline.DataTextField = DataTextField;
                    ddlUpline.DataValueField = DataValuefield;
                    ddlUpline.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            ddlUpline.Items.Insert(0, new ListItem("All", "0"));

        }

       

        //Dropdown for Position Title
        public void DropdownPosition(DropDownList ddlUpline, string sSQL, string DataTextField, string DataValuefield)
        {

            DataTable dtUpline = new DataTable();

            using (SqlConnection con = new SqlConnection(sCon))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sSQL, con);
                    adapter.Fill(dtUpline);

                    ddlUpline.DataSource = dtUpline;
                    ddlUpline.DataTextField = DataTextField;
                    ddlUpline.DataValueField = DataValuefield;
                    ddlUpline.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            ddlUpline.Items.Insert(0, new ListItem("Select Position", "0"));

        }

        //Dropdown for Employee
        public void DropdownEmployee(DropDownList ddlUpline, string sSQL, string DataTextField, string DataValuefield)
        {

            DataTable dtUpline = new DataTable();

            using (SqlConnection con = new SqlConnection(sCon))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sSQL, con);
                    adapter.Fill(dtUpline);

                    ddlUpline.DataSource = dtUpline;
                    ddlUpline.DataTextField = DataTextField;
                    ddlUpline.DataValueField = DataValuefield;
                    ddlUpline.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            ddlUpline.Items.Insert(0, new ListItem("Select Employee", "0"));

        }

        public void SaveDataTable(DataTable dt)
        {
            try
            {
                con = new SqlConnection(sCon);
                con.Open();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string timekeeping = dt.Rows[i]["Time"].ToString();
                    timekeeping = timekeeping.Replace("\r", "");

                    string tempSql = "INSERT INTO db_owner.Timelogs (bio_number, Timelogs_date, Timelogs_time) VALUES ("+dt.Rows[i]["Bio_number"].ToString()+" "+","+"'"+ dt.Rows[i]["Date"].ToString()+ "', '" + timekeeping +"')";
                    SqlCommand tempCommand = new SqlCommand(tempSql, con);
                    tempCommand.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveDataTableEmployee(DataTable dt)
        {
            //try
            //{
            //    con = new SqlConnection(sCon);
            //    con.Open();
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        string timekeeping = dt.Rows[i]["Timekeeping"].ToString();
            //        timekeeping = timekeeping.Replace("\r", "");

            //        string tempSql = "INSERT INTO db_owner.Timelogs (Emp_ID, Timelogs_date, Timelogs_time) VALUES (" + dt.Rows[i]["EmployeeID"].ToString() + " " + "," + "'" + dt.Rows[i]["Date"].ToString() + "', '" + timekeeping + "')";
            //        SqlCommand tempCommand = new SqlCommand(tempSql, con);
            //        tempCommand.ExecuteNonQuery();

            //    }
            //    con.Close();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        public void ChangePassword(string email, string newpass)
        {
            try
            {
                SqlConnection con = new SqlConnection(sCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE db_owner.Employee SET Emp_pass = @password WHERE Emp_email='" + email + "'", con);
                cmd.Parameters.AddWithValue("@password", newpass);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public object ConvertToEmptyIfNothing(object Value)
        {
            if (Value == null)
                return string.Empty;
            else
                return Value;
        }

        public string CapitalizeFirst(string s)
        {
            bool IsNewSentense = true;
            var result = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (IsNewSentense && char.IsLetter(s[i]))
                {
                    result.Append(char.ToUpper(s[i]));
                    IsNewSentense = false;
                }
                else
                    result.Append(s[i]);

                if (s[i] == '!' || s[i] == '?' || s[i] == '.')
                {
                    IsNewSentense = true;
                }
            }

            return result.ToString();
        }

        public bool IsNumber(string Value)
        {
            char cValue;
            var sValue = Value.Trim();

            int iIndex;

            for (iIndex = 0; iIndex <= (sValue.Length - 1); iIndex++)
            {
                cValue = char.Parse(sValue.Substring(iIndex, 1));
                if (char.IsNumber(cValue) == false)
                    return false;
            }

            return true;
        }

        public object MemoPoints(DataTable dt)
        {
            //Add another column "MemoPoints" on table Time Records
            dt.Columns.Add("MemoPoints", typeof(int));

            //Add value to the new column MemoPoints
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tardiness_min = dt.Rows[i]["TARDINESS"].ToString();
                double memo_points = double.Parse(tardiness_min) / 9;
                memo_points = Math.Round(memo_points, 0);
                dt.Rows[i]["MemoPoints"] = memo_points.ToString();
            }

            return dt;
        }

    }
}
