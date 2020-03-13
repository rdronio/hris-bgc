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
    public partial class AutoRefresh : System.Web.UI.Page
    {
        Common objCommon = new Common();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = "select * from dbo.SampleLogs order by Timelogs_ID desc";
            objCommon.LoadDataTable(sql, dt);

            Label1.Text = dt.Rows[0]["Timelogs_ID"].ToString();
        }
    }
}