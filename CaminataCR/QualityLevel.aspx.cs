using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QualityLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            QualityLevelBusiness business = new QualityLevelBusiness();

            List<string> qualityLevelList = business.getQualityLevels();
            showQualityLevels(qualityLevelList);
            Session["QualityLeveslList"] = qualityLevelList;
        }
        else
        {
            List<string> qualityLevelList = (List<string>)Session["QualityLeveslList"];
            showQualityLevels(qualityLevelList);
        }
    }

    protected void showQualityLevels(List<string> qualityList)
    {
        qualityLevelS.InnerHtml = "";

        foreach (string walkTypeName in qualityList)
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

            qualityLevelS.Controls.Add(tableInfo);

        }
    }

    public Button addButton(string walkType)
    {
        Button btn = new Button();
        btn.Attributes["class"] = "btn btn-outline btn-success";
        btn.Click += new System.EventHandler(editQualityLevel);
        btn.Text = "Editar";
        btn.ID = walkType;
        return btn;
    }

    public void editQualityLevel(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string quality = btn.ID;
        Session["EditQuality"] = quality;
        Response.Redirect("EditQualityLevel.aspx");
    }

    public void addQualityLevel(object sender, EventArgs e)
    {
        Response.Redirect("AddQualityLevel.aspx");
    }
}