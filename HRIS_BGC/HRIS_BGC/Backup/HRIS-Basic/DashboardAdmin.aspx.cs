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
    public partial class DashboardAdmin : System.Web.UI.Page
    {
        Common objCommon = new Common();
        DataTable dt = new DataTable();
        DataTable dtovertime = new DataTable();
        DataTable dtalteration = new DataTable();
        DataTable dtDashboard = new DataTable();
        DataTable dtpayroll = new DataTable();
        DataTable dtexpense = new DataTable();
        DataTable dtundertime = new DataTable();
        DataTable dtregular = new DataTable();
        DataTable dtLeaveToday = new DataTable();
        DataTable dtEvaluation = new DataTable();
        DataTable dtHoliday = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Reminders

                //Payroll Count
                string sqlPayroll = "select COUNT(Payroll_ID) as payrollcount from db_owner.Payroll where payroll_status = 0";
                objCommon.LoadDataTable(sqlPayroll, dtpayroll);

                if (dtpayroll.Rows.Count != 0)
                {
                    lblPPA.InnerText = dtpayroll.Rows[0]["payrollcount"].ToString();
                }
                //Leave Count
                string sqlLeave = "select COUNT(Leave_ID) as leavecount from db_owner.LeaveRecord where leave_status = 0";
                objCommon.LoadDataTable(sqlLeave, dt);

                if (dt.Rows.Count != 0)
                {
                    lblPLA.InnerText = dt.Rows[0]["leavecount"].ToString();
                }

                //Overtime Count
                string sqlOvertime = "select COUNT(Overtime_ID) overtimecount from db_owner.Overtime where overtime_status = 0";
                objCommon.LoadDataTable(sqlOvertime, dtovertime);

                if (dtovertime.Rows.Count != 0)
                {
                    lblPOA.InnerText = dtovertime.Rows[0]["overtimecount"].ToString();
                }

                //TimeAlteration Count
                string sqlAlteration = "select COUNT(timealteration_id) timealterationcount from db_owner.TimeAlteration where timealteration_status = 0";
                objCommon.LoadDataTable(sqlAlteration, dtalteration);

                if (dtalteration.Rows.Count != 0)
                {
                    lblPTAA.InnerText = dtalteration.Rows[0]["timealterationcount"].ToString();
                }

                //Expense Count
                string sqlExpense = "select COUNT(expense_id) expensecount from db_owner.Expense where expense_status = 0";
                objCommon.LoadDataTable(sqlExpense, dtexpense);

                if (dtexpense.Rows.Count != 0)
                {
                    lblEA.InnerText = dtexpense.Rows[0]["expensecount"].ToString();
                }
                
                //Undertime Count
                string sqlUndertime = "select COUNT(undertime_id) undertimecount from db_owner.Undertime where undertime_status = 0";
                objCommon.LoadDataTable(sqlUndertime, dtundertime);

                if (dtundertime.Rows.Count != 0)
                {
                    lblUA.InnerText = dtundertime.Rows[0]["undertimecount"].ToString();
                }

                //Holiday
                string sqlHoliday = "select holiday_desc, convert(varchar, holiday_date, 107) holiday_date from db_owner.Holiday where DATENAME(MONTH, holiday_date) = DATENAME(MONTH, GETDATE()) order by holiday_date";
                objCommon.LoadDataTable(sqlHoliday, dtHoliday);

                if (dtHoliday.Rows.Count != 0)
                {
                    dgHoliday.HeaderStyle.Font.Bold = true;
                    dgHoliday.HeaderStyle.Font.Size = 8;


                    dgHoliday.DataSource = dtHoliday;
                    dgHoliday.DataBind();
                }
                else
                {
                    dgHoliday.Visible = false;
                    lblNoHoliday.Visible = true;
                }

                //Today's Employee Time Record
                string sqlDashboard = "SELECT C.Timelogs_date, E.Position_title AS POSITION ,(UPPER(LEFT(cast(D.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(D.Emp_lname as nvarchar(max)),2,LEN(cast(D.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(D.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(D.Emp_fname as nvarchar(max)),2,LEN(cast(D.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(D.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(D.Emp_mname as nvarchar(max)),2,LEN(cast(D.Emp_mname as nvarchar(max)))))) AS FullName, CONVERT(varchar(15), CAST(C.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15), CAST(C.TIMEOUT AS TIME),100) AS TIMEOUT from (select Timelogs_date,Emp_ID, Min(Timelogs_time) AS TIMEIN, CASE WHEN COUNT(Timelogs_time) > 1 THEN MAX(Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs where Timelogs_date = '" + objCommon.pacificdate + "' GROUP BY Emp_ID, Timelogs_date) C left join db_owner.Employee D on C.Emp_ID = D.Emp_ID left join db_owner.PositionTitle E on D.Position_ID = E.Position_ID";
                objCommon.LoadDataTable(sqlDashboard, dtDashboard);

                if (dtDashboard.Rows.Count != 0)
                {
                    dgDashboard.HeaderStyle.Font.Bold = true;
                    dgDashboard.HeaderStyle.Font.Size = 8;


                    dgDashboard.DataSource = dtDashboard;
                    dgDashboard.DataBind();
                }
                else
                {
                    dgDashboard.Visible = false;
                    lblNoRecords.Visible = true;
                }

                //Leave Today
                string sqlLeaveToday = "select C.Position_title, (UPPER(LEFT(cast(B.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_lname as nvarchar(max)),2,LEN(cast(B.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(B.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_fname as nvarchar(max)),2,LEN(cast(B.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(B.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_mname as nvarchar(max)),2,LEN(cast(B.Emp_mname as nvarchar(max)))))) AS FullName, a.Leave_ID, a.Emp_ID, a.leave_from, a.leave_to from db_owner.LeaveRecord A left join db_owner.Employee B on A.Emp_ID = b.Emp_ID left join db_owner.PositionTitle C on B.Position_ID = C.Position_ID where a.leave_from <= GETDATE() AND a.leave_to >= GETDATE() AND (DATENAME(WEEKDAY, GETDATE()) <> 'Saturday') AND (DATENAME(WEEKDAY, GETDATE()) <> 'Sunday')";
                objCommon.LoadDataTable(sqlLeaveToday, dtLeaveToday);

                if (dtLeaveToday.Rows.Count != 0)
                {

                    dgLeaveToday.HeaderStyle.Font.Bold = true;
                    dgLeaveToday.HeaderStyle.Font.Size = 8;


                    dgLeaveToday.DataSource = dtLeaveToday;
                    dgLeaveToday.DataBind();
                }
                else
                {
                    dgLeaveToday.Visible = false;
                    lblLeaveToday.Visible = true;
                }

                //Evaluation Notice 
                int countMonth = 5; //count of month before evaluation
                string sqlEvaluation = "select a.date_employed, A.Employment_status, A.Emp_ID, (UPPER(LEFT(cast(A.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_lname as nvarchar(max)),2,LEN(cast(A.Emp_lname as nvarchar(max)))))+ ', ' + UPPER(LEFT(cast(A.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_fname as nvarchar(max)),2,LEN(cast(A.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(A.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_mname as nvarchar(max)),2,LEN(cast(A.Emp_mname as nvarchar(max)))))) AS FullName, B.Department_name, C.Position_title from db_owner.Employee A left join db_owner.Department B on A.Department_ID = B.Department_ID left join db_owner.PositionTitle C on A.Position_ID = C.Position_ID where A.date_employed <= DATEADD(MONTH, "+ countMonth +", Date_employed) and a.employment_status not in ('regular', 'end') order by a.emp_lname";
                objCommon.LoadDataTable(sqlEvaluation, dtEvaluation);

                if (dtEvaluation.Rows.Count != 0)
                {

                    dgEvaluation.HeaderStyle.Font.Bold = true;
                    dgEvaluation.HeaderStyle.Font.Size = 8;


                    dgEvaluation.DataSource = dtEvaluation;
                    dgEvaluation.DataBind();
                }
                else
                {
                    dgEvaluation.Visible = false;
                    lblEvaluation.Visible = true;
                }

                //List of Regular Employees
                string sqlRegular = "select A.Employment_status, A.Emp_ID, (UPPER(LEFT(cast(A.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_lname as nvarchar(max)),2,LEN(cast(A.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(A.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_fname as nvarchar(max)),2,LEN(cast(A.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(A.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_mname as nvarchar(max)),2,LEN(cast(A.Emp_mname as nvarchar(max)))))) AS FullName, B.Department_name, C.Position_title from db_owner.Employee A left join db_owner.Department B on A.Department_ID = B.Department_ID left join db_owner.PositionTitle C on A.Position_ID = C.Position_ID where A.Employment_status = 'regular'";
                objCommon.LoadDataTable(sqlRegular, dtregular);

                if (dtregular.Rows.Count != 0)
                {
                    
                    dgRegular.HeaderStyle.Font.Bold = true;
                    dgRegular.HeaderStyle.Font.Size = 8;


                    dgRegular.DataSource = dtregular;
                    dgRegular.DataBind();
                }
                else
                {
                    dgRegular.Visible = false;
                    NoList.Visible = true;
                }
                lblRegCount.InnerText = dtregular.Rows.Count.ToString();
               
              
            }
        }
    }
}