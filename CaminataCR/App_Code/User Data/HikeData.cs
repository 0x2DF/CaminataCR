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
            SqlConnection connection = ManageDatabaseConnection("Open");

            using (SqlCommand sqlCommand = new SqlCommand("getReviewsFromUser", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idUsuario", user.IdUsuario);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        Review review = new Review();
                        review.IdReview = (int)sqlReader["idReview"];
                        review.CalificacionCalidad = Convert.ToInt32(sqlReader["calificacionCalidad"]);
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
                        listOfReviews.Add(review);
                    }
                }
            }
            ManageDatabaseConnection("Close");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        return listOfHikes;
    }

    public void InsertHike(Hike hike, RegularUser regularUser)
    {
        SqlConnection connection = ManageDatabaseConnection("Open");
        using (SqlCommand sqlCommand = new SqlCommand("addReviews", connection))
        {
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@descrip", review.Descripcion);
            sqlCommand.Parameters.AddWithValue("@calificacionPre", review.CalificacionPrecio.ToString());
            sqlCommand.Parameters.AddWithValue("@calficacionCali", review.CalificacionCalidad.ToString());
            sqlCommand.Parameters.AddWithValue("@idLoc", review.Local.IdLocal);
            sqlCommand.Parameters.AddWithValue("@idUsua", user.IdUsuario);
            sqlCommand.Parameters.AddWithValue("@date", DateTime.Today);
            sqlCommand.ExecuteNonQuery();
        }
        ManageDatabaseConnection("Close");
    }

}