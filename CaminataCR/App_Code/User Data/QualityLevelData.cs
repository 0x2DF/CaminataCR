using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

/// <summary>
/// Descripción breve de QualityLevelData
/// </summary>
public class QualityLevelData : BaseData
{
    public QualityLevelData()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public List<string> getQualityLevels()
    {
        List<string> dificultyLevels = new List<string>();
        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open", "admin");

            using (SqlCommand sqlCommand = new SqlCommand("dbo.getQualityLevels", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        string walk = sqlReader["nivelDeCalidad"].ToString();
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

    public int InsertQualityLevel(string qualityLevel, int state)
    {
        int error = 0;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", ("admin"));
            using (SqlCommand sqlCommand = new SqlCommand("dbo.addQualityLevel", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@qualityLevel", qualityLevel);
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

    public void deleteQualityLevel(string qualitylevel)
    {
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", ("admin"));
            using (SqlCommand sqlCommand = new SqlCommand("dbo.deleteQualityLevel", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@quality", qualitylevel);

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