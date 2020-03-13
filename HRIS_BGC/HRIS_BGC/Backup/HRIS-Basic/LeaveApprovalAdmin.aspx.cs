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
    public partial class LeaveApprovalAdmin : System.Web.UI.Page
    {
        Lib_Leave objLeave = new Lib_Leave();
        Common objCommon = new Common();
        DataTable dt = new DataTable();
        DataTable dtleave = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlStatement = "select Leave_ID, B.Emp_ID, (UPPER(LEFT(cast(A.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_lname as nvarchar(max)),2,LEN(cast(A.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(A.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_fname as nvarchar(max)),2,LEN(cast(A.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(A.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_mname as nvarchar(max)),2,LEN(cast(A.Emp_mname as nvarchar(max)))))) AS FullName, leave_type, convert(varchar, file_date, 107) file_date, convert(varchar, leave_from, 107) leave_from, convert(varchar, leave_to, 107) leave_to, reason, numberOfDays from db_owner.LeaveRecord B LEFT JOIN db_owner.Employee A on B.Emp_ID = A.Emp_ID WHERE leave_status = '0'";
            objCommon.LoadDataTable(sqlStatement, dt);

            if (dt.Rows.Count != 0)
            {
                dgLeaveApproval.HeaderStyle.Font.Bold = true;
                dgLeaveApproval.HeaderStyle.Font.Size = 8;


                dgLeaveApproval.DataSource = dt;
                dgLeaveApproval.DataBind();
            }
            else
            {
                lblNoData.Visible = true;
                dgLeaveApproval.Visible = false;
            }
        }

        public void itemcommand(object sender, CommandEventArgs c)
        {
            //string payroll_id = Session["payroll_id"].ToString();
            //string emp_id = Session["emp_id"].ToString();

            LinkButton btn = (LinkButton)(sender);
            string leave_id = btn.CommandArgument;
            string status = "";

            Session["leaveid"] = leave_id;

            if (c.CommandName == "Approve")
            {
                
                ClientScript.RegisterStartupScript(this.GetType(), "key", "openModalWithPay()", true);
                return;
            }
            if (c.CommandName == "Reject")
            {
                lblNotEnoughLeave.Attributes.Add("class", "notif notif-red hidden");
                ClientScript.RegisterStartupScript(this.GetType(), "key", "openModalLeaveRemarks();", true);
                return;
               
            }

            Response.Redirect("LeaveApprovalAdmin.aspx");
        }

        protected void btnApproveLeave_Click(object sender, EventArgs e)
        {
            string withpay = "";
            string status = "1";
            string leave_id = Session["leaveid"].ToString();
            double remaining_vleave = 0;
            double remaining_sleave = 0;

            if (rdYesPay.Checked == true)
            {
                //Load Data Table if leave count is still available
                string sqlLeaveCount = "select * from db_owner.LeaveRecord A left join db_owner.LeaveCount B on A.Emp_ID = B.Emp_id where A.leave_id = " + leave_id + " and B.leavecount_year = '" + objCommon.pacificyear + "'";
                objCommon.LoadDataTable(sqlLeaveCount, dtleave);
                if (dtleave.Rows.Count != 0)
                {
                    string available_vleave = dtleave.Rows[0]["vacation_leave"].ToString();
                    string used_vleave = dtleave.Rows[0]["used_vacation_leave"].ToString();
                    string available_sleave = dtleave.Rows[0]["sick_leave"].ToString();
                    string used_sleave = dtleave.Rows[0]["used_sickleave"].ToString();

                    remaining_vleave = double.Parse(available_vleave) - double.Parse(used_vleave);
                    remaining_sleave = double.Parse(available_sleave) - double.Parse(used_sleave);
                }

                //Load the Leave Record to get the leeave type
                string sql = "SELECT * from db_owner.LeaveRecord where leave_id=" + leave_id;
               
                
                objCommon.LoadDataTable(sql, dt);

                string leave_type = dt.Rows[0]["leave_type"].ToString().Trim();
                string empid = dt.Rows[0]["Emp_ID"].ToString();
                string numberOfDays = dt.Rows[0]["numberOfDays"].ToString();

                if (leave_type == "Vacation" && remaining_vleave >= int.Parse(numberOfDays))
                {
                    
                objLeave.UpdateVacationLeaveCount(int.Parse(empid), int.Parse(numberOfDays));
                }
                else if (leave_type == "Sick" && remaining_sleave >= int.Parse(numberOfDays))
                {
                    objLeave.UpdateSickLeaveCount(int.Parse(empid), int.Parse(numberOfDays));
                }
                else
                {
                    lblNotEnoughLeave.Attributes.Add("class", "notif notif-red");
                    ClientScript.RegisterStartupScript(this.GetType(), "key", "openModalLeaveRemarks();", true);
                    return;
                
                }
                withpay = "1";
                

            }
            else if (rdNoPay.Checked == true)
            {
                withpay = "0";
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "key", "openModalWithPay()", true);
                return;
                
            }

            //Update Leave Record Status
            objLeave.UpdateLeave(withpay, status, int.Parse(leave_id));

            Response.Redirect("LeaveApprovalAdmin.aspx");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            string status = "2";
            string withpay = "0";
            string leave_id = Session["leaveid"].ToString();
            string remarks = modalRemarksTA.Value.Trim();

            if (remarks == "")
            {
                Response.Write("<script>confirm('Remarks is required.');</script>");
                return;
                
            }

            objLeave.RejectLeave(withpay, status, int.Parse(leave_id), remarks);
            Response.Redirect("LeaveApprovalAdmin.aspx");
        }


    }
}