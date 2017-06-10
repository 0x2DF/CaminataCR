using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShowHike : System.Web.UI.Page
{
    protected string sJSON;
    protected string sJSON2;
    protected string sJSON3;
    protected int numMarcadores = 0;
    protected List<List<double>> listOfLong = new List<List<double>>();
    protected List<List<double>> listOfLat = new List<List<double>>();
    protected List<int> listOfLen = new List<int>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["REG_USER"] == null)
        {
            Response.Redirect("SignInRegular.aspx");
        }
        if (Session["HIKE"] == null)
        {
            Response.Redirect("HikeLobby.aspx");
        }
        if (!IsPostBack)
        {
            if (dd_init_hiketype.Items.Count == 0)
            {
                FillDropDownLists();
            }
            RegularUser regularUser = (RegularUser)Session["REG_USER"];
            LoggedInUsername.Text = regularUser.Account;
        }
        displayRoutes();
    }
    private void FillDropDownLists()
    {
        HikeBusiness hb = new HikeBusiness();
        List<string> HikeTypeList = hb.getHikeTypes();
        dd_init_hiketype.Items.Clear();
        foreach (string p in HikeTypeList)
        {
            dd_init_hiketype.Items.Add(p);
        }

        List<string> DifficultyLevelList = hb.getDifficultyLevels();
        dd_init_difficultylevel.Items.Clear();
        foreach (string p in DifficultyLevelList)
        {
            dd_init_difficultylevel.Items.Add(p);
        }
    }
    private void displayRoutes()
    {
        Hike hike = (Hike)Session["HIKE"];
        if (hike.ListOfRoutes != null)
        {
            hikeContainer.InnerHtml = "";
            hikeContainer.Controls.Add(DisplayHikeInfo(ref hike));

            listOfLong = new List<List<double>>();
            listOfLat = new List<List<double>>();
            listOfLen = new List<int>();

            List<double> startLong = new List<double>();
            List<double> startLat = new List<double>();
            startLong.Add(hike.Longitud);
            startLat.Add(hike.Latitud);
            listOfLong.Add(startLong);
            listOfLat.Add(startLat);
            listOfLen.Add(1);

            List<double> pointLong = new List<double>();
            List<double> pointtLat = new List<double>();

            int i = 1;
            int j;
            foreach (Route r in hike.ListOfRoutes)
            {
                hikeContainer.Controls.Add(addLinkButton(r, i));
                j = 0;
                foreach(Point p in r.ListOfPoints)
                {
                    pointLong.Add(p.Longitud);
                    pointtLat.Add(p.Latitud);
                    j++;
                }
                listOfLong.Add(pointLong);
                listOfLat.Add(pointtLat);
                listOfLen.Add(j);
                ++i;
            }
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            sJSON = oSerializer.Serialize(listOfLong);
            sJSON2 = oSerializer.Serialize(listOfLat);
            sJSON3 = oSerializer.Serialize(listOfLen);
            numMarcadores = listOfLen.Count();
        }
    }
    private System.Web.UI.HtmlControls.HtmlGenericControl DisplayHikeInfo(ref Hike hike)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl divContainer =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainer.Attributes["class"] = "panel panel-default";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerHeading =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerHeading.Attributes["class"] = "panel-heading";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerTitle =
                new System.Web.UI.HtmlControls.HtmlGenericControl("H4");
        divContainerTitle.Attributes["class"] = "panel-title";

        System.Web.UI.HtmlControls.HtmlGenericControl linkContainer =
                new System.Web.UI.HtmlControls.HtmlGenericControl("a");
        linkContainer.Attributes["data-toggle"] = "collapse";
        linkContainer.Attributes["data-parent"] = "#accordion_" + hike.HikeId.ToString();
        linkContainer.Attributes["href"] = "#collapse_Init";

        System.Web.UI.HtmlControls.HtmlGenericControl MapMarker =
                new System.Web.UI.HtmlControls.HtmlGenericControl("i");
        MapMarker.Attributes["class"] = "fa fa-flag-checkered fa-fw";

        Label labelPos = new Label();
        labelPos.Text = " Inicio";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerCollapse =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerCollapse.Attributes["class"] = "panel-collapse collapse";
        divContainerCollapse.Attributes["id"] = "collapse_Init";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerBody =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerBody.Attributes["class"] = "panel-body";

        System.Web.UI.HtmlControls.HtmlGenericControl NavTab =
                new System.Web.UI.HtmlControls.HtmlGenericControl("ul");
        NavTab.Attributes["class"] = "nav nav-tabs";

        System.Web.UI.HtmlControls.HtmlGenericControl NavListItemDetails =
                new System.Web.UI.HtmlControls.HtmlGenericControl("li");

        System.Web.UI.HtmlControls.HtmlGenericControl NavListItemLinkDetails =
                new System.Web.UI.HtmlControls.HtmlGenericControl("a");
        NavListItemLinkDetails.Attributes["data-toggle"] = "tab";
        NavListItemLinkDetails.Attributes["href"] = "#details_Init";

        System.Web.UI.HtmlControls.HtmlGenericControl DetailsMarker =
                new System.Web.UI.HtmlControls.HtmlGenericControl("i");
        DetailsMarker.Attributes["class"] = "fa fa-file-text-o fa-fw";

        Label labelTabDetails = new Label();
        labelTabDetails.Text = " Detalles";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabContent =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerTabContent.Attributes["class"] = "tab-content";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabPaneDetails =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerTabPaneDetails.Attributes["class"] = "tab-pane fade in active";
        divContainerTabPaneDetails.Attributes["id"] = "details_Init";

        Label labelName = new Label();
        labelName.Text = "Nombre del Lugar: " + hike.NameOfLocation;

        Label labelProvince = new Label();
        labelProvince.Text = "Provincia: " + hike.Province;

        Label labelCanton = new Label();
        labelCanton.Text = "Canton: " + hike.Canton;

        Label labelDistrict = new Label();
        labelDistrict.Text = "Distrito: " + hike.District;

        Label labelDetails = new Label();
        labelDetails.Text = "Detalles: " + hike.Details;

        Label labelLatitud = new Label();
        labelLatitud.Text = "Latitud: " + hike.Latitud.ToString();

        Label labelLongitud = new Label();
        labelLongitud.Text = "Longitud: " + hike.Longitud.ToString();

        System.Web.UI.HtmlControls.HtmlGenericControl breakLine1 =
                new System.Web.UI.HtmlControls.HtmlGenericControl("BR");
        System.Web.UI.HtmlControls.HtmlGenericControl breakLine2 =
                new System.Web.UI.HtmlControls.HtmlGenericControl("BR");
        System.Web.UI.HtmlControls.HtmlGenericControl breakLine3 =
                new System.Web.UI.HtmlControls.HtmlGenericControl("BR");
        System.Web.UI.HtmlControls.HtmlGenericControl breakLine4 =
                new System.Web.UI.HtmlControls.HtmlGenericControl("BR");
        System.Web.UI.HtmlControls.HtmlGenericControl breakLine5 =
                new System.Web.UI.HtmlControls.HtmlGenericControl("BR");
        System.Web.UI.HtmlControls.HtmlGenericControl breakLine6 =
                new System.Web.UI.HtmlControls.HtmlGenericControl("BR");
        

        //Structure
        linkContainer.Controls.Add(MapMarker);
        linkContainer.Controls.Add(labelPos);
        divContainerTitle.Controls.Add(linkContainer);
        divContainerHeading.Controls.Add(divContainerTitle);
        
        NavListItemLinkDetails.Controls.Add(DetailsMarker);
        NavListItemLinkDetails.Controls.Add(labelTabDetails);
        
        NavListItemDetails.Controls.Add(NavListItemLinkDetails);
        
        NavTab.Controls.Add(NavListItemDetails);

        divContainerTabPaneDetails.Controls.Add(labelName);
        divContainerTabPaneDetails.Controls.Add(breakLine1);
        divContainerTabPaneDetails.Controls.Add(labelProvince);
        divContainerTabPaneDetails.Controls.Add(breakLine2);
        divContainerTabPaneDetails.Controls.Add(labelCanton);
        divContainerTabPaneDetails.Controls.Add(breakLine3);
        divContainerTabPaneDetails.Controls.Add(labelDistrict);
        divContainerTabPaneDetails.Controls.Add(breakLine4);
        divContainerTabPaneDetails.Controls.Add(labelDetails);
        divContainerTabPaneDetails.Controls.Add(breakLine5);
        divContainerTabPaneDetails.Controls.Add(labelLatitud);
        divContainerTabPaneDetails.Controls.Add(breakLine6);
        divContainerTabPaneDetails.Controls.Add(labelLongitud);
        
        divContainerTabContent.Controls.Add(divContainerTabPaneDetails);

        divContainerBody.Controls.Add(NavTab);
        divContainerBody.Controls.Add(divContainerTabContent);
        divContainerCollapse.Controls.Add(divContainerBody);

        divContainer.Controls.Add(divContainerHeading);
        divContainer.Controls.Add(divContainerCollapse);
        return divContainer;
    }

    public LinkButton addLinkButton(Route r, int i)
    {
        LinkButton lbtn = new LinkButton();
        lbtn.Click += new System.EventHandler(selectRoute);
        lbtn.Text = "<i class=\"fa fa-circle-thin\"></i> " + i.ToString() +" | # Puntos: " + r.ListOfPoints.Count().ToString();
        lbtn.CssClass = "list-group-item";
        lbtn.ID = r.RouteId.ToString();
        return lbtn;
    }
    protected void selectRoute(object sender, EventArgs e)
    {
        HikeBusiness hb = new HikeBusiness();
        LinkButton lbtn = (LinkButton)sender;
        Hike hike = new Hike();
        hike.Route = new Route();
        hike.Route.RouteId = Int32.Parse(lbtn.ID);
        hike = hb.loadHike(hike);
        Session["HIKE"] = hike;
        Response.Redirect("ExistingRoute.aspx");
    }

    protected void InitRoute(object sender, EventArgs e)
    {
        Hike hike = (Hike)Session["HIKE"];
        hike.HikeType = dd_init_hiketype.SelectedItem.Text;
        hike.Difficulty = dd_init_difficultylevel.SelectedItem.Text;
        if (imageupload.HasFile)
        {
            hike.Image = imageupload.FileBytes;
        }
        else
        {
            hike.Image = null;
        }
        hike.ListOfRoutes = null;
        hike.SameHike = true;
        Session["HIKE"] = hike;
        Response.Redirect("NewHike.aspx");
    }
}