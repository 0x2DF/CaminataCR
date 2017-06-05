using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;


public class DonationData : BaseData
{
    public DonationData()
    {
    }

    public string InsertDonation(Donation donation)
    {
        string responseMessage = "-------------------------------------------------------------------";
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("addDonation", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@amount", donation.Amount);
                var returnParameter = sqlCommand.Parameters.AddWithValue("@responseMessage", responseMessage);

                //var returnParameter = sqlCommand.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                returnParameter.Direction = ParameterDirection.InputOutput;
                sqlCommand.ExecuteNonQuery();
                responseMessage = Convert.IsDBNull(returnParameter.Value) ? null : returnParameter.Value.ToString();
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {

            throw sqlException;
        }
        return responseMessage;
    }
}