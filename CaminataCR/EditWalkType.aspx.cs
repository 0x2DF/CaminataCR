using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditWalkType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void submitEditWalkType(object sender, EventArgs e)
    {
        WalkTypeBusiness business = new WalkTypeBusiness();

        string previousWalkType = (string)Session["EditWalk"];
        string newWalkType = walkType.Text;
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

        string error = business.editWalkType(previousWalkType, newWalkType, valueCheckBox);
        if (error != null)
        {
            Errors.Text = error;
        }
        else
        {
            Response.Redirect("WalkType.aspx");
        }
        
    }

    protected void deleteWalkType(object sender, EventArgs e)
    {
        WalkTypeBusiness business = new WalkTypeBusiness();
        string walkTypeToDelete = (string)Session["EditWalk"]; ;
        business.deleteWalkType(walkTypeToDelete);
        Response.Redirect("WalkType.aspx");
    }

    
}