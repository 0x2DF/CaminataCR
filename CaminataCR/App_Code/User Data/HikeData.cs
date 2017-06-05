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

    public List<Hike> LoadListOfHikes(RegularUser regularUser)
    {
        List<Hike> listOfHikes = new List<Hike>();
        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getReviewsFromUser", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idUsuario", regularUser.UserId);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        Hike hike = new Hike();
                        hike.HikeId = (int)sqlReader["idReview"];
                        /*review.CalificacionCalidad = Convert.ToInt32(sqlReader["calificacionCalidad"]);
                        review.CalificacionPrecio = Convert.ToInt32(sqlReader["calificacionPrecio"]);
                        review.Descripcion = sqlReader["descripcion"].ToString();
                        review.FechaHora = Convert.ToDateTime(sqlReader["fechaHora"]);
                        Branch local = new Branch();
                        local.Provincia = sqlReader["provincia"].ToString();
                        local.Canton = sqlReader["canton"].ToString();
                        local.Distrito = sqlReader["distrito"].ToString();
                        local.Detalle = sqlReader["detalle"].ToString();
                        local.Latitud = sqlReader.GetDouble(9);
                        local.Longitud = sqlReader.GetDouble(10);

                        if (Convert.IsDBNull(sqlReader["fotografiaLocal"]))
                        {
                            local.Fotografia = null;
                        }
                        else
                        {
                            local.Fotografia = (byte[])sqlReader["fotografiaLocal"];
                        }

                        review.Local = local;
                        Restaurant r = new Restaurant();
                        r.Nombre = sqlReader["nombre"].ToString();
                        if (Convert.IsDBNull(sqlReader["fotografiaRestaurante"]))
                        {
                            r.Fotografia = null;
                        }
                        else
                        {
                            r.Fotografia = (byte[])sqlReader["fotografiaRestaurante"];
                        }
                        r.TipoRestaurante = sqlReader["nombreTipoRestaurante"].ToString();
                        review.Restaurant = r;
                        listOfReviews.Add(review);*/
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
    public int InsertRoute(int idUserPerHike)
    {
        int resultID = 0;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("addRoute", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserPerHikeID", idUserPerHike);
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
                sqlCommand.Parameters.AddWithValue("@UserPerHikeID", idRoutePerUPC);
                //Point
                sqlCommand.Parameters.AddWithValue("@Latitud", p.Latitud);
                sqlCommand.Parameters.AddWithValue("@Longitud", p.Longitud);
                //PointPerRPUPC
                sqlCommand.Parameters.AddWithValue("@Pos", p.Pos);
                sqlCommand.Parameters.AddWithValue("@Pos", p.Comment);
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