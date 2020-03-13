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
    public partial class TimeRecords : System.Web.UI.Page
    {
        Common objCommon = new Common();
        Timelogs objTimelogs = new Timelogs();
        DataTable dt = new DataTable();
        DataTable dtpending = new DataTable();
       

        protected void Page_Load(object sender, EventArgs e)
        {
            //NOTE: Standard time is 8:30AM - 5:00PM
            //Tardiness starts when beyond 8:30AM
            //Overtime Starts at 5PM
            if (!IsPostBack)
            {
                if (Session["Username"] == null)
                {
                    Response.Redirect("LoginPage.aspx");
                }
            }

            int empid = int.Parse(Session["Employee_ID"].ToString());


            //int empid = 1; //int.Parse(Session["Employee_ID"].ToString());
            //Query with TimelogsID TimeIN and TimelogsID Timeout if Generate DTR Report remove those id's
            //string sSQLStatement = "SELECT C.Emp_ID, C.DATE, C.TimelogsID_TimeIN, C.TIMEIN, C.TimelogsID_TimeOut, C.TIMEOUT, (C.TOTAL - C.OVERTIME) AS TOTAL, C.OVERTIME, CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT B.TimelogsID_TimeIN, B.TimelogsID_TimeOut, B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME, CAST(ROUND(DATEDIFF(MINUTE, '8:30:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY FROM (select (select Timelogs_ID from db_owner.Timelogs where Emp_ID = "+ empid +" AND Timelogs_time = (Select MIN(Timelogs_time) from db_owner.Timelogs)) AS TimelogsID_TimeIN, (select Timelogs_ID from db_owner.Timelogs where Emp_ID = "+ empid +" AND Timelogs_time = (Select MAX(Timelogs_time) from db_owner.Timelogs)) AS TimelogsID_TimeOut, Emp_ID, Timelogs_date, MIN(Timelogs_time) AS TIMEIN, CASE WHEN COUNT(Timelogs_time) > 1 THEN MAX(Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE Emp_ID = "+ empid +" GROUP BY EMP_iD, Timelogs_date) B) C";
            //string sSQLStatement = "SELECT C.Emp_ID, C.DATE, C.TimelogsID_TimeIN, C.TIMEIN, C.TimelogsID_TimeOut, C.TIMEOUT, (C.TOTAL - C.OVERTIME) AS TOTAL, C.OVERTIME, CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) WHEN C.TARDINESS < 0 THEN '0' ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT B.TimelogsID_TimeIN, B.TimelogsID_TimeOut, B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME, CAST(ROUND(DATEDIFF(MINUTE, '8:30:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY FROM (select (select Timelogs_ID from db_owner.Timelogs where Emp_ID = " + empid + " AND Timelogs_time = (Select MIN(Timelogs_time) from db_owner.Timelogs)) AS TimelogsID_TimeIN, (select Timelogs_ID from db_owner.Timelogs where Emp_ID = " + empid + " AND Timelogs_time = (Select MAX(Timelogs_time) from db_owner.Timelogs)) AS TimelogsID_TimeOut, Emp_ID, Timelogs_date, MIN(Timelogs_time) AS TIMEIN, CASE WHEN COUNT(Timelogs_time) > 1 THEN MAX(Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE Emp_ID = " + empid + " GROUP BY EMP_iD, Timelogs_date) B) C";
            string sSQLStatement = "SELECT C.Emp_ID, C.DATE, C.TIMEIN, C.TIMEOUT, (C.TOTAL - C.OVERTIME) AS TOTAL, C.OVERTIME, CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) WHEN C.TARDINESS < 0 THEN '0' ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME, CAST(ROUND(DATEDIFF(MINUTE, '8:30:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY FROM (select A.Emp_ID, A.Timelogs_date, MIN(A.Timelogs_time) AS TIMEIN, CASE WHEN COUNT(A.Timelogs_time) > 1 THEN MAX(A.Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE Emp_ID = "+ empid+" GROUP BY EMP_iD, Timelogs_date) B) C";
            objTimelogs.LoadDataTable(sSQLStatement, dt);

            if (dt.Rows.Count != 0)
            {
                dgTimeRecord.HeaderStyle.Font.Bold = true;
                dgTimeRecord.HeaderStyle.Font.Size = 8;

                 
                dgTimeRecord.DataSource = dt;
                dgTimeRecord.DataBind();
            }
            else
            {
                lblNoleaveRecords.Visible = true;
                dgTimeRecord.Visible = false;
            }

            //Pending Time Alteration
            string sqlStatement = "Select *, convert(varchar, timealteration_date, 107) DATE from db_owner.TimeAlteration where timealteration_status = 0 AND emp_id=" + empid;
            objTimelogs.LoadDataTable(sqlStatement, dtpending);

            if (dtpending.Rows.Count != 0)
            {
                dgPendingTimeAlteration.HeaderStyle.Font.Bold = true;
                dgPendingTimeAlteration.HeaderStyle.Font.Size = 8;


                dgPendingTimeAlteration.DataSource = dtpending;
                dgPendingTimeAlteration.DataBind();
            }
            else
            {
                lblNoData.Visible = true;
                dgPendingTimeAlteration.Visible = false;
            }

        }

        //public void itemcommand(object sender, CommandEventArgs c)
        //{
        //    ////string payroll_id = Session["payroll_id"].ToString();
        //    ////string emp_id = Session["emp_id"].ToString();


        //    LinkButton lnk = (LinkButton)(sender);
        //    string timelogsID_TimeIn = lnk.CommandArgument;
        //    string timelogsID_TimeOut = lnk.CommandName;

        //    ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnTimeAlteration`);", true);
        //    return;
        //    //objLeave.CancelLeave(int.Parse(leave_id));
        //    //Response.Redirect("LeaveRecords.aspx");
        //}

        protected void btnAlterTime_Click(object sender, EventArgs e)
        {
            int empid = int.Parse(Session["Employee_ID"].ToString());
            string timealter_date = txtDate.Value.Trim();
            string timealter_type = "";
            string timealter_time = txtTime.Value.Trim();
            string timealteration_reason = txtAreaReasonTA.Value.Trim();

            //Date validation
            if (timealter_date == "")
            {
                Response.Write("<script>confirm('Date is required.');</script>");
                ShowModal();
                return;
            }

            if (objCommon.ConvertToEmptyIfNothing(timealter_date).ToString().Length != 0)
            {
                    if (timealter_date.ToString().Length != 10)
                    {
                        Response.Write("<script>alert('Invalid date format!');</script>");
                        return;
                    }
            }

            if (DateTime.Parse(timealter_date) > DateTime.Parse(objCommon.pacificdate))
            {
                Response.Write("<script>confirm('From Date is greater than date today. Please change the date');</script>");
                ShowModal();
                return;
            }

            //Checkbox type time-in -- time-out
            if (chkIn.Checked == true)
            {
                timealter_type = "Time-in";
            }
            else if (chkOut.Checked == true)
            {
                timealter_type = "Time-out";
            }
            else
            {
                Response.Write("<script>alert('Please choose if In or Out.');</script>");
                return;
            }
            //Time
            if (timealter_time == "")
            {
                Response.Write("<script>alert('Please choose if In or Out.');</script>");
                return;
            }

            //Reason
            if (timealteration_reason == "")
            {
                Response.Write("<script>alert('Please choose if In or Out.');</script>");
                return;
            }


            objTimelogs.AddTimeAlteration(empid, timealter_date, timealter_time, timealteration_reason, timealter_type);
            Response.Redirect("TimeRecords.aspx");

        }

        public void itemcommand(object sender, CommandEventArgs c)
        {
            //string payroll_id = Session["payroll_id"].ToString();
            //string emp_id = Session["emp_id"].ToString();


            LinkButton btn = (LinkButton)(sender);
            string timealteration_id = btn.CommandArgument;

            objTimelogs.CancelTimeAlteration(int.Parse(timealteration_id));
            Response.Redirect("TimeRecords.aspx");
        }

        public void ShowModal()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnTimeAlteration`);", true);
        }
    } 
}