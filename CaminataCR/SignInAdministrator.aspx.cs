using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

public partial class SignInAdministrator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["REG_USER"] != null)
        {
            //Try to log in with session details;

            Response.Redirect("muro.aspx");
        }
    }

    protected void SignIn(object sender, EventArgs e)
    {
        LoginBusiness lb = new LoginBusiness();
        User user = new User();
        user.Account = username.Text;
        user.Password = password.Text;
        user.RoleId = 3;

        string errorText = lb.ValidateCredentials(user);

        if (errorText == null)
        {
            RegularUser regularUser = lb.GetRegularUser(user);
            
            Session["REG_USER"] = regularUser;
            Response.Redirect("muro.aspx");

        }
        else
        {
            Errores.Text = errorText;
        }

    }
}