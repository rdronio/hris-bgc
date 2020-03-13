using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;

namespace HRIS_Basic
{
    public partial class SetLeave : System.Web.UI.Page
    {
        Common objCommon = new Common();
        Lib_Leave objLeave = new Lib_Leave();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                objCommon.DropdownEmployee(drpEmployee, "select Emp_ID, (UPPER(LEFT(cast(Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(Emp_lname as nvarchar(max)),2,LEN(cast(Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(Emp_fname as nvarchar(max)),2,LEN(cast(Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(Emp_mname as nvarchar(max)),2,LEN(cast(Emp_mname as nvarchar(max)))))) AS Employee from db_owner.Employee", "Employee", "Emp_ID");
            }
        }

        public void btnSet_Click(object sender, EventArgs e)
        {
            string emp_id = drpEmployee.SelectedValue;
            string vacation_leave = txtVacation.Text.Trim();
            string sick_leave = txtSick.Text.Trim();
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

            objLeave.AddLeaveCount(int.Parse(emp_id), int.Parse(vacation_leave), int.Parse(sick_leave), year);

            Response.Redirect("SetLeave.aspx");

        }
    }
}