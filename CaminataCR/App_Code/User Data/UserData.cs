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
            SqlConnection connection = ManageDatabaseConnection("Open", (user.RoleId == 3 ? "regular" : (user.RoleId == 2 ? "ICT" : "admin")) );
            using (SqlCommand sqlCommand = new SqlCommand("dbo.checkUser", connection))
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
            ManageDatabaseConnection("Close", (user.RoleId == 3 ? "regular" : (user.RoleId == 2 ? "ICT" : "admin")));

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
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

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

            ManageDatabaseConnection("Close", "regular");

        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        return regularUser;
    }

    public string InsertUser(User user)
    {
        string responseMessage = "-------------------------------------------------------------------";
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", (user.RoleId == 3 ? "regular" : (user.RoleId == 2 ? "ICT" : "admin")));
            using (SqlCommand sqlCommand = new SqlCommand("addUser", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@account", user.Account);
                sqlCommand.Parameters.AddWithValue("@password", user.Password);
                sqlCommand.Parameters.AddWithValue("@roleId", user.RoleId);
                var returnParameter = sqlCommand.Parameters.AddWithValue("@responseMessage", responseMessage);

                //var returnParameter = sqlCommand.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                returnParameter.Direction = ParameterDirection.InputOutput;
                sqlCommand.ExecuteNonQuery();
                responseMessage = Convert.IsDBNull(returnParameter.Value) ? null : returnParameter.Value.ToString();
            }
            ManageDatabaseConnection("Close", (user.RoleId == 3 ? "regular" : (user.RoleId == 2 ? "ICT" : "admin")));
        }
        catch (SqlException sqlException)
        {

            throw sqlException;
        }
        return responseMessage;
    }

    public string InsertUser(RegularUser regularUser)
    {
        string responseMessage = "-------------------------------------------------------------------";
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
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
            ManageDatabaseConnection("Close", "regular");
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
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
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
            ManageDatabaseConnection("Close", "regular");
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
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("checkUsername", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@account", regularUser.Account);

                var returnParameter = sqlCommand.Parameters.Add("@errorId", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery();
                errorId = Convert.ToInt32(returnParameter.Value);

            }
            ManageDatabaseConnection("Close", "regular");

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
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("checkEmail", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@email", regularUser.Email);

                var returnParameter = sqlCommand.Parameters.Add("@errorId", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery();
                errorId = Convert.ToInt32(returnParameter.Value);

            }
            ManageDatabaseConnection("Close", "regular");

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
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getFriendsFromUser", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", regularUser.UserId);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        RegularUser friend = new RegularUser();
                        
                        friend.UserId = (int)sqlReader["idUsuario"];
                        friend.FirstName = sqlReader["primerNombre"].ToString();
                        friend.MiddleName = sqlReader["segundoNombre"].ToString();
                        friend.Surname = sqlReader["primerApellido"].ToString();
                        friend.SecondSurname = sqlReader["segundoApellido"].ToString();
                        if (Convert.IsDBNull(sqlReader["fotografia"]))
                        {
                            friend.ProfilePicture = null;
                        }
                        else
                        {
                            friend.ProfilePicture = (byte[])sqlReader["fotografia"];
                        }
                        listOfFriends.Add(friend);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        return listOfFriends;
    }

    public List<RegularUser> LoadListOfUsersNotFriends(RegularUser regularUser, string name)
    {
        List<RegularUser> listOfUsers = new List<RegularUser>();

        try
        {
            //open database connection
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");

            using (SqlCommand sqlCommand = new SqlCommand("getNotFriendsFromUser", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", regularUser.UserId);
                sqlCommand.Parameters.AddWithValue("@nombre", name);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        RegularUser user = new RegularUser();

                        user.UserId = (int)sqlReader["idUsuario"];
                        user.FirstName = sqlReader["primerNombre"].ToString();
                        user.MiddleName = sqlReader["segundoNombre"].ToString();
                        user.Surname = sqlReader["primerApellido"].ToString();
                        user.SecondSurname = sqlReader["segundoApellido"].ToString();
                        if (Convert.IsDBNull(sqlReader["fotografia"]))
                        {
                            user.ProfilePicture = null;
                        }
                        else
                        {
                            user.ProfilePicture = (byte[])sqlReader["fotografia"];
                        }
                        listOfUsers.Add(user);
                    }
                }
            }
            ManageDatabaseConnection("Close", "regular");
        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        return listOfUsers;
    }

    public int InsertFriend(RegularUser user, RegularUser friend)
    {
        int resultID;

        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("addFriend", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", user.UserId);
                sqlCommand.Parameters.AddWithValue("@FriendId", friend.UserId);

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
    public int RemoveFriend(RegularUser user, RegularUser friend)
    {
        int resultID;

        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "regular");
            using (SqlCommand sqlCommand = new SqlCommand("removeFriend", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", user.UserId);
                sqlCommand.Parameters.AddWithValue("@FriendId", friend.UserId);

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

    public List<User> getRegularUsers()
    {
        List<User> regularUsers = new List<User>();
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "admin");
            using (SqlCommand sqlCommand = new SqlCommand("getRegularUsers", connection))
            {
                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        User user = new User();

                        user.Account = sqlReader["cuenta"].ToString();
                        user.State = (bool)sqlReader["activo"];

                        regularUsers.Add(user);
                    }
                }
            }
            ManageDatabaseConnection("Close", "admin");

        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        } 

        return regularUsers;
    }

    public void editRegularUser(string userName, int state)
    {
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "admin");
            using (SqlCommand sqlCommand = new SqlCommand("editRegularUser", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@account", userName);
                sqlCommand.Parameters.AddWithValue("@state", state);                

                sqlCommand.ExecuteNonQuery();
            }
            ManageDatabaseConnection("Close", "admin");

        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }
    }

    public List<User> getUsers(int roleId, string userName)
    {
        List<User> users = new List<User>();
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", "admin");
            using (SqlCommand sqlCommand = new SqlCommand("getusers", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@roleId", roleId);
                sqlCommand.Parameters.AddWithValue("@account", userName);

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {                    
                    while (sqlReader.Read())
                    {
                        User user = new User();

                        user.Account = sqlReader["cuenta"].ToString();

                        users.Add(user);
                    }
                }
            }
            ManageDatabaseConnection("Close", "admin");

        }
        catch (SqlException sqlException)
        {
            throw sqlException;
        }

        return users;
    }

    

       
    public int editUser(string newUserName, string oldUserName)
    {
        int error = 0;
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", ("admin"));
            using (SqlCommand sqlCommand = new SqlCommand("dbo.edituser", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@oldUsername", oldUserName);
                sqlCommand.Parameters.AddWithValue("@newUserName", newUserName);

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

    public void deletUser(string userName)
    {
        try
        {
            SqlConnection connection = ManageDatabaseConnection("Open", ("admin"));
            using (SqlCommand sqlCommand = new SqlCommand("dbo.deleteuser", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@userName", userName);

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