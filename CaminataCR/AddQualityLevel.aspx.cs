using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddQualityLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreateQualityLevel(object sender, EventArgs e)
    {
        QualityLevelBusiness business = new QualityLevelBusiness();

        string newQualityLevel = quality.Text;
        int state = 1;

        string error = business.insertQualityLevel(newQualityLevel, state);
        if (error != null)
        {
            Errors.Text = error;
        }
        else
        {
            Response.Redirect("QualityLevel.aspx");
        }
    }
}