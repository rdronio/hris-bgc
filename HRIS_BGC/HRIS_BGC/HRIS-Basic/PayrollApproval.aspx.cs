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
    public partial class PayrollApproval : System.Web.UI.Page
    {
        Timelogs objTimelogs = new Timelogs();
        Lib_Payroll objPayroll = new Lib_Payroll();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sSQLStatement = "select A.Payroll_ID, A.Emp_ID, (convert(varchar, A.Payroll_fromDate, 107) + ' - ' + convert(varchar, A.Payroll_toDate, 107)) AS PAYPERIOD, (UPPER(LEFT(cast(B.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_lname as nvarchar(max)),2,LEN(cast(B.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(B.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_fname as nvarchar(max)),2,LEN(cast(B.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(B.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_mname as nvarchar(max)),2,LEN(cast(B.Emp_mname as nvarchar(max)))))) AS Employee, C.Position_title, CAST(ROUND(A.NetPay, 2) AS Numeric(36, 2)) AS NetPay from db_owner.Payroll A left join db_owner.Employee B on A.Emp_ID = B.Emp_ID left join db_owner.PositionTitle C on C.Position_ID = B.Position_ID where A.payroll_status = 0";
                objTimelogs.LoadDataTable(sSQLStatement, dt);

                if (dt.Rows.Count != 0)
                {
                    dgPayrollApproval.DataSource = dt;
                    dgPayrollApproval.DataBind();
                }
                else
                {
                    lblNoData.Visible = true;
                    dgPayrollApproval.Visible = false;
                }
            
            }
        }

        public void itemcommand(object sender, CommandEventArgs c)
        {
            //string payroll_id = Session["payroll_id"].ToString();
            //string emp_id = Session["emp_id"].ToString();

            LinkButton btn = (LinkButton)(sender);
            string payroll_id = btn.CommandArgument;
            string status = "";

            Session["payroll_id"] = payroll_id;

            if (c.CommandName == "Approve")
            {
                status = "1";
                objPayroll.UpdatePayroll(status, int.Parse(payroll_id));
            }
            if (c.CommandName == "Reject")
            {
                status = "2";
                objPayroll.UpdatePayroll(status, int.Parse(payroll_id));
            }

            Response.Redirect("PayrollApproval.aspx");
        }

    }
}