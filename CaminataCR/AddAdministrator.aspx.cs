using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddAdministrator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreateAdministrator(object sender, EventArgs e)
    {
        User user = new User();
        user.Account = administrator.Text;
        user.Password = password.Text;
        user.RoleId = 1;

        UsersBusiness business = new UsersBusiness();
        string error = business.addUser(user);
        if(error == null)
        {
            Response.Redirect("UserAdministrator.aspx");
        }
        else
        {
            Errors.Text = error;
        }
    }
}