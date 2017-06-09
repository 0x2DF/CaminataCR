using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

/// <summary>
/// Descripción breve de DificultyLevel
/// </summary>
public class DificultyLevelData : BaseData
{
    public DificultyLevelData()
    {
       
    }

    public List<string> getDificultyLevels()
    {
        List<string> dificultyLevels = new List<string>();
        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open", "admin");

            using (SqlCommand sqlCommand = new SqlCommand("dbo.getDificultyLevels", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        string walk = sqlReader["nivelDeDificultad"].ToString();
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

    public int InsertWalkType(string dificultyLevel, int state)
    {
        int error = 0;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", ("admin"));
            using (SqlCommand sqlCommand = new SqlCommand("dbo.addDificultyLevel", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@dificultyLevel", dificultyLevel);
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

    public void deleteDificultyLevel(string dificultylevel)
    {
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", ("admin"));
            using (SqlCommand sqlCommand = new SqlCommand("dbo.deleteDificultyLevel", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@dificulty", dificultylevel);

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