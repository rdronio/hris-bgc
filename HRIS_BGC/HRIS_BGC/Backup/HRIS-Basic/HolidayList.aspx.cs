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
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlStatement = "SELECT * FROM db_owner.Holiday";
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
            string date = txtDate.Value.Trim();
            string description = txtDescription.Value.Trim();

            if (date == "")
            {
                Response.Write("<script>confirm('Date is required.');</script>");

                return;
            }

            if (objCommon.ConvertToEmptyIfNothing(date).ToString().Length != 0)
            {
                if (date.ToString().Length != 10)
                {
                    Response.Write("<script>alert('Invalid to date format!');</script>");

                    return;
                }
            }

            if (description == "")
            {
                Response.Write("<script>confirm('Description is required.');</script>");

                return;
            }

            objHoliday.AddHoliday(date, description);
            Response.Redirect("HolidayList.aspx");
        }

        public void itemcommand(object sender, CommandEventArgs c)
        {
            //string payroll_id = Session["payroll_id"].ToString();
            //string emp_id = Session["emp_id"].ToString();

            LinkButton btn = (LinkButton)(sender);
            string holiday_id = btn.CommandArgument;
           

            if (c.CommandName == "Edit")
            {
                string sql = "SELECT * from db_owner.Holiday where holiday_id =" + holiday_id;
                objCommon.LoadDataTable(sql, dt);

                if (dt.Rows.Count != 0)
                {
                    Session["holiday_id"] = holiday_id;
                    string date = dt.Rows[0]["holiday_date"].ToString();
                    date = date.Substring(0, 9);
                    txtEditDate.Value = date;
                    txtEditDescription.Value = dt.Rows[0]["holiday_desc"].ToString();
                }
                ClientScript.RegisterStartupScript(this.GetType(), "key", "openModal('#btnEditHoliday')", true);
            }
            if (c.CommandName == "Delete")
            {
                objHoliday.DeleteHoliday(int.Parse(holiday_id));
                Response.Redirect("HolidayList.aspx");
            }
        }

        protected void btnSubmitEditHoliday_Click(object sender, EventArgs e)
        {
            string holiday_id = Session["holiday_id"].ToString();
            string edit_date = txtEditDate.Value.Trim();
            string edit_desc = txtEditDescription.Value.Trim();

            if (edit_date == "")
            {
                Response.Write("<script>confirm('Date is required.');</script>");

                return;
            }

            if (edit_desc == "")
            {
                Response.Write("<script>confirm('Description is required.');</script>");

                return;
            }

            objHoliday.EditHoliday(int.Parse(holiday_id), edit_date, edit_desc);
            return;
        }

    }
}