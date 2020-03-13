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
    public partial class UndertimeRecordsApproval : System.Web.UI.Page
    {
        Lib_Undertime objUndertime = new Lib_Undertime();
        Common objCommon = new Common();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlStatement = "select A.*, convert(varchar, A.undertime_date, 107) DATE,  CONVERT(varchar(15), CAST(A.time_in AS TIME),100) AS TIMEIN,  CONVERT(varchar(15), CAST(A.time_out AS TIME),100) AS TIMEOUT, CASE WHEN A.undertime_status = 1 THEN 'Approved' WHEN A.undertime_status = 2 THEN 'Rejected' ELSE 'Pending' END AS status, (UPPER(LEFT(cast(B.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_lname as nvarchar(max)),2,LEN(cast(B.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(B.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_fname as nvarchar(max)),2,LEN(cast(B.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(B.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_mname as nvarchar(max)),2,LEN(cast(B.Emp_mname as nvarchar(max)))))) AS FullName from db_owner.Undertime A left join db_owner.Employee B on A.Emp_ID = B.Emp_ID  where A.undertime_status = 0";
            objCommon.LoadDataTable(sqlStatement, dt);

            if (dt.Rows.Count != 0)
            {
                dgUndertimeAdmin.HeaderStyle.Font.Bold = true;
                dgUndertimeAdmin.HeaderStyle.Font.Size = 8;


                dgUndertimeAdmin.DataSource = dt;
                dgUndertimeAdmin.DataBind();
            }
            else
            {
                lblNoData.Visible = true;
                dgUndertimeAdmin.Visible = false;
            }
        }

        public void itemcommand(object sender, CommandEventArgs c)
        {
            ////string payroll_id = Session["payroll_id"].ToString();
            ////string emp_id = Session["emp_id"].ToString();

            LinkButton btn = (LinkButton)(sender);
            string undertime_id = btn.CommandArgument;
            string status = "";

            Session["undertime_id"] = undertime_id;

            //load the undertime table to get the value of total undertime hours and the emp id
            string sql = "select * from db_owner.Undertime where undertime_id =" + undertime_id;
            objCommon.LoadDataTable(sql, dt);

            string numberofHours = dt.Rows[0]["total_undertime"].ToString();
            string empid = dt.Rows[0]["emp_id"].ToString();

            if (c.CommandName == "Approve")
            {
                status = "1";
                objUndertime.UpdateUndertime(status, int.Parse(undertime_id));
                objUndertime.UpdateVacationLeaveCount(int.Parse(empid), double.Parse(numberofHours));

            }
            if (c.CommandName == "Reject")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "key", "openModalUndertimeRemarks();", true);
                return;
            }

            Response.Redirect("UndertimeRecordsApproval.aspx");
        }

        protected void btnRemarks_Click(object sender, EventArgs e)
        {
            string status = "2";
            string undertime_id = Session["undertime_id"].ToString();
            string remarks = txtRemarks.Value.Trim();

            if (remarks == "")
            {
                Response.Write("<script>confirm('Remarks is required.');</script>");
                return;
            }

            objUndertime.UpdateRejectedUndertime(status, int.Parse(undertime_id), remarks);

            Response.Redirect("UndertimeRecordsApproval.aspx");
        }
    }
}