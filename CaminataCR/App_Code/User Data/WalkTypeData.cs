using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

/// <summary>
/// Descripción breve de WalkTypeData
/// </summary>
public class WalkTypeData : BaseData
{
    
    public WalkTypeData()
    {

    }

    public int InsertWalkType(string walkType, int state)
    {
        int error = 0;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", ("admin"));
            using (SqlCommand sqlCommand = new SqlCommand("dbo.addWalkType", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@walkType", walkType);
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

    public List<string> getWalkTypes()
    {
        List<string> walkTypes = new List<string>();
        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open","admin");

            using (SqlCommand sqlCommand = new SqlCommand("getWalkTypes", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        string walk = sqlReader["tipoDeCaminata"].ToString();
                        walkTypes.Add(walk);
                    }
                }
            }
            ManageDatabaseConnection("Close","admin");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        return walkTypes;
    }

    public void deleteWalkType(string walkType)
    {
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", ("admin"));
            using (SqlCommand sqlCommand = new SqlCommand("dbo.deleteWalkType", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@walkType", walkType);              

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