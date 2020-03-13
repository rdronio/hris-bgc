using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SRV
{
    public class Lib_Overtime
    {
        string sMessage;
        Common common = new Common();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sSQL;

        public void CancelOvertime(int overtimeid)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("Delete from db_owner.Overtime where Overtime_ID =" + overtimeid, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOvertime(string status, int overtimeid)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("UPDATE db_owner.Overtime SET overtime_status = '" + status + "' WHERE Overtime_ID =" + overtimeid, con);

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
