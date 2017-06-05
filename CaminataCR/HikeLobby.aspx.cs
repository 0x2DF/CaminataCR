using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class HikeLobby : System.Web.UI.Page
{
    protected string sJSON;
    protected string sJSON2;
    protected int numMarcadores = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["REG_USER"] == null)
        {
            Response.Redirect("SignInRegular.aspx");
        }
        else
        {
            RegularUser regularUser = (RegularUser)Session["REG_USER"];
            LoggedInUsername.Text = regularUser.Account;
        }

        if(!IsPostBack)
        {
            if (dd_filter_province.Items.Count == 0)
            {
                FillDropDownLists();
            }
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
        dd_filter_difficultylevel.Items.Clear();
        foreach (string p in QualityLevelList)
        {
            dd_filter_difficultylevel.Items.Add(p);
        }

        List<string> PriceLevelList = hb.getPriceLevels();
        dd_filter_difficultylevel.Items.Clear();
        foreach (string p in PriceLevelList)
        {
            dd_filter_difficultylevel.Items.Add(p);
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
            outputErrors(errorList);
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
    protected void outputErrors(List<string> errorList)
    {
        foreach (string error in errorList)
        {
            Errors.Text += error;
            Errors.Text += " <br> ";
        }
    }
}