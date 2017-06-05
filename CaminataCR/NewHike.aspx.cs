using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class NewHike : System.Web.UI.Page
{
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
        else
        {
            RegularUser regularUser = (RegularUser)Session["REG_USER"];
            LoggedInUsername.Text = regularUser.Account;
        }
        if (!IsPostBack)
        {
            if (dd_qualitylevel.Items.Count == 0)
            {
                FillDropDownLists();
            }
        }
        DisplayRoute();
    }

    private void FillDropDownLists()
    {
        HikeBusiness hb = new HikeBusiness();
        
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
        accordion.InnerHtml = "";
        Hike hike = (Hike)Session["HIKE"];
        DisplayStart(ref hike);
        List<Point> SortedList = hike.Route.ListOfPoints.OrderBy(o => o.Pos).ToList();

        int i = 0;
        foreach(Point p in SortedList)
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
            linkContainer.Attributes["href"] = "#collapse_"+i.ToString();

            System.Web.UI.HtmlControls.HtmlGenericControl MapMarker =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("i");
            MapMarker.Attributes["class"] = "fa fa-map-marker fa-fw";

            Label labelPos = new Label();
            labelPos.Text = p.Pos.ToString();



            System.Web.UI.HtmlControls.HtmlGenericControl divContainerCollapse =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            divContainerCollapse.Attributes["class"] = "panel-collapse collapse in";
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

            Label labelTabImage = new Label();
            labelTabImage.Text = "Image";

            Label labelTabDetails = new Label();
            labelTabDetails.Text = "Details";

            Label labelTabComment = new Label();
            labelTabComment.Text = "Comment";

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
            //imgThumbnail.Attributes["style"] = "display: block; max-width:75px; max-height:100px; width: auto; height: auto;";

            //Structure

            linkContainer.Controls.Add(labelPos);
            linkContainer.Controls.Add(MapMarker);
            divContainerTitle.Controls.Add(linkContainer); 
            divContainerHeading.Controls.Add(divContainerTitle);

            NavListItemLinkImage.Controls.Add(NavListItemLinkImage);
            NavListItemLinkDetails.Controls.Add(NavListItemLinkDetails);
            NavListItemLinkComment.Controls.Add(NavListItemLinkComment);

            NavListItemImage.Controls.Add(labelTabImage);
            NavListItemDetails.Controls.Add(labelTabDetails);
            NavListItemComment.Controls.Add(labelTabComment);

            NavTab.Controls.Add(NavListItemImage);
            NavTab.Controls.Add(NavListItemDetails);
            NavTab.Controls.Add(NavListItemComment);

            divContainerTabPaneImage.Controls.Add(imgThumbnail);
            divContainerTabPaneDetails.Controls.Add(labelLatitud);
            divContainerTabPaneDetails.Controls.Add(labelLongitud);
            divContainerTabPaneComment.Controls.Add(labelComment);

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
    private void DisplayStart(ref Hike hike)
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
        MapMarker.Attributes["class"] = "fa fa-map-marker fa-fw";

        Label labelPos = new Label();
        labelPos.Text = "Start";



        System.Web.UI.HtmlControls.HtmlGenericControl divContainerCollapse =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerCollapse.Attributes["class"] = "panel-collapse collapse in";
        divContainerCollapse.Attributes["id"] = "collapse_Init";

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
        NavListItemLinkImage.Attributes["href"] = "#image_Init";

        System.Web.UI.HtmlControls.HtmlGenericControl NavListItemDetails =
                new System.Web.UI.HtmlControls.HtmlGenericControl("li");

        System.Web.UI.HtmlControls.HtmlGenericControl NavListItemLinkDetails =
                new System.Web.UI.HtmlControls.HtmlGenericControl("a");
        NavListItemLinkDetails.Attributes["data-toggle"] = "tab";
        NavListItemLinkDetails.Attributes["href"] = "#details_Init";

        Label labelTabImage = new Label();
        labelTabImage.Text = "Image";

        Label labelTabDetails = new Label();
        labelTabDetails.Text = "Details";
        

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabContent =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerTabContent.Attributes["class"] = "tab-content";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabPaneImage =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerTabPaneImage.Attributes["class"] = "tab-pane fade in active";
        divContainerTabPaneImage.Attributes["id"] = "image_Init";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabPaneDetails =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerTabPaneDetails.Attributes["class"] = "tab-pane fade";
        divContainerTabPaneDetails.Attributes["id"] = "details_Init";

        Label labelLatitud = new Label();
        labelLatitud.Text = "Latitud: " + hike.Latitud.ToString();

        Label labelLongitud = new Label();
        labelLongitud.Text = "Longitud: " + hike.Longitud.ToString();

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
        //imgThumbnail.Attributes["style"] = "display: block; max-width:75px; max-height:100px; width: auto; height: auto;";

        //Structure

        linkContainer.Controls.Add(labelPos);
        linkContainer.Controls.Add(MapMarker);
        divContainerTitle.Controls.Add(linkContainer);
        divContainerHeading.Controls.Add(divContainerTitle);

        NavListItemLinkImage.Controls.Add(NavListItemLinkImage);
        NavListItemLinkDetails.Controls.Add(NavListItemLinkDetails);

        NavListItemImage.Controls.Add(labelTabImage);
        NavListItemDetails.Controls.Add(labelTabDetails);

        NavTab.Controls.Add(NavListItemImage);
        NavTab.Controls.Add(NavListItemDetails);

        divContainerTabPaneImage.Controls.Add(imgThumbnail);
        divContainerTabPaneDetails.Controls.Add(labelLatitud);
        divContainerTabPaneDetails.Controls.Add(labelLongitud);

        divContainerTabContent.Controls.Add(divContainerTabPaneImage);
        divContainerTabContent.Controls.Add(divContainerTabPaneDetails);

        divContainerBody.Controls.Add(NavTab);
        divContainerBody.Controls.Add(divContainerTabContent);
        divContainerCollapse.Controls.Add(divContainerBody);

        divContainer.Controls.Add(divContainerHeading);
        divContainer.Controls.Add(divContainerCollapse);
        accordion.Controls.Add(divContainer);
    }

    protected void addPoint(object sender, EventArgs e)
    {
        List<string> errorList = new List<string>();
        errorList = validateAddPointInput();
        if (!errorList.Any())
        {
            Hike hike = (Hike)Session["HIKE"];
            Point point = new Point();
            point.Latitud = double.Parse(tb_longitud.Text, System.Globalization.CultureInfo.InvariantCulture);
            point.Longitud = double.Parse(tb_latitud.Text, System.Globalization.CultureInfo.InvariantCulture);
            point.Comment = tb_add_commentary.Text;
            if (imageupload.HasFile)
            {
                point.Image = imageupload.FileBytes;
            }
            else
            {
                point.Image = null;
            }
            point.Pos = hike.Route.ListOfPoints.Count;
            hike.Route.ListOfPoints.Add(point);
            Session["HIKE"] = hike;
        }
        else
        {
            outputAddPointErrors(errorList);
        }
    }

    public List<string> validateAddPointInput()
    {
        List<string> errorList = new List<string>();
        //rName : alphanumeric
        Regex rDetails = new Regex("^[a-zA-Z0-9\\s\\.]*$");
        //GPS : numeric
        Regex rFloat = new Regex("^-?[0-9]\\d*(\\.\\d+)?$");


        //Commentary
        if ((tb_add_commentary.Text.Length == 0) || (tb_add_commentary.Text.Length > 500)) errorList.Add("Comentario : Tamaño incorrecto [500] maximo.");
        if (!rDetails.IsMatch(tb_add_commentary.Text)) errorList.Add("Comentario : Solo caracteres alphanumericos (incluyendo espacios).");

        //Longitud
        if ((tb_longitud.Text.Length == 0) || (tb_longitud.Text.Length > 20)) errorList.Add("Longitud : Tamaño incorrecto [20] maximo.");
        if (!rFloat.IsMatch(tb_longitud.Text)) errorList.Add("Longitud : (-)XX.YY");

        //Latitud
        if ((tb_latitud.Text.Length == 0) || (tb_latitud.Text.Length > 20)) errorList.Add("Latitud : Tamaño incorrecto [20] maximo.");
        if (!rFloat.IsMatch(tb_latitud.Text)) errorList.Add("Latitud : (-)XX.YY");

        //fotografia
        if (imageupload.HasFile)
        {
            string extension = System.IO.Path.GetExtension(imageupload.FileName);
            if ((extension != ".jpg") && (extension != ".png"))
            {
                errorList.Add("Fotografia : Solo se permiten '.jpg' y '.png'");
            }
        }
        return errorList;
    }

    protected void outputAddPointErrors(List<string> errorList)
    {
        foreach (string error in errorList)
        {
            AddPointErrors.Text += error;
            AddPointErrors.Text += " <br> ";
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
            hike.Comment = tb_end_commentary.Text;

            HikeBusiness hb = new HikeBusiness();
            RegularUser regularUser = (RegularUser)Session["REG_USER"];
            hb.InsertHike(ref hike, ref regularUser);

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
        Regex rDetails = new Regex("^[a-zA-Z0-9\\s\\.]*$");
        
        //Commentary
        if ((tb_end_commentary.Text.Length == 0) || (tb_end_commentary.Text.Length > 500)) errorList.Add("Comentario : Tamaño incorrecto [500] maximo.");
        if (!rDetails.IsMatch(tb_end_commentary.Text)) errorList.Add("Comentario : Solo caracteres alphanumericos (incluyendo espacios).");
        
        return errorList;
    }
    public void outputFinalizeHikeErrors(List<string> errorList)
    {
        foreach (string error in errorList)
        {
            FinishErrors.Text += error;
            FinishErrors.Text += " <br> ";
        }
    }
}