using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddWalkType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreateWalkType(object sender, EventArgs e)
    {
        WalkTypeBusiness business = new WalkTypeBusiness();

        string newWalkType = walkType.Text;

        string error = business.insertWalkType(newWalkType);
        if (error != null)
        {
            Errors.Text = error;
        }
        else
        {
            Response.Redirect("Walktype.aspx");
        }
        
    }
}