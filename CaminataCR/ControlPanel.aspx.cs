using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ControlPanel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["REG_USER"] == null)
        {
            Response.Redirect("SignInRegular.aspx");
        }
        if (!IsPostBack)
        {
            RegularUser regularUser = (RegularUser)Session["REG_USER"];
            LoggedInUsername.Text = regularUser.Account;
            if (regularUser.ProfilePicture == null)
            {
                //ImageUser.Attributes["src"] = "/css/images/defaultThumb.png";
            }
            else
            {
                //ImageUser.Attributes["src"] = "data:Image/png;base64," + Convert.ToBase64String(regularUser.ProfilePicture);
            }
        }
    }
}