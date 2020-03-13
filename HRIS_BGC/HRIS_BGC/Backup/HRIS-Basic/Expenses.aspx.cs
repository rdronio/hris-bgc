using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;
using System.Data;

namespace HRIS_Basic
{
    public partial class Expenses : System.Web.UI.Page
    {
        Lib_Expenses objExpense = new Lib_Expenses();
        Common objCommon = new Common();
        DataTable dt = new DataTable();
        DataTable dtpending = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["Username"] == null)
                {
                    Response.Redirect("LoginPage.aspx");
                }
            }

            int empid = int.Parse(Session["Employee_ID"].ToString());
            //string sSQLStatement = "SELECT C.Emp_ID, C.DATE, C.TIMEIN, C.TIMEOUT, C.TOTAL, C.OVERTIME, CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) - (CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2))) - 1 AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME , CAST(ROUND(DATEDIFF(MINUTE, '8:00:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY FROM (select Emp_ID, Timelogs_date, MIN(Timelogs_time) AS TIMEIN, CASE WHEN COUNT(Timelogs_time) > 1 THEN MAX(Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE Emp_ID = " + empid + " GROUP BY EMP_iD, Timelogs_date) B) C";
            string sSQLStatement = "SELECT *, convert(varchar, expense_date, 107) DATE, CASE WHEN expense_status = 1 THEN 'Not Applicable' ELSE expense_remarks END AS REMARKS, CASE WHEN expense_status = 1 THEN 'Approved' ELSE 'Rejected' END AS STATUS from db_owner.Expense where expense_status not in (0) and emp_id =" + empid;

            //Table for Overtime 
            objCommon.LoadDataTable(sSQLStatement, dt);

            if (dt.Rows.Count != 0)
            {
                dgExpenses.DataSource = dt;
                dgExpenses.DataBind();
            }
            else
            {
                lblNoRecords.Visible = true;
                dgExpenses.Visible = false;

            }

            //Pending Expenses
            //Table for Pending Overtime
            string sql = "SELECT *, convert(varchar, expense_date, 107) DATE from db_owner.Expense where expense_status = 0 and emp_id =" + empid;

            objCommon.LoadDataTable(sql, dtpending);

            if (dtpending.Rows.Count != 0)
            {
                dgPendingExpenses.DataSource = dtpending;
                dgPendingExpenses.DataBind();
            }
            else
            {
                lblNoPendingRecords.Visible = true;
                dgPendingExpenses.Visible = false;
            }

        }

        protected void btnSubmitExpense_Click(object sender, EventArgs e)
        {
            int emp_id = int.Parse(Session["Employee_ID"].ToString());
            string expense_date = txtDateExpense.Value.Trim();
            string expense_desc = txtDescExpense.Value.Trim();
            double expense_rate = double.Parse(txtRateExpense.Value.Trim());
            string expense_reason = txtAreaReason.Value.Trim();
            string expense_status = "0";

            objExpense.AddExpenses(emp_id, expense_date, expense_desc, expense_reason, expense_rate, expense_status );
            Response.Redirect("Expenses.aspx");
        }

        //FOR CANCEL BUTTON - PENDING EXPENSES
        public void itemcommand(object sender, CommandEventArgs c)
        {
            LinkButton btn = (LinkButton)(sender);
            string expense_id = btn.CommandArgument;

            objExpense.CancelExpenses(int.Parse(expense_id));
            Response.Redirect("Expenses.aspx");
        }
    }
}