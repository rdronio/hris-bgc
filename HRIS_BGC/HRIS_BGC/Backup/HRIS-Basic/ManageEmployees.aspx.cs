using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;
using System.Data;
using System.Globalization;
using System.IO;

namespace HRIS_Basic
{
    public partial class ManageEmployees : System.Web.UI.Page
    {
        Common objCommon = new Common();
        Employee objEmployee = new Employee();
        DataTable dt = new DataTable();
        DataTable dtleave = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["Username"] != "hradmin")
                {
                    Response.Redirect("LoginPage.aspx");
                }

                objCommon.LoadDataTable("SELECT UPPER(LEFT(cast(A.Employment_status  as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Employment_status  as nvarchar(max)),2,LEN(cast(A.Employment_status  as nvarchar(max))))) status , C.Department_name as Department, B.Position_title, (A.Street + ' '+ A.City +' '+ A.Province) AS Address,  A.* ,(UPPER(LEFT(cast(A.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_lname as nvarchar(max)),2,LEN(cast(A.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(A.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_fname as nvarchar(max)),2,LEN(cast(A.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(A.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_mname as nvarchar(max)),2,LEN(cast(A.Emp_mname as nvarchar(max)))))) AS FullName FROM db_owner.Employee A left join db_owner.PositionTitle B on A.Position_ID = B.Position_ID left join db_owner.Department C on A.Department_ID = C.Department_ID order by A.emp_lname", dt);

                dgManageEmployee.DataSource = dt;
                dgManageEmployee.DataBind();

