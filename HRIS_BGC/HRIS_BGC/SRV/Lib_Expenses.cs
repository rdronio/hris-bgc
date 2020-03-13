using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace SRV
{
    public class Lib_Expenses
    {
        string sMessage;
        Common common = new Common();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sSQL;

        public void AddExpenses(int empid, string expense_date, string expense_desc, string expense_reason, double expense_rate, string status)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("INSERT INTO db_owner.Expense (emp_id, expense_date, expense_description, expense_reason, expense_rate, expense_status) VALUES(@emp_id, @expense_date, @expense_description, @expense_reason, @expense_rate, @expense_status) ", con);

                cmd.Parameters.AddWithValue("emp_id", empid);
                cmd.Parameters.AddWithValue("expense_date", DateTime.Parse(expense_date));
                cmd.Parameters.AddWithValue("expense_description", expense_desc);
                cmd.Parameters.AddWithValue("expense_reason", expense_reason);
                cmd.Parameters.AddWithValue("expense_rate", expense_rate);
                cmd.Parameters.AddWithValue("expense_status", status);

                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelExpenses(int expense_id)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("Delete from db_owner.Expense where expense_id =" + expense_id, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateApprovedExpense(string status, int expenseid)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("UPDATE db_owner.Expense SET expense_status = '" + status + "' WHERE expense_id =" + expenseid, con);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRejectedExpense(string status, int expenseid, string remarks)
        {
            try
            {
                con = new SqlConnection(common.sCon);
                con.Open();
                cmd = new SqlCommand("UPDATE db_owner.Expense SET expense_status = '" + status + "', expense_remarks = '"+ remarks +"' WHERE expense_id =" + expenseid, con);

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
