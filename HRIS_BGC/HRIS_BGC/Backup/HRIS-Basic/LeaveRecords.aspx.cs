using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;
using System.Data;
using System.Text;

namespace HRIS_Basic
{
    public partial class LeaveRecords : System.Web.UI.Page
    {
        Lib_Leave objLeave = new Lib_Leave();
        Common objCommon = new Common();
        DataTable dtleave = new DataTable();
        DataTable dtpendingleave = new DataTable();
        DataTable dtleavecount = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                 if (Session["Username"] == null)
                {
                    Response.Redirect("LoginPage.aspx");
                }

                int empid = int.Parse(Session["Employee_ID"].ToString());

                //Table for Leave Records
                string sqlStatement = "select CASE WHEN leave_status = 1 Then 'Not Applicable' WHEN leave_status = 2 THEN leave_remarks END AS REMARKS, numberOfDays, Leave_ID, Emp_ID, leave_type, convert(varchar, file_date, 107) file_date, convert(varchar, leave_from, 107) leave_from, convert(varchar, leave_to, 107) leave_to, reason, CASE WHEN withpay = '1' Then 'Yes' ELSE 'No' end as withpay, CASE WHEN leave_status = '0' THEN 'Pending' WHEN leave_status = '1' THEN 'Approved' WHEN leave_status = '2' THEN 'Rejected' END AS leave_status from db_owner.LeaveRecord WHERE leave_status not in ('0') AND emp_id =" + empid;
                objCommon.LoadDataTable(sqlStatement, dtleave);

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

                //Table for Pending Leave Records
                string sSQLStatement = "select numberOfDays, Leave_ID, Emp_ID, leave_type, convert(varchar, file_date, 107) file_date, convert(varchar, leave_from, 107) leave_from, convert(varchar, leave_to, 107) leave_to, reason, CASE WHEN withpay IS NULL Then 'Pending' ELSE withpay end as withpay, CASE WHEN leave_status = '0' THEN 'Pending' WHEN leave_status = '1' THEN 'Approved' WHEN leave_status = '2' THEN 'Rejected' END AS leave_status from db_owner.LeaveRecord where leave_status = '0' AND emp_ID =" +empid;
                objCommon.LoadDataTable(sSQLStatement, dtpendingleave);

                if (dtpendingleave.Rows.Count != 0)
                {
                    dgPendingLeave.HeaderStyle.Font.Bold = true;
                    dgPendingLeave.HeaderStyle.Font.Size = 8;


                    dgPendingLeave.DataSource = dtpendingleave;
                    dgPendingLeave.DataBind();
                }
                else 
                {
                    lblNoData.Visible = true;
                    dgPendingLeave.Visible = false;
                }

                //Leave 
                string sqlLeaveCount = "Select * from db_owner.LeaveCount where emp_id=" + empid + " AND leavecount_year='" + objCommon.pacificyear + "'";
                objCommon.LoadDataTable(sqlLeaveCount, dtleavecount);

                if (dtleavecount.Rows.Count != 0)
                {
                    string available_vleave = dtleavecount.Rows[0]["vacation_leave"].ToString();
                    string used_vleave = dtleavecount.Rows[0]["used_vacation_leave"].ToString();
                    string available_sleave = dtleavecount.Rows[0]["sick_leave"].ToString();
                    string used_sleave = dtleavecount.Rows[0]["used_sickleave"].ToString();

                    //Available vacation and sick Leave
                    availableVELeave.InnerText = String.Format("{0:n}", Decimal.Parse(available_vleave));
                    availableSLeave.InnerText = String.Format("{0:n}", Decimal.Parse(available_sleave));

                    //Used and remaining vacation Leave
                    if (used_vleave != "")
                    {
                        double remaining_vleave = double.Parse(available_vleave) - double.Parse(used_vleave);
                        Session["remaining_vleave"] = remaining_vleave;
                        usedVELeave.InnerText = String.Format("{0:n}", Decimal.Parse(used_vleave));
                        remainingVELeave.InnerText = String.Format("{0:n}", Decimal.Parse(remaining_vleave.ToString()));
                    }
                    else
                    {
                        Session["remaining_vleave"] = "0";
                    }

                    //Used and remaining sick Leave
                    if (used_sleave != "")
                    {
                        double remaining_sleave = double.Parse(available_sleave) - double.Parse(used_sleave);
                        Session["remaining_sleave"] = remaining_sleave;
                        usedSLeave.InnerText = String.Format("{0:n}", Decimal.Parse(used_sleave));
                        remainingSLeave.InnerText = String.Format("{0:n}", Decimal.Parse(remaining_sleave.ToString()));
                    }
                    else
                    {
                        Session["remaining_sleave"] = "0";
                       
                    }

                }
                else
                {
                    //To disabled the button "File a leave"
                    btnFileLeave.Attributes.Add("class", "btn-sm btn-primary mx-3 disabled");
                }
            }

        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            //int empid = 4;
            int empid = int.Parse(Session["Employee_ID"].ToString());
            string status = "0";
            string leave = "";

            if (rdSick.Checked == true)
            {
                leave = "Sick";
            }
            else if (rdVacation.Checked == true)
            {
                leave = "Vacation";
            }
            else if (rdEmergency.Checked == true)
            {
                leave = "Emergency";
            }
            else
            {
                Response.Write("<script>confirm('Leave Type is required.');</script>");
                ShowModal();
                return;
            }

            string fromDate = txtDateFrom.Value.Trim();
            string toDate = txtDateTo.Value.Trim();
            string reason = txtAreaReason.Value.Trim();

            //From date
            if (fromDate == "")
            {
                Response.Write("<script>confirm('From Date is required.');</script>");
                ShowModal();
                return;
            }

            if (objCommon.ConvertToEmptyIfNothing(fromDate).ToString().Length != 0)
            {
                if (fromDate.ToString().Length != 10)
                {
                    Response.Write("<script>alert('Invalid from date format!');</script>");
                    return;
                }
            }

            if (DateTime.Parse(fromDate) < DateTime.Parse(objCommon.pacificdate))
            {
                Response.Write("<script>confirm('From Date is less than date today. Please change the date');</script>");
                ShowModal();
                return;
            }

            //To date
            if (toDate == "")
            {
                Response.Write("<script>confirm('To Date is required.');</script>");
                ShowModal();
                return;
            }

            if (objCommon.ConvertToEmptyIfNothing(toDate).ToString().Length != 0)
            {
                if (toDate.ToString().Length != 10)
                {
                    Response.Write("<script>alert('Invalid to date format!');</script>");
                    return;
                }
            }

            if (DateTime.Parse(fromDate) > DateTime.Parse(toDate))
            {
                Response.Write("<script>confirm('From Date is greater than To Date.');</script>");
                ShowModal();
                return;
            }

            //Count of days
            List<DateTime> allDates = new List<DateTime>();


            for (DateTime date = DateTime.Parse(fromDate); date <= DateTime.Parse(toDate); date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    allDates.Add(date);
                }
            }

            int numberOfDays = allDates.Count;
         
            //Load the table of Leave Cunt
            objCommon.LoadDataTable("Select * from db_owner.LeaveCount where emp_id=" + empid + " AND leavecount_year='" + objCommon.pacificyear + "'", dtleavecount);

            if (dtleavecount.Rows.Count != 0)
            {
                string available_vleave = dtleavecount.Rows[0]["vacation_leave"].ToString();
                string used_vleave = dtleavecount.Rows[0]["used_vacation_leave"].ToString();
                string available_sleave = dtleavecount.Rows[0]["sick_leave"].ToString();
                string used_sleave = dtleavecount.Rows[0]["used_sickleave"].ToString();

                double remaining_vleave = double.Parse(available_vleave) - double.Parse(used_vleave);
                double remaining_sleave = double.Parse(available_sleave) - double.Parse(used_sleave);

                if (leave == "Sick" && remaining_sleave < numberOfDays)
                {
                    Response.Write("<script>confirm('Sick Leave credit exceed.');</script>");
                    ShowModal();
                    return;
                }

                if (leave == "Vacation" && remaining_vleave < numberOfDays)
                {
                    Response.Write("<script>confirm('VL/EL credit exceed.');</script>");
                    ShowModal();
                    return;
                }
            }
            else
            {
                Response.Write("<script>confirm('Leave credit is not available.');</script>");
                ShowModal();
                return;
            
            }

           


            //Load the table of Leave Cunt
            //objCommon.LoadDataTable("Select * from db_owner.LeaveCount where emp_id =" + empid, dtleavecount);
            
            //if (dtleavecount.Rows.Count != 0)
            //{
            //    string available_vleave = dtleavecount.Rows[0]["vacation_leave"].ToString();
            //    string used_vleave = dtleavecount.Rows[0]["used_vacation_leave"].ToString();
            //    string available_sleave = dtleavecount.Rows[0]["sick_leave"].ToString();
            //    string used_sleave = dtleavecount.Rows[0]["used_sickleave"].ToString();

            //    int remaining_vleave = int.Parse(available_vleave) - int.Parse(used_vleave);
            //    int remaining_sleave = int.Parse(available_sleave) - int.Parse(used_sleave);

            //    if (leave == "Sick")
            //    {
            //        if (remaining_sleave < numberOfDays)
            //        {
            //            ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>alert('You don't have enough vacation leave credit');</script>'");
            //            ShowModal();
            //            return;
            //        }
                
            //    }
            //    else if (leave == "Vacation")
            //    {
            //        if (remaining_vleave < numberOfDays)
            //        {
            //            Response.Write("<script>confirm('You don't have enough vacation leave credit.');</script>");
            //            ShowModal();
            //            return;
            //        }

            //    }
            //    return;
            //}
            //else
            //{
            //    Response.Write("<script>confirm('You don't have leave credits.');</script>");
            //    ShowModal();
            //    return;
            //}


            //Reason
            if (reason == "")
            {
                Response.Write("<script>confirm('Reason is required.');</script>");
                ShowModal();
                return;
            }

            objLeave.FileLeave(empid, leave, fromDate, toDate, reason, status, numberOfDays);
            Response.Redirect("LeaveRecords.aspx");
        }

        public void itemcommand(object sender, CommandEventArgs c)
        {
            //string payroll_id = Session["payroll_id"].ToString();
            //string emp_id = Session["emp_id"].ToString();
            

            LinkButton btn = (LinkButton)(sender);
            string leave_id = btn.CommandArgument;

            objLeave.CancelLeave(int.Parse(leave_id));
            Response.Redirect("LeaveRecords.aspx");
        }

        public void ShowModal()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnFileLeave`);", true);
        }

    }
}