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
    public partial class TimeAlterationApproval : System.Web.UI.Page
    {
        Common objCommon = new Common();
        Timelogs objTimelogs = new Timelogs();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlStatement = "select convert(varchar, B.timealteration_date, 107) DATE, (UPPER(LEFT(cast(A.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_lname as nvarchar(max)),2,LEN(cast(A.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(A.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_fname as nvarchar(max)),2,LEN(cast(A.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(A.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_mname as nvarchar(max)),2,LEN(cast(A.Emp_mname as nvarchar(max)))))) AS FullName, B.* from db_owner.TimeAlteration B left join db_owner.Employee A on B.Emp_id = A.Emp_ID where timealteration_status= 0";
            objCommon.LoadDataTable(sqlStatement, dt);

            if (dt.Rows.Count != 0)
            {
                dgTimeAlterationApproval.HeaderStyle.Font.Bold = true;
                dgTimeAlterationApproval.HeaderStyle.Font.Size = 8;


                dgTimeAlterationApproval.DataSource = dt;
                dgTimeAlterationApproval.DataBind();
            }
            else
            {
                lblNoData.Visible = true;
                dgTimeAlterationApproval.Visible = false;
            }
        }

        public void itemcommand(object sender, CommandEventArgs c)
        {
            //string payroll_id = Session["payroll_id"].ToString();
            //string emp_id = Session["emp_id"].ToString();

            LinkButton btn = (LinkButton)(sender);
            string timealteration_id = btn.CommandArgument;
            string status = "";

            //UPDATE Status
            if (c.CommandName == "Approve")
            {
                status = "1";
                //Load data table of time alteration
                objCommon.LoadDataTable("Select * from db_owner.TimeAlteration where timealteration_id=" + timealteration_id, dt);

                if (dt.Rows.Count != 0)
                {
                    string emp_id = dt.Rows[0]["emp_id"].ToString();
                    string date = dt.Rows[0]["timealteration_date"].ToString();
                    string time = dt.Rows[0]["timealteration_time"].ToString();
                    string type = "";

                    objTimelogs.InsertTimeInOut(int.Parse(emp_id), date, time, type);
                }

            }
            else if (c.CommandName == "Reject")
            {
                status = "2";
            }

            objTimelogs.UpdateTimeAlteration(int.Parse(timealteration_id), status);
            Response.Redirect("TimeAlterationApproval.aspx");
        }

    }
}