using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;


public class LikeData : BaseData
{
    public LikeData()
    {
    }
    public int InsertLike(RegularUser user, Hike hike)
    {
        int resultID;

        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("addLike", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", user.UserId);
                sqlCommand.Parameters.AddWithValue("@HikeId", hike.HikeId);

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
        //Debug.WriteLine(resultID);
        return resultID;
    }
    public int RemoveLike(RegularUser user, Hike hike)
    {
        int resultID;

        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("removeLike", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", user.UserId);
                sqlCommand.Parameters.AddWithValue("@HikeId", hike.HikeId);

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
        //Debug.WriteLine(resultID);
        return resultID;
    }
    public int getLikeStatus(RegularUser user, Hike hike)
    {
        int resultID;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("getLikeStatus", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", user.UserId);
                sqlCommand.Parameters.AddWithValue("@HikeId", hike.HikeId);

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
        //Debug.WriteLine(resultID);
        return resultID;
    }
}