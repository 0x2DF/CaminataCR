using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditPoint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["REG_USER"] == null)
        {
            Response.Redirect("SignInRegular.aspx");
        }
        else if (Session["HIKE"] == null)
        {
            Response.Redirect("HikeLobby.aspx");
        }else if (Session["POINT"] == null)
        {
            Response.Redirect("ExistingRoute.aspx");
        }
        if (!IsPostBack)
        {
            RegularUser regularUser = (RegularUser)Session["REG_USER"];
            LoggedInUsername.Text = regularUser.Account;
        }
    }
    protected void Edit(object sender, EventArgs e)
    {
        Hike hike = (Hike)Session["HIKE"];
        
        Point point = (Point)Session["POINT"];

        foreach(Point p in hike.Route.ListOfPoints)
        {
            if(p.PointId == point.PointId)
            {
                p.Comment = tb_add_commentary.Text;
                if (imageupload.HasFile)
                {
                    p.Image = imageupload.FileBytes;
                }
                else
                {
                    p.Image = null;
                }
            }
        }
        Session["POINT"] = null;
        Session["HIKE"] = hike;
        Response.Redirect("ExistingRoute.aspx");
    }
}