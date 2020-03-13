using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;
using System.Data;
using System.Collections;

namespace HRIS_Basic
{
    public partial class DashboardAdmin : System.Web.UI.Page
    {
        Common objCommon = new Common();
        sSQLStatement objSql = new sSQLStatement();
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
        DataTable dtTardiness = new DataTable();
        DataTable dtHoliday = new DataTable();
        //sql for regular employee
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] != "hradmin")
                {
                    Response.Redirect("LoginPage.aspx");
                }
                //Reminders

                ////Payroll Count
                //string sqlPayroll = "select COUNT(Payroll_ID) as payrollcount from db_owner.Payroll where payroll_status = 0";
                //objCommon.LoadDataTable(sqlPayroll, dtpayroll);

                //if (dtpayroll.Rows.Count != 0)
                //{
                //    lblPPA.InnerText = dtpayroll.Rows[0]["payrollcount"].ToString();
                //}
                ////Leave Count
                //string sqlLeave = "select COUNT(Leave_ID) as leavecount from db_owner.LeaveRecord where leave_status = 0";
                //objCommon.LoadDataTable(sqlLeave, dt);

                //if (dt.Rows.Count != 0)
                //{
                //    lblPLA.InnerText = dt.Rows[0]["leavecount"].ToString();
                //}

                ////Overtime Count
                //string sqlOvertime = "select COUNT(Overtime_ID) overtimecount from db_owner.Overtime where overtime_status = 0";
                //objCommon.LoadDataTable(sqlOvertime, dtovertime);

                //if (dtovertime.Rows.Count != 0)
                //{
                //    lblPOA.InnerText = dtovertime.Rows[0]["overtimecount"].ToString();
                //}

                ////TimeAlteration Count
                //string sqlAlteration = "select COUNT(timealteration_id) timealterationcount from db_owner.TimeAlteration where timealteration_status = 0";
                //objCommon.LoadDataTable(sqlAlteration, dtalteration);

                //if (dtalteration.Rows.Count != 0)
                //{
                //    lblPTAA.InnerText = dtalteration.Rows[0]["timealterationcount"].ToString();
                //}

                ////Expense Count
                //string sqlExpense = "select COUNT(expense_id) expensecount from db_owner.Expense where expense_status = 0";
                //objCommon.LoadDataTable(sqlExpense, dtexpense);

                //if (dtexpense.Rows.Count != 0)
                //{
                //    lblEA.InnerText = dtexpense.Rows[0]["expensecount"].ToString();
                //}
                
                ////Undertime Count
                //string sqlUndertime = "select COUNT(undertime_id) undertimecount from db_owner.Undertime where undertime_status = 0";
                //objCommon.LoadDataTable(sqlUndertime, dtundertime);

                //if (dtundertime.Rows.Count != 0)
                //{
                //    lblUA.InnerText = dtundertime.Rows[0]["undertimecount"].ToString();
                //}
            }

            BuildGrid();
        }

        public void BuildGrid()
        {
            //Holiday
            string sqlHoliday = "select holiday_desc, convert(varchar, holiday_date, 107) holiday_date from db_owner.Holiday where DATENAME(MONTH, holiday_date) = DATENAME(MONTH, '" + objCommon.pacificdate + "') AND DATENAME(YEAR, holiday_date) = DATENAME(YEAR, '" + objCommon.pacificdate + "')  order by holiday_date";
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

            ////Today's Employee Time Record
            //string sqlDashboard = "SELECT C.Timelogs_date, E.Position_title AS POSITION ,(UPPER(LEFT(cast(D.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(D.Emp_lname as nvarchar(max)),2,LEN(cast(D.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(D.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(D.Emp_fname as nvarchar(max)),2,LEN(cast(D.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(D.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(D.Emp_mname as nvarchar(max)),2,LEN(cast(D.Emp_mname as nvarchar(max)))))) AS FullName, CONVERT(varchar(15), CAST(C.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15), CAST(C.TIMEOUT AS TIME),100) AS TIMEOUT from (select Timelogs_date,Emp_ID, Min(Timelogs_time) AS TIMEIN, CASE WHEN COUNT(Timelogs_time) > 1 THEN MAX(Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs where Timelogs_date = '" + objCommon.pacificdate + "' GROUP BY Emp_ID, Timelogs_date) C left join db_owner.Employee D on C.Emp_ID = D.Emp_ID left join db_owner.PositionTitle E on D.Position_ID = E.Position_ID";
            //objCommon.LoadDataTable(sqlDashboard, dtDashboard);

            //if (dtDashboard.Rows.Count != 0)
            //{
            //    dgDashboard.HeaderStyle.Font.Bold = true;
            //    dgDashboard.HeaderStyle.Font.Size = 8;


            //    dgDashboard.DataSource = dtDashboard;
            //    dgDashboard.DataBind();
            //}
            //else
            //{
            //    dgDashboard.Visible = false;
            //    lblNoRecords.Visible = true;
            //}

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

            //Tardiness Notice
            string sqlTardiness = "";
            sqlTardiness = objSql.timerecordsTardiness(sqlTardiness);

            objCommon.LoadDataTable(sqlTardiness, dtTardiness);

            if (dtTardiness.Rows.Count != 0)
            {
                objCommon.MemoPoints(dtTardiness);


                var resultDt = dtTardiness.AsEnumerable().GroupBy(m => m.Field<string>("fullName"))
                .Select(n => new
                {
                    bio_number = n.Key,
                    SumOfPoints = n.Sum(s => s.Field<int>("MemoPoints"))
                });

                dgTardiness.DataSource = resultDt;
                dgTardiness.DataBind();
            }
            else
            {
                lblTardiness.Visible = true;
                dgTardiness.Visible = false;
            }


            //Evaluation Notice 
            int countMonth = 5; //count of month before evaluation
            //string sqlEvaluation = "select a.date_employed, A.Employment_status, A.Emp_ID, (UPPER(LEFT(cast(A.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_lname as nvarchar(max)),2,LEN(cast(A.Emp_lname as nvarchar(max)))))+ ', ' + UPPER(LEFT(cast(A.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_fname as nvarchar(max)),2,LEN(cast(A.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(A.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_mname as nvarchar(max)),2,LEN(cast(A.Emp_mname as nvarchar(max)))))) AS FullName, B.Department_name, C.Position_title from db_owner.Employee A left join db_owner.Department B on A.Department_ID = B.Department_ID left join db_owner.PositionTitle C on A.Position_ID = C.Position_ID where A.date_employed <= DATEADD(MONTH, "+ countMonth +", Date_employed) and a.employment_status not in ('regular', 'end') order by a.emp_lname";
            string sqlEvaluation = "select a.date_employed, A.Employment_status, A.Emp_ID, (UPPER(LEFT(cast(A.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_lname as nvarchar(max)),2,LEN(cast(A.Emp_lname as nvarchar(max)))))+ ', ' + UPPER(LEFT(cast(A.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_fname as nvarchar(max)),2,LEN(cast(A.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(A.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_mname as nvarchar(max)),2,LEN(cast(A.Emp_mname as nvarchar(max)))))) AS FullName, B.Department_name, C.Position_title from db_owner.Employee A left join db_owner.Department B on A.Department_ID = B.Department_ID left join db_owner.PositionTitle C on A.Position_ID = C.Position_ID where DATEDIFF(m, A.date_employed, '" + objCommon.pacificdate + "') >= " + countMonth + " and a.employment_status not in ('regular', 'end') order by a.emp_lname";
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
            // string sqlRegular = "select A.emp_number, A.Employment_status, A.Emp_ID, (UPPER(LEFT(cast(A.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_lname as nvarchar(max)),2,LEN(cast(A.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(A.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_fname as nvarchar(max)),2,LEN(cast(A.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(A.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_mname as nvarchar(max)),2,LEN(cast(A.Emp_mname as nvarchar(max)))))) AS FullName, B.Department_name, C.Position_title from db_owner.Employee A left join db_owner.Department B on A.Department_ID = B.Department_ID left join db_owner.PositionTitle C on A.Position_ID = C.Position_ID where A.Employment_status = 'regular'";
            string sqlRegular = "";
            string sSearch = txtSeachRegular.Value.Trim();
            sqlRegular = objSql.regularEmployee(sSearch, sqlRegular);

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
            
            //For Pagination of List of employees
            Session["FORDATABASE"] = dtregular;
            BindRepeater(); 
        }

        //PAGINATION BEHIND CODE
        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                {
                    return Convert.ToInt32(ViewState["PageNumber"]);
                }
                else
                {
                    return 0;
                }
            }
            set { ViewState["PageNumber"] = value; }
        }


        private void BindRepeater()
        {

            //objCommon.LoadDataTable(sql, dt);
            DataTable dtr = Session["FORDATABASE"] as DataTable;

            //Create the PagedDataSource that will be used in paging
            PagedDataSource pgitems = new PagedDataSource();
            pgitems.DataSource = dtr.DefaultView;
            pgitems.AllowPaging = true;

            //Control page size from here 
            pgitems.PageSize = 5;
            pgitems.CurrentPageIndex = PageNumber;


            if (pgitems.PageCount > 1)
            {
                rptPaging.Visible = true;
                ArrayList pages = new ArrayList();
                for (int i = 0; i <= pgitems.PageCount - 1; i++)
                {
                    if (i == PageNumber)
                    {
                        pages.Add("<span class='active'>" + (i + 1).ToString() + "</span>");


                    }
                    else
                    {
                        pages.Add("<span>" + (i + 1).ToString() + "</span>");
                    }
                }
                rptPaging.DataSource = pages;
                rptPaging.DataBind();
            }
            else
            {
                rptPaging.Visible = false;
            }

            //Finally, set the datasource of the repeater
            dgRegular.DataSource = pgitems;
            dgRegular.DataBind();
        }

        //This method will fire when clicking on the page no link from the pager repeater
        protected void rptPaging_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            string com = e.CommandArgument.ToString();
            com = com.Replace("<span>", "");
            com = com.Replace("</span>", "");
            PageNumber = Convert.ToInt32(com) - 1;
            
            BindRepeater();

            string url = "Pagination.aspx?Page=" + PageNumber;
            string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);


        }
    }
}