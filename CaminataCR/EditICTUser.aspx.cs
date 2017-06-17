using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditICTUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void editUser(object sender, EventArgs e)
    {
        string oldUserName = (string)Session["editUser"];
        string newUserName = administrator.Text;
        UsersBusiness business = new UsersBusiness();
        string error = business.editUser(newUserName, oldUserName);
        if (error == null)
        {
            Response.Redirect("UserICT.aspx");
        }
        else
        {
            Errors.Text = error;
        }


    }

    public void deleteUser(object sender, EventArgs e)
    {
        string userName = (string)Session["editUser"];
        UsersBusiness business = new UsersBusiness();
        business.deleteUser(userName);
        Response.Redirect("UserICT.aspx");
    }
}