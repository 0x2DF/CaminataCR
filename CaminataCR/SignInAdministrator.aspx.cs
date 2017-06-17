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
        
    }

    protected void SignIn(object sender, EventArgs e)
    {
        LoginBusiness lb = new LoginBusiness();
        User user = new User();
        int role = 1;
        user.Account = username.Text;
        user.Password = password.Text;
        user.RoleId = role;

        string errorText = lb.ValidateCredentials(user);

        if (errorText == null)
        {           

            Session["ADMIN"] = user.Account;
            Response.Redirect("CatalogMaintenance.aspx");

        }
        else
        {
            Errors.Text = errorText;
        }
    }
}