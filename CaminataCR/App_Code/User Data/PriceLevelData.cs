using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

/// <summary>
/// Descripción breve de PriceLevelData
/// </summary>
public class PriceLevelData : BaseData
{
    public PriceLevelData()
    {
    }

    public List<string> getPriceLevels()
    {
        List<string> dificultyLevels = new List<string>();
        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open", "admin");

            using (SqlCommand sqlCommand = new SqlCommand("dbo.getPriceLevels", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        string walk = sqlReader["nivelDePrecio"].ToString();
                        dificultyLevels.Add(walk);
                    }
                }
            }
            ManageDatabaseConnection("Close", "admin");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        return dificultyLevels;
    }

    public int InsertPriceLevel(string priceLevel, int state)
    {
        int error = 0;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", ("admin"));
            using (SqlCommand sqlCommand = new SqlCommand("dbo.addPriceLevel", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@priceLevel", priceLevel);
                sqlCommand.Parameters.AddWithValue("@state", state);

                var returnParameter = sqlCommand.Parameters.Add("@error", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery();
                error = Convert.ToInt32(returnParameter.Value);

            }
            ManageDatabaseConnection("Close", ("admin"));

        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        return error;
    }

    public void deletePriceLevel(string pricelevel)
    {
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", ("admin"));
            using (SqlCommand sqlCommand = new SqlCommand("dbo.deletePriceLevel", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@price", pricelevel);

                sqlCommand.ExecuteNonQuery();

            }
            ManageDatabaseConnection("Close", ("admin"));

        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
    }
}