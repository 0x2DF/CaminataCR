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
            using (SqlCommand sqlCommand = new SqlCommand("addUsuario", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@nombreDeCuenta", regularUser.Account);
                sqlCommand.Parameters.AddWithValue("@contrasena", regularUser.Password);
                sqlCommand.Parameters.AddWithValue("@rol", regularUser.RoleId);
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
            using (SqlCommand sqlCommand = new SqlCommand("addCliente", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@nombreDeCuenta", regularUser.Account);

                if (regularUser.ProfilePicture == null)
                {
                    sqlCommand.Parameters.AddWithValue("@fotografia", DBNull.Value);
                }
                else
                {
                    //sqlCommand.Parameters.AddWithValue("@fotografia", regularUser.ProfilePicture);
                    sqlCommand.Parameters.Add("@fotografia", SqlDbType.VarBinary).Value = regularUser.ProfilePicture;
                }

                sqlCommand.Parameters.AddWithValue("@primerNombre", regularUser.FirstName);

                if (regularUser.MiddleName == null)
                {
                    sqlCommand.Parameters.AddWithValue("@segundoNombre", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@segundoNombre", regularUser.MiddleName);
                }

                sqlCommand.Parameters.AddWithValue("@primerApellido", regularUser.Surname);
                sqlCommand.Parameters.AddWithValue("@segundoApellido", regularUser.SecondSurname);
                sqlCommand.Parameters.AddWithValue("@correo", regularUser.Email);
                sqlCommand.Parameters.AddWithValue("@telefono", regularUser.TelephoneNumber);
                sqlCommand.Parameters.AddWithValue("@fechaNacimiento", regularUser.Birthdate);
                sqlCommand.Parameters.AddWithValue("@sexo", regularUser.Sex);
                sqlCommand.Parameters.AddWithValue("@nacionalidad", regularUser.Nacionality);
                sqlCommand.Parameters.AddWithValue("@cuentaBancaria", regularUser.BankAccount);
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
        int result = -1;
        string query = "SELECT COUNT(*) FROM Usuarios WHERE nombreDeCuenta = '" + regularUser.Account + "'";
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.CommandType = CommandType.Text;

                result = Convert.ToInt32(sqlCommand.ExecuteScalar());
            }
            connection.Close();
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        if (result == 0)
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
        int result = -1;
        string query = "SELECT COUNT(correo) FROM Usuarios WHERE correo = '" + regularUser.Email + "'";
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.CommandType = CommandType.Text;

                result = Convert.ToInt32(sqlCommand.ExecuteScalar());
            }
            connection.Close();
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        if (result == 0)
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