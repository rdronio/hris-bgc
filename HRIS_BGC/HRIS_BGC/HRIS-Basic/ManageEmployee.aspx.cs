using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;

namespace HRIS_Basic
{
    public partial class ManageEmployee : System.Web.UI.Page
    {
        Employee objEmployee = new Employee();
        Common objCommon = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["Username"] != "hradmin")
                {
                    Response.Redirect("LoginPage.aspx");
                }
                

                objCommon.DropdownDepartment(drpDepartment, "Select * from db_owner.Department", "Department_name", "Department_ID");
            }

        }
        protected void btnModalAddEmployee_Click(object sender, EventArgs e)
        {
            string lastname = txtLastName.Value.Trim();
            string firstname = txtFirstName.Value.Trim();
            string middlename = txtMiddleName.Value.Trim();
            string birthday = dtPickerBirthday.Value.Trim();
            string gender = "";
            string civilsatus = "";
            string street = "";
            string province = selectCity.Value.Trim();
            string city = selectCity.Value.Trim();
            string state = txtState.Value.Trim();
            string email = txtEmail.Value.Trim();
            string phone = txtPhone.Value.Trim();
            string emergency_name = txtEmergencyContact.Value.Trim();
            string emergency_no = txtEmergencyContactNo.Value.Trim();
            string password = "12345";

            //objEmployee.AddEmployee(email, password, firstname, middlename, lastname, gender, civilsatus, phone, street, province, city, state, emergency_name, emergency_no);
            Response.Write("ManageEmployee.aspx");
        }
        protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string department_id= drpDepartment.SelectedValue.Trim();
            objCommon.DropdownDepartment(drpPosition, "Select * from db_owner.PositionTitle Where Department_ID =" + department_id , "Position_title", "Position_ID");
        }

        protected void btnModalAddJobInfo_Click(object sender, EventArgs e)
        {

            string password = txtPassword.Value.Trim();
            string department_id = drpDepartment.SelectedValue.Trim();
            string position_id = drpPosition.SelectedValue.Trim();


        }

        
    }
}