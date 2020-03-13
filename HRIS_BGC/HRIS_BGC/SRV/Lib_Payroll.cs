using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net;
using System.IO;


namespace SRV
{
    public class Lib_Payroll
    {
        string sMessage;
        public string sCon = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString; //@"Data Source=DESKTOP-991KFNO\SQLEXPRESS;Initial Catalog=RHBPDB;Integrated Security=True";
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sSQL;
        public void CreatePayslip(int empid, string fromDate, string toDate, string totalhourpay, string totalovertimepay, string totaltardiness, int leave, string sss, string philhealth, string pagibig, int otherdeductions, string grosspay, string netpay, string payroll_status)
        {
            try
            {
                con = new SqlConnection(sCon);
                con.Open();
                cmd = new SqlCommand("INSERT INTO db_owner.Payroll (Emp_ID, Payroll_fromDate, Payroll_toDate, TotalHourPay, TotalOvertimePay, TotalTardinessPay, Leave, SSS, PhilHealth, Pagibig, OtherDeductions, GrossPay, NetPay, payroll_status) VALUES(@Emp_ID, @Payroll_fromDate, @Payroll_toDate, @TotalHourPay, @TotalOvertimePay, @TotalTardinessPay, @Leave, @SSS, @PhilHealth, @Pagibig, @OtherDeductions, @GrossPay, @NetPay, @payroll_status) ", con);


                cmd.Parameters.AddWithValue("Emp_ID", empid);
                cmd.Parameters.AddWithValue("Payroll_fromDate", DateTime.Parse(fromDate));
                cmd.Parameters.AddWithValue("Payroll_toDate", DateTime.Parse(toDate));
                cmd.Parameters.AddWithValue("TotalHourPay", Double.Parse(totalhourpay));
                cmd.Parameters.AddWithValue("TotalOvertimePay", Double.Parse(totalovertimepay));
                cmd.Parameters.AddWithValue("TotalTardinessPay", Double.Parse(totaltardiness));
                cmd.Parameters.AddWithValue("Leave", leave);
                cmd.Parameters.AddWithValue("SSS", Double.Parse(sss));
                cmd.Parameters.AddWithValue("PhilHealth", Double.Parse(philhealth));
                cmd.Parameters.AddWithValue("Pagibig", Double.Parse(pagibig));
                cmd.Parameters.AddWithValue("OtherDeductions", otherdeductions);
                cmd.Parameters.AddWithValue("GrossPay", Double.Parse(grosspay));
                cmd.Parameters.AddWithValue("NetPay", Double.Parse(netpay));
                cmd.Parameters.AddWithValue("payroll_status", payroll_status);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePayroll(string status, int payrollid)
        {
            try
            {
                con = new SqlConnection(sCon);
                con.Open();
                cmd =  new SqlCommand("UPDATE db_owner.Payroll SET payroll_status = '" + status + "' WHERE Payroll_ID =" + payrollid, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelPayroll(int payroll_id)
        {
            try
            {
                con = new SqlConnection(sCon);
                con.Open();
                cmd = new SqlCommand("Delete from db_owner.Payroll where Payroll_ID =" + payroll_id, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveDataTable(DataTable dt)
        {
            try
            {
                con = new SqlConnection(sCon);
                con.Open();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["Emp_ID"].ToString();
                    string dateFrom = dt.Rows[i]["Payroll_fromDate"].ToString();
                    string dateTo = dt.Rows[i]["Payroll_toDate"].ToString();
                    string totalhourpay = dt.Rows[i]["TotalHourPay"].ToString();
                    string totalovertimepay = dt.Rows[i]["TotalOvertimePay"].ToString();
                    string totaltardiness = dt.Rows[i]["TotalTardinessPay"].ToString();
                    string grosspay = dt.Rows[i]["GrossPay"].ToString();
                    string netpay = dt.Rows[i]["NetPay"].ToString();
                    int leave = 0;
                    string sss = dt.Rows[i]["SSS"].ToString();
                    string phil = dt.Rows[i]["PhilHealth"].ToString();
                    string pagibig = dt.Rows[i]["Pagibig"].ToString();
                    int otherdeductions = 0;
                    string payroll_status = "0";

                    string tempSql = "INSERT INTO db_owner.Payroll (Emp_ID, Payroll_fromDate, Payroll_toDate, TotalHourPay, TotalOvertimePay, TotalTardinessPay, Leave, SSS, PhilHealth, Pagibig, OtherDeductions, GrossPay, NetPay, payroll_status) ";
                    tempSql += "VALUES ";
                    tempSql += "("+ empid +", '"+ dateFrom +"', '"+ dateTo +"', "+ totalhourpay +", "+ totalovertimepay +", "+ totaltardiness +", "+ leave +", "+ sss +", "+ phil +", "+ pagibig +", "+ otherdeductions +", "+ grosspay +", "+ netpay +", "+ payroll_status +")";
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

    }
}
