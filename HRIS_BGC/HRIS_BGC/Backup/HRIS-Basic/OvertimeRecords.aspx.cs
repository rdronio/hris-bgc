﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;
using System.Data;

namespace HRIS_Basic
{
    public partial class OvertimeRecords : System.Web.UI.Page
    {
        Timelogs objTimelogs = new Timelogs();
        Common objCommon = new Common();
        Lib_Overtime objOvertime = new Lib_Overtime();
        DataTable dt = new DataTable();
        DataTable dtot = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["Username"] == null)
                {
                    Response.Redirect("LoginPage.aspx");
                }
            }

            //NOTE: Standard time is 8:00AM - 5:00PM
            //Tardiness starts when beyond 8:A00AM
            //Overtime Starts at 5PM
            int empid = int.Parse(Session["Employee_ID"].ToString());
            //string sSQLStatement = "SELECT C.Emp_ID, C.DATE, C.TIMEIN, C.TIMEOUT, C.TOTAL, C.OVERTIME, CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) - (CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2))) - 1 AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME , CAST(ROUND(DATEDIFF(MINUTE, '8:00:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY FROM (select Emp_ID, Timelogs_date, MIN(Timelogs_time) AS TIMEIN, CASE WHEN COUNT(Timelogs_time) > 1 THEN MAX(Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE Emp_ID = " + empid + " GROUP BY EMP_iD, Timelogs_date) B) C";
            string sSQLStatement = "select *, convert(varchar, overtime_date, 107) DATE,  CONVERT(varchar(15), CAST(time_in AS TIME),100) AS TIMEIN,  CONVERT(varchar(15), CAST(time_out AS TIME),100) AS TIMEOUT, CASE WHEN overtime_status = 1 THEN 'Approved' WHEN overtime_status = 2 THEN 'Rejected' ELSE 'Pending' END AS status from db_owner.Overtime where overtime_status not in (0) AND emp_id = " + empid;
            
            //Table for Overtime 
            objTimelogs.LoadDataTable(sSQLStatement, dt);

            if (dt.Rows.Count != 0)
            {
                dgOvertimeRecord.DataSource = dt;
                dgOvertimeRecord.DataBind();
            }
            else
            {
                lblNoRecords.Visible = true;
                dgOvertimeRecord.Visible = false;

            }

            //Table for Pending Overtime
            string sql = "select *, convert(varchar, overtime_date, 107) DATE,  CONVERT(varchar(15), CAST(time_in AS TIME),100) AS TIMEIN,  CONVERT(varchar(15), CAST(time_out AS TIME),100) AS TIMEOUT, CASE WHEN overtime_status = 1 THEN 'Approved' WHEN overtime_status = 2 THEN 'Rejected' ELSE 'Pending' END AS status from db_owner.Overtime where overtime_status = 0 AND emp_id = " + empid;
           
            objTimelogs.LoadDataTable(sql, dtot);

            if (dtot.Rows.Count != 0)
            {
                dgPendingOvertime.DataSource = dtot;
                dgPendingOvertime.DataBind();
            }
            else
            {
                lblNoData.Visible = true;
                dgPendingOvertime.Visible = false;
            }
            
        }

        protected void btnRequestOvertime_Click(object sender, EventArgs e)
        {
            int empid = int.Parse(Session["Employee_ID"].ToString());
            string ot_date = txtDateOT.Value.Trim();
            string ot_reason = txtAreaReasonOT.Value.Trim();

            if (ot_date == "")
            {
                Response.Write("<script>confirm('Overtime date is required.');</script>");
                ShowModal();
                return;
            }

            if (objCommon.ConvertToEmptyIfNothing(ot_date).ToString().Length != 0)
            {
                if (ot_date.ToString().Length != 10)
                {
                    Response.Write("<script>alert('Invalid overtime date format!');</script>");
                    return;
                }
            }
            
            if (ot_reason == "")
            {
                Response.Write("<script>confirm('Overtime reason is required.');</script>");
                ShowModal();
                return;
            }

            string sqlStatement = "SELECT C.Emp_ID, C.DATE, C.TIMEIN, C.TIMEOUT, C.TOTAL, C.OVERTIME, CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) - (CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2))) - 1 AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME , CAST(ROUND(DATEDIFF(MINUTE, '8:00:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY FROM (select Emp_ID, Timelogs_date, MIN(Timelogs_time) AS TIMEIN, CASE WHEN COUNT(Timelogs_time) > 1 THEN MAX(Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE Emp_ID = " + empid + " AND Timelogs_date = '" + ot_date + "' GROUP BY EMP_iD, Timelogs_date) B) C";
            objTimelogs.LoadDataTable(sqlStatement, dt);

            if (dt.Rows.Count != 0)
            {
                string date = dt.Rows[0]["DATE"].ToString();
                string timein = dt.Rows[0]["TIMEIN"].ToString();
                string timeout = dt.Rows[0]["TIMEOUT"].ToString();
                double totalovertime = double.Parse(dt.Rows[0]["OVERTIME"].ToString());
                double totalhours = double.Parse(dt.Rows[0]["TOTAL"].ToString());

               // validation - if totalovertime is 1 hour above
                if (totalovertime >= 1)
                {

                    objTimelogs.LoadDataTable("Select * from db_owner.Overtime where Emp_ID=" + empid + " AND overtime_date ='" + date + "'", dtot);

                    //Validation - if the date of overtime was already filed or not
                    if (dtot.Rows.Count == 0)
                    {
                        objTimelogs.FileOvertime(empid, timein, timeout, date, totalovertime, ot_reason, totalhours);
                        ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>alert('Successfully filed an overtime!');window.location='OvertimeRecords.aspx';</script>'");
                    }
                    else
                    {
                        Response.Write("<script>confirm('You already filed an overtime on this date.');</script>");
                        ShowModal();
                        return;
                    }
                }

                else
                {
                    Response.Write("<script>confirm('1 hour above');</script>");
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
            string overtime_id = btn.CommandArgument;

            objOvertime.CancelOvertime(int.Parse(overtime_id));
            Response.Redirect("OvertimeRecords.aspx");
        }

        public void ShowModal()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnOvertimeApplication`);", true);
        }
    }
}