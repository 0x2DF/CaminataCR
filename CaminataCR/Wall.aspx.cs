using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModernControls;

public partial class Wall : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["REG_USER"] == null)
        {
            Response.Redirect("SignInRegular.aspx");
        }
        
        RegularUser regularUser = (RegularUser)Session["REG_USER"];
        LoggedInUsername.Text = regularUser.Account;
        Session["TMP_REG_USER"] = regularUser;
        FriendBusiness fb = new FriendBusiness();
        fb.LoadHikesOfFriends(ref regularUser);
        Session["REG_USER"] = regularUser;
        DisplayFriendHikes();
    }

    private void DisplayFriendHikes()
    {
        rowContainer.InnerHtml = "";
        DisplayMainHeader();
        RegularUser regularUser = (RegularUser)Session["REG_USER"];
        foreach (RegularUser ru in regularUser.ListOfFriends)
        {
            int j = 0;
            foreach(Hike h in ru.ListOfHikes)
            {
                DisplayHike(ru, h, j, regularUser.UserId);
                ++j;
            }
        }
        RegularUser oldregularUser = (RegularUser)Session["TMP_REG_USER"];
        Session["REG_USER"] = oldregularUser;
        Session["TMP_REG_USER"] = null;
    }
    private void DisplayMainHeader()
    {
        System.Web.UI.HtmlControls.HtmlGenericControl divContainerRow =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerRow.Attributes["class"] = "row";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerCol =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerCol.Attributes["class"] = "col-lg-12";

        System.Web.UI.HtmlControls.HtmlGenericControl header =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("H1");
        header.Attributes["class"] = "page-header";

        header.InnerHtml = "Muro";
        divContainerCol.Controls.Add(header);
        divContainerRow.Controls.Add(divContainerCol);
        rowContainer.Controls.Add(divContainerRow);
    }
    private void DisplayHike(RegularUser ru, Hike h, int id, int userId)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl divContainerRow =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerRow.Attributes["class"] = "row";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerCol =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerCol.Attributes["class"] = "col-lg-12";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerPanelDefault =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerPanelDefault.Attributes["class"] = "panel panel-default";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerPanelHeading =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerPanelHeading.Attributes["class"] = "panel-heading";

        System.Web.UI.HtmlControls.HtmlGenericControl icon =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("i");
        icon.Attributes["class"] = "fa fa-road fa-fw";

        Label labelTitle = new Label();
        labelTitle.Text = "Caminata";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerPanelBody =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerPanelBody.Attributes["class"] = "panel-body";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerPanelBodyUser =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerPanelBodyUser.Attributes["class"] = "panel-group";

        System.Web.UI.HtmlControls.HtmlGenericControl horizontalLine =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("hr");

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerPanelBodyAccordion =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerPanelBodyAccordion.Attributes["class"] = "panel-group";
        divContainerPanelBodyAccordion.Attributes["id"] = "accordion_"+id.ToString();

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerPanelFooter =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerPanelFooter.Attributes["class"] = "panel-footer";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerPanelFooterText =
                        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerPanelFooterText.Attributes["class"] = "text-center";

        //Structure

        //divContainerPanelHeading
        divContainerPanelHeading.Controls.Add(icon);
        divContainerPanelHeading.Controls.Add(labelTitle);
        //divContainerPanelBody
        divContainerPanelBodyUser.Controls.Add(DisplayFriend(ru, id));
        divContainerPanelBodyAccordion.Controls.Add(DisplayHikeStart(h, id));

        List<Point> SortedList = h.Route.ListOfPoints.OrderBy(o => o.Pos).ToList();
        int i = 0;
        foreach (Point p in SortedList)
        {
            divContainerPanelBodyAccordion.Controls.Add(DisplayHikeRoute(p, id, i));
            ++i;
        }
        divContainerPanelBody.Controls.Add(divContainerPanelBodyUser);
        divContainerPanelBody.Controls.Add(horizontalLine);
        divContainerPanelBody.Controls.Add(divContainerPanelBodyAccordion);
        //divContainerPanelFooter
        LikeBusiness lb = new LikeBusiness();
        RegularUser oldregularUser = new RegularUser();
        oldregularUser.UserId = userId;
        if (lb.getLikeStatus(oldregularUser, h))
        {
           divContainerPanelFooterText.Controls.Add(addButton(userId, h, 1));
        }
        else
        {
            divContainerPanelFooterText.Controls.Add(addButton(userId, h, 0));
        }
        
        divContainerPanelFooter.Controls.Add(divContainerPanelFooterText);

        //divContainerPanelDefault
        divContainerPanelDefault.Controls.Add(divContainerPanelHeading);
        divContainerPanelDefault.Controls.Add(divContainerPanelBody);
        divContainerPanelDefault.Controls.Add(divContainerPanelFooter);

        divContainerCol.Controls.Add(divContainerPanelDefault);
        divContainerRow.Controls.Add(divContainerCol);
        rowContainer.Controls.Add(divContainerRow);
    }

    public System.Web.UI.HtmlControls.HtmlGenericControl DisplayFriend(RegularUser ru, int id)
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
        linkContainer.Attributes["data-parent"] = "#accordion_" + id.ToString();
        linkContainer.Attributes["href"] = "#collapse_User";

        System.Web.UI.HtmlControls.HtmlGenericControl MapMarker =
                new System.Web.UI.HtmlControls.HtmlGenericControl("i");
        MapMarker.Attributes["class"] = "fa fa-user fa-fw";

        Label labelPos = new Label();
        labelPos.Text = " "+ru.FirstName + " " + ru.Surname;

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerCollapse =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerCollapse.Attributes["class"] = "panel-collapse collapse";
        divContainerCollapse.Attributes["id"] = "collapse_User";

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
        NavListItemLinkImage.Attributes["href"] = "#image_User";

        Label labelTabImage = new Label();
        labelTabImage.Text = "Imagen";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabContent =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerTabContent.Attributes["class"] = "tab-content";

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabPaneImage =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerTabPaneImage.Attributes["class"] = "tab-pane fade in active";
        divContainerTabPaneImage.Attributes["id"] = "image_User";


        System.Web.UI.HtmlControls.HtmlGenericControl imgThumbnail =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("IMG");

        if (ru.ProfilePicture == null)
        {
            imgThumbnail.Attributes["src"] = "/images/defaultThumb.png";
        }
        else
        {
            imgThumbnail.Attributes["src"] = "data:Image/png;base64," + Convert.ToBase64String(ru.ProfilePicture);
        }
        imgThumbnail.Attributes["style"] = "margin: 0 auto; display: block; width: 40%; length: 40%;";

        //Structure
        linkContainer.Controls.Add(MapMarker);
        linkContainer.Controls.Add(labelPos);
        divContainerTitle.Controls.Add(linkContainer);
        divContainerHeading.Controls.Add(divContainerTitle);

        NavListItemLinkImage.Controls.Add(labelTabImage);

        NavListItemImage.Controls.Add(NavListItemLinkImage);

        NavTab.Controls.Add(NavListItemImage);

        divContainerTabPaneImage.Controls.Add(imgThumbnail);

        divContainerTabContent.Controls.Add(divContainerTabPaneImage);

        divContainerBody.Controls.Add(NavTab);
        divContainerBody.Controls.Add(divContainerTabContent);
        divContainerCollapse.Controls.Add(divContainerBody);

        divContainer.Controls.Add(divContainerHeading);
        divContainer.Controls.Add(divContainerCollapse);
        return divContainer;
    }

    public System.Web.UI.HtmlControls.HtmlGenericControl DisplayHikeStart(Hike h, int id)
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
        linkContainer.Attributes["data-parent"] = "#accordion_" + id.ToString();
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

        System.Web.UI.HtmlControls.HtmlGenericControl NavListItemExperience =
                new System.Web.UI.HtmlControls.HtmlGenericControl("li");

        System.Web.UI.HtmlControls.HtmlGenericControl NavListItemLinkExperience =
                new System.Web.UI.HtmlControls.HtmlGenericControl("a");
        NavListItemLinkExperience.Attributes["data-toggle"] = "tab";
        NavListItemLinkExperience.Attributes["href"] = "#experience_Init";

        System.Web.UI.HtmlControls.HtmlGenericControl ImageMarker =
                new System.Web.UI.HtmlControls.HtmlGenericControl("i");
        ImageMarker.Attributes["class"] = "fa fa-camera fa-fw";

        System.Web.UI.HtmlControls.HtmlGenericControl DetailsMarker =
                new System.Web.UI.HtmlControls.HtmlGenericControl("i");
        DetailsMarker.Attributes["class"] = "fa fa-file-text-o fa-fw";

        System.Web.UI.HtmlControls.HtmlGenericControl ExperienceMarker =
                new System.Web.UI.HtmlControls.HtmlGenericControl("i");
        ExperienceMarker.Attributes["class"] = "fa fa-asterisk fa-fw";

        Label labelTabImage = new Label();
        labelTabImage.Text = " Imagen";

        Label labelTabDetails = new Label();
        labelTabDetails.Text = " Detalles";

        Label labelTabExperience = new Label();
        labelTabExperience.Text = " Experiencia";

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

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabPaneExperience =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerTabPaneExperience.Attributes["class"] = "tab-pane fade";
        divContainerTabPaneExperience.Attributes["id"] = "experience_Init";

        Label labelName = new Label();
        labelName.Text = "Nombre del Lugar: " + h.NameOfLocation;

        Label labelProvince = new Label();
        labelProvince.Text = "Provincia: " + h.Province;

        Label labelCanton = new Label();
        labelCanton.Text = "Canton: " + h.Canton;

        Label labelDistrict = new Label();
        labelDistrict.Text = "Distrito: " + h.District;

        Label labelDetails = new Label();
        labelDetails.Text = "Detalles: " + h.Details;

        Label labelLatitud = new Label();
        labelLatitud.Text = "Latitud: " + h.Latitud.ToString();

        Label labelLongitud = new Label();
        labelLongitud.Text = "Longitud: " + h.Longitud.ToString();

        Label labelType = new Label();
        labelType.Text = "Tipo de Caminata: " + h.HikeType;

        Label labelDifficulty = new Label();
        labelDifficulty.Text = "Dificultad: " + h.Difficulty;

        Label labelQuality = new Label();
        labelQuality.Text = "Calidad: " + h.Quality;

        Label labelPrice = new Label();
        labelPrice.Text = "Precio: " + h.Price;

        Label labelDate = new Label();
        labelDate.Text = "Fecha: " + h.DateTime.ToString();

        Label labelComment = new Label();
        labelComment.Text = "Comentario: " + h.Comment;

        //Label labelLikes = new Label();
        //labelLikes.Text = "Likes: " + h.Likes.ToString();

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
        System.Web.UI.HtmlControls.HtmlGenericControl breakLine7 =
                new System.Web.UI.HtmlControls.HtmlGenericControl("BR");
        System.Web.UI.HtmlControls.HtmlGenericControl breakLine8 =
                new System.Web.UI.HtmlControls.HtmlGenericControl("BR");
        System.Web.UI.HtmlControls.HtmlGenericControl breakLine9 =
                new System.Web.UI.HtmlControls.HtmlGenericControl("BR");
        System.Web.UI.HtmlControls.HtmlGenericControl breakLine10 =
                new System.Web.UI.HtmlControls.HtmlGenericControl("BR");
        System.Web.UI.HtmlControls.HtmlGenericControl breakLine11 =
                new System.Web.UI.HtmlControls.HtmlGenericControl("BR");
        //System.Web.UI.HtmlControls.HtmlGenericControl breakLine12 =
                //new System.Web.UI.HtmlControls.HtmlGenericControl("BR");


        System.Web.UI.HtmlControls.HtmlGenericControl imgThumbnail =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("IMG");
        if (h.Image == null)
        {
            imgThumbnail.Attributes["src"] = "/images/defaultThumb.png";
        }
        else
        {
            imgThumbnail.Attributes["src"] = "data:Image/png;base64," + Convert.ToBase64String(h.Image);
        }
        imgThumbnail.Attributes["style"] = "margin: 0 auto; display: block; width: 80%; length: 80%;";
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
        NavListItemLinkExperience.Controls.Add(ExperienceMarker);
        NavListItemLinkExperience.Controls.Add(labelTabExperience);

        NavListItemImage.Controls.Add(NavListItemLinkImage);
        NavListItemDetails.Controls.Add(NavListItemLinkDetails);
        NavListItemExperience.Controls.Add(NavListItemLinkExperience);

        NavTab.Controls.Add(NavListItemImage);
        NavTab.Controls.Add(NavListItemDetails);
        NavTab.Controls.Add(NavListItemExperience);

        divContainerTabPaneImage.Controls.Add(imgThumbnail);

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

        divContainerTabPaneExperience.Controls.Add(labelType);
        divContainerTabPaneExperience.Controls.Add(breakLine7);
        divContainerTabPaneExperience.Controls.Add(labelDifficulty);
        divContainerTabPaneExperience.Controls.Add(breakLine8);
        divContainerTabPaneExperience.Controls.Add(labelQuality);
        divContainerTabPaneExperience.Controls.Add(breakLine9);
        divContainerTabPaneExperience.Controls.Add(labelPrice);
        divContainerTabPaneExperience.Controls.Add(breakLine10);
        divContainerTabPaneExperience.Controls.Add(labelDate);
        divContainerTabPaneExperience.Controls.Add(breakLine11);
        divContainerTabPaneExperience.Controls.Add(labelComment);
        //divContainerTabPaneExperience.Controls.Add(breakLine12);
        //divContainerTabPaneExperience.Controls.Add(labelLikes);

        divContainerTabContent.Controls.Add(divContainerTabPaneImage);
        divContainerTabContent.Controls.Add(divContainerTabPaneDetails);
        divContainerTabContent.Controls.Add(divContainerTabPaneExperience);

        divContainerBody.Controls.Add(NavTab);
        divContainerBody.Controls.Add(divContainerTabContent);
        divContainerCollapse.Controls.Add(divContainerBody);

        divContainer.Controls.Add(divContainerHeading);
        divContainer.Controls.Add(divContainerCollapse);
        return divContainer;
    }

    public System.Web.UI.HtmlControls.HtmlGenericControl DisplayHikeRoute(Point p, int id, int id2)
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
        linkContainer.Attributes["data-parent"] = "#accordion_" + id.ToString();
        linkContainer.Attributes["href"] = "#collapse_" + id2.ToString();

        System.Web.UI.HtmlControls.HtmlGenericControl MapMarker =
                new System.Web.UI.HtmlControls.HtmlGenericControl("i");
        MapMarker.Attributes["class"] = "fa fa-map-marker fa-fw";

        Label labelPos = new Label();
        labelPos.Text = (p.Pos + 1).ToString();



        System.Web.UI.HtmlControls.HtmlGenericControl divContainerCollapse =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerCollapse.Attributes["class"] = "panel-collapse collapse";
        divContainerCollapse.Attributes["id"] = "collapse_" + id2.ToString();

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
        NavListItemLinkImage.Attributes["href"] = "#image_" + id2.ToString();

        System.Web.UI.HtmlControls.HtmlGenericControl NavListItemDetails =
                new System.Web.UI.HtmlControls.HtmlGenericControl("li");

        System.Web.UI.HtmlControls.HtmlGenericControl NavListItemLinkDetails =
                new System.Web.UI.HtmlControls.HtmlGenericControl("a");
        NavListItemLinkDetails.Attributes["data-toggle"] = "tab";
        NavListItemLinkDetails.Attributes["href"] = "#details_" + id2.ToString();

        System.Web.UI.HtmlControls.HtmlGenericControl NavListItemComment =
                new System.Web.UI.HtmlControls.HtmlGenericControl("li");

        System.Web.UI.HtmlControls.HtmlGenericControl NavListItemLinkComment =
                new System.Web.UI.HtmlControls.HtmlGenericControl("a");
        NavListItemLinkComment.Attributes["data-toggle"] = "tab";
        NavListItemLinkComment.Attributes["href"] = "#comment_" + id2.ToString();

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
        divContainerTabPaneImage.Attributes["id"] = "image_" + id2.ToString();

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabPaneDetails =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerTabPaneDetails.Attributes["class"] = "tab-pane fade";
        divContainerTabPaneDetails.Attributes["id"] = "details_" + id2.ToString();

        System.Web.UI.HtmlControls.HtmlGenericControl divContainerTabPaneComment =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        divContainerTabPaneComment.Attributes["class"] = "tab-pane fade";
        divContainerTabPaneComment.Attributes["id"] = "comment_" + id2.ToString();

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

        divContainerTabPaneImage.Controls.Add(imgThumbnail);
        divContainerTabPaneDetails.Controls.Add(labelLatitud);
        divContainerTabPaneDetails.Controls.Add(breakLine);
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
        return divContainer;
    }

    public ModernButton addButton(int userId, Hike h, int status)
    {
        ModernButton btn = new ModernButton();
        
        //System.Web.UI.HtmlControls.HtmlGenericControl icon =
                        //new System.Web.UI.HtmlControls.HtmlGenericControl("i");
        string iconStr = "";
        if (status == 1) 
        {
            btn.Click += new System.EventHandler(UnLike);
            btn.CssClass = "btn btn-warning btn-circle btn-lg";
            iconStr = "<i class=\"fa fa-times\"></i>";
            //icon.Attributes["class"] = "fa fa-times";
        }
        else 
        {
            btn.Click += new System.EventHandler(Like);
            btn.CssClass = "btn btn-danger btn-circle btn-lg";
            iconStr = "<i class=\"fa fa-heart\"></i>";
            //icon.Attributes["class"] = "fa fa-heart";
        }
        btn.Attributes["type"] = "button";
        btn.ID = userId.ToString()+"_"+h.HikeId.ToString();
        //btn.Controls.Add(icon);
        btn.Text = iconStr;
        return btn;
    }
    protected void Like(object sender, EventArgs e)
    {
        LikeBusiness lb = new LikeBusiness();
        ModernButton btn = (ModernButton)sender;

        string[] ids = btn.ID.Split('_');
        RegularUser regularUser = new RegularUser();
        regularUser.UserId = Int32.Parse(ids[0]);

        Hike hike = new Hike();
        hike.HikeId = Int32.Parse(ids[1]);

        Tuple<int, string> t = new Tuple<int, string>(0, "");
        t = lb.addLike(regularUser, hike);

        Notification n = new Notification();
        n.Type = t.Item1;
        n.Message = t.Item2;
        Session["NOTIFICATION"] = n;
        Response.Redirect("Notifications.aspx");
        //Response.Redirect("Wall.aspx");
    }
    protected void UnLike(object sender, EventArgs e)
    {
        LikeBusiness lb = new LikeBusiness();
        ModernButton btn = (ModernButton)sender;

        string[] ids = btn.ID.Split('_');
        RegularUser regularUser = new RegularUser();
        regularUser.UserId = Int32.Parse(ids[0]);

        Hike hike = new Hike();
        hike.HikeId = Int32.Parse(ids[1]);

        Tuple<int, string> t = new Tuple<int, string>(0, "");
        t = lb.removeLike(regularUser, hike);

        Notification n = new Notification();
        n.Type = t.Item1;
        n.Message = t.Item2;
        Session["NOTIFICATION"] = n;
        Response.Redirect("Notifications.aspx");
        //Response.Redirect("Wall.aspx");
    }
}