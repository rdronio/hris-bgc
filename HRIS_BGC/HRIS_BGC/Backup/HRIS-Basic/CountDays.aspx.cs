using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRIS_Basic
{
    public partial class CountDays : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            string fromDate = txtDateFrom.Value.Trim();
            string toDate = txtDateTo.Value.Trim();


            List<DateTime> allDates = new List<DateTime>();


            for (DateTime date = DateTime.Parse(fromDate); date <= DateTime.Parse(toDate); date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    allDates.Add(date);
                }
            }

            int numberOfDays = allDates.Count;

        }
    }
}