using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SearchFriends : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["REG_USER"] == null)
        {
            Response.Redirect("SignInRegular.aspx");
        }

        if(IsPostBack)
        {
            RegularUser regularUser = (RegularUser)Session["REG_USER"];
            LoggedInUsername.Text = regularUser.Account;
            if(regularUser.ListOfFriends != null)
            {
                displayUsers(regularUser);
            }
        }
    }

    protected void SearchUser(object sender, EventArgs e)
    {
        FriendBusiness fb = new FriendBusiness();
        RegularUser regularUser = (RegularUser)Session["REG_USER"];
        fb.LoadListOfRegularUsers(ref regularUser, searchInput.Text);
        displayUsers(regularUser);

        Session["REG_USER"] = regularUser;
    }

    protected void displayUsers(RegularUser regularUser)
    {
        main.InnerHtml = "";
        foreach (var r in regularUser.ListOfFriends)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divContainer =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            divContainer.Attributes["class"] = "col-lg-3 col-md-6";

            System.Web.UI.HtmlControls.HtmlGenericControl divContainerPanel =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            divContainerPanel.Attributes["class"] = "panel panel-green";

            System.Web.UI.HtmlControls.HtmlGenericControl divContainerHeading =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            divContainerHeading.Attributes["class"] = "panel-heading";

            System.Web.UI.HtmlControls.HtmlGenericControl divContainerRow =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            divContainerRow.Attributes["class"] = "row";

            System.Web.UI.HtmlControls.HtmlGenericControl divContainerImage =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            divContainerImage.Attributes["class"] = "col-xs-12";
            
            System.Web.UI.HtmlControls.HtmlGenericControl divContainerFooter =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            divContainerFooter.Attributes["class"] = "panel-footer";

            System.Web.UI.HtmlControls.HtmlGenericControl divContainerClearFix =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            divContainerClearFix.Attributes["class"] = "clearfix";

            System.Web.UI.HtmlControls.HtmlGenericControl imgThumbnail =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("IMG");

            if (r.ProfilePicture == null)
            {
                imgThumbnail.Attributes["src"] = "/images/defaultThumb.png";
            }
            else
            {
                imgThumbnail.Attributes["src"] = "data:Image/png;base64," + Convert.ToBase64String(r.ProfilePicture);
            }
            imgThumbnail.Attributes["style"] = "margin: 0 auto; display: block; width: 70%; length: 70%;";

            
            System.Web.UI.HtmlControls.HtmlGenericControl spanText =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("SPAN");
            spanText.Attributes["class"] = "pull-left";
            spanText.InnerText = r.FirstName + " " + r.Surname;

            System.Web.UI.HtmlControls.HtmlGenericControl spanImage =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("SPAN");
            spanImage.Attributes["class"] = "pull-right";
            


            //Structure

            divContainerImage.Controls.Add(imgThumbnail);
            divContainerRow.Controls.Add(divContainerImage);
            divContainerHeading.Controls.Add(divContainerRow);
            
            spanImage.Controls.Add(addLinkButton(r.UserId));

            divContainerFooter.Controls.Add(spanText);
            divContainerFooter.Controls.Add(spanImage);
            divContainerFooter.Controls.Add(divContainerClearFix);

            divContainerPanel.Controls.Add(divContainerHeading);
            divContainerPanel.Controls.Add(divContainerFooter);
            divContainer.Controls.Add(divContainerPanel);
            main.Controls.Add(divContainer);
        }
    }

    public LinkButton addLinkButton(int id)
    {
        LinkButton lbtn = new LinkButton();
        lbtn.Click += new System.EventHandler(addFriend);
        lbtn.Text = "<i class=\"fa fa-plus\"></i>";
        lbtn.CssClass = "btn btn-default";
        lbtn.ID = id.ToString();
        return lbtn;
    }

    protected void addFriend(object sender, EventArgs e)
    {
        FriendBusiness fb = new FriendBusiness();
        RegularUser regularUser = (RegularUser)Session["REG_USER"];
        RegularUser friend = new RegularUser();

        LinkButton lbtn = (LinkButton)sender;
        friend.UserId = Int32.Parse(lbtn.ID);

        Tuple<int, string> t = new Tuple<int, string>(0,"");
        t = fb.InsertFriend(ref regularUser, ref friend);

        Notification n = new Notification();
        n.Type = t.Item1;
        n.Message = t.Item2;
        Session["NOTIFICATION"] = n;
        Response.Redirect("Notifications.aspx");
    }

}