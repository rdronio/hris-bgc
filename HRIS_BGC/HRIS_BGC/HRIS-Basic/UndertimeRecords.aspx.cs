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
    public partial class UndertimeRecords : System.Web.UI.Page
    {
        Common objCommon = new Common();
        Lib_Undertime objUndertime = new Lib_Undertime();
        DataTable dt = new DataTable();
        DataTable dtut = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["Username"] == null)
                {
                    Response.Redirect("LoginPage.aspx");
                }

                objCommon.DropdownDepartment(drpDepartment, "Select * from db_owner.Department", "Department_name", "Department_ID");
            }

            int empid = 5;//int.Parse(Session["Employee_ID"].ToString());
            string sSQLStatement = "select *,  convert(varchar, undertime_date, 107) DATE,  CONVERT(varchar(15), CAST(time_in AS TIME),100) AS TIMEIN,  CONVERT(varchar(15), CAST(time_out AS TIME),100) AS TIMEOUT, CASE WHEN undertime_status = 1 THEN 'Approved' WHEN undertime_status = 2 THEN 'Rejected' ELSE 'Pending' END AS status, CASE WHEN undertime_status = 1 Then 'Not Applicable' ELSE undertime_remarks END AS remarks from db_owner.Undertime where undertime_status not in (0) AND emp_id = " + empid;

            //Table for Undertime 
            objCommon.LoadDataTable(sSQLStatement, dt);

            if (dt.Rows.Count != 0)
            {
                dgUndertime.DataSource = dt;
                dgUndertime.DataBind();
            }
            else
            {
                lblNoRecords.Visible = true;
                dgUndertime.Visible = false;

            }

            //Table for Pending Undertime
            string sqlPending = "select *,  convert(varchar, undertime_date, 107) DATE,  CONVERT(varchar(15), CAST(time_in AS TIME),100) AS TIMEIN,  CONVERT(varchar(15), CAST(time_out AS TIME),100) AS TIMEOUT, CASE WHEN undertime_status = 1 THEN 'Approved' WHEN undertime_status = 2 THEN 'Rejected' ELSE 'Pending' END AS status from db_owner.Undertime where undertime_status = 0 AND emp_id = " + empid;

            //Table for Undertime 
            objCommon.LoadDataTable(sqlPending, dtut);

            if (dtut.Rows.Count != 0)
            {
                dgPendingUndertime.DataSource = dtut;
                dgPendingUndertime.DataBind();
            }
            else
            {
                lblPendingNoRecords.Visible = true;
                dgPendingUndertime.Visible = false;

            }

        }

        protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string department_id = drpDepartment.SelectedValue.Trim();
            string sqlstatement = "select Emp_ID, (UPPER(LEFT(cast(Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(Emp_lname as nvarchar(max)),2,LEN(cast(Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(Emp_fname as nvarchar(max)),2,LEN(cast(Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(Emp_mname as nvarchar(max)),2,LEN(cast(Emp_mname as nvarchar(max)))))) AS Employee from db_owner.Employee where department_id=" + department_id;

            objCommon.DropdownEmployee(drpEmployee, sqlstatement, "Employee", "Emp_ID");
            ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnOvertimeApplication`);", true);
        }

        protected void btnRequestUndertime_Click(object sender, EventArgs e)
        {
            int empid = 5;//int.Parse(Session["Employee_ID"].ToString());
            string ut_date = txtDateUT.Value.Trim();
            string ut_reason = txtAreaReasonUT.Value.Trim();

            if (ut_date == "")
            {
                Response.Write("<script>confirm('Overtime date is required.');</script>");
                ShowModal();
                return;
            }

            if (objCommon.ConvertToEmptyIfNothing(ut_date).ToString().Length != 0)
            {
                if (ut_date.ToString().Length != 10)
                {
                    Response.Write("<script>alert('Invalid overtime date format!');</script>");
                    return;
                }
            }

            if (ut_reason == "")
            {
                Response.Write("<script>confirm('Undertime reason is required.');</script>");
                ShowModal();
                return;
            }

           // string sqlStatement = "SELECT C.Emp_ID, C.DATE, C.TIMEIN, C.TIMEOUT, C.TOTAL, C.OVERTIME, CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) - (CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2))) - 1 AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME , CAST(ROUND(DATEDIFF(MINUTE, '8:00:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY FROM (select Emp_ID, Timelogs_date, MIN(Timelogs_time) AS TIMEIN, CASE WHEN COUNT(Timelogs_time) > 1 THEN MAX(Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE Emp_ID = " + empid + " AND Timelogs_date = '" + ut_date + "' GROUP BY EMP_iD, Timelogs_date) B) C";
            string sqlStatement = "SELECT C.Emp_ID, C.DATE, C.TIMEIN, C.TIMEOUT, (C.TOTAL - C.OVERTIME) AS TOTAL, C.OVERTIME, CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) WHEN C.TARDINESS < 0 THEN '0' ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME, CAST(ROUND(DATEDIFF(MINUTE, '8:30:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY FROM (select A.Emp_ID, A.Timelogs_date, MIN(A.Timelogs_time) AS TIMEIN, CASE WHEN COUNT(A.Timelogs_time) > 1 THEN MAX(A.Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE Emp_ID = " + empid + " AND Timelogs_date = '" + ut_date + "'  GROUP BY EMP_iD, Timelogs_date) B) C";
            objCommon.LoadDataTable(sqlStatement, dt);

            if (dt.Rows.Count != 0)
            {
                string date = dt.Rows[0]["DATE"].ToString();
                string timein = dt.Rows[0]["TIMEIN"].ToString();
                string timeout = dt.Rows[0]["TIMEOUT"].ToString();
                double totalovertime = double.Parse(dt.Rows[0]["OVERTIME"].ToString());
                double totalhours = double.Parse(dt.Rows[0]["TOTAL"].ToString());
                double totalundertime = 0;

                if (totalhours != 0 && totalhours < 7.5)
                {
                    objCommon.LoadDataTable( "SELECT * from db_owner.Undertime where emp_id =" + empid + " AND undertime_date = '"+ date +"'", dtut);

                    if (dtut.Rows.Count == 0)
                    {
                        totalundertime = 9 - totalhours;
                        totalundertime = Math.Round(totalundertime, 1);
                        objUndertime.FileUndertime(empid, timein, timeout, date, totalundertime, ut_reason);
                        ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>alert('Successfully filed an undertime!');window.location='UndertimeRecords.aspx';</script>'");
                    }
                    else
                    {
                        Response.Write("<script>confirm('You already filed an undertime on this date.');</script>");
                        ShowModal();
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>confirm('30 minutes above');</script>");
                    ShowModal();
                    return;
                }
            }
            else
            {
                Response.Write("<script>confirm('No records of timekeeping. Please check the date.');</script>");
                ShowModal();
                return;
            }
        }

        public void itemcommand(object sender, CommandEventArgs c)
        {
            //string payroll_id = Session["payroll_id"].ToString();
            //string emp_id = Session["emp_id"].ToString();

            LinkButton btn = (LinkButton)(sender);
            string undertime_id = btn.CommandArgument;

            objUndertime.CancelUndertime(int.Parse(undertime_id));
            Response.Redirect("UndertimeRecords.aspx");
        }

        public void ShowModal()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnOpenUndertimeApplication`);", true);
        }
    }
}