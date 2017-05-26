using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

public class UserData : BaseData
{
    public UserData()
    {
    }

    public int ValidateCredentials(User user)
    {
        int errorId = 0;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand sqlCommand = new SqlCommand("checkUser", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@account", user.Account);
                sqlCommand.Parameters.AddWithValue("@password", user.Password);
                sqlCommand.Parameters.AddWithValue("@roleId", user.RoleId);

                var returnParameter = sqlCommand.Parameters.Add("@errorId", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery();
                errorId = Convert.ToInt32(returnParameter.Value);

            }
            ManageDatabaseConnection("Close");

        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        return errorId;
    }

    public RegularUser GetRegularUser(User user)
    {
        RegularUser regularUser = new RegularUser();
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open");

            using (SqlCommand sqlCommand = new SqlCommand("getUser", connection))
            {

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@account", user.Account);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    if (sqlReader.Read())
                    {
                        regularUser.UserId = (int)sqlReader["idUsuario"];
                        regularUser.FirstName = sqlReader["primerNombre"].ToString();
                        if (sqlReader["segundoNombre"] != null)
                        {
                            regularUser.MiddleName = sqlReader["segundoNombre"].ToString();
                        }
                        regularUser.Surname = sqlReader["primerApellido"].ToString();
                        regularUser.SecondSurname = sqlReader["segundoApellido"].ToString();
                        regularUser.BankAccount = sqlReader["cuentaBancaria"].ToString();

                        //fotografia
                        if (Convert.IsDBNull(sqlReader["fotografia"]))
                        {
                            regularUser.ProfilePicture = null;
                        }
                        else
                        {
                            regularUser.ProfilePicture = (byte[])sqlReader["fotografia"];
                        }
                    }
                    sqlReader.Close();

                }

            }

            ManageDatabaseConnection("Close");

        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        return regularUser;
    }

    public string InsertUser(RegularUser regularUser)
    {
        string responseMessage = "-------------------------------------------------------------------";
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand sqlCommand = new SqlCommand("addUser", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@account", regularUser.Account);
                sqlCommand.Parameters.AddWithValue("@password", regularUser.Password);
                sqlCommand.Parameters.AddWithValue("@roleId", regularUser.RoleId);
                var returnParameter = sqlCommand.Parameters.AddWithValue("@responseMessage", responseMessage);

                //var returnParameter = sqlCommand.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                returnParameter.Direction = ParameterDirection.InputOutput;
                sqlCommand.ExecuteNonQuery();
                responseMessage = Convert.IsDBNull(returnParameter.Value) ? null : returnParameter.Value.ToString();
            }
            ManageDatabaseConnection("Close");
        }
        catch (SqlException sqlException)
        {

            throw sqlException;
        }
        return responseMessage;
    }

    public string InsertRegularUser(RegularUser regularUser)
    {
        string responseMessage = "------------------------------------------------------------------";
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand sqlCommand = new SqlCommand("addRegUser", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@account", regularUser.Account);

                if (regularUser.ProfilePicture == null)
                {
                    //sqlCommand.Parameters.AddWithValue("@photo", DBNull.Value);
                }
                else
                {
                    //sqlCommand.Parameters.AddWithValue("@fotografia", regularUser.ProfilePicture);
                    sqlCommand.Parameters.Add("@photo", SqlDbType.VarBinary).Value = regularUser.ProfilePicture;
                }

                sqlCommand.Parameters.AddWithValue("@FirstName", regularUser.FirstName);

                if (regularUser.MiddleName == null)
                {
                    sqlCommand.Parameters.AddWithValue("@MiddleName", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@MiddleName", regularUser.MiddleName);
                }

                sqlCommand.Parameters.AddWithValue("@Surname", regularUser.Surname);
                sqlCommand.Parameters.AddWithValue("@SecondSurname", regularUser.SecondSurname);
                sqlCommand.Parameters.AddWithValue("@Email", regularUser.Email);
                sqlCommand.Parameters.AddWithValue("@TelephoneNumber", regularUser.TelephoneNumber);
                sqlCommand.Parameters.AddWithValue("@Birthdate", regularUser.Birthdate.ToString("yyyy/MM/dd"));
                sqlCommand.Parameters.AddWithValue("@Sex", regularUser.Sex);
                sqlCommand.Parameters.AddWithValue("@Nacionality", regularUser.Nacionality);
                sqlCommand.Parameters.AddWithValue("@BankAccount", regularUser.BankAccount);
                var returnParameter = sqlCommand.Parameters.AddWithValue("@responseMessage", responseMessage);

                //var returnParameter = sqlCommand.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                returnParameter.Direction = ParameterDirection.InputOutput;
                sqlCommand.ExecuteNonQuery();
                responseMessage = Convert.IsDBNull(returnParameter.Value) ? null : returnParameter.Value.ToString();
            }
            ManageDatabaseConnection("Close");
        }
        catch (SqlException sqlException)
        {

            throw sqlException;
        }
        //Debug.WriteLine(responseMessage);
        return responseMessage;
    }

    public bool CheckUsername(RegularUser regularUser)
    {
        int errorId = -1;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand sqlCommand = new SqlCommand("checkUsername", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@account", regularUser.Account);

                var returnParameter = sqlCommand.Parameters.Add("@errorId", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery();
                errorId = Convert.ToInt32(returnParameter.Value);

            }
            ManageDatabaseConnection("Close");

        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        if (errorId == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckEmail(RegularUser regularUser)
    {
        int errorId = -1;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand sqlCommand = new SqlCommand("checkEmail", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@email", regularUser.Email);

                var returnParameter = sqlCommand.Parameters.Add("@errorId", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery();
                errorId = Convert.ToInt32(returnParameter.Value);

            }
            ManageDatabaseConnection("Close");

        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        if (errorId == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<RegularUser> LoadListOfFriends(RegularUser regularUser)
    {
        List<RegularUser> listOfFriends = new List<RegularUser>();

        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open");

            using (SqlCommand sqlCommand = new SqlCommand("getFriendsFromUser", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idUsuario", regularUser.UserId);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        User friend = new User();
                        /*
                        friend.IdUsuario = (int)sqlReader["idUsuario"];
                        friend.PrimerNombre = sqlReader["primerNombre"].ToString();
                        friend.SegundoNombre = sqlReader["segundoNombre"].ToString();
                        friend.PrimerApellido = sqlReader["primerApellido"].ToString();
                        friend.SegundoApellido = sqlReader["segundoApellido"].ToString();
                        if (Convert.IsDBNull(sqlReader["fotografia"]))
                        {
                            friend.Fotografia = null;
                        }
                        else
                        {
                            friend.Fotografia = (byte[])sqlReader["fotografia"];
                        }
                        listOfFriends.Add(friend);*/
                    }
                }
            }
            ManageDatabaseConnection("Close");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        return listOfFriends;
    }

    public int InsertFriend(RegularUser user, RegularUser friend)
    {
        int resultID;

        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand sqlCommand = new SqlCommand("addAmigo", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idUsuario", user.UserId);
                sqlCommand.Parameters.AddWithValue("@idAmigo", friend.UserId);

                var returnParameter = sqlCommand.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery();
                resultID = Convert.ToInt32(returnParameter.Value);
            }
            ManageDatabaseConnection("Close");

        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
        //Debug.WriteLine(resultID);
        return resultID;
    }
}