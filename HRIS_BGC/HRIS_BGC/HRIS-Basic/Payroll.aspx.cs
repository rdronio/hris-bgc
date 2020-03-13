using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;
using System.Data;
using iTextSharp;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace HRIS_Basic
{
    public partial class Payroll : System.Web.UI.Page
    {
        Timelogs timelogs = new Timelogs();
        Lib_Payroll payroll = new Lib_Payroll();
        Common objCommon = new Common();
        DataTable dt = new DataTable();
        DataTable dtpending = new DataTable();
        DataTable dtsalary = new DataTable();
        DataTable dtoldtable = new DataTable();
        DataTable dtnewtable = new DataTable();
        DataTable dtemp_id = new DataTable();
        double numberofDays = 26;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["Username"] != "hradmin")
                {
                    Response.Redirect("LoginPage.aspx");
                }
                
                
                //For Approved Payroll
                string sSQLStatement = "select A.Payroll_ID, A.Emp_ID, (convert(varchar, A.Payroll_fromDate, 107) + ' - ' + convert(varchar, A.Payroll_toDate, 107)) AS PAYPERIOD, (UPPER(LEFT(cast(B.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_lname as nvarchar(max)),2,LEN(cast(B.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(B.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_fname as nvarchar(max)),2,LEN(cast(B.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(B.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_mname as nvarchar(max)),2,LEN(cast(B.Emp_mname as nvarchar(max)))))) AS Employee, C.Position_title, CAST(ROUND(A.NetPay, 2) AS Numeric(36, 2)) AS NetPay from db_owner.Payroll A left join db_owner.Employee B on A.Emp_ID = B.Emp_ID left join db_owner.PositionTitle C on C.Position_ID = B.Position_ID where A.payroll_status = 1";
                timelogs.LoadDataTable(sSQLStatement, dt);

                if (dt.Rows.Count != 0)
                {
                    dgPayroll.DataSource = dt;
                    dgPayroll.DataBind();
                }
                else
                {
                    lblNoRecords.Visible = true;
                    dgPayroll.Visible = false;
                }

                //For Pending Payroll
                string sqlPending = "select A.Payroll_ID, A.Emp_ID, (convert(varchar, A.Payroll_fromDate, 107) + ' - ' + convert(varchar, A.Payroll_toDate, 107)) AS PAYPERIOD, (UPPER(LEFT(cast(B.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_lname as nvarchar(max)),2,LEN(cast(B.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(B.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_fname as nvarchar(max)),2,LEN(cast(B.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(B.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_mname as nvarchar(max)),2,LEN(cast(B.Emp_mname as nvarchar(max)))))) AS Employee, C.Position_title, CAST(ROUND(A.NetPay, 2) AS Numeric(36, 2)) AS NetPay from db_owner.Payroll A left join db_owner.Employee B on A.Emp_ID = B.Emp_ID left join db_owner.PositionTitle C on C.Position_ID = B.Position_ID where A.payroll_status = 0";
                timelogs.LoadDataTable(sqlPending, dtpending);

                if (dtpending.Rows.Count != 0)
                {
                    dgPendingPayroll.DataSource = dtpending;
                    dgPendingPayroll.DataBind();
                }
                else
                {
                    lblNoPending.Visible = true;
                    dgPendingPayroll.Visible = false;
                }

                    DropDownList1.Enabled = false;
                    drpDownEmployee.Items.Insert(0, new System.Web.UI.WebControls.ListItem("No Employee", "0"));
                    drpDownEmployee.Enabled = false;

                //Load dropdown in Mass Compute
                    objCommon.DropdownDepartmentMass(drpDepartmentMass, "Select * from db_owner.Department", "Department_name", "Department_ID");
            }
        }
        //Show Employee by Department or Position dropdown
        protected void drpFormChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpFormChoose.SelectedValue.ToString() == "1")
            {
                objCommon.DropdownDepartment(drpFormDepartment, "Select * from db_owner.Department", "Department_name", "Department_ID");
                drpFormDepartment.Visible = true;
                drpFormPosition.Visible = false;
                DropDownList2.Visible = false;
            }
            else if (drpFormChoose.SelectedValue.ToString() == "2")
            {
                objCommon.DropdownPosition(drpFormPosition, "Select * from db_owner.PositionTitle", "Position_title", "Position_ID");
                drpFormPosition.Visible = true;
                drpFormDepartment.Visible = false;
                DropDownList2.Visible = false;
            }
            else
            {
                DropDownList2.Visible = true;
                DropDownList2.Enabled = false;
                drpFormDepartment.Visible = false;
                drpFormPosition.Visible = false;
            }
        }

        //Event for Search Button
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string drpchoose = drpFormChoose.SelectedItem.Text;

            if (drpchoose == "1")
            {
                string drpdepartment = drpFormDepartment.SelectedItem.Text.Trim();
                string sqldeparment = "Department_name ='" + drpdepartment + "'";
            }
            if (drpchoose == "2")
            {
                string drpposition = drpFormPosition.SelectedItem.Text.Trim();
                string sqlposition = "Position_title = '" + drpposition +"'";
            }
        }
        //Dropdown Choose Department/Position - Inside MODAL
        protected void drpChoose_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drpChoose.SelectedValue.ToString() == "1")
            {
                objCommon.DropdownDepartment(drpEmployeeDepartment, "Select * from db_owner.Department", "Department_name", "Department_ID");
                drpEmployeeDepartment.Visible = true;
                drpEmployeePosition.Visible = false;
                DropDownList1.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnCreatePayroll`);", true);
            }
            else if (drpChoose.SelectedValue.ToString() == "2")
            {
                objCommon.DropdownPosition(drpEmployeePosition, "Select * from db_owner.PositionTitle", "Position_title", "Position_ID");
                drpEmployeePosition.Visible = true;
                drpEmployeeDepartment.Visible = false;
                DropDownList1.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnCreatePayroll`);", true);
            }
            else
            {
                DropDownList1.Visible = true;
                DropDownList1.Enabled = false;
                drpEmployeeDepartment.Visible = false;
                drpEmployeePosition.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnCreatePayroll`);", true);
            }
        }

        //Dropdown for Department - Inside MODAL
        protected void drpEmployeeDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpDownEmployee.Items.Clear();

            string department_id = drpEmployeeDepartment.SelectedValue;
            string sqlstatement = "select A.bio_number, A.Emp_ID, (UPPER(LEFT(cast(A.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_lname as nvarchar(max)),2,LEN(cast(A.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(A.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_fname as nvarchar(max)),2,LEN(cast(A.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(A.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_mname as nvarchar(max)),2,LEN(cast(A.Emp_mname as nvarchar(max)))))) AS Employee from db_owner.Employee A left join db_owner.Department B on A.Department_ID = B.Department_ID where B.Department_ID =" + department_id;
            objCommon.LoadDataTable(sqlstatement, dt);


            //Employee Dropdown
            if (dt.Rows.Count != 0)
            {
                objCommon.DropdownEmployee(drpDownEmployee, sqlstatement, "Employee", "bio_number");
                drpDownEmployee.Enabled = true;
            }
            else
            {
                drpDownEmployee.Items.Insert(0, new System.Web.UI.WebControls.ListItem("No Employee", "0"));
                drpDownEmployee.Enabled = false;
            }
            ShowModal();
        }

        //Dropdown for Position - Inside MODAL
        protected void drpEmployeePosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpDownEmployee.Items.Clear();

            string position_id = drpEmployeePosition.SelectedValue;
            string sqlstatement = "select A.bio_number, A.Emp_ID, (UPPER(LEFT(cast(A.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_lname as nvarchar(max)),2,LEN(cast(A.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(A.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_fname as nvarchar(max)),2,LEN(cast(A.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(A.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_mname as nvarchar(max)),2,LEN(cast(A.Emp_mname as nvarchar(max)))))) AS Employee from db_owner.Employee A left join db_owner.PositionTitle B on A.Position_ID = B.Position_ID where B.Position_ID =" + position_id;
            objCommon.LoadDataTable(sqlstatement, dt);

            if (dt.Rows.Count != 0)
            {
                objCommon.DropdownEmployee(drpDownEmployee, sqlstatement, "Employee", "bio_number");
                drpDownEmployee.Enabled = true;
            }
            else
            {
                drpDownEmployee.Items.Insert(0, new System.Web.UI.WebControls.ListItem("No Employee", "0"));
                drpDownEmployee.Enabled = false;
            }
            ShowModal();
        }

        //Create Payroll Modal per Employee
        protected void btnCreatePayrollModal_Click(object sender, EventArgs e)
        {
            CreatePayroll();

            string empid = Session["Emp_id"].ToString();
            string dateFrom = txtDateFromModal.Value.Trim();
            string dateTo = txtDateToModal.Value.Trim();
            string totalhourpay = Session["TOTALHOURPAY"].ToString();
            string totalovertimepay = Session["TOTALOVERTIMEPAY"].ToString();
            string totaltardiness = Session["TOTALTARDINESSPAY"].ToString();
            string grosspay = Session["GROSSPAY"].ToString();
            string netpay = Session["NETPAY"].ToString();
            int leave = 0;
            string sss = Session["SSS"].ToString();
            string phil = Session["PHILHEALTH"].ToString();
            string pagibig = Session["PAGIBIG"].ToString();
            int otherdeductions = 0;
            string payroll_status = "0";

            payroll.CreatePayslip(int.Parse(empid), dateFrom, dateTo, totalhourpay, totalovertimepay, totaltardiness, leave, sss, phil , pagibig, otherdeductions, grosspay, netpay, payroll_status);

            Response.Redirect("Payroll.aspx");
        }

        protected void drpDownEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowModal();
        }
        protected void txtDateFromModal_TextChanged(object sender, EventArgs e)
        {
            
        }

        //Compute Button Per Employee
        protected void btnComputeModal_Click(object sender, EventArgs e)
        {
            //From Date
            if (txtDateFromModal.Value == "")
            {
                Response.Write("<script>confirm('From Date is required.');</script>");
                ShowModal();
                return;
            }
            //To Date
            if (txtDateToModal.Value == "")
            {
                Response.Write("<script>confirm('To Date is required.');</script>");
                ShowModal();
                return;
            }

            CreatePayroll();
        }


        //Compute Mass Payroll
        protected void btnMassCompute_Click(object sender, EventArgs e)
        {
            string from_date = txtDateFromModal2.Value.Trim();
            string to_date = txtDateToModal2.Value.Trim();
            string dept_id = drpDepartmentMass.SelectedValue;
            double sss = 581.30;
            double phil = 275.00;
            double pagibig = 100.00;
            string sqlMassCompute = "select A.*, C.Department_name as Department,(UPPER(LEFT(cast(A.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_lname as nvarchar(max)),2,LEN(cast(A.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(A.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_fname as nvarchar(max)),2,LEN(cast(A.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(A.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(A.Emp_mname as nvarchar(max)),2,LEN(cast(A.Emp_mname as nvarchar(max)))))) AS FullName, Position_title AS Position from db_owner.Employee A left join db_owner.PositionTitle B on A.Position_ID = B.Position_ID left join db_owner.Department C on A.Department_ID = C.Department_ID";
            
            //Validation 
            //If selected sepcific department
            if (dept_id != "0")
            {
                sqlMassCompute += " where A.Department_id =" + dept_id;
            }

            //Load the table of employee
            objCommon.LoadDataTable(sqlMassCompute, dtoldtable);

            if (dtoldtable.Rows.Count != 0)
            {   
                dtnewtable.Columns.Add("Emp_ID");
                dtnewtable.Columns.Add("PayPeriod");
                dtnewtable.Columns.Add("FullName");
                dtnewtable.Columns.Add("Position");
                dtnewtable.Columns.Add("Department");
                dtnewtable.Columns.Add("Payroll_fromDate");
                dtnewtable.Columns.Add("Payroll_toDate");
                dtnewtable.Columns.Add("TotalHourPay");
                dtnewtable.Columns.Add("TotalOvertimePay");
                dtnewtable.Columns.Add("TotalTardinessPay");
                dtnewtable.Columns.Add("Leave");
                dtnewtable.Columns.Add("SSS");
                dtnewtable.Columns.Add("PhilHealth");
                dtnewtable.Columns.Add("Pagibig");
                dtnewtable.Columns.Add("OtherDeductions");
                dtnewtable.Columns.Add("GrossPay");
                dtnewtable.Columns.Add("NetPay");

                for (int i = 0; i < dtoldtable.Rows.Count; i++)
                {
                    string salary = dtoldtable.Rows[i]["Emp_BasicSalary"].ToString();
                    string emp_id = dtoldtable.Rows[i]["Emp_ID"].ToString();
                    string bio_number = dtoldtable.Rows[i]["bio_number"].ToString();
                    string position = dtoldtable.Rows[i]["Position"].ToString();
                    string department = dtoldtable.Rows[i]["department"].ToString();
                    string fullname = dtoldtable.Rows[i]["FullName"].ToString();
                    double salary_perday = double.Parse(salary) / 22;
                    double salary_perhour = salary_perday / 8;
                    double salary_perminute = salary_perhour / 60;

                    string sqlStatement = "SELECT SUM(D.TOTAL) AS TOTALHOUR, SUM(D.TOTAL) * " + salary_perhour + " AS TOTALHOURPAY, SUM(D.OVERTIME) AS TOTALOVERTIME, SUM(D.OVERTIME) * " + salary_perhour + " AS TOTALOVERTIMEPAY, SUM(D.TARDINESS) AS TOTALTARDINESSMINUTES, SUM(D.TARDINESS) * " + salary_perminute + " AS TOTALTARDINESSPAY,CAST(ROUND(SUM(CAST(D.HALFDAY AS int))/60, 2) AS Numeric(36, 2)) AS ABSENT, SUM(CAST(D.HALFDAY AS int))/60 * " + salary_perhour + " AS TOTALABSENT FROM (SELECT C.Emp_ID , C.DATE, C.TIMEIN, C.TIMEOUT, C.TOTAL, C.OVERTIME ,CASE WHEN HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY, B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) - (CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2))) - 1 AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME , CAST(ROUND(DATEDIFF(MINUTE, '8:00:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS FROM (select Emp_ID, Timelogs_date, MIN(Timelogs_time) AS TIMEIN, CASE WHEN COUNT(Timelogs_time) > 1 THEN MAX(Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE bio_number = " + bio_number + " AND (Timelogs_date >= '" + from_date + "' AND '" + to_date + "' >= Timelogs_date ) GROUP BY EMP_iD, Timelogs_date) B) C) D";
                    timelogs.LoadDataTable(sqlStatement, dt);

                    if (dt.Rows.Count != 0)
                    {
                        string totalhourpay = dt.Rows[0]["TOTALHOURPAY"].ToString();
                        string totalovertime = dt.Rows[0]["TOTALOVERTIMEPAY"].ToString();
                        string totaltardiness = dt.Rows[0]["TOTALTARDINESSPAY"].ToString();
                        string totalabsent = dt.Rows[0]["TOTALABSENT"].ToString();

                        double grosspay = Double.Parse(totalhourpay) + Double.Parse(totalovertime) + Double.Parse(totaltardiness) - Double.Parse(totalabsent);
                        double netpay = grosspay - sss - phil - pagibig;

                        DataRow row = dtnewtable.NewRow();
                        row["Emp_ID"] = emp_id;
                        row["PayPeriod"] = DateTime.Parse(from_date).ToString("MMMM dd, yyyy") + "-" + DateTime.Parse(to_date).ToString("MMMM dd, yyyy");
                        row["FullName"] = fullname;
                        row["Position"] = position;
                        row["Department"] = department;
                        row["Payroll_fromDate"] = from_date;
                        row["Payroll_toDate"] = to_date;
                        row["TotalHourPay"] = totalhourpay;
                        row["TotalOvertimePay"] = totalovertime;
                        row["TotalTardinessPay"] = totaltardiness;
                        row["SSS"] = sss;
                        row["PhilHealth"] = phil;
                        row["Pagibig"] = pagibig;
                        //row["OtherDeductions"] = to_date;
                        row["GrossPay"] = Math.Round(grosspay, 2);
                        row["NetPay"] = Math.Round(netpay, 2);

                        dtnewtable.Rows.Add(row);
                    }

                }

                Session["dtnewtable"] = dtnewtable;

                dgMassPayroll.DataSource = dtnewtable;
                dgMassPayroll.DataBind();
            }
        }

        protected void btnMassGenerate_Click(object sender, EventArgs e)
        {
            DataTable dtr = Session["dtnewtable"] as DataTable;
            payroll.SaveDataTable(dtr);
        }

        public void CreatePayroll()
        {
            string dateFrom = txtDateFromModal.Value.Trim();
            string dateTo = txtDateToModal.Value.Trim();
            string bio_number = drpDownEmployee.SelectedValue;
            string emp_id = "";
            //To get the employee id
            objCommon.LoadDataTable("select * from db_owner.Employee where bio_number =" + bio_number, dtemp_id);
            if (dtemp_id.Rows.Count != 0)
            {
                emp_id = dtemp_id.Rows[0]["Emp_ID"].ToString();
            }
            

            double sss = 581.30;
            double phil = 275.00;
            double pagibig = 100.00;

            if (dateFrom == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnCreatePayroll`);", true);
                return;
            }

            if (dateTo == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnCreatePayroll`);", true);
                return;
            }
            
            //Load the Basic Salary 
            string sql = "select * from db_owner.Employee where bio_number =" + bio_number;
            objCommon.LoadDataTable(sql, dtsalary);

            if (dtsalary.Rows.Count != 0)
            {
                string salary = dtsalary.Rows[0]["Emp_BasicSalary"].ToString();
                double salary_perday = double.Parse(salary) / numberofDays; // Work days per month
                double salary_perhour = salary_perday / 8;
                double salary_perminute = salary_perhour / 60;


                //string sqlStatement = "SELECT SUM(D.TOTAL) AS TOTALHOUR, SUM(D.TOTAL) * 62.5 AS TOTALHOURPAY, SUM(D.OVERTIME) AS TOTALOVERTIME, SUM(D.OVERTIME) * 62.5 AS TOTALOVERTIMEPAY, SUM(D.TARDINESS) AS TOTALTARDINESSMINUTES, SUM(D.TARDINESS) * 1.04 AS TOTALTARDINESSPAY,CAST(ROUND(SUM(CAST(D.HALFDAY AS int))/60, 2) AS Numeric(36, 2)) AS ABSENT, SUM(CAST(D.HALFDAY AS int))/60 * 62.5 AS TOTALABSENT FROM (SELECT C.Emp_ID , C.DATE, C.TIMEIN, C.TIMEOUT, C.TOTAL, C.OVERTIME ,CASE WHEN HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY, B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) - (CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2))) - 1 AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME , CAST(ROUND(DATEDIFF(MINUTE, '8:00:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS FROM (select Emp_ID, Timelogs_date, MIN(Timelogs_time) AS TIMEIN, CASE WHEN COUNT(Timelogs_time) > 1 THEN MAX(Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE Emp_ID = " + empid + " AND (Timelogs_date >= '" + dateFrom + "' AND '" + dateTo + "' >= Timelogs_date ) GROUP BY EMP_iD, Timelogs_date) B) C) D";
                string sqlStatement = "SELECT SUM(D.TOTAL) AS TOTALHOUR, SUM(D.TOTAL) * "+ salary_perhour +" AS TOTALHOURPAY, SUM(D.OVERTIME) AS TOTALOVERTIME, SUM(D.OVERTIME) * "+ salary_perhour +" AS TOTALOVERTIMEPAY, SUM(D.TARDINESS) AS TOTALTARDINESSMINUTES, SUM(D.TARDINESS) * "+ salary_perminute +" AS TOTALTARDINESSPAY,CAST(ROUND(SUM(CAST(D.HALFDAY AS int))/60, 2) AS Numeric(36, 2)) AS ABSENT, SUM(CAST(D.HALFDAY AS int))/60 * " + salary_perhour + " AS TOTALABSENT FROM (SELECT C.Emp_ID , C.DATE, C.TIMEIN, C.TIMEOUT, C.TOTAL, C.OVERTIME ,CASE WHEN HALFDAY = '300' THEN (C.TARDINESS - C.HALFDAY) ELSE C.TARDINESS END AS TARDINESS, C.HALFDAY FROM (SELECT CASE WHEN B.TIMEIN >= '13:00:00' THEN '300' ELSE '0' END AS HALFDAY, B.Emp_ID,convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT, CAST(ROUND(DATEDIFF(MINUTE, B.TIMEIN, B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) - (CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2))) - 1 AS TOTAL, CASE WHEN B.TIMEOUT > '17:00:00' THEN CAST(ROUND(DATEDIFF(MINUTE, '17:00:00', B.TIMEOUT)/60.0, 2) AS Numeric(36, 2)) else '0.00' END AS OVERTIME , CAST(ROUND(DATEDIFF(MINUTE, '8:00:00', B.TIMEIN), 2) AS Numeric(36, 2)) AS TARDINESS FROM (select Emp_ID, Timelogs_date, MIN(Timelogs_time) AS TIMEIN, CASE WHEN COUNT(Timelogs_time) > 1 THEN MAX(Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A WHERE bio_number = " + bio_number + " AND (Timelogs_date >= '" + dateFrom + "' AND '" + dateTo + "' >= Timelogs_date ) GROUP BY EMP_iD, Timelogs_date) B) C) D";
                timelogs.LoadDataTable(sqlStatement, dt);

                if (dt.Rows.Count != 0)
                {
                    string totalhourpay = dt.Rows[0]["TOTALHOURPAY"].ToString();
                    string totalovertime = dt.Rows[0]["TOTALOVERTIMEPAY"].ToString();
                    string totaltardiness = dt.Rows[0]["TOTALTARDINESSPAY"].ToString();
                    string totalabsent = dt.Rows[0]["TOTALABSENT"].ToString();


                    lblTotalHours.InnerText = String.Format("{0:n}", Decimal.Parse(totalhourpay.ToString()));
                    lblOvertime.InnerText = String.Format("{0:n}", Decimal.Parse(totalovertime.ToString()));
                    lblTardiness.InnerText = String.Format("{0:n}", Decimal.Parse(totaltardiness.ToString()));
                    lblAbsent.InnerHtml = String.Format("{0:n}", Decimal.Parse(totalabsent.ToString()));

                    lblSssGsis.InnerText = String.Format("{0:n}", Decimal.Parse(sss.ToString().ToString()));
                    lblPhilhealth.InnerText = String.Format("{0:n}", Decimal.Parse(phil.ToString().ToString()));
                    lblPagibig.InnerText = String.Format("{0:n}", Decimal.Parse(pagibig.ToString().ToString()));

                    double grosspay = Double.Parse(totalhourpay) + Double.Parse(totalovertime) + Double.Parse(totaltardiness) - Double.Parse(totalabsent);
                    double netpay = grosspay - sss - phil - pagibig;

                    lblGrossPay.InnerText = String.Format("{0:n}", Decimal.Parse(grosspay.ToString().ToString()));
                    lblNetPay.InnerText = String.Format("{0:n}", Decimal.Parse(netpay.ToString().ToString()));

                    ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnCreatePayroll`);", true);

                    Session["Emp_id"] = emp_id;
                    Session["TOTALHOURPAY"] = totalhourpay;
                    Session["TOTALOVERTIMEPAY"] = totalovertime;
                    Session["TOTALTARDINESSPAY"] = totaltardiness;
                    Session["SSS"] = sss.ToString();
                    Session["PHILHEALTH"] = phil.ToString();
                    Session["PAGIBIG"] = pagibig.ToString();
                    Session["GROSSPAY"] = grosspay.ToString();
                    Session["NETPAY"] = netpay.ToString();
                }
            }

        }

        protected void dgPayroll_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblPayrollID = (Label)e.Item.FindControl("lblPayrollID");
                Label lblEmpID = (Label)e.Item.FindControl("lblEmpID");
                
                Session["payroll_id"] = lblPayrollID.Text;
                Session["emp_id"] = lblEmpID.Text;
            }
        }

        //Print Button or Print Payslip
        public void itemcommand(object sender, CommandEventArgs c)
        {
            //string payroll_id = Session["payroll_id"].ToString();
            //string emp_id = Session["emp_id"].ToString();

            Button btn = (Button)(sender);
            string payroll_id = btn.CommandArgument;

            string sql = "select B.Emp_BasicSalary ,(convert(varchar, A.Payroll_fromDate, 107) + ' - ' + convert(varchar, A.Payroll_toDate, 107)) AS PAYPERIOD, (UPPER(LEFT(cast(B.Emp_lname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_lname as nvarchar(max)),2,LEN(cast(B.Emp_lname as nvarchar(max))))) + ', ' + UPPER(LEFT(cast(B.Emp_fname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_fname as nvarchar(max)),2,LEN(cast(B.Emp_fname as nvarchar(max)))))+ ' ' + UPPER(LEFT(cast(B.Emp_mname as nvarchar(max)),1)) + LOWER(SUBSTRING(cast(B.Emp_mname as nvarchar(max)),2,LEN(cast(B.Emp_mname as nvarchar(max)))))) AS EmployeeFullName , A.* from db_owner.Payroll A left join db_owner.Employee B on A.Emp_ID = B.Emp_ID where Payroll_ID =" + payroll_id;

            objCommon.LoadDataTable(sql, dt);
            if (dt.Rows.Count != 0)
            {
                //Data
                string fullname = dt.Rows[0]["EmployeeFullName"].ToString();
                string payperiod = dt.Rows[0]["PAYPERIOD"].ToString();
                string basicsalary = String.Format("{0:n}", Decimal.Parse(dt.Rows[0]["Emp_BasicSalary"].ToString()));
                string totalhourpay = String.Format("{0:n}", Decimal.Parse(dt.Rows[0]["TotalHourPay"].ToString()));
                string totaltardiness = String.Format("{0:n}", Decimal.Parse(dt.Rows[0]["TotalTardinessPay"].ToString()));
                string totalovertime = String.Format("{0:n}", Decimal.Parse(dt.Rows[0]["TotalOvertimePay"].ToString()));
                string totalleave = String.Format("{0:n}", Decimal.Parse(dt.Rows[0]["Leave"].ToString()));
                string SSS = String.Format("{0:n}", Decimal.Parse(dt.Rows[0]["SSS"].ToString()));
                string pagibig = String.Format("{0:n}", Decimal.Parse(dt.Rows[0]["Pagibig"].ToString()));
                string philhealth = String.Format("{0:n}", Decimal.Parse(dt.Rows[0]["Philhealth"].ToString()));
                string totaldecutions = String.Format("{0:n}", Decimal.Parse(dt.Rows[0]["OtherDeductions"].ToString()));
                string grosspay = String.Format("{0:n}", Decimal.Parse(dt.Rows[0]["GrossPay"].ToString()));
                string netpay = String.Format("{0:n}", Decimal.Parse(dt.Rows[0]["NetPay"].ToString()));


                Document pdoc = new Document(PageSize.A4, 20f, 20f, 30f, 30f);

                try 
                {
                    string foldername = "Payslip";
                    string directoryPath = Server.MapPath(string.Format("C:/{0}/", foldername));
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                }
                catch
                {
                     Response.Write("<script>confirm('Please contact the admin');</script>");
                    return;
                }
                try
                {
                    PdfWriter pWriter = PdfWriter.GetInstance(pdoc, new FileStream("\\Payslip\\Payslip.pdf", FileMode.Create));
                }
                catch
                {
                    Response.Write("<script>confirm('Please create a folder.');</script>");
                    return;
                }
                pdoc.Open();

                // Read in the contents of the Receipt.htm file...
                string contents = File.ReadAllText(Server.MapPath("~/HTMLPage1.htm"));

                // Replace the placeholders with the user-specified text
                contents = contents.Replace("[EmployeeFullName]", fullname);
                contents = contents.Replace("[PAYPERIOD]", payperiod);
                contents = contents.Replace("[Emp_BasicSalary]", basicsalary);
                contents = contents.Replace("[TotalHours]", totalhourpay);
                contents = contents.Replace("[TotalTardiness]", totaltardiness);
                contents = contents.Replace("[TotalOvertimePay]", totalovertime);
                contents = contents.Replace("[Leave]", totalleave);
                contents = contents.Replace("[SSS]", SSS);
                contents = contents.Replace("[Pagibig]", pagibig);
                contents = contents.Replace("[Philhealth]", philhealth);
                contents = contents.Replace("[OtherDeductions]", totaldecutions);
                contents = contents.Replace("[GrossPay]", grosspay);
                contents = contents.Replace("[NetPay]", netpay);

                // Step 4: Parse the HTML string into a collection of elements...
                var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents), null);

                // Enumerate the elements, adding each one to the Document...
                foreach (var htmlElement in parsedHtmlElements)
                    pdoc.Add(htmlElement as IElement);

                pdoc.Close();
                System.Diagnostics.Process.Start("\\Payslip\\Payslip.pdf");
            }
        }

        //To Delete Button for Pending payroll
        public void itemcommanddelete(object sender, CommandEventArgs c)
        {
            Button btn = (Button)(sender);
            string payroll_id = btn.CommandArgument;

            payroll.CancelPayroll(int.Parse(payroll_id));
            Response.Redirect("Payroll.aspx");
        }
        public void ShowModal()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnCreatePayroll`);", true);
        }
        protected void btnCloseModal_Click(object sender, EventArgs e)
        {
            Response.Redirect("Payroll.aspx");
        }
    }
}