using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace SRV
{
    public class Lib_Undertime
    {
        string sMessage;
        Common common = new Common();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sSQL;

        public void FileUndertime(int employeeid, string timein, string timeout, string date, double totalundertime, string reason)
        {
            try
            {
                string status = "0";
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("INSERT INTO db_owner.Undertime (Emp_ID, undertime_date, time_in, time_out, total_undertime, undertime_reason, undertime_status) VALUES(@Emp_ID, @undertime_date, @time_in, @time_out, @total_undertime, @undertime_reason, @undertime_status) ", con);


                cmd.Parameters.AddWithValue("Emp_ID", employeeid);
                cmd.Parameters.AddWithValue("undertime_date", date);
                cmd.Parameters.AddWithValue("time_in", timein);
                cmd.Parameters.AddWithValue("time_out", timeout);
                cmd.Parameters.AddWithValue("undertime_reason", reason);
                cmd.Parameters.AddWithValue("undertime_status", status);
                cmd.Parameters.AddWithValue("total_undertime", totalundertime);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateVacationLeaveCount(int emp_id, double numberofHours)
        {
            try
            {

                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("UPDATE db_owner.LeaveCount SET used_vacation_leave = used_vacation_leave + " + numberofHours + " where emp_id = " + emp_id, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSickLeaveCount(int emp_id, double numberofHours)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("UPDATE db_owner.LeaveCount SET used_sickLeave = used_sickLeave + " + numberofHours + " where emp_id = " + emp_id, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateUndertime(string status, int undertime_id)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("UPDATE db_owner.Undertime SET undertime_status = '" + status + "' WHERE undertime_id =" + undertime_id, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRejectedUndertime(string status, int undertime_id, string remarks)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("UPDATE db_owner.Undertime SET undertime_status = '" + status + "', undertime_remarks = '" + remarks + "' WHERE undertime_id =" + undertime_id, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelUndertime(int undertimeid)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("Delete from db_owner.Undertime where undertime_id =" + undertimeid, con);

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
