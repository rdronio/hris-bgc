using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;
using System.Data;
using System.Text;
using System.Collections.ObjectModel;
using NativeWifi;
using System.Net.NetworkInformation;

namespace HRIS_Basic
{
    public partial class LoginPage : System.Web.UI.Page
    {
        Common objCommon = new Common();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                Session.Abandon();
            }   
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {

            string sUsername = txtUsername.Value.Trim();
            string sPassword = txtPass.Value.Trim();

            objCommon.LogInPersist(sUsername, sPassword, dt);

            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["Emp_pass"].ToString() != "12345")
                {
                   
                            Session["Employee_ID"] = dt.Rows[0]["Emp_ID"];
                            Session["Username"] = dt.Rows[0]["Emp_email"];
                            Response.Write("<script>alert('Success!');</script>");
                            Response.Redirect("Home.aspx");

                }
                else
                {
                Session["Emp_email"] = dt.Rows[0]["Emp_email"];
                Response.Redirect("ChangePassword.aspx");
                //lblChangePassword.Attributes.Add("class", "warning");
                //lblInvalid.Attributes.Add("class", "warning hidden");
                //return;
                }
            }
            
            else if (sUsername == "hradmin" && sPassword == "admin")
            {
                    Session["Username"] = "hradmin";
                    Response.Redirect("DashboardAdmin.aspx");
            }
            else if (sUsername == "hrpayroll" && sPassword == "admin")
            {
                Session["Username"] = "hradmin";
                Response.Redirect("Payroll.aspx");
            }
            else
            {
                lblInvalid.Attributes.Add("class", "notif notif-red");
                lblChangePassword.Attributes.Add("class", "notif notif-red hidden");
                return;
            }
        }

        protected void btnClickHere_Click(object sender, EventArgs e)
        {
            Session["Emp_email"] = txtUsername.Value;
            Response.Redirect("ChangePassword.aspx");
        }
            
    }
}
