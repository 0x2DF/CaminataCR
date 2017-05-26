using System.Data.SqlClient;


public class BaseData
{
     //method to open or close the database connection
    public SqlConnection ManageDatabaseConnection(string actionToPerform)
    {
        string connectionString = "Data Source=0X2F-\\SQLEXPRESS;Initial Catalog=caminataCR;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection sqlConnection = new SqlConnection(connectionString);

        try
        {
            //desicion to whether open or close the database connection
            if (actionToPerform.Equals("Open"))
            {
                sqlConnection.Open();
            }
            else
            {
                sqlConnection.Close();
            }

        }
        catch (SqlException sqlException)
        {

            //throw the exception to upper layers
            throw sqlException;
        }

        return sqlConnection;

    }
}