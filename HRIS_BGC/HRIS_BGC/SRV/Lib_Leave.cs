using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SRV
{
    public class Lib_Leave
    {
        string sMessage;
        Common common = new Common();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sSQL;

        public void FileLeave(int empid, string leavetype, string date_from, string date_to, string reason, string status, int numberOfDays, List<DateTime> allDates)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("INSERT INTO db_owner.LeaveRecord (Emp_ID, file_date, leave_type, leave_from, leave_to, Reason, leave_status, numberOfDays) VALUES(@Emp_ID, @file_date, @leave_type, @leave_from, @leave_to, @Reason, @leave_status, @numberOfDays)", con);

                cmd.Parameters.AddWithValue("Emp_ID", empid);
                cmd.Parameters.AddWithValue("file_date", DateTime.Parse(common.pacifictime));
                cmd.Parameters.AddWithValue("leave_type", leavetype);
                cmd.Parameters.AddWithValue("leave_from", DateTime.Parse(date_from));
                cmd.Parameters.AddWithValue("leave_to", DateTime.Parse(date_to));
                cmd.Parameters.AddWithValue("Reason", reason);
                cmd.Parameters.AddWithValue("leave_status", status);
                cmd.Parameters.AddWithValue("numberOfDays", numberOfDays);

                // //TO GET THE Identity ID
                //int modified = Convert.ToInt32(cmd.ExecuteScalar());

                cmd.ExecuteNonQuery();
                con.Close();

                ////To LeaveDateList -- History of all dates
                //LeaveDates(allDates, empid, modified);


                if (leavetype == "Vacation")
                {

                    UpdateVacationLeaveCount(empid,numberOfDays);
                }
                else if (leavetype == "Sick")
                {
                    UpdateSickLeaveCount(empid,numberOfDays);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void LeaveDates(List<DateTime> alldates, int empid, int leave_id)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                for (int i = 0; i < alldates.Count; i++)
                {
                    string tempSql = "INSERT INTO db_owner.LeaveDateList (leave_id, emp_id, leave_date) VALUES (" + leave_id + ", " + empid + ", '" + alldates[i] + "')";
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

        //Update leave status
        public void UpdateLeave(string withpay, string status, int leaveid)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("UPDATE db_owner.LeaveRecord SET leave_status = '" + status + "' WHERE Leave_ID =" + leaveid, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Reject leave with remarks

        public void RejectLeave(string withpay, string status, int leaveid, string remarks)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("UPDATE db_owner.LeaveRecord SET leave_status = '" + status + "', withpay='" + withpay + "', leave_remarks = '" + remarks + "' WHERE Leave_ID =" + leaveid, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Update LeaveCount Table - Vacation Columns
        public void UpdateVacationLeaveCount(int emp_id, int numberOfDays)
        {
            try
            {
                //numberOfDays = numberOfDays * 8; // multiply by 8 to convert into hours
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("UPDATE db_owner.LeaveCount SET used_vacation_leave = used_vacation_leave + "+ numberOfDays +" where emp_id = " + emp_id, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Update LeaveCount Table - Sick Columns
        public void UpdateSickLeaveCount(int emp_id, int numberOfdays)
        {
            try
            {
                numberOfdays = numberOfdays * 8; // multiply by 8 to convert into hours

                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("UPDATE db_owner.LeaveCount SET used_sickLeave = used_sickLeave + "+ numberOfdays +" where emp_id = " + emp_id, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelLeave(int leaveid)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("Delete from db_owner.LeaveRecord where Leave_ID =" + leaveid, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddLeaveCount(int empid, int vacation_leave, int sick_leave, string leavecount_year)
        {
            try
            {
                int used_vleave = 0;
                int used_sleave = 0;

                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("INSERT INTO db_owner.LeaveCount (Emp_ID, vacation_leave, sick_leave, leavecount_year, used_vacation_leave, used_sickleave) VALUES(@Emp_ID, @vacation_leave, @sick_leave, @leavecount_year, @used_vacation_leave, @used_sickleave) ", con);

                cmd.Parameters.AddWithValue("Emp_ID", empid);
                cmd.Parameters.AddWithValue("vacation_leave", vacation_leave);
                cmd.Parameters.AddWithValue("sick_leave", sick_leave);
                cmd.Parameters.AddWithValue("leavecount_year", leavecount_year);
                cmd.Parameters.AddWithValue("used_vacation_leave", used_vleave);
                cmd.Parameters.AddWithValue("used_sickleave", used_sleave);

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
