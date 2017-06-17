using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StrategicInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void aply_Click(object sender, EventArgs e)
    {
        if (options.SelectedValue == "0") { Response.Redirect("usersForContributions.aspx"); }
        if (options.SelectedValue == "1") { Response.Redirect("RouteLikes.aspx"); }
        if (options.SelectedValue == "2") { Response.Redirect("ClassificationOfRoutes.aspx"); }
        if (options.SelectedValue == "3") { Response.Redirect("RemunerationsReport.aspx"); }
    }
}