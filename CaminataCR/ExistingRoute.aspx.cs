using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class ExistingRoute : System.Web.UI.Page
{
    protected string sJSON;
    protected string sJSON2;
    protected int numMarcadores = 0;
    protected List<double> listOfLong = new List<double>();
    protected List<double> listOfLat = new List<double>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["REG_USER"] == null)
        {
            Response.Redirect("SignInRegular.aspx");
        }
        else if (Session["HIKE"] == null)
        {
            Response.Redirect("HikeLobby.aspx");
        }
        if (!IsPostBack)
        {
            if (dd_qualitylevel.Items.Count == 0)
            {
                FillDropDownLists();
            }
            RegularUser regularUser = (RegularUser)Session["REG_USER"];
            LoggedInUsername.Text = regularUser.Account;
        }
        DisplayRoute();
    }
    private void FillDropDownLists()
    {
        HikeBusiness hb = new HikeBusiness();

        List<string> HikeTypeList = hb.getHikeTypes();
        dd_hiketype.Items.Clear();
        foreach (string p in HikeTypeList)
        {
            dd_hiketype.Items.Add(p);
        }

        List<string> DifficultyLevelList = hb.getDifficultyLevels();
        dd_difficultylevel.Items.Clear();
        foreach (string p in DifficultyLevelList)
        {
            dd_difficultylevel.Items.Add(p);
        }

        List<string> QualityLevelList = hb.getQualityLevels();
        dd_qualitylevel.Items.Clear();
        foreach (string p in QualityLevelList)
        {
            dd_qualitylevel.Items.Add(p);
        }

        List<string> PriceLevelList = hb.getPriceLevels();
        dd_pricelevel.Items.Clear();
        foreach (string p in PriceLevelList)
        {
            dd_pricelevel.Items.Add(p);
        }
    }

    private void DisplayRoute()
    {
        displayPointsInMap();
        accordion.InnerHtml = "";
        Hike hike = (Hike)Session["HIKE"];
        DisplayStart(hike);
        if (hike.Route.ListOfPoints != null)
        {
            List<Point> SortedList = hike.Route.ListOfPoints.OrderBy(o => o.Pos).ToList();

            int i = 0;
            foreach (Point p in SortedList)
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
                linkContainer.Attributes["data-parent"] = "#accordion";
                linkContainer.Attributes["href"] = "#collapse_" + i.ToString();

                System.Web.UI.HtmlControls.HtmlGenericControl MapMarker =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("i");
                MapMarker.Attributes["class"] = "fa fa-map-marker fa-fw";

                Label labelPos = new Label();
                labelPos.Text = (p.Pos + 1).ToString();



                System.Web.UI.HtmlControls.HtmlGenericControl divContainerCollapse =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                divContainerCollapse.Attributes["class"] = "panel-collapse collapse";
                divContainerCollapse.Attributes["id"] = "collapse_" + i.ToString();

                System.Web.UI.HtmlControls.HtmlGenericControl divContainerBody =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                divContainerBody.Attributes["class"] = "panel-body";

                System.Web.UI.HtmlControls.HtmlGenericControl NavTab =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("ul");
                NavTab.Attributes["class"] = "nav nav-tabs";

                System.Web.UI.HtmlControls.HtmlGenericControl NavListItemImage =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("li");
                NavListItemImage.Attributes["class"] = "active";

                System.Web.UI.HtmlControls.HtmlGenericControl NavListItemLinkImage =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("a");
                NavListItemLinkImage.Attributes["data-toggle"] = "tab";
                NavListItemLinkImage.Attributes["href"] = "#image_" + i.ToString();

                System.Web.UI.HtmlControls.HtmlGenericControl NavListItemDetails =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("li");

                System.Web.UI.HtmlControls.HtmlGenericControl NavListItemLinkDetails =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("a");
                NavListItemLinkDetails.Attributes["data-toggle"] = "tab";
                NavListItemLinkDetails.Attributes["href"] = "#details_" + i.ToString();

                System.Web.UI.HtmlControls.HtmlGenericControl NavListItemComment =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("li");

                System.Web.UI.HtmlControls.HtmlGenericControl NavListItemLinkComment =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("a");
                NavListItemLinkComment.Attributes["data-toggle"] = "tab";
                NavListItemLinkComment.Attributes["href"] = "#comment_" + i.ToString();

                System.Web.UI.HtmlControls.HtmlGenericControl ImageMarker =
                new System.Web.UI.HtmlControls.HtmlGenericControl("i");
                ImageMarker.Attributes["class"] = "fa fa-camera fa-fw";

                System.Web.UI.HtmlControls.HtmlGenericControl DetailsMarker =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("i");
                DetailsMarker.Attributes["class"] = "fa fa-file-text-o fa-fw";

                System.Web.UI.HtmlControls.HtmlGenericControl CommentMarker =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("i");
                CommentMarker.Attributes["class"] = "fa fa-comment-o fa-fw";

                Label labelTabImage = new Label();
                labelTabImage.Text = " Imagen";

                Label labelTabDetails = new Label();
                labelTabDetails.Text = " Detalles";

                Label labelTabComment = new Label();
                labelTabComment.Text = " Comentario";

                System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabContent =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                divContainerTabContent.Attributes["class"] = "tab-content";

                System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabPaneImage =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                divContainerTabPaneImage.Attributes["class"] = "tab-pane fade in active";
                divContainerTabPaneImage.Attributes["id"] = "image_" + i.ToString();

                System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabPaneDetails =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                divContainerTabPaneDetails.Attributes["class"] = "tab-pane fade";
                divContainerTabPaneDetails.Attributes["id"] = "details_" + i.ToString();

                System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabPaneComment =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                divContainerTabPaneComment.Attributes["class"] = "tab-pane fade";
                divContainerTabPaneComment.Attributes["id"] = "comment_" + i.ToString();

                Label labelLatitud = new Label();
                labelLatitud.Text = "Latitud: " + p.Latitud.ToString();

                Label labelLongitud = new Label();
                labelLongitud.Text = "Longitud: " + p.Longitud.ToString();

                Label labelComment = new Label();
                labelComment.Text = p.Comment;

                System.Web.UI.HtmlControls.HtmlGenericControl breakLine =
                new System.Web.UI.HtmlControls.HtmlGenericControl("BR");

                System.Web.UI.HtmlControls.HtmlGenericControl imgThumbnail =
                            new System.Web.UI.HtmlControls.HtmlGenericControl("IMG");
                if (p.Image == null)
                {
                    imgThumbnail.Attributes["src"] = "/images/defaultThumb.png";
                }
                else
                {
                    imgThumbnail.Attributes["src"] = "data:Image/png;base64," + Convert.ToBase64String(p.Image);
                }
                imgThumbnail.Attributes["style"] = "margin: 0 auto; display: block; width: 70%; length: 70%;";
                //imgThumbnail.Attributes["style"] = "display: block; max-width:75px; max-height:100px; width: auto; height: auto;";

                //Structure

                linkContainer.Controls.Add(MapMarker);
                linkContainer.Controls.Add(labelPos);
                divContainerTitle.Controls.Add(linkContainer);
                divContainerHeading.Controls.Add(divContainerTitle);

                NavListItemLinkImage.Controls.Add(ImageMarker);
                NavListItemLinkImage.Controls.Add(labelTabImage);
                NavListItemLinkDetails.Controls.Add(DetailsMarker);
                NavListItemLinkDetails.Controls.Add(labelTabDetails);
                NavListItemLinkComment.Controls.Add(CommentMarker);
                NavListItemLinkComment.Controls.Add(labelTabComment);

                NavListItemImage.Controls.Add(NavListItemLinkImage);
                NavListItemDetails.Controls.Add(NavListItemLinkDetails);
                NavListItemComment.Controls.Add(NavListItemLinkComment);

                NavTab.Controls.Add(NavListItemImage);
                NavTab.Controls.Add(NavListItemDetails);
                NavTab.Controls.Add(NavListItemComment);

                if(p.Image == null)
                {
                    divContainerTabPaneImage.Controls.Add(addButton(p, i));
                }
                else
                {
                    divContainerTabPaneImage.Controls.Add(imgThumbnail);
                }
                
                divContainerTabPaneDetails.Controls.Add(labelLatitud);
                divContainerTabPaneDetails.Controls.Add(breakLine);
                divContainerTabPaneDetails.Controls.Add(labelLongitud);

                if(p.Comment == null)
                {
                    
                }
                else
                {
                    divContainerTabPaneComment.Controls.Add(labelComment);
                }

                divContainerTabContent.Controls.Add(divContainerTabPaneImage);
                divContainerTabContent.Controls.Add(divContainerTabPaneDetails);
                divContainerTabContent.Controls.Add(divContainerTabPaneComment);

                divContainerBody.Controls.Add(NavTab);
                divContainerBody.Controls.Add(divContainerTabContent);
                divContainerCollapse.Controls.Add(divContainerBody);

                divContainer.Controls.Add(divContainerHeading);
                divContainer.Controls.Add(divContainerCollapse);
                accordion.Controls.Add(divContainer);

                ++i;
            }
        }
    }
    private void DisplayStart(Hike hike)
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
        linkContainer.Attributes["data-parent"] = "#accordion";
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
        NavListItemDetails.Attributes["class"] = "active";

        System.Web.UI.HtmlControls.HtmlGenericControl NavListItemLinkDetails =
                new System.Web.UI.HtmlControls.HtmlGenericControl("a");
        NavListItemLinkDetails.Attributes["data-toggle"] = "tab";
        NavListItemLinkDetails.Attributes["href"] = "#details_Init";

        System.Web.UI.HtmlControls.HtmlGenericControl DetailsMarker =
                new System.Web.UI.HtmlControls.HtmlGenericControl("i");
        DetailsMarker.Attributes["class"] = "fa fa-file-text-o fa-fw";
        
        Label labelTabDetails = new Label();
        labelTabDetails.Text = "Detalles";


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

        System.Web.UI.HtmlControls.HtmlGenericControl imgThumbnail =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("IMG");
        if (hike.Image == null)
        {
            imgThumbnail.Attributes["src"] = "/images/defaultThumb.png";
        }
        else
        {
            imgThumbnail.Attributes["src"] = "data:Image/png;base64," + Convert.ToBase64String(hike.Image);
        }
        imgThumbnail.Attributes["style"] = "margin: 0 auto; display: block; width: 80%; length: 80%;";
        //imgThumbnail.Attributes["style"] = "display: block; max-width:75px; max-height:100px; width: auto; height: auto;";

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
        divContainerTabPaneDetails.Controls.Add(labelLatitud);
        divContainerTabPaneDetails.Controls.Add(breakLine5);
        divContainerTabPaneDetails.Controls.Add(labelLongitud);
        
        divContainerTabContent.Controls.Add(divContainerTabPaneDetails);

        divContainerBody.Controls.Add(NavTab);
        divContainerBody.Controls.Add(divContainerTabContent);
        divContainerCollapse.Controls.Add(divContainerBody);

        divContainer.Controls.Add(divContainerHeading);
        divContainer.Controls.Add(divContainerCollapse);
        accordion.Controls.Add(divContainer);
    }

    public Button addButton(Point p, int i)
    {
        Button btn = new Button();
        btn.Click += new System.EventHandler(EditPoint);
        btn.CssClass = "btn btn-default";
        btn.Attributes["type"] = "button";
        btn.ID = p.PointId.ToString() + "_" + i.ToString();
        btn.Text = "Editar";
        return btn;
    }
    protected void EditPoint(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string[] ids = btn.ID.Split('_');
        Point p = new Point();
        p.PointId = Int32.Parse(ids[0]);
        Session["POINT"] = p;
        Response.Redirect("EditPoint.aspx");
    }
    private void displayPointsInMap()
    {
        Hike hike = (Hike)Session["HIKE"];
        if (hike.Route.ListOfPoints != null)
        {
            listOfLong = new List<double>();
            listOfLat = new List<double>();

            listOfLong.Add(hike.Longitud);
            listOfLat.Add(hike.Latitud);

            foreach (Point p in hike.Route.ListOfPoints)
            {
                listOfLong.Add(p.Longitud);
                listOfLat.Add(p.Latitud);
            }

            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            sJSON = oSerializer.Serialize(listOfLong);
            sJSON2 = oSerializer.Serialize(listOfLat);
            numMarcadores = hike.Route.ListOfPoints.Count() + 1; // +1 for the starting point
        }
    }
    

    public void finalizeHike(object sender, EventArgs e)
    {
        List<string> errorList = new List<string>();
        errorList = validateFinalizeHikeInput();
        if (!errorList.Any())
        {
            Hike hike = (Hike)Session["HIKE"];

            hike.Price = dd_pricelevel.SelectedItem.Text;
            hike.Quality = dd_qualitylevel.SelectedItem.Text;
            hike.HikeType = dd_hiketype.SelectedItem.Text;
            hike.Difficulty = dd_difficultylevel.SelectedItem.Text;
            hike.Comment = tb_end_commentary.Text;
            if (imageupload.HasFile)
            {
                hike.Image = imageupload.FileBytes;
            }
            else
            {
                hike.Image = null;
            }
            HikeBusiness hb = new HikeBusiness();
            RegularUser regularUser = (RegularUser)Session["REG_USER"];

            hb.InsertHikeWithExistingRoute(ref hike, ref regularUser);

            Session["HIKE"] = null;

            Notification n = new Notification();
            n.Type = 1;
            n.Message = "Su caminata ha sido guardada en nuestro sistema! Gracias por utilizar CaminataCR! :)";
            Session["NOTIFICATION"] = n;
            Response.Redirect("Notifications.aspx");
        }
        else
        {
            outputFinalizeHikeErrors(errorList);
        }
    }
    public List<string> validateFinalizeHikeInput()
    {
        List<string> errorList = new List<string>();
        //rName : alphanumeric
        Regex rDetails = new Regex("^[a-zA-Z0-9\\s\\.\\,]*$");

        //Commentary
        if ((tb_end_commentary.Text.Length == 0) || (tb_end_commentary.Text.Length > 500)) errorList.Add("Comentario : Tamaño incorrecto [500] maximo.");
        if (!rDetails.IsMatch(tb_end_commentary.Text)) errorList.Add("Comentario : Solo caracteres alphanumericos (incluyendo espacios).");
        
        return errorList;
    }
    public void outputFinalizeHikeErrors(List<string> errorList)
    {
        FinishErrors.Text = "";
        foreach (string error in errorList)
        {
            FinishErrors.Text += error;
            FinishErrors.Text += " <br> ";
        }
    }
}