using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

public class HikeData : BaseData
{
    public HikeData()
    {
    }

    //Loads hikes of the user in the past month
    public List<Hike> LoadListOfHikes(RegularUser regularUser)
    {
        List<Hike> listOfHikes = new List<Hike>();
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getHikesFromUser", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", regularUser.UserId);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        Hike hike = new Hike();
                        hike.NameOfLocation = sqlReader["nombreDelLugar"].ToString();
                        hike.Province = sqlReader["provincia"].ToString();
                        hike.Canton = sqlReader["canton"].ToString();
                        hike.District = sqlReader["distrito"].ToString();
                        hike.Details = sqlReader["detalle"].ToString();
                        hike.Latitud = sqlReader.GetDouble(5);
                        hike.Longitud = sqlReader.GetDouble(6);
                        hike.DateTime = Convert.ToDateTime(sqlReader["fechaHora"]);
                        hike.Quality = sqlReader["nivelDeCalidad"].ToString();
                        hike.Difficulty = sqlReader["nivelDeDificultad"].ToString();
                        hike.Price = sqlReader["nivelDePrecio"].ToString();
                        hike.HikeType = sqlReader["tipoDeCaminata"].ToString();
                        if (Convert.IsDBNull(sqlReader["fotografia"]))
                        {
                            hike.Image = null;
                        }
                        else
                        {
                            hike.Image = (byte[])sqlReader["fotografia"];
                        }
                        hike.Comment = sqlReader["comentario"].ToString();
                        hike.HikeId = (int)sqlReader["idUsuarioPorCaminata"];
                        hike.Route = new Route();
                        hike.Route.RouteId = (int)sqlReader["idRutaPorUPC"];
                        listOfHikes.Add(hike);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        return listOfHikes;
    }
    
    public Route LoadRoute(Hike hike)
    {
        List<Point> listOfPoints = new List<Point>();
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getPointsInUserRoute", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idRutaPorUPC", hike.Route.RouteId);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        Point p = new Point();
                        p.Latitud = sqlReader.GetDouble(0);
                        p.Longitud = sqlReader.GetDouble(1);
                        p.Pos = (int)sqlReader["posicion"];
                        p.Comment = sqlReader["comentario"].ToString();
                        if (Convert.IsDBNull(sqlReader["fotografia"]))
                        {
                            p.Image = null;
                        }
                        else
                        {
                            p.Image = (byte[])sqlReader["fotografia"];
                        }
                        listOfPoints.Add(p);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        hike.Route.ListOfPoints = listOfPoints;
        return hike.Route;
    }

    //Filter hikes
    public List<Hike> LoadListOfHikes(Hike hike, bool GPS)
    {
        List<Hike> listOfHikes = new List<Hike>();
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getFilteredHikes", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (hike.NameOfLocation != null) sqlCommand.Parameters.AddWithValue("@NameOfLocation", hike.NameOfLocation);
                if (hike.Province != null) sqlCommand.Parameters.AddWithValue("@Province", hike.Province);
                if (hike.Canton != null) sqlCommand.Parameters.AddWithValue("@Canton", hike.Canton);
                if (hike.District != null) sqlCommand.Parameters.AddWithValue("@District", hike.District);
                if (GPS) 
                {
                    sqlCommand.Parameters.AddWithValue("@Longitud", hike.Longitud);
                    sqlCommand.Parameters.AddWithValue("@Latitud", hike.Latitud);
                }
                if (hike.HikeType != null) sqlCommand.Parameters.AddWithValue("@HikeType", hike.HikeType);
                if (hike.Difficulty != null) sqlCommand.Parameters.AddWithValue("@Difficulty", hike.Difficulty);
                if (hike.Price != null) sqlCommand.Parameters.AddWithValue("@Price", hike.Price);
                if (hike.Quality != null) sqlCommand.Parameters.AddWithValue("@Quality", hike.Quality);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        Hike FilteredHike = new Hike();
                        FilteredHike.HikeId = (int)sqlReader["idCaminata"];
                        FilteredHike.NameOfLocation = sqlReader["nombreDelLugar"].ToString();
                        FilteredHike.Latitud = sqlReader.GetDouble(2);
                        FilteredHike.Longitud = sqlReader.GetDouble(3);
                        listOfHikes.Add(FilteredHike);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        return listOfHikes;
    }

    public Hike loadHikeInfo(Hike hike)
    {
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getHikeInfo", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idCaminata", hike.HikeId);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    if (sqlReader.Read())
                    {
                        hike.NameOfLocation = sqlReader["nombreDelLugar"].ToString();
                        hike.Province = sqlReader["provincia"].ToString();
                        hike.Canton = sqlReader["canton"].ToString();
                        hike.District = sqlReader["distrito"].ToString();
                        hike.Details = sqlReader["detalle"].ToString();
                        hike.Latitud = sqlReader.GetDouble(5);
                        hike.Longitud = sqlReader.GetDouble(6);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        return hike;
    }

    public Hike loadHikeInfo(Route route)
    {
        Hike hike = new Hike();
        hike.Route = route;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getHikeInfoWithRoute", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idRuta", route.RouteId);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    if (sqlReader.Read())
                    {
                        hike.HikeId = (int)sqlReader["idCaminata"];
                        hike.Latitud = sqlReader.GetDouble(1);
                        hike.Longitud = sqlReader.GetDouble(2);
                        hike.NameOfLocation = sqlReader["nombreDelLugar"].ToString();
                        hike.Province = sqlReader["provincia"].ToString();
                        hike.Canton = sqlReader["canton"].ToString();
                        hike.District = sqlReader["distrito"].ToString();
                        hike.Details = sqlReader["detalle"].ToString();
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        return hike;
    }

    public List<Route> loadRouteInfo(Hike hike)
    {
        List<Route> listOfRoutes = new List<Route>();
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getRouteInfo", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idCaminata", hike.HikeId);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        Route route = new Route();
                        route.RouteId = (int)sqlReader["idRuta"];
                        listOfRoutes.Add(route);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        return listOfRoutes;
    }

    public List<Point> loadPointInfo(Route route)
    {
        List<Point> listOfPoints = new List<Point>();
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getPointInfo", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idRuta", route.RouteId);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        Point point = new Point();
                        point.PointId = (int)sqlReader["idPuntosImportantes"];
                        point.Pos = (int)sqlReader["posicion"];
                        point.Longitud = sqlReader.GetDouble(2);
                        point.Latitud = sqlReader.GetDouble(3);
                        listOfPoints.Add(point);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        return listOfPoints;
    }

    public int InsertHike(ref Hike hike, ref RegularUser regularUser)
    {
        int resultID = 0;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("addHike", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //Usuario
                sqlCommand.Parameters.AddWithValue("@UserId", regularUser.UserId);
                //Caminata
                sqlCommand.Parameters.AddWithValue("@NameOfLocation", hike.NameOfLocation);
                sqlCommand.Parameters.AddWithValue("@Province", hike.Province);
                sqlCommand.Parameters.AddWithValue("@Canton", hike.Canton);
                sqlCommand.Parameters.AddWithValue("@District", hike.District);
                sqlCommand.Parameters.AddWithValue("@Details", hike.Details);
                sqlCommand.Parameters.AddWithValue("@Longitud", hike.Longitud);
                sqlCommand.Parameters.AddWithValue("@Latitud", hike.Latitud);
                //UsuarioPorCaminata
                sqlCommand.Parameters.AddWithValue("@HikeType", hike.HikeType);
                sqlCommand.Parameters.AddWithValue("@Price", hike.Price);
                sqlCommand.Parameters.AddWithValue("@Quality", hike.Quality);
                sqlCommand.Parameters.AddWithValue("@Difficulty", hike.Difficulty);
                if (hike.Image != null)
                {
                    sqlCommand.Parameters.Add("@image", SqlDbType.VarBinary).Value = hike.Image;
                }
                sqlCommand.Parameters.AddWithValue("@Comment", hike.Comment);

                var returnParameter = sqlCommand.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery();
                resultID = Convert.ToInt32(returnParameter.Value);
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {

            throw sqlException;
        }
        return resultID;
    }

    public int InsertHikeWithHikeId(ref Hike hike, ref RegularUser regularUser)
    {
        int resultID = 0;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("addHikeWithNewRoute", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //Usuario
                sqlCommand.Parameters.AddWithValue("@UserId", regularUser.UserId);
                //Caminata
                sqlCommand.Parameters.AddWithValue("@HikeId", hike.HikeId);
                sqlCommand.Parameters.AddWithValue("@NameOfLocation", hike.NameOfLocation);
                sqlCommand.Parameters.AddWithValue("@Province", hike.Province);
                sqlCommand.Parameters.AddWithValue("@Canton", hike.Canton);
                sqlCommand.Parameters.AddWithValue("@District", hike.District);
                sqlCommand.Parameters.AddWithValue("@Details", hike.Details);
                sqlCommand.Parameters.AddWithValue("@Longitud", hike.Longitud);
                sqlCommand.Parameters.AddWithValue("@Latitud", hike.Latitud);
                //UsuarioPorCaminata
                sqlCommand.Parameters.AddWithValue("@HikeType", hike.HikeType);
                sqlCommand.Parameters.AddWithValue("@Price", hike.Price);
                sqlCommand.Parameters.AddWithValue("@Quality", hike.Quality);
                sqlCommand.Parameters.AddWithValue("@Difficulty", hike.Difficulty);
                if (hike.Image != null)
                {
                    sqlCommand.Parameters.Add("@image", SqlDbType.VarBinary).Value = hike.Image;
                }
                sqlCommand.Parameters.AddWithValue("@Comment", hike.Comment);

                var returnParameter = sqlCommand.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery();
                resultID = Convert.ToInt32(returnParameter.Value);
            }
            ManageDatabaseConnection("Close", "regular");
        }

        catch (SqlException sqlException)
        {

            throw sqlException;
        }
        return resultID;
    }
    public int InsertRoute(int idUserPerHike)
    {
        int resultID = 0;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("addRoute", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserPerHikeId", idUserPerHike);
                var returnParameter = sqlCommand.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery();
                resultID = Convert.ToInt32(returnParameter.Value);
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {

            throw sqlException;
        }
        return resultID;
    }
    public int InsertRouteWithRouteId(int idUserPerHike, int RouteId)
    {
        int resultID = 0;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("addRouteWithRouteId", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserPerHikeId", idUserPerHike);
                sqlCommand.Parameters.AddWithValue("@RouteId", RouteId);
                var returnParameter = sqlCommand.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery();
                resultID = Convert.ToInt32(returnParameter.Value);
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {

            throw sqlException;
        }
        return resultID;
    }
    public int InsertPoint(Point p, int idRoutePerUPC)
    {
        int resultID = 0;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("addPoint", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idRutaPorUPC", idRoutePerUPC);
                //Point
                sqlCommand.Parameters.AddWithValue("@Latitud", p.Latitud);
                sqlCommand.Parameters.AddWithValue("@Longitud", p.Longitud);
                //PointPerRPUPC
                sqlCommand.Parameters.AddWithValue("@Pos", p.Pos);
                sqlCommand.Parameters.AddWithValue("@Comment", p.Comment);
                if (p.Image != null)
                {
                    sqlCommand.Parameters.Add("@Image", SqlDbType.VarBinary).Value = p.Image;
                }

                var returnParameter = sqlCommand.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery();
                resultID = Convert.ToInt32(returnParameter.Value);
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {

            throw sqlException;
        }
        return resultID;
    }
    public int InsertPointWithPointId(Point p, int idRoutePerUPC)
    {
        int resultID = 0;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("addPointWithPointId", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idRutaPorUPC", idRoutePerUPC);
                sqlCommand.Parameters.AddWithValue("@idPuntosImportantes", p.PointId);
                //PointPerRPUPC
                sqlCommand.Parameters.AddWithValue("@Pos", p.Pos);
                sqlCommand.Parameters.AddWithValue("@Comment", p.Comment);
                if (p.Image != null)
                {
                    sqlCommand.Parameters.Add("@Image", SqlDbType.VarBinary).Value = p.Image;
                }

                var returnParameter = sqlCommand.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery();
                resultID = Convert.ToInt32(returnParameter.Value);
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {

            throw sqlException;
        }
        return resultID;
    }

    public List<string> LoadListOfProvinces()
    {
        List<string> listOfProvinces = new List<string>();
        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getProvinces", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        string province = sqlReader["provincia"].ToString();
                        listOfProvinces.Add(province);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        return listOfProvinces;
    }

    public List<string> LoadListOfCantons(string province)
    {
        List<string> listOfCantons = new List<string>();
        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getCantons", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@province", province);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        string canton = sqlReader["canton"].ToString();
                        listOfCantons.Add(canton);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        return listOfCantons;
    }

    public List<string> LoadListOfDistricts(string canton)
    {
        List<string> listOfDistricts = new List<string>();
        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getDistricts", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@canton", canton);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        string district = sqlReader["distrito"].ToString();
                        listOfDistricts.Add(district);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        return listOfDistricts;
    }

    public List<string> LoadListOfHikeTypes()
    {
        List<string> listOfHikeTypes = new List<string>();
        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getHikeTypes", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        string hikeType = sqlReader["tipoDeCaminata"].ToString();
                        listOfHikeTypes.Add(hikeType);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        return listOfHikeTypes;
    }
    public List<string> LoadListOfDifficultyLevels()
    {
        List<string> listOfDifficultyLevels = new List<string>();
        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getDifficultyLevels", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        string difficulty = sqlReader["nivelDeDificultad"].ToString();
                        listOfDifficultyLevels.Add(difficulty);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        return listOfDifficultyLevels;
    }
    public List<string> LoadListOfQualityLevels()
    {
        List<string> listOfQualityLevels = new List<string>();
        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getQualityLevels", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        string quality = sqlReader["nivelDeCalidad"].ToString();
                        listOfQualityLevels.Add(quality);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        return listOfQualityLevels;
    }
    public List<string> LoadListOfPriceLevels()
    {
        List<string> listOfPriceLevels = new List<string>();
        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getPriceLevels", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        string price = sqlReader["nivelDePrecio"].ToString();
                        listOfPriceLevels.Add(price);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        return listOfPriceLevels;
    }
}