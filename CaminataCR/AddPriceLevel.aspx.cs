using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddPriceLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreatePriceLevel(object sender, EventArgs e)
    {
        PriceLevelBusiness business = new PriceLevelBusiness();

        string newPriceLevel = price.Text;
        int state = 1;

        string error = business.insertPriceLevel(newPriceLevel, state);
        if (error != null)
        {
            Errors.Text = error;
        }
        else
        {
            Response.Redirect("PriceLevel.aspx");
        }
    }
}