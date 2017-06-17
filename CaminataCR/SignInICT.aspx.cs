using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SignInICT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SignIn(object sender, EventArgs e)
    {
        signInICT temp = new signInICT();
        User user = new User();
        int role = 2;
        user.Account = username.Text;
        user.Password = password.Text;
        user.RoleId = role;

        string errorText = temp.ValidateCredentials(user);

        if (errorText == null)
        {

            Session["ICT"] = user.Account;
            Response.Redirect("StrategicInformation.aspx");

        }
        else
        {
           
        }

    }
}