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
    public partial class OvertimeApprovalAdmin : System.Web.UI.Page
    {
        Lib_Leave objLeave = new Lib_Leave();
        Lib_Overtime objOvertime = new Lib_Overtime();
        Common objCommon = new Common();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlStatement = "select A.*, convert(varchar, A.overtime_date, 107) DATE,  CONVERT(varchar(15), CAST(A.time_in AS TIME),100) AS TIMEIN,  CONVERT(varchar(15), CAST(A.time_out AS TIME),100) AS TIMEOUT, CASE WHEN A.overtime_status = 1 THEN 'Approved' WHEN A.overtime_status = 2 THEN 'Rejected' ELSE 'Pending' END AS status, (UPPER(LEFT(cast(B.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_lname as nvarchar(max)),2,LEN(cast(B.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(B.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_fname as nvarchar(max)),2,LEN(cast(B.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(B.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_mname as nvarchar(max)),2,LEN(cast(B.Emp_mname as nvarchar(max)))))) AS FullName from db_owner.Overtime A left join db_owner.Employee B on A.Emp_ID = B.Emp_ID  where A.overtime_status = 0";
            objCommon.LoadDataTable(sqlStatement, dt);

            if (dt.Rows.Count != 0)
            {
                dgOvertimeAdmin.HeaderStyle.Font.Bold = true;
                dgOvertimeAdmin.HeaderStyle.Font.Size = 8;


                dgOvertimeAdmin.DataSource = dt;
                dgOvertimeAdmin.DataBind();
            }
            else
            {
                lblNoData.Visible = true;
                dgOvertimeAdmin.Visible = false;
            }
        }

        public void itemcommand(object sender, CommandEventArgs c)
        {
            //string payroll_id = Session["payroll_id"].ToString();
            //string emp_id = Session["emp_id"].ToString();

            LinkButton btn = (LinkButton)(sender);
            string overtime_id = btn.CommandArgument;
            string status = "";

            if (c.CommandName == "Approve")
            {
                status = "1";
                objOvertime.UpdateOvertime(status, int.Parse(overtime_id));
                return;
            }
            if (c.CommandName == "Reject")
            {
                status = "2";
                objOvertime.UpdateOvertime(status, int.Parse(overtime_id));
            }

            Response.Redirect("OvertimeApprovalAdmin.aspx");
        }
    }
}