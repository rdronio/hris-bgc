using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace SRV
{
    public class Lib_Holiday
    {
        string sMessage;
        Common common = new Common();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sSQL;

        public void AddHolidayList(string startDate, string endDate, int numberOfDays, string description, List<DateTime> allDates)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                //cmd = new SqlCommand("INSERT INTO db_owner.Holiday (holiday_date, holiday_desc) VALUES(@holiday_date, @holiday_desc) ", con);

                //cmd.Parameters.AddWithValue("holiday_date", DateTime.Parse(date));
                //cmd.Parameters.AddWithValue("holiday_desc", description);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "db_owner.AddHolidayList_PKG";
                cmd.Connection = con;
                cmd.Parameters.Add("@holidaynotice_id", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@holiday_startDate", SqlDbType.Date).Value = startDate;
                cmd.Parameters.Add("@holiday_endDate", SqlDbType.Date).Value = endDate;
                cmd.Parameters.Add("@holiday_desc", SqlDbType.VarChar).Value = description;
                cmd.Parameters.Add("@holiday_numberOfDays", SqlDbType.Int).Value = numberOfDays;

                try
                {
                    con.Open();
                    object obj = cmd.ExecuteScalar();

                    SaveHoliday(int.Parse(obj.ToString()), allDates, description);

                    //string holiday_id = cmd.Parameters["holiday_id"].Value.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveHoliday(int holiday_id, List<DateTime> allDates, string holiday_desc)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                for (int i = 0; i < allDates.Count; i++)
                {
                    string holidaysQL = "INSERT INTO db_owner.Holiday (holidaynotice_id, holiday_date, holiday_desc) VALUES (" + holiday_id + ", '" + allDates[i] + "', '" + holiday_desc + "')";
                    SqlCommand tempCommand = new SqlCommand(holidaysQL, con);
                    tempCommand.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditHoliday(int holidayid, string date, string description)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("UPDATE db_owner.Holiday SET holiday_date = "+ date +", holiday_desc = "+ description +" where holiday_id =" + holidayid, con);

                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteHoliday(int holidaynotice_id)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("DELETE FROM db_owner.HolidayList where holiday_id=" + holidaynotice_id +"; DELETE FROM db_owner.Holiday where holidaynotice_id =" + holidaynotice_id, con);

                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int holidaynotice_id)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("DELETE FROM db_owner.Holiday where holidaynotice_id =" + holidaynotice_id, con);

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
