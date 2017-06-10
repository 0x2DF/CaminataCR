using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditDificultyLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void submitEditDificultyLevel(object sender, EventArgs e)
    {
        DificultyLevelBusiness business = new DificultyLevelBusiness();

        string previousDificultyLevel = (string)Session["EditDificulty"];
        string newDificultyLevel = dificultylevel.Text;
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

        string error = business.editDificultyLevel(previousDificultyLevel, newDificultyLevel, valueCheckBox);
        if (error != null)
        {
            Errors.Text = error;
        }
        else
        {
            Response.Redirect("DificultyLevel.aspx");
        }

    }

    protected void deleteDificultyLevel(object sender, EventArgs e)
    {
        DificultyLevelBusiness business = new DificultyLevelBusiness();
        string dificultyLevelToDelete = (string)Session["EditDificulty"]; ;
        business.deleteDificultyLevel(dificultyLevelToDelete);
        Response.Redirect("DificultyLevel.aspx");
    }
}