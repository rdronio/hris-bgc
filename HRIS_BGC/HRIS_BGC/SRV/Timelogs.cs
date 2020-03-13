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
    public class Timelogs
    {
        Common common = new Common();
        string sMessage;
        public string date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("yyyy-MM-dd");
        string sCon = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString; //@"Data Source=DESKTOP-991KFNO\SQLEXPRESS;Initial Catalog=RHBPDB;Integrated Security=True";
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sSQL;
        public void InsertTimeInOut(int employeeid, string date, string time, string type)
        {
            try
            {
                con = new SqlConnection(sCon);
                con.Open();
                cmd = new SqlCommand("INSERT INTO db_owner.Timelogs (Emp_ID, Timelogs_date, Timelogs_time) VALUES(@Emp_ID, @Timelogs_date, @Timelogs_time) ", con);


                cmd.Parameters.AddWithValue("Emp_ID", employeeid);
                cmd.Parameters.AddWithValue("Timelogs_date", date);
                cmd.Parameters.AddWithValue("Timelogs_time", time);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object IsTimeIn(int empid, DataTable dt)
        {
            try
            {
                con = new SqlConnection(sCon);
                con.Open();
                sSQL = "select MIN(Timelogs_time) as Timelogs_time from db_owner.Timelogs where Emp_ID = "+ empid+ " AND Timelogs_date = '"+ date +"' GROUP BY Timelogs_date";
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

        public object IsTimeOut(int empid, DataTable dt)
        {
            try
            {
                dt.Clear();
                con = new SqlConnection(sCon);
                con.Open();
                //sSQL = "select * from db_owner.Timelogs where Emp_ID = " + empid + " AND Timelogs_date = '" + date + "' and Timelogs_type = 0";
                sSQL = "select CASE WHEN COUNT(Timelogs_time) = 1 then NULL else MAX(Timelogs_time) END AS Timelogs_time from db_owner.Timelogs where Emp_ID = "+ empid +" and Timelogs_date = '"+ date +"' GROUP BY Timelogs_date";
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


        public void FileOvertime(int employeeid, double totalhours, string reason, string overtime_date)
        {
            try
            {
                con = new SqlConnection(sCon);
                con.Open();
                cmd = new SqlCommand("INSERT INTO db_owner.Overtime (Emp_ID, reason, overtime_date, total_hours) VALUES(@Emp_ID, @reason, @overtime_date, @total_hours) ", con);


                cmd.Parameters.AddWithValue("Emp_ID", employeeid);
                cmd.Parameters.AddWithValue("overtime_date", overtime_date);
                cmd.Parameters.AddWithValue("reason", reason);
                cmd.Parameters.AddWithValue("total_hours", totalhours);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddTimeAlteration(int employeeid, string timealteration_date, string timealteration_time, string timealteration_reason, string timealteration_type)
        {
            try
            {
                string status = "0";
                con = new SqlConnection(sCon);
                con.Open();
                cmd = new SqlCommand("INSERT INTO db_owner.TimeAlteration (emp_ID, timealteration_date, timealteration_time, timealteration_reason, timealteration_type, timealteration_status) VALUES(@emp_ID, @timealteration_date, @timealteration_time, @timealteration_reason, @timealteration_type, @timealteration_status) ", con);


                cmd.Parameters.AddWithValue("Emp_ID", employeeid);
                cmd.Parameters.AddWithValue("timealteration_date", timealteration_date);
                cmd.Parameters.AddWithValue("timealteration_time", timealteration_time);
                cmd.Parameters.AddWithValue("timealteration_reason", timealteration_reason);
                cmd.Parameters.AddWithValue("timealteration_type", timealteration_type);
                cmd.Parameters.AddWithValue("timealteration_status", status);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelTimeAlteration(int timealteration_id)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("Delete from db_owner.TimeAlteration where timealteration_id =" + timealteration_id, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateTimeAlteration(int timealteration_id, string timealteration_status)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("UPDATE db_owner.TimeAlteration SET timealteration_status = '" + timealteration_status + "' WHERE timealteration_id =" + timealteration_id, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
