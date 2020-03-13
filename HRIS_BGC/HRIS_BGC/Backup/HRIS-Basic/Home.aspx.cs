using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;
using System.Data;
using System.Text;
using System.Globalization;

namespace HRIS_Basic
{
    public partial class Home : System.Web.UI.Page
    {
        //string date = DateTime.Now.ToString("yyyy-MM-dd");
        string date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("yyyy-MM-dd");
        string time = DateTime.Now.ToString("HH:mm");
        string pacific = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("HH:mm");
        Timelogs timelogs = new Timelogs();
        DataTable dt = new DataTable();
        DataTable dtr = new DataTable();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] == null)
                {
                    Response.Redirect("LoginPage.aspx");
                }
            }

            int empid = int.Parse(Session["Employee_ID"].ToString());

           //Time In ----
           timelogs.IsTimeIn(empid, dt);

           if (dt.Rows.Count != 0)
           {
               btnIn.Attributes.Add("class", "btn btn-green disabled");

               string input = dt.Rows[0]["Timelogs_time"].ToString();

               var timeFromInput = DateTime.ParseExact(input, "HH:mm:ss", null, DateTimeStyles.None);

               string timeIn12HourFormatForDisplay = timeFromInput.ToString(
                   "hh:mm tt",
                   CultureInfo.InvariantCulture);

               lblTimeIn2.InnerHtml = timeIn12HourFormatForDisplay.ToString();

               //Tardiness
               TimeSpan tardiness = TimeSpan.Parse(input).Subtract(new TimeSpan(8, 30, 0));

                   double tadinesshr = tardiness.Hours;
                   double tardinessmin = tardiness.Minutes;

                   double tardinesstotalmin = (tadinesshr * 60) + tardinessmin;

                   //To validate the negative value
                   if (tardinesstotalmin > 0)
                   {


                       lblTardiness.InnerHtml = String.Format("{0:n}", Decimal.Parse(tardinesstotalmin.ToString())) + " min/s";
                   }
               

              //Computation of total hours without timeout
          
               TimeSpan ts = DateTime.Parse(DateTime.Now.ToString("HH:mm")).Subtract(DateTime.Parse(input));
               //TimeSpan ts = (new TimeSpan(19, 35, 0)).Subtract(TimeSpan.Parse(input)); Sample time data for total hours

               double hr = ts.Hours;
               double min = ts.Minutes;

               double totalminutes = (hr * 60) + min;

               double totalhours = totalminutes / 60;

               //If the time is 5:00pm onwards
               if (TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) >= new TimeSpan(17, 0, 0))
               {
                   //Overtime
                   TimeSpan overtimets = TimeSpan.Parse(DateTime.Now.ToString("HH:mm")).Subtract(new TimeSpan(17, 0, 0));
                   //TimeSpan overtimets = (new TimeSpan(19, 35, 0)).Subtract(new TimeSpan(17, 0, 0)); Sample time data for overtime
                   double overtimehr = overtimets.Hours;
                   double overtimemin = overtimets.Minutes;

                   double totalovertimemin = (overtimehr * 60) + overtimemin;
                   double totalovertimehour = totalovertimemin / 60;

                   totalovertimehour = Math.Round(totalovertimehour, 2);

                   totalhours = Math.Round(totalhours - totalovertimehour, 2);
               }
               else
               {    //if the is 1pm onwards subtract one hour for the lunch time
                   if (TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) >= new TimeSpan(13, 0, 0))
                   {
                       totalhours = Math.Round(totalhours - 1, 2);
                   }
                   else //no need to subtract one hour
                   {
                       totalhours = Math.Round(totalhours, 2);
                   }
               }

               Session["totaltime"] = totalhours;
               lblTotal2.InnerHtml = "0.00"; //Total hours label
               Session["Timein"] = timeIn12HourFormatForDisplay;
           }
           else
           {
               btnOut.Attributes.Add("class", "btn btn-red disabled");
           }

           //Time Out -------------------------------------------------------
           timelogs.IsTimeOut(empid, dtr);


           if (dtr.Rows.Count != 0)
           {
               string timeout = dtr.Rows[0]["Timelogs_time"].ToString();

               if (timeout != "")
               {
                   btnOut.Attributes.Add("class", "btn btn-red disabled");
                   string input = timeout;

                   var timeFromInput = DateTime.ParseExact(input, "HH:mm:ss", null, DateTimeStyles.None);

                   string timeIn12HourFormatForDisplay = timeFromInput.ToString(
                       "hh:mm tt",
                       CultureInfo.InvariantCulture);

                   lblTimeOut2.InnerHtml = timeIn12HourFormatForDisplay.ToString();

                   //Computation of total hours with timeout
                   TimeSpan ts = DateTime.Parse(input).Subtract(DateTime.Parse(Session["Timein"].ToString()));
                   double hr = ts.Hours;
                   double min = ts.Minutes;

                   double totalminutes = (hr * 60) + min;

                   double totalhours = totalminutes / 60;

                   double totalovertimehour = 0;
                   //If the time is 5:00pm onwards
                   if (TimeSpan.Parse(input) >= new TimeSpan(17, 0, 0))
                   {
                       //Overtime
                       TimeSpan overtimets = TimeSpan.Parse(input).Subtract(new TimeSpan(17, 0, 0));
                       double overtimehr = overtimets.Hours;
                       double overtimemin = overtimets.Minutes;

                       double totalovertimemin = (overtimehr * 60) + overtimemin;
                       totalovertimehour = totalovertimemin / 60;

                       totalovertimehour = Math.Round(totalovertimehour, 2);
                   }

                   lblOvertime2.InnerHtml = String.Format("{0:n}", Decimal.Parse(totalovertimehour.ToString())) + " hour/s"; //Total overtime hours label


                   totalhours = Math.Round(totalhours - totalovertimehour, 2); //1 is for one hour lunch time

                   Session["totaltime"] = totalhours;
                   lblTotal2.InnerHtml = String.Format("{0:n}", Decimal.Parse(totalhours.ToString())) + " hour/s"; //Total hours label
                   Session["Timein"] = timeIn12HourFormatForDisplay;
               }
           }
        }

        protected void btnTimeIn_Click(object sender, EventArgs e)
        {
            string timein = pacific;
            string type = "1";
            int empid = int.Parse(Session["Employee_ID"].ToString());

            timelogs.InsertTimeInOut(empid, date, timein, type);
            Response.Redirect("Home.aspx");
        }

        protected void btnTimeOut_Click(object sender, EventArgs e)
        {
            string timeout = pacific;
            string type = "0";
            int empid = int.Parse(Session["Employee_ID"].ToString());

            timelogs.InsertTimeInOut(empid, date, timeout, type);
            Response.Redirect("Home.aspx");
        }
    }
}