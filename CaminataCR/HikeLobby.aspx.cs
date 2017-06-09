using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web.Script.Serialization;

public partial class HikeLobby : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            if (dd_filter_province.Items.Count == 0)
            {
                FillDropDownLists();
            }
            RegularUser regularUser = (RegularUser)Session["REG_USER"];
            LoggedInUsername.Text = regularUser.Account;
        }
        else
        {
            displayFilteredHikes();
        }

    }

    private void FillDropDownLists()
    {
        HikeBusiness hb = new HikeBusiness();

        List<string> ProvinceList = hb.getProvinces();
        dd_filter_province.Items.Clear();
        dd_init_province.Items.Clear();
        foreach (string p in ProvinceList)
        {
            dd_filter_province.Items.Add(p);
            dd_init_province.Items.Add(p);
        }

        List<string> HikeTypeList = hb.getHikeTypes();
        dd_filter_hiketype.Items.Clear();
        dd_init_hiketype.Items.Clear();
        foreach (string p in HikeTypeList)
        {
            dd_filter_hiketype.Items.Add(p);
            dd_init_hiketype.Items.Add(p);
        }

        List<string> DifficultyLevelList = hb.getDifficultyLevels();
        dd_filter_difficultylevel.Items.Clear();
        dd_init_difficultylevel.Items.Clear();
        foreach (string p in DifficultyLevelList)
        {
            dd_filter_difficultylevel.Items.Add(p);
            dd_init_difficultylevel.Items.Add(p);
        }

        List<string> QualityLevelList = hb.getQualityLevels();
        dd_filter_pricelevel.Items.Clear();
        foreach (string p in QualityLevelList)
        {
            dd_filter_pricelevel.Items.Add(p);
        }

        List<string> PriceLevelList = hb.getPriceLevels();
        dd_filter_qualitylevel.Items.Clear();
        foreach (string p in PriceLevelList)
        {
            dd_filter_qualitylevel.Items.Add(p);
        }
    }

    protected void FillInitCanton(object sender, EventArgs e)
    {
        HikeBusiness hb = new HikeBusiness();

        List<string> CantonList = hb.getCantons(dd_init_province.SelectedItem.Text);
        dd_init_canton.Items.Clear();
        foreach (string p in CantonList)
        {
            dd_init_canton.Items.Add(p);
        }
    }

    protected void FillInitDistrict(object sender, EventArgs e)
    {
        HikeBusiness hb = new HikeBusiness();

        List<string> DistrictList = hb.getDistricts(dd_init_canton.SelectedItem.Text);
        dd_init_district.Items.Clear();
        foreach (string p in DistrictList)
        {
            dd_init_district.Items.Add(p);
        }
    }

    protected void FillFilterCanton(object sender, EventArgs e)
    {
        HikeBusiness hb = new HikeBusiness();

        List<string> CantonList = hb.getCantons(dd_filter_province.SelectedItem.Text);
        dd_filter_canton.Items.Clear();
        foreach (string p in CantonList)
        {
            dd_filter_canton.Items.Add(p);
        }
    }

    protected void FillFilterDistrict(object sender, EventArgs e)
    {
        HikeBusiness hb = new HikeBusiness();

        List<string> DistrictList = hb.getDistricts(dd_filter_canton.SelectedItem.Text);
        dd_filter_district.Items.Clear();
        foreach (string p in DistrictList)
        {
            dd_filter_district.Items.Add(p);
        }
    }

    protected void filterHikes(object sender, EventArgs e)
    {
        List<string> errorList = new List<string>();
        errorList = validateFilterInput();
        if (!errorList.Any())
        {
            Hike hike = new Hike();
            if (cb_filter_name.Checked) hike.NameOfLocation = tb_filter_name.Text;
            else hike.NameOfLocation = null;

            if (cb_filter_direction.Checked)
            {
                hike.Province = dd_filter_province.SelectedItem.Text;
                hike.Canton = dd_filter_canton.SelectedItem.Text;
                hike.District = dd_filter_district.SelectedItem.Text;
            }
            else
            {
                hike.Province = null;
                hike.Canton = null;
                hike.District = null;
            }
            bool GPS = false;
            if (cb_filter_initpoint.Checked)
            {
                hike.Longitud = double.Parse(tb_filter_longitud.Text, System.Globalization.CultureInfo.InvariantCulture);
                hike.Latitud = double.Parse(tb_filter_latitud.Text, System.Globalization.CultureInfo.InvariantCulture);
                GPS = true;
            }

            if (cb_filter_hiketype.Checked) hike.HikeType = dd_filter_hiketype.SelectedItem.Text;
            else hike.HikeType = null;

            if (cb_filter_difficultylevel.Checked) hike.Difficulty = dd_filter_difficultylevel.SelectedItem.Text;
            else hike.Difficulty = null;

            if (cb_filter_pricelevel.Checked) hike.Price = dd_filter_pricelevel.SelectedItem.Text;
            else hike.Price = null;

            if (cb_filter_qualitylevel.Checked) hike.Quality = dd_filter_qualitylevel.SelectedItem.Text;
            else hike.Quality = null;

            RegularUser regularUser = (RegularUser)Session["REG_USER"];

            HikeBusiness hb = new HikeBusiness();
            regularUser.ListOfHikes = hb.filterHikes(hike, GPS);
            Session["REG_USER"] = regularUser;
            displayFilteredHikes();
        }
        else
        {
            outputFilterErrors(errorList);
        }
    }

    public List<string> validateFilterInput()
    {
        List<string> errorList = new List<string>();
        if (cb_filter_name.Checked)
        {
            Regex rName = new Regex("^[a-zA-Z\\s]*$");
            if ((tb_filter_name.Text.Length == 0) || (tb_filter_name.Text.Length > 20)) errorList.Add("Nombre del lugar : Tamaño incorrecto [20] maximo.");
            if (!rName.IsMatch(tb_filter_name.Text)) errorList.Add("Nombre del lugar : Solo caracteres alfabeticos (incluyendo espacios).");
        }
        if (cb_filter_initpoint.Checked)
        {
            Regex rFloat = new Regex("^-?[0-9]\\d*(\\.\\d+)?$");
            if ((tb_filter_longitud.Text.Length == 0) || (tb_filter_longitud.Text.Length > 20)) errorList.Add("Longitud : Tamaño incorrecto [20] maximo.");
            if (!rFloat.IsMatch(tb_filter_longitud.Text)) errorList.Add("Longitud : (-)XX.YY");

            if ((tb_filter_latitud.Text.Length == 0) || (tb_filter_latitud.Text.Length > 20)) errorList.Add("Latitud : Tamaño incorrecto [20] maximo.");
            if (!rFloat.IsMatch(tb_filter_latitud.Text)) errorList.Add("Latitud : (-)XX.YY");
        }
        return errorList;
    }

    protected void outputFilterErrors(List<string> errorList)
    {
        FilterErrors.Text = "";
        foreach (string error in errorList)
        {
            FilterErrors.Text += error;
            FilterErrors.Text += " <br> ";
        }
    }

    protected void InitHike(object sender, EventArgs e)
    {
        List<string> errorList = new List<string>();
        errorList = validateInitInput();
        if (!errorList.Any())
        {
            Hike hike = new Hike();

            hike.NameOfLocation = tb_init_name.Text;
            hike.Province = dd_init_province.SelectedItem.Text;
            hike.Canton = dd_init_canton.SelectedItem.Text;
            hike.District = dd_init_district.SelectedItem.Text;
            hike.Details = tb_init_details.Text;
            hike.Longitud = double.Parse(tb_init_longitud.Text, System.Globalization.CultureInfo.InvariantCulture);
            hike.Latitud = double.Parse(tb_init_latitud.Text, System.Globalization.CultureInfo.InvariantCulture);
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
            Session["HIKE"] = hike;
            Response.Redirect("NewHike.aspx");

        }
        else
        {
            outputInitErrors(errorList);
        }
    }

    public List<string> validateInitInput()
    {
        List<string> errorList = new List<string>();
        //rName : alpha
        Regex rName = new Regex("^[a-zA-Z\\s]*$");
        //rName : alphanumeric
        Regex rDetails = new Regex("^[a-zA-Z0-9\\s\\.]*$");
        //GPS : numeric
        Regex rFloat = new Regex("^-?[0-9]\\d*(\\.\\d+)?$");

        //Name of Location
        if ((tb_init_name.Text.Length == 0) || (tb_init_name.Text.Length > 20)) errorList.Add("Nombre del lugar : Tamaño incorrecto [20] maximo.");
        if (!rName.IsMatch(tb_init_name.Text)) errorList.Add("Nombre del lugar : Solo caracteres alfabeticos (incluyendo espacios).");

        //Details
        if ((tb_init_details.Text.Length == 0) || (tb_init_name.Text.Length > 500)) errorList.Add("Detalles : Tamaño incorrecto [500] maximo.");
        if (!rDetails.IsMatch(tb_init_details.Text)) errorList.Add("Detalles : Solo caracteres alphanumericos (incluyendo espacios).");

        //Longitud
        if ((tb_init_longitud.Text.Length == 0) || (tb_init_longitud.Text.Length > 20)) errorList.Add("Longitud : Tamaño incorrecto [20] maximo.");
        if (!rFloat.IsMatch(tb_init_longitud.Text)) errorList.Add("Longitud : (-)XX.YY");

        //Latitud
        if ((tb_init_latitud.Text.Length == 0) || (tb_init_latitud.Text.Length > 20)) errorList.Add("Latitud : Tamaño incorrecto [20] maximo.");
        if (!rFloat.IsMatch(tb_init_latitud.Text)) errorList.Add("Latitud : (-)XX.YY");

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

    protected void outputInitErrors(List<string> errorList)
    {
        InitErrors.Text = "";
        foreach (string error in errorList)
        {
            InitErrors.Text += error;
            InitErrors.Text += " <br> ";
        }
    }

    private void displayFilteredHikes()
    {
        RegularUser regularUser = (RegularUser)Session["REG_USER"];
        if(regularUser.ListOfHikes != null)
        {
            hikeFilterContainer.InnerHtml = "";

            listOfLong = new List<double>();
            listOfLat = new List<double>();

            foreach (Hike h in regularUser.ListOfHikes)
            {
                hikeFilterContainer.Controls.Add(addLinkButton(h));
                listOfLong.Add(h.Longitud);
                listOfLat.Add(h.Latitud);
            }
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            sJSON = oSerializer.Serialize(listOfLong);
            sJSON2 = oSerializer.Serialize(listOfLat);
            numMarcadores = regularUser.ListOfHikes.Count();
        }
    }

    public LinkButton addLinkButton(Hike h)
    {
        LinkButton lbtn = new LinkButton();
        lbtn.Click += new System.EventHandler(selectHike);
        lbtn.Text = "<i class=\"fa fa-circle-thin\"></i> "+h.NameOfLocation+" | Latitud: "+h.Latitud.ToString()+" Longitud: "+h.Longitud.ToString();
        lbtn.CssClass = "list-group-item";
        lbtn.ID = h.HikeId.ToString();
        return lbtn;
    }
    protected void selectHike(object sender, EventArgs e)
    {
        HikeBusiness hb = new HikeBusiness();
        LinkButton lbtn = (LinkButton)sender;
        Hike hike = new Hike();
        hike.HikeId = Int32.Parse(lbtn.ID);
        hike = hb.loadHike(hike);
        Session["HIKE"] = hike;
        Response.Redirect("ShowHike.aspx");
    }
}