using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addDificultyLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreateWalkType(object sender, EventArgs e)
    {
        DificultyLevelBusiness business = new DificultyLevelBusiness();

        string newDificultyLevel = dificulty.Text;
        int state = 1;

        string error = business.insertDificultyLevel(newDificultyLevel, state);
        if (error != null)
        {
            Errors.Text = error;
        }
        else
        {
            Response.Redirect("DificultyLevel.aspx");
        }
    }
}