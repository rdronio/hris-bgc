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
    public partial class EmployeeLeaveRecords : System.Web.UI.Page
    {
        Common objCommon = new Common();
        Lib_Leave objLeave = new Lib_Leave();
        DataTable dtleave = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sqlStatement = "select A.*, (UPPER(LEFT(cast(B.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_lname as nvarchar(max)),2,LEN(cast(B.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(B.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_fname as nvarchar(max)),2,LEN(cast(B.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(B.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_mname as nvarchar(max)),2,LEN(cast(B.Emp_mname as nvarchar(max)))))) AS FullName from db_owner.LeaveCount A left join db_owner.Employee B on A.Emp_id = B.emp_id";
                objCommon.LoadDataTable(sqlStatement, dtleave);


                dgSetLeave.HeaderStyle.Font.Bold = true;
                dgSetLeave.HeaderStyle.Font.Size = 8;


                dgSetLeave.DataSource = dtleave;
                dgSetLeave.DataBind();

                objCommon.DropdownEmployee(drpEmployee, "select Emp_ID, (UPPER(LEFT(cast(Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(Emp_lname as nvarchar(max)),2,LEN(cast(Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(Emp_fname as nvarchar(max)),2,LEN(cast(Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(Emp_mname as nvarchar(max)),2,LEN(cast(Emp_mname as nvarchar(max)))))) AS Employee from db_owner.Employee", "Employee", "Emp_ID");
            }
        }

        public void btnSet_Click(object sender, EventArgs e)
        {
            string emp_id = drpEmployee.SelectedValue;
            string emp_name = Request.Form.Get("drpEmployee");
            string vacation_leave = txtVacation.Value.Trim();
            string sick_leave = txtSick.Value.Trim();
            string year = Request.Form.Get("drpYear");

            //Employee Dropdown
            if (emp_id == "0")
            {
                Response.Write("<script>confirm('Please select an Employee.');</script>");
                return;
            }

            if (vacation_leave == "")
            {
                Response.Write("<script>confirm('Please enter a vacation leave.');</script>");
                return;
            }

            if (sick_leave == "")
            {
                Response.Write("<script>confirm('Please enter a sick leave.');</script>");
                return;
            }

            objCommon.LoadDataTable("select A.*, (UPPER(LEFT(cast(B.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_lname as nvarchar(max)),2,LEN(cast(B.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(B.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_fname as nvarchar(max)),2,LEN(cast(B.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(B.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_mname as nvarchar(max)),2,LEN(cast(B.Emp_mname as nvarchar(max)))))) AS FullName from db_owner.LeaveCount A left join db_owner.Employee B on A.Emp_id = B.emp_id where A.Emp_ID = "+ emp_id +" AND A.leavecount_year = '"+ year +"'", dtleave);

            if (dtleave.Rows.Count == 0)
            {
                objLeave.AddLeaveCount(int.Parse(emp_id), int.Parse(vacation_leave), int.Parse(sick_leave), year);
            }
            else
            {
                Response.Write("<script>confirm('You already set a leave for "+ dtleave.Rows[0]["FullName"].ToString() +" in the year "+ year +" .');</script>");
                return;
            }

            Response.Redirect("EmployeeLeaveRecords.aspx");

        }
    }
}