                objCommon.DropdownDepartment(drpDepartment, "Select * from db_owner.Department", "Department_name", "Department_ID");
                objCommon.DropdownDepartment(drpDepartmentUpdate, "Select * from db_owner.Department", "Department_name", "Department_ID");
            }
            RedirectUrlHf.Value = ResolveUrl("ManageEmployees.aspx");
        }

        //Modal -- Department Dropdown
        protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string department_id = drpDepartment.SelectedValue.Trim();
            objCommon.DropdownPosition(drpPosition, "Select * from db_owner.PositionTitle Where Department_ID =" + department_id, "Position_title", "Position_ID");
            ClientScript.RegisterStartupScript(this.GetType(), "key", "openTabHrInfo();", true);
        }

        protected void drpDepartmentUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string department_id = drpDepartmentUpdate.SelectedValue.Trim();
            objCommon.DropdownPosition(drpPosition, "Select * from db_owner.PositionTitle Where Department_ID =" + department_id, "Position_title", "Position_ID");
            ClientScript.RegisterStartupScript(this.GetType(), "key", "openTabHrInfo();", true);
        }

        protected void btnModalAddEmployee_Click(object sender, EventArgs e)
        {
            //Personal Information
            string lastname = txtLastName.Value.Trim();
            string firstname = txtFirstName.Value.Trim();
            string middlename = txtMiddleName.Value.Trim();
            string birthday = dtPickerBirthday.Value.Trim();
            string gender = "";
            if (optFemale.Checked == true)
            {
                 gender = "f";
            }
            else if (optMale.Checked == true)
            {
                gender = "m";
            }
            string civilsatus = "";

            if (optSingle.Checked == true)
            {
                civilsatus = "S";
            }
            else if (optMarried.Checked == true)
            {
                civilsatus = "M";
            }
            else if (optWidowed.Checked == true)
            {
                civilsatus = "W";
            }
            string street = txtHouse.Value.Trim(); ;
            string province = txtProvince.Value.Trim();
            string city = txtCity.Value.Trim();
            string state = txtState.Value.Trim();
            string email = txtEmail.Value.Trim();
            string phone = txtPhone.Value.Trim();
            string emergency_name = txtEmergencyContact.Value.Trim();
            string emergency_no = txtEmergencyContactNo.Value.Trim();
            string password = "12345";

            //Hr Information
            string date_employed = txtDateEmployed.Value.Trim();
            string department_id = drpDepartment.SelectedValue;
            string position_id = drpPosition.SelectedValue;
            string basic_salary = txtSalary.Value.Trim();
            string shift = Request.Form["selectShift"];
            string employment_status = Request.Form["selectEmploymentStatus"];
            string termination_date = dtTermDate.Value;
            string tin_no = txtTinNumber.Value.Trim();
            string philhealth_no = txtPhilHealth.Value.Trim();
            string hdmf_no = txtHdmfNo.Value.Trim();
            string sss_no = txtSssNo.Value.Trim();
            string account_no = txtCardNo.Value.Trim();

            //Validation
            if (lastname == "")
            {
                Response.Write("<script>confirm('Last Name is required.');</script>");
                ShowModal();
                return;
            }
            if (firstname == "")
            {
                Response.Write("<script>confirm('First Name is required.');</script>");
                ShowModal();
                return;
            }

            //if email exists
            //objEmployee.LoadCountUsername(email, dt);
            //if (dt.Rows.Count != 0)
            //{
            //    Response.Write("<script>alert('Email is already used!');</script>");
            //    ShowModal();
            //    return;
            //}


            objEmployee.AddEmployee(birthday, email, password, firstname, middlename, lastname, gender, civilsatus, phone, street, province, city, state, emergency_name, emergency_no, date_employed, department_id, position_id, basic_salary, shift, employment_status, termination_date, tin_no, philhealth_no, hdmf_no, sss_no, account_no);
            Response.Redirect("ManageEmployees.aspx");
        }

        protected void btnModalUpdateEmployee_Click(object sender, EventArgs e)
        {
            //Personal Information
            string lastname = txtLastName.Value.Trim();
            string firstname = txtFirstName.Value.Trim();
            string middlename = txtMiddleName.Value.Trim();
            string birthday = dtPickerBirthday.Value.Trim();
            string gender = "";
            if (optFemale.Checked == true)
            {
                gender = "f";
            }
            else if (optMale.Checked == true)
            {
                gender = "m";
            }
            string civilsatus = "";

            if (optSingle.Checked == true)
            {
                civilsatus = "S";
            }
            else if (optMarried.Checked == true)
            {
                civilsatus = "M";
            }
            else if (optWidowed.Checked == true)
            {
                civilsatus = "W";
            }
            string street = txtHouse.Value.Trim(); ;
            string province = txtProvince.Value.Trim();
            string city = txtCity.Value.Trim();
            string state = txtState.Value.Trim();
            string email = txtEmail.Value.Trim();
            string phone = txtPhone.Value.Trim();
            string emergency_name = txtEmergencyContact.Value.Trim();
            string emergency_no = txtEmergencyContactNo.Value.Trim();

            //Hr Information
            string date_employed = txtDateEmployed.Value.Trim();
            string department_id = drpDepartment.SelectedValue;
            string position_id = drpPosition.SelectedValue;
            string basic_salary = txtSalary.Value.Trim();
            string shift = Request.Form["selectShift"];
            string employment_status = Request.Form["selectEmploymentStatus"];
            string termination_date = dtTermDate.Value;
            string tin_no = txtTinNumber.Value.Trim();
            string philhealth_no = txtPhilHealth.Value.Trim();
            string hdmf_no = txtHdmfNo.Value.Trim();
            string sss_no = txtSssNo.Value.Trim();
            string account_no = txtCardNo.Value.Trim();

            //Validation
            if (lastname == "")
            {
                Response.Write("<script>confirm('Last Name is required.');</script>");
                ShowModal();
                return;
            }
            if (firstname == "")
            {
                Response.Write("<script>confirm('First Name is required.');</script>");
                ShowModal();
                return;
            }

         
            int empid = int.Parse(Session["Emp_ID"].ToString());

            objEmployee.UpdateProfile(empid, birthday, email, firstname, middlename, lastname, gender, civilsatus, phone, street, province, city, state, emergency_name, emergency_no, date_employed, department_id, position_id, basic_salary, shift, employment_status, termination_date, tin_no, philhealth_no, hdmf_no, sss_no, account_no);
            Response.Redirect("ManageEmployees.aspx");

        }

        protected void btnModalClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageEmployees.aspx");
        }

        public void itemcommand(object sender, CommandEventArgs c)
        {
            LinkButton lnkbtn = (LinkButton)(sender);
            string emp_id = lnkbtn.CommandArgument;
            Session["Emp_ID"] = emp_id;

            objCommon.DropdownDepartment(drpDepartment, "Select * from db_owner.Department", "Department_name", "Department_ID");
            drpPosition.Enabled = false;
            ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnUpdateEmployee`);", true);
            objCommon.LoadDataTable("Select B.Position_title, C.Department_name,  A.* from db_owner.Employee A left join db_owner.PositionTitle B on A.Position_ID = B.Position_ID left join db_owner.Department C on A.Department_ID = C.Department_ID where Emp_id =" + emp_id, dt);

            if (dt.Rows.Count != 0)
            {
                //Personal Information
                txtLastName.Value = dt.Rows[0]["Emp_lname"].ToString();
                txtFirstName.Value = dt.Rows[0]["Emp_fname"].ToString();
                txtMiddleName.Value = dt.Rows[0]["Emp_mname"].ToString();
                txtHouse.Value = dt.Rows[0]["Street"].ToString();
                txtProvince.Value = dt.Rows[0]["Province"].ToString();
                txtCity.Value = dt.Rows[0]["City"].ToString();
                txtState.Value = dt.Rows[0]["State"].ToString();
                txtEmail.Value = dt.Rows[0]["Emp_email"].ToString();
                txtPhone.Value = dt.Rows[0]["Emp_phone"].ToString();
                txtEmergencyContact.Value = dt.Rows[0]["emergency_contactname"].ToString();
                txtEmergencyContactNo.Value = dt.Rows[0]["emergency_contactno"].ToString();
                txtSalary.Value = dt.Rows[0]["Emp_BasicSalary"].ToString();

                //birthday
                string birthday = dt.Rows[0]["emp_bday"].ToString();

                if (birthday != "")
                {
                    birthday = birthday.Substring(0, 8);
                    dtPickerBirthday.Value = birthday; // SUBSTRING

                }
               
                
                //HR Information
                string date_employed = dt.Rows[0]["Date_employed"].ToString();

                if (date_employed != "")
                {
                    date_employed = date_employed.Substring(0, 8);
                    txtDateEmployed.Value = date_employed;
                }
              
                
                string department_id = dt.Rows[0]["Department_ID"].ToString();

                if (department_id != "")
                {
                    drpDepartmentUpdate.SelectedValue = dt.Rows[0]["Department_ID"].ToString();
                    objCommon.DropdownPosition(drpPosition, "Select * from db_owner.PositionTitle Where Department_ID =" + department_id, "Position_title", "Position_ID");
                    drpPosition.SelectedValue = dt.Rows[0]["Position_ID"].ToString();
                    drpPosition.Enabled = true;
                }
            }

        }

        //For View Button ----
        public void itemcommandview(object sender, CommandEventArgs c)
        {
            LinkButton lnkbtn = (LinkButton)(sender);
            string emp_id = lnkbtn.CommandArgument;
            Session["Emp_ID"] = emp_id;

            ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnViewEmployee`);", true);
            //Modal Leave Records Tab
            //Table for Leave Records
            string sqlLeave = "select CASE WHEN leave_status = 1 Then 'Not Applicable' WHEN leave_status = 2 THEN leave_remarks END AS REMARKS, numberOfDays, Leave_ID, Emp_ID, leave_type, convert(varchar, file_date, 107) file_date, convert(varchar, leave_from, 107) leave_from, convert(varchar, leave_to, 107) leave_to, reason, CASE WHEN withpay = '1' Then 'Yes' ELSE 'No' end as withpay, CASE WHEN leave_status = '0' THEN 'Pending' WHEN leave_status = '1' THEN 'Approved' WHEN leave_status = '2' THEN 'Rejected' END AS leave_status from db_owner.LeaveRecord WHERE leave_status not in ('0') AND emp_id =" + emp_id;
            objCommon.LoadDataTable(sqlLeave, dtleave);

            if (dtleave.Rows.Count != 0)
            {
                dgLeave.HeaderStyle.Font.Bold = true;
                dgLeave.HeaderStyle.Font.Size = 8;


                dgLeave.DataSource = dtleave;
                dgLeave.DataBind();
            }
            else
            {
                lblNoleaveRecords.Visible = true;
                dgLeave.Visible = false;
            }

            //Modal -- Overtime Records Tab --
            //Table for Overtime 
            string sqlOvertime = "select *, convert(varchar, overtime_date, 107) DATE,  CONVERT(varchar(15), CAST(time_in AS TIME),100) AS TIMEIN,  CONVERT(varchar(15), CAST(time_out AS TIME),100) AS TIMEOUT, CASE WHEN overtime_status = 1 THEN 'Approved' WHEN overtime_status = 2 THEN 'Rejected' ELSE 'Pending' END AS status from db_owner.Overtime where overtime_status not in (0) AND emp_id = " + emp_id;
            objCommon.LoadDataTable(sqlOvertime, dt);

            if (dt.Rows.Count != 0)
            {
                dgOvertimeRecord.DataSource = dt;
                dgOvertimeRecord.DataBind();
            }
            else
            {
                lblNoRecords.Visible = true;
                dgOvertimeRecord.Visible = false;

            }

            //Modal -- for Time Records Tab -- 
            //Table for Time Records
            string sqlTimeRecords = "SELECT C.Emp_ID, C.DATE, C.TIMEIN, C.TIMEOUT, (C.TOTAL - C.OVERTIME) AS TOTAL, C.OVERTIME, CASE WHEN C.HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) WHEN C.TARDINESS < 0 THEN '0' ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME, CAST(ROUND(DATEDIFF(MINUTE, '8:30:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS, CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY FROM (select A.Emp_ID, A.Timelogs_date, MIN(A.Timelogs_time) AS TIMEIN, CASE WHEN COUNT(A.Timelogs_time) > 1 THEN MAX(A.Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE Emp_ID = " + emp_id + " GROUP BY EMP_iD, Timelogs_date) B) C";
            objCommon.LoadDataTable(sqlTimeRecords, dt);

            if (dt.Rows.Count != 0)
            {
                dgTimeRecord.HeaderStyle.Font.Bold = true;
                dgTimeRecord.HeaderStyle.Font.Size = 8; 


                dgTimeRecord.DataSource = dt;
                dgTimeRecord.DataBind();
            }
            else
            {
                lblTimeNoRecords.Visible = true;
                dgTimeRecord.Visible = false;
            }

            //Modal -- For Primary Information and HR Information
            objCommon.LoadDataTable("Select UPPER(LEFT(cast(A.Shift  as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Shift  as nvarchar(max)),2,LEN(cast(A.Shift  as nvarchar(max))))) shift_cap,  B.Position_title, C.Department_name,  A.* from db_owner.Employee A left join db_owner.PositionTitle B on A.Position_ID = B.Position_ID left join db_owner.Department C on A.Department_ID = C.Department_ID where Emp_id =" + emp_id, dt);

            if (dt.Rows.Count != 0)
            {
               
                string fullname = dt.Rows[0]["Emp_fname"].ToString() + " " + dt.Rows[0]["Emp_lname"].ToString();
                string gender = dt.Rows[0]["Emp_gender"].ToString();
                string civil_status = dt.Rows[0]["Emp_gender"].ToString();
                string shift = dt.Rows[0]["shift_cap"].ToString();
                string email = dt.Rows[0]["Emp_email"].ToString();
                string phone = dt.Rows[0]["Emp_phone"].ToString();
                string emergency_contactname = dt.Rows[0]["emergency_contactname"].ToString();
                string emergency_contactno  = dt.Rows[0]["emergency_contactno"].ToString(); ;
                string street = dt.Rows[0]["Street"].ToString();
                string province = dt.Rows[0]["Province"].ToString();
                string city = dt.Rows[0]["City"].ToString();
                string state = dt.Rows[0]["State"].ToString();
                string bday = dt.Rows[0]["emp_bday"].ToString();


                //HR INFO
                string date_employed = dt.Rows[0]["Date_employed"].ToString();
                string department_name = dt.Rows[0]["Department_name"].ToString();
                string position_title = dt.Rows[0]["Position_title"].ToString();
                string basic_salary = dt.Rows[0]["Emp_BasicSalary"].ToString();
                string employ_status = dt.Rows[0]["Employment_status"].ToString(); 

                if (gender == "f")
                {
                    txtEmpGender.InnerText = "Female";
                }
                else if (gender == "m")
                {
                    txtEmpGender.InnerText = "Male";
                }
                else
                {
                    txtEmpGender.InnerText = "---";
                }

                if (civil_status == "S")
                {
                    txtEmpCivilStats.InnerText = "Single";
                }
                else if (civil_status == "M")
                {
                    txtEmpCivilStats.InnerText = "Married";
                }
                else if (civil_status == "W")
                {
                    txtEmpCivilStats.InnerText = "Widowed";
                }
                else
                {
                    txtEmpCivilStats.InnerText = "---";
                }

                if (bday != "")
                {
                    bday = bday.Substring(0, 8);
                    txtEmpBday.InnerText = bday;
                }
                txtModalEmpName.InnerText = fullname;
                txtEmpEmail.InnerText = email;

                if (phone != "")
                {
                    txtEmpPhone.InnerText = phone;
                }

                if (emergency_contactname != "")
                {
                    txtEmpEmergencyContact.InnerText = emergency_contactname;
                }

                if (emergency_contactno != "")
                {
                    txtEmpEmergencyContactNo.InnerText = emergency_contactno;
                }

                if (street != "")
                {
                    txtEmpAddress.InnerText = street;
                }

                if (province != "")
                {
                    txtEmpProvince.InnerText = province;
                }

                if (city != "")
                {
                    txtEmpCity.InnerText = city;
                }

                if (state != "")
                {
                    txtEmpState.InnerText = state;
                }

                //HR INFOR TAB
                if (date_employed != "")
                {
                    date_employed = date_employed.Substring(0, 8);
                    lblDateEmployed.InnerText = date_employed;
                }
                lblDepartment.InnerText = department_name;
                lblPosition.InnerText = position_title;
                //lblManager.InnerText = "Manager";
                lblBasicSalary.InnerText = basic_salary;
               
               
                lblShift.InnerText = shift + " Shift";
                lblEmploymentStatus.InnerText = objCommon.CapitalizeFirst(employ_status);

                //Summary Employee Information
                txtSummaryEmpID.InnerText = emp_id;
                txtSummaryEmpName.InnerText = fullname;
                txtSummaryEmpDept.InnerText = department_name;
                txtSummaryEmpPosition.InnerText = position_title;
                txtSummaryEmpStatus.InnerText = objCommon.CapitalizeFirst(employ_status);
            }

        }

        ////Import Employees
        //protected void UploadButton_Click(object sender, EventArgs e)
        //{
        //    if (FileUploader.HasFile)
        //    {
        //        try
        //        {
        //            DataTable dt = new DataTable();
        //            dt = ReadCsvFile();
        //            Session["FORDATABASE"] = dt; //RETRIEVE TO SAVE IN DATABASE

        //            //HEADER TEX
        //            dgImportEmployee.HeaderStyle.Font.Bold = true;
        //            dgImportEmployee.HeaderStyle.Font.Size = 8;

        //            dgImportEmployee.DataSource = dt;
        //            dgImportEmployee.DataBind();

        //            dgImportEmployee.Visible = true;

        //            //lblCircle.Visible = false;
        //            //lblSuccess.Visible = false;

        //            lblSuccess.Attributes.Add("class", "hidden");

        //            btnSaveMasterlist.Visible = true;
        //            btnClearMasterlist.Visible = true;

        //            Response.Write("<script>confirm('You uploaded a file!');</script>");
        //            return;
        //        }
        //        catch (Exception ex)
        //        {
        //            Label1.Text = ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("<script>confirm('You have not specified a file.');</script>");
        //        return;
        //    }
        //}
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    DataTable dtr = Session["FORDATABASE"] as DataTable;
        //    objCommon.SaveDataTableEmployee(dtr);

        //    //lblCircle.Visible = true;
        //    //lblSuccess.Visible = true;

        //    lblSuccess.Attributes.Add("class", "success");

        //    btnSaveMasterlist.Visible = false;
        //    btnClearMasterlist.Visible = false;

        //    dgImportEmployee.Visible = false;
        //}

        //public DataTable ReadCsvFile()
        //{

        //    DataTable dtCsv = new DataTable();
        //    string Fulltext;
        //    if (FileUploader.HasFile)
        //    {
        //        string FileSaveWithPath = Server.MapPath("\\Upload\\Import" + System.DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".csv");
        //        FileUploader.SaveAs(FileSaveWithPath);
        //        using (StreamReader sr = new StreamReader(FileSaveWithPath))
        //        {
        //            while (!sr.EndOfStream)
        //            {
        //                Fulltext = sr.ReadToEnd().ToString(); //read full file text  
        //                string[] rows = Fulltext.Split('\n'); //split full file text into rows  
        //                for (int i = 0; i < rows.Count() - 1; i++)
        //                {
        //                    string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
        //                    {
        //                        if (i == 0)
        //                        {
        //                            for (int j = 0; j < rowValues.Count(); j++)
        //                            {
        //                                dtCsv.Columns.Add(rowValues[j].Replace(" ", "").Replace("\r", "")); //add headers  
        //                            }
        //                        }
        //                        else
        //                        {
        //                            DataRow dr = dtCsv.NewRow();
        //                            for (int k = 0; k < rowValues.Count(); k++)
        //                            {
        //                                dr[k] = rowValues[k].ToString();
        //                            }
        //                            dtCsv.Rows.Add(dr); //add other rows  
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return dtCsv;
        //}
     

        public void ShowModal()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnAddEmployee`);", true);
        }
    }
}