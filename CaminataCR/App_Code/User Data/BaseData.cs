using System.Data.SqlClient;


public class BaseData
{
     //method to open or close the database connection
    public SqlConnection ManageDatabaseConnection(string actionToPerform, string userType)
    {
        string connectionString;
        if (userType == "admin")
        {
            connectionString = "Data Source=DESKTOP-3VGUL7E;Integrated Security=False;User ID=admin;Password=admin;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=caminataCR";
        }else if (userType == "ICT")
        {
            connectionString = "Data Source=0X2F-\\SQLEXPRESS;Integrated Security=False;User ID=ICT;Password=ICT;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database = caminataCR";
        }
        else if (userType == "regular")
        {
            connectionString = "Data Source=0X2F-\\SQLEXPRESS;Integrated Security=False;User ID=regular;Password=regular;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database = caminataCR";
        }
        else
        {
            //connectionString = "Data Source=0X2F-\\SQLEXPRESS;Initial Catalog=caminataCR;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            connectionString = "error";
        }
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