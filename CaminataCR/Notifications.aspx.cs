using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Notifications : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["REG_USER"] == null)
        {
            Response.Redirect("SignInRegular.aspx");
        }
        if (Session["NOTIFICATION"] == null)
        {
            Response.Redirect("Wall.aspx");
        }
        else
        { 
            RegularUser regularUser = (RegularUser)Session["REG_USER"];
            Notification notification = (Notification)Session["NOTIFICATION"];

            LoggedInUsername.Text = regularUser.Account;
            AddMessageText(notification);
        }
    }

    public void AddMessageText(Notification notification)
    {
        heading.InnerHtml = "";
        System.Web.UI.HtmlControls.HtmlGenericControl divContainerMessage =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        switch(notification.Type)
        {
            case 1:
                divContainerMessage.Attributes["class"] = "alert alert-success";
                break;
            case 2:
                divContainerMessage.Attributes["class"] = "alert alert-danger";
                break;
            case 3:
                divContainerMessage.Attributes["class"] = "alert alert-info";
                break;
            default:
                divContainerMessage.Attributes["class"] = "alert alert-warning";
                break;
        }
        

        Label labelMessage = new Label();
        labelMessage.Text = notification.Message;

        divContainerMessage.Controls.Add(labelMessage);
        heading.Controls.Add(divContainerMessage);

        Session["NOTIFICATION"] = null;
    }
}