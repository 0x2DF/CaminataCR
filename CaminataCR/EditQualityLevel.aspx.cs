using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditQualityLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void submitEditQualityLevel(object sender, EventArgs e)
    {
        QualityLevelBusiness business = new QualityLevelBusiness();

        string previousQualityLevel = (string)Session["EditQuality"];
        string newQualityLevel = qualitylevel.Text;
        bool activated = CheckBox1.Checked;
        int valueCheckBox;
        if (activated)
        {
            valueCheckBox = 1;
        }
        else
        {
            valueCheckBox = 0;
        }

        string error = business.editQualityLevel(previousQualityLevel, newQualityLevel, valueCheckBox);
        if (error != null)
        {
            Errors.Text = error;
        }
        else
        {
            Response.Redirect("QualityLevel.aspx");
        }

    }

    protected void deleteQualityLevel(object sender, EventArgs e)
    {
        QualityLevelBusiness business = new QualityLevelBusiness();
        string qualityLevelToDelete = (string)Session["EditQuality"]; ;
        business.deleteQualityLevel(qualityLevelToDelete);
        Response.Redirect("QualityLevel.aspx");
    }
}