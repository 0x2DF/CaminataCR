using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PriceLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PriceLevelBusiness business = new PriceLevelBusiness();

            List<string> priceLevelList = business.getPriceLevels();
            showPriceLevels(priceLevelList);
            Session["PriceLeveslList"] = priceLevelList;
        }
        else
        {
            List<string> priceLevelList = (List<string>)Session["PriceLeveslList"];
            showPriceLevels(priceLevelList);
        }
    }

    protected void showPriceLevels(List<string> priceList)
    {
        priceLevelS.InnerHtml = "";

        foreach (string walkTypeName in priceList)
        {
            System.Web.UI.WebControls.Table tableInfo = new System.Web.UI.WebControls.Table();
            tableInfo.Attributes["class"] = "table table-striped table-bordered table-hover";

            TableRow tableRow = new TableRow();
            TableCell tableCell1 = new TableCell();
            tableCell1.Attributes["class"] = "col-md-3 text-center";
            TableCell tableCell2 = new TableCell();
            tableCell2.Attributes["class"] = "col-md-3 text-center";

            Label labelWalkType = new Label();
            labelWalkType.Text = walkTypeName;

            tableCell1.Controls.Add(labelWalkType);
            tableRow.Cells.Add(tableCell1);

            tableInfo.Rows.Add(tableRow);

            tableCell2.Controls.Add(addButton(walkTypeName));
            tableRow.Cells.Add(tableCell2);

            priceLevelS.Controls.Add(tableInfo);

        }
    }

    public Button addButton(string walkType)
    {
        Button btn = new Button();
        btn.Attributes["class"] = "btn btn-outline btn-success";
        btn.Click += new System.EventHandler(editPriceLevel);
        btn.Text = "Editar";
        btn.ID = walkType;
        return btn;
    }

    public void editPriceLevel(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string price = btn.ID;
        Session["EditPrice"] = price;
        Response.Redirect("EditPriceLevel.aspx");
    }

    public void addPriceLevel(object sender, EventArgs e)
    {
        Response.Redirect("AddPriceLevel.aspx");
    }
}