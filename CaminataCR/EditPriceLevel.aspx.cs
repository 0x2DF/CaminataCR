using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditPriceLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void submitEditPriceLevel(object sender, EventArgs e)
    {
        PriceLevelBusiness business = new PriceLevelBusiness();

        string previousPriceLevel = (string)Session["EditPrice"];
        string newPriceLevel = pricelevel.Text;
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

        string error = business.editPriceLevel(previousPriceLevel, newPriceLevel, valueCheckBox);
        if (error != null)
        {
            Errors.Text = error;
        }
        else
        {
            Response.Redirect("PriceLevel.aspx");
        }

    }

    protected void deletePriceLevel(object sender, EventArgs e)
    {
        PriceLevelBusiness business = new PriceLevelBusiness();
        string priceLevelToDelete = (string)Session["EditPrice"]; ;
        business.deletePriceLevel(priceLevelToDelete);
        Response.Redirect("PriceLevel.aspx");
    }
}