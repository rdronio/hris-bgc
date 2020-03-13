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
    public partial class ExpensesApproval : System.Web.UI.Page
    {
        Common objCommon = new Common();
        DataTable dt = new DataTable();
        Lib_Expenses objExpense = new Lib_Expenses();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlStatement = "select A.*, convert(varchar, A.expense_date, 107) DATE,  (UPPER(LEFT(cast(B.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_lname as nvarchar(max)),2,LEN(cast(B.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(B.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_fname as nvarchar(max)),2,LEN(cast(B.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(B.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_mname as nvarchar(max)),2,LEN(cast(B.Emp_mname as nvarchar(max)))))) AS FullName from db_owner.Expense A left join db_owner.Employee B on A.emp_id = B.Emp_ID where expense_status = 0";
            objCommon.LoadDataTable(sqlStatement, dt);

            if (dt.Rows.Count != 0)
            {
                dgExpenseAdmin.HeaderStyle.Font.Bold = true;
                dgExpenseAdmin.HeaderStyle.Font.Size = 8;


                dgExpenseAdmin.DataSource = dt;
                dgExpenseAdmin.DataBind();
            }
            else
            {
                lblNoData.Visible = true;
                dgExpenseAdmin.Visible = false;
            }
        }

        public void itemcommand(object sender, CommandEventArgs c)
        {
            //string payroll_id = Session["payroll_id"].ToString();
            //string emp_id = Session["emp_id"].ToString();

            LinkButton btn = (LinkButton)(sender);
            string expense_id = btn.CommandArgument;
            string status = "";

            Session["expense_id"] = expense_id;

            if (c.CommandName == "Approve")
            {
                status = "1";
                objExpense.UpdateApprovedExpense(status, int.Parse(expense_id));
                return;
            }
            if (c.CommandName == "Reject")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "key", "openModalExpenseRemarks();", true);
                return;
            }

            Response.Redirect("ExpensesApproval.aspx");
        }

        protected void btnRemarks_Click(object sender, EventArgs e)
        {
            string status = "";
            string expense_id = Session["expense_id"].ToString();
            string remarks = txtRemarks.Value.Trim();

            if (remarks == "")
            {
                Response.Write("<script>confirm('Remarks is required.');</script>");
                return;
            }

            status = "2";
            objExpense.UpdateRejectedExpense(status, int.Parse(expense_id), remarks);

            Response.Redirect("ExpensesApproval.aspx");
        }
    }
}