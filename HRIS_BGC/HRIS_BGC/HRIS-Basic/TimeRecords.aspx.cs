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
    public partial class TimeRecords : System.Web.UI.Page
    {
        Common objCommon = new Common();
        Timelogs objTimelogs = new Timelogs();
        sSQLStatement objSql = new sSQLStatement();
        DataTable dt = new DataTable();
        DataTable dtpending = new DataTable();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            //NOTE: Standard time is 8:30AM - 5:00PM
            //Tardiness starts when beyond 8:30AM
            //Overtime Starts at 5PM
            if (!IsPostBack)
            {
                if (Session["Username"] != "hradmin")
                {
                    Response.Redirect("LoginPage.aspx");
                  
                }

                string from_date = txtDateFrom.Value.Trim();
                string end_Date = txtDateTo.Value.Trim();
                string department_id = drpDepartment.SelectedValue.Trim();
                string position_id = drpPosition.SelectedValue.Trim();
                BuildGrid(from_date, end_Date, department_id, position_id);
            }
  
        }

        public void BuildGrid(string from_date, string end_Date, string department_id, string position_id)
        {
            objCommon.DropdownDepartment(drpDepartment, "Select * from db_owner.Department", "Department_name", "Department_ID");
            
            string sSQLStatement = "";
            sSQLStatement = objSql.timerecords(sSQLStatement, from_date, end_Date, department_id, position_id);

            objTimelogs.LoadDataTable(sSQLStatement, dt);

            if (dt.Rows.Count != 0)
            {
                //To add new column "Memo Points" and to add value
                objCommon.MemoPoints(dt);

                //
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
            string sqlStatement = "";
            sqlStatement = objSql.pendingtimealteration(sqlStatement);

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

            Session["FORDATABASE"] = dt;

            BindRepeater(dt);

            return;
        }

        protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string department_id = drpDepartment.SelectedValue.Trim();
            objCommon.DropdownPosition(drpPosition, "Select * from db_owner.PositionTitle Where Department_ID =" + department_id, "Position_title", "Position_ID");
            ShowSearch();
        }
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

        //PAGINATION BEHIND CODE Manage Employee
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

        private void BindRepeater(DataTable dt)
        {
            //objCommon.LoadDataTable(sql, dt);

            ////To add another column for memopoints
            //MemoPoints(dt);

            //Create the PagedDataSource that will be used in paging
            PagedDataSource pgitems = new PagedDataSource();
            pgitems.DataSource = dt.DefaultView;
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
            dgTimeRecord.DataSource = pgitems;
            dgTimeRecord.DataBind();
        }

        //This method will fire when clicking on the page no link from the pager repeater
        protected void rptPaging_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            string com = e.CommandArgument.ToString();
            com = com.Replace("<span>", "");
            com = com.Replace("</span>", "");
            PageNumber = Convert.ToInt32(com) - 1;

            //string sql = "";
            
            //sql = objSql.timerecords(sql);
            DataTable dtr = Session["FORDATABASE"] as DataTable;

            BindRepeater(dtr);

            string url = "Pagination.aspx?Page=" + PageNumber;
            string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }

        public void ShowSearch()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key", "toggleSearchFilter();", true);
        }
    } 
}