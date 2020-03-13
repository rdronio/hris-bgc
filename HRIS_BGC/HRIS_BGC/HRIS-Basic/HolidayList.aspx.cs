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
    public partial class HolidayList : System.Web.UI.Page
    {
        Common objCommon = new Common();
        Lib_Holiday objHoliday = new Lib_Holiday();
        sSQLStatement objSql = new sSQLStatement();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            BuildGrid();
        }

        public void BuildGrid()
        {
            string sqlStatement = "";

            sqlStatement = objSql.holidayList(sqlStatement);

            objCommon.LoadDataTable(sqlStatement, dt);

            if (dt.Rows.Count != 0)
            {
                dgHoliday.DataSource = dt;
                dgHoliday.DataBind();
            }
            else
            {
                lblNoData.Visible = true;
                dgHoliday.Visible = false;
            }
        }

        protected void btnSubmitAddHoliday_Click(object sender, EventArgs e)
        {
            //string date = txtDate.Value.Trim();

            string start_date = txtStartDate.Value.Trim();
            string end_date = txtEndDate.Value.Trim();
            string description = txtDescription.Value.Trim();

            if (start_date == "")
            {
                Response.Write("<script>confirm('Date is required.');</script>");
                ShowModal();
                return;
            }

            if (objCommon.ConvertToEmptyIfNothing(start_date).ToString().Length != 0)
            {
                try
                {
                    DateTime.Parse(start_date).ToString("MM/dd/yyyy");
                }
                catch
                {
                    Response.Write("<script>alert('Invalid holiday date format!');</script>");
                    ShowModal();
                    return;
                }
            }
            if (end_date == "")
            {
                Response.Write("<script>confirm('Date is required.');</script>");
                ShowModal();
                return;
            }

            if (objCommon.ConvertToEmptyIfNothing(start_date).ToString().Length != 0)
            {
                try
                {
                    DateTime.Parse(start_date).ToString("MM/dd/yyyy");
                }
                catch
                {
                    Response.Write("<script>alert('Invalid holiday date format!');</script>");
                    ShowModal();
                    return;
                }
            }

            if (DateTime.Parse(start_date) > DateTime.Parse(end_date))
            {
                Response.Write("<script>alert('Invalid! Start Date is greater than End Date!');</script>");
                ShowModal();
                return;
            }

            //Count of days
            List<DateTime> allDates = new List<DateTime>();


            for (DateTime date = DateTime.Parse(start_date); date <= DateTime.Parse(end_date); date = date.AddDays(1))
            {
                    allDates.Add(date);
            }

            //Check if holiday is already existing
            for (int i = 0; i < allDates.Count; i++)
            {
                string sqlHoliday = "select * from db_owner.Holiday where holiday_date = '" + allDates[i] + "'";

                objCommon.LoadDataTable(sqlHoliday, dt);

                if (dt.Rows.Count != 0)
                {
                    Response.Write("<script>alert('Holiday date already exist.');</script>");
                    ShowModal();
                    return;
                }
            }

            int numberOfDays = allDates.Count;

            if (description == "")
            {
                Response.Write("<script>confirm('Description is required.');</script>");
                ShowModal();
                return;
            }

            objHoliday.AddHolidayList(start_date, end_date, numberOfDays, description, allDates);
            Response.Redirect("HolidayList.aspx");
        }

        public void itemcommand(object sender, CommandEventArgs c)
        {
            //string payroll_id = Session["payroll_id"].ToString();
            //string emp_id = Session["emp_id"].ToString();

            LinkButton btn = (LinkButton)(sender);
            string holidaynotice_id = btn.CommandArgument;
           

            if (c.CommandName == "Edit")
            {
                string sql = "SELECT * from db_owner.HolidayList where holidaynotice_id =" + holidaynotice_id;
                objCommon.LoadDataTable(sql, dt);

                if (dt.Rows.Count != 0)
                {
                    Session["holidaynotice_id"] = holidaynotice_id;
                    string start_date = dt.Rows[0]["holiday_startDate"].ToString();
                    string end_date = dt.Rows[0]["holiday_endDate"].ToString();
                    start_date = start_date.Substring(0, 9);
                    end_date = end_date.Substring(0, 9);
                    txtEditStartDate.Value = start_date;
                    txtEditEndDate.Value = end_date;
                    txtEditDesc.Value = dt.Rows[0]["holiday_desc"].ToString();
                }
                ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal('#btnEditHoliday')", true);
            }
            if (c.CommandName == "Delete")
            {
                objHoliday.DeleteHoliday(int.Parse(holidaynotice_id));
                Response.Redirect("HolidayList.aspx");
            }
        }

        protected void btnSubmitEditHoliday_Click(object sender, EventArgs e)
        {
            //string holiday_id = Session["holiday_id"].ToString();
            //string edit_date = txtEditDate.Value.Trim();
            //string edit_desc = txtEditDescription.Value.Trim();

            //if (edit_date == "")
            //{
            //    Response.Write("<script>confirm('Date is required.');</script>");

            //    return;
            //}

            ////if (objCommon.ConvertToEmptyIfNothing(edit_date).ToString().Length != 0)
            ////{
            ////    try
            ////    {
            ////        DateTime.Parse(edit_date).ToString("MM/dd/yyyy");
            ////    }
            ////    catch
            ////    {
            ////        Response.Write("<script>alert('Invalid holiday date format!');</script>");
                  
            ////        return;
            ////    }
            ////}

            ////string sqlHoliday = "select * from db_owner.Holiday where holiday_date = '" + edit_date + "'";

            ////objCommon.LoadDataTable(sqlHoliday, dt);

            ////if (dt.Rows.Count != 0)
            ////{
            ////    Response.Write("<script>alert('Holiday date already exist.');</script>");

            ////    return;
            ////}

            //if (edit_desc == "")
            //{
            //    Response.Write("<script>confirm('Description is required.');</script>");

            //    return;
            //}

            //objHoliday.EditHoliday(int.Parse(holiday_id), edit_date, edit_desc);
            //return;
        }

        public void ShowModal()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal(`#btnAddHoliday`);", true);
        }


    }
}