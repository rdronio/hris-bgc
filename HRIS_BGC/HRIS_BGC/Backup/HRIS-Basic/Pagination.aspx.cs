using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;
using System.Data;
using System.Collections;
using System.Drawing;

namespace HRIS_Basic
{
    public partial class Pagination : System.Web.UI.Page
    {
        Common objCommon = new Common();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindRepeater();
        }

        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                {
                    return Convert.ToInt32(ViewState["PageNumber"]);
                }
                else
                {
                    return 0;
                }
            }
            set { ViewState["PageNumber"] = value; }
        }

        private void BindRepeater()
        {
            string sql = "Select * from db_owner.Timelogs";
            objCommon.LoadDataTable(sql, dt);
            
            //Create the PagedDataSource that will be used in paging
            PagedDataSource pgitems = new PagedDataSource();
            pgitems.DataSource = dt.DefaultView;
            pgitems.AllowPaging = true;

            //Control page size from here 
            pgitems.PageSize = 4;
            pgitems.CurrentPageIndex = PageNumber;
          
            
            if (pgitems.PageCount > 1)
            {
                rptPaging.Visible = true;
                ArrayList pages = new ArrayList();
                for (int i = 0; i <= pgitems.PageCount - 1; i++)
                {
                    if (i == PageNumber)
                    {
                        pages.Add("<span class='active'>" + (i + 1).ToString() + "</span>");

                       
                    }
                    else
                    {
                        pages.Add("<span>" + (i + 1).ToString() + "</span>");
                    }
                }
                rptPaging.DataSource = pages;
                rptPaging.DataBind();
            }
            else
            {
                rptPaging.Visible = false;
            }

            //Finally, set the datasource of the repeater
            dgTimeLogs.DataSource = pgitems;
            dgTimeLogs.DataBind();
        }

        //This method will fire when clicking on the page no link from the pager repeater
        protected void rptPaging_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            string com = e.CommandArgument.ToString();
            com = com.Replace("<span>", "");
            com = com.Replace("</span>", "");
            PageNumber = Convert.ToInt32(com) - 1;
            BindRepeater();

            string url = "Pagination.aspx?Page=" + PageNumber;
            string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

         
        }
    }    
}