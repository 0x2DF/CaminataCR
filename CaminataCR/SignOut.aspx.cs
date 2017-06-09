using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SignOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USER"] != null)
        {
            Session["USER"] = null;
            Response.Redirect("SignInAdministrator.aspx");
        }

        if (Session["REG_USER"] != null)
        {
            Session["REG_USER"] = null;

            if (Session["HIKE"] != null)
            {
                Session["HIKE"] = null;
                Response.Redirect("SignInRegular.aspx");
            }

            Response.Redirect("SignInRegular.aspx");
        }

        //ICT SESSION

        //ELSE
        Response.Redirect("SignInRegular.aspx");
    }
}