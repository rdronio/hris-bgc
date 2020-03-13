using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;
using System.Data;
using System.IO;
using System.Text;

namespace HRIS_Basic
{
    public partial class GenerateDtrReport : System.Web.UI.Page
    {
        Common objCommon = new Common();
        DataTable dt = new DataTable();
        DataTable dtTimelogs = new DataTable();
        DataTable dtexport = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Load dropdown in Mass Compute
            if (!IsPostBack)
            {

                objCommon.DropdownDepartmentMass(drpDepartment, "Select * from db_owner.Department", "Department_name", "Department_ID");
            }

        }

        protected void drpChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpChoose.SelectedValue == "1")
            {
                objCommon.DropdownDepartmentMass(drpEmployeeByDepartment, "Select * from db_owner.Department", "Department_name", "Department_ID");
                drpEmployeeByDepartment.Visible = true;
                drpEmployeeByPosition.Visible = false;
                
            }
            else if (drpChoose.SelectedValue == "2")
            {
                objCommon.DropdownPosition(drpEmployeeByPosition, "select * from db_owner.PositionTitle", "Position_title", "Position_ID");
                drpEmployeeByPosition.Visible = true;
                drpEmployeeByDepartment.Visible = false;
            }

           
      
        }

        protected void drpEmployeeByDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dept_id = drpEmployeeByDepartment.SelectedValue;

            string sql = "Select * from db_owner.Employee";

            if (dept_id != "0")
            {
                sql += " where department_id= " + dept_id;
            }
            objCommon.LoadDataTable(sql, dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();

        }

        protected void drpEmployeeByPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            string position_id = drpEmployeeByPosition.SelectedValue;

            string sql = "Select * from db_owner.Employee";
            if (position_id != "0")
            {
                sql += " where position_id= " + position_id;
            }
            objCommon.LoadDataTable(sql, dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }

        protected void btnGenerateDTR_Click(object sender, EventArgs e)
        {
            string date_from = txtDateFrom.Value.Trim();
            string date_to = txtDateTo.Value.Trim();
            string dept_id = "";
            string position_id = "";

            if (date_from == "")
            {
                Response.Write("<script>confirm('Date From is required.');</script>");
                return;
            }

            if (date_to == "")
            {
                Response.Write("<script>confirm('Date To is required.');</script>");
                return;
            }

            if (optAll.Checked == true)
            {
                dept_id = drpDepartment.SelectedValue;
            }
            else if (optCustom.Checked == true)
            {
                dept_id = drpEmployeeByDepartment.SelectedValue;
                position_id = drpEmployeeByPosition.SelectedValue;
            }
            
            string sql = "SELECT * from db_owner.Employee";
            string sqlexport = "SELECT B.Emp_ID, convert(varchar, B.Timelogs_date, 107) AS DATE, CONVERT(varchar(15), CAST(B.TIMEIN AS TIME),100) AS TIMEIN, CONVERT(varchar(15),CAST(B.TIMEOUT AS TIME),100) AS TIMEOUT FROM (select A.Emp_ID, A.Timelogs_date, MIN(A.Timelogs_time) AS TIMEIN, CASE WHEN COUNT(A.Timelogs_time) > 1 THEN MAX(A.Timelogs_time) ELSE NULL END AS TIMEOUT from db_owner.Timelogs A left join db_owner.Employee B on A.Emp_ID = B.Emp_ID WHERE (A.Timelogs_date >= '" + date_from + "' AND '" + date_to + "' >= A.Timelogs_date)";

            if (dept_id != "0")
            {
                sql += " WHERE department_id =" + dept_id;
                sqlexport += " AND department_id =" + dept_id;
            }

            //if (position_id != "0" || position_id != "")
            //{
            //    sql += " AND position_id=" + position_id;
            //}

            sqlexport += "GROUP BY A.EMP_iD, A.Timelogs_date) B";


            //For Generate DTR Table
            objCommon.LoadDataTable(sql, dt);

            if (dt.Rows.Count != 0)
            {
                dgGenerateDTR.DataSource = dt;
                dgGenerateDTR.DataBind();
            }
            else
            {
                dgGenerateDTR.Visible = false;
                lblNoData.Visible = true;
            }

            Session["dt"] = dt;

            //For Export DTR 
            objCommon.LoadDataTable(sqlexport, dtexport);

            if (dtexport.Rows.Count != 0)
            {
                dgDownload.DataSource = dtexport;
                dgDownload.DataBind();
            }
           

        }

        public void itemcommand(object sender, CommandEventArgs c)
        {
            //string payroll_id = Session["payroll_id"].ToString();
            //string emp_id = Session["emp_id"].ToString();

            string date_from = txtDateFrom.Value.Trim();
            string date_to = txtDateTo.Value.Trim();

            LinkButton btn = (LinkButton)(sender);
            string emp_id = btn.CommandArgument;

            ClientScript.RegisterStartupScript(this.GetType(), "key", "openModalViewDTR();", true);

            objCommon.LoadDataTableView(int.Parse(emp_id), date_from, date_to, dtTimelogs);

            if (dtTimelogs.Rows.Count != 0)
            {
                dgTimeRecordTable.DataSource = dtTimelogs;
                dgTimeRecordTable.DataBind();
            }
            else
            {
                dgTimeRecordTable.Visible = false;
                lblNoTimeRecord.Visible = true;
            }

        }


        //Download Excel
        protected void btnExportToExcelModal_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=ExportDTR.xls");
            Response.ContentType = "application/vnd.xls";
            StringWriter stringwriter = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(stringwriter);
            dgDownload.RenderControl(htw);
            Response.Write(stringwriter.ToString());
            Response.End();
        }


        //Pagination

     

    }
}