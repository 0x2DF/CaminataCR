using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddICTUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreateICTUser(object sender, EventArgs e)
    {
        User user = new User();
        user.Account = administrator.Text;
        user.Password = password.Text;
        user.RoleId = 2;

        UsersBusiness business = new UsersBusiness();
        string error = business.addUser(user);
        if (error == null)
        {
            Response.Redirect("UserICT.aspx");
        }
        else
        {
            Errors.Text = error;
        }
    }
}