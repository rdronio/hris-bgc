using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;

namespace HRIS_Basic
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        Common objCommon = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {   
            
        }

        protected void btnchangepass_Click(object sender, EventArgs e)
        {
            string password = txtPass.Value.Trim();
            string confirmPassword = txtPass2.Value.Trim();
            string email = Session["Emp_email"].ToString(); ;
            objCommon.ChangePassword(email, password);
            ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>alert('Successfully changed password!');window.location='LoginPage.aspx';</script>'");
        }
    }
}