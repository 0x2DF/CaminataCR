using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de QueryData
/// </summary>
public class QueryData : BaseData
{
    public QueryData()
    {
    }

    public List<Query> LoadListOfQueries(string message)
    {
        List<Query> listOfQueries = new List<Query>();
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "admin");

            using (SqlCommand sqlCommand = new SqlCommand(message, connection))
            {
                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        Query query = new Query();
                        query.Date = sqlReader["fechaHora"].ToString();
                        query.Description = sqlReader["descripcion"].ToString();
                        query.User = sqlReader["idUsuario"].ToString();
                        query.Type = sqlReader["tipoCambio"].ToString();
                        query.Table = sqlReader["objeto"].ToString();
                        listOfQueries.Add(query);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        return listOfQueries;
    }
}