using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

public class IctConsultation:BaseData
{
    private int usersContributionSize=0;
    private string[,] usersContributionsMatrix;

    private int remunerationsReportSize = 0;
    private string[,] remunerationsReportMatrix;

    private int classificationOfRoutesSize = 0;
    private string[,] classificationOfRoutesMatrix;

    private int classificationOfRoutesSizeTemp = 0;
    private string[,] classificationOfRoutesMatrixTemp;


    public IctConsultation()
    {
        
    }

    public void loadClassificationOfRoutesTemp(string groupBy,string startDate,string endDate) {
        classificationOfRoutesSizeTemp = 0;
        classificationOfRoutesMatrixTemp = new string[10000, 3];

        SqlConnection connection = ManageDatabaseConnection("Open", "other");

        using (SqlCommand sqlCommand = new SqlCommand("getTableByParam", connection)) {
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@param", groupBy);
            sqlCommand.Parameters.AddWithValue("@startDate", startDate);
            sqlCommand.Parameters.AddWithValue("@endDate", endDate);

            using (SqlDataReader sqlReader = sqlCommand.ExecuteReader()) {
                while (sqlReader.Read()) {
                    classificationOfRoutesMatrixTemp[classificationOfRoutesSizeTemp,0]= sqlReader["cantidadPuntos"].ToString();
                    classificationOfRoutesMatrixTemp[classificationOfRoutesSizeTemp,1]= sqlReader["likes"].ToString();
                    classificationOfRoutesMatrixTemp[classificationOfRoutesSizeTemp,2]= sqlReader[groupBy].ToString();

                    classificationOfRoutesSizeTemp++;
                }
            }

        }


            ManageDatabaseConnection("Close", "other");
    }

    public void LoadClassificationOfRoutes() {
        classificationOfRoutesSize = 0;
        classificationOfRoutesMatrix = new string[10000, 3];

        SqlConnection connection = ManageDatabaseConnection("Open", "other");

        using (SqlCommand sqlCommand = new SqlCommand("ClassificationOfRoutes", connection)) {
            sqlCommand.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader sqlReader = sqlCommand.ExecuteReader()) {
                while (sqlReader.Read()) {

                    classificationOfRoutesMatrix[classificationOfRoutesSize, 0] = sqlReader["caminata"].ToString();
                    classificationOfRoutesMatrix[classificationOfRoutesSize, 1] = sqlReader["cantidadRutas"].ToString();
                    classificationOfRoutesMatrix[classificationOfRoutesSize, 2] = sqlReader["likes"].ToString();

                    classificationOfRoutesSize++;

                }
            }

        }
        ManageDatabaseConnection("Close", "other");
    }

    public void loadRemunerationsReport(int top,string startDate,string endDate){
        remunerationsReportSize = 0;
        remunerationsReportMatrix= new string[10000, 3];

        SqlConnection connection = ManageDatabaseConnection("Open", "other");

        using (SqlCommand sqlCommand = new SqlCommand("remunerationsReport", connection)) {
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@top", top);
            sqlCommand.Parameters.AddWithValue("@startDateP", startDate);
            sqlCommand.Parameters.AddWithValue("@endDateP", endDate);

            using (SqlDataReader sqlReader = sqlCommand.ExecuteReader()) {

                while (sqlReader.Read()) {
                    remunerationsReportMatrix[remunerationsReportSize,0]= sqlReader["nombreCompleto"].ToString();
                    remunerationsReportMatrix[remunerationsReportSize,1] = sqlReader["idUsuario"].ToString();
                    remunerationsReportMatrix[remunerationsReportSize,2] = sqlReader["total"].ToString();

                    remunerationsReportSize++;
                }
            }

        }
        ManageDatabaseConnection("Close", "other");
    }

    public void loadUserContributions(string parameterName,string parameterLastName, string parameterAcsOdes, string parameterOrderBy) {
        usersContributionSize = 0;
        usersContributionsMatrix = new string[10000, 7];

        SqlConnection connection = ManageDatabaseConnection("Open","other");

        using (SqlCommand sqlCommand = new SqlCommand("usersForContributions", connection))
        {

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@primerNombre", parameterName);
            sqlCommand.Parameters.AddWithValue("@primerApellido", parameterLastName);
            sqlCommand.Parameters.AddWithValue("@ascOdes", parameterAcsOdes);
            sqlCommand.Parameters.AddWithValue("@orderBy", parameterOrderBy);

            using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
            {
                
                while (sqlReader.Read())
                {
                    usersContributionsMatrix[usersContributionSize, 0] = sqlReader["primerNombre"].ToString();
                    usersContributionsMatrix[usersContributionSize, 1] = sqlReader["primerApellido"].ToString();
                    usersContributionsMatrix[usersContributionSize, 2] = sqlReader["correo"].ToString();
                    usersContributionsMatrix[usersContributionSize, 3] = sqlReader["telefono"].ToString();
                    usersContributionsMatrix[usersContributionSize, 4] = sqlReader["cantidadCaminatas"].ToString();
                    usersContributionsMatrix[usersContributionSize, 5] = sqlReader["cantidadPuntos"].ToString();
                    usersContributionsMatrix[usersContributionSize, 6] = sqlReader["likes"].ToString();

                    usersContributionSize++;
                }


            }
       }

        ManageDatabaseConnection("Close", "other");

    }


    public int getSizeUsersContributionsMatrix() {
        return usersContributionSize;
    }

    public string getMatrixUsersContributionsElement(int row,int columm) {
        return usersContributionsMatrix[row,columm];
    }

    public string getRemunerationsReportMatrixElement(int row, int column) {
        return remunerationsReportMatrix[row, column];
    }

    public int getRemunerationsReportSize() {
        return remunerationsReportSize;
    }

    public string getUsersForContributionsMatrix(int row, int column)
    {
        return classificationOfRoutesMatrix[row, column];
    }

    public int getUsersForContributionsSize()
    {
        return classificationOfRoutesSize;
    }

    public string getClassificationOfRoutesMatrixTemp(int row, int column)
    {
        return classificationOfRoutesMatrixTemp[row, column];
    }

    public int getClassificationOfRoutesSizeTemp()
    {
        return classificationOfRoutesSizeTemp;
    }



}