using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class QueryBusiness
{
    QueryData queryData = new QueryData();

    public QueryBusiness()
    {
    }

    public List<Query> ReadQuery(Query query)
    {
        return queryData.LoadListOfQueries(WriteMessage(query));
    }

    public string WriteMessage(Query query)
    {
        string message = "SELECT * FROM Bitacora B";
        int counter = 0;

        if(!query.All)
        {
            message += " WHERE ";

            if(query.StartDate != "")
            {
                message += "B.fechaHora BETWEEN '" + query.StartDate + "' AND '" + query.FinalDate + "'";
                counter++;
            }

            if(query.Time != "")
            {
                if(counter != 0) { message += " AND "; }

                message += "B.fechaHora = '" + query.Time + "'";
                counter++;
            }


            if(query.Type != "")
            {
                if (counter != 0) { message += " AND "; }

                message += "B.tipoCambio = '" + query.Type + "'";
                counter++;
            }

            if(query.Table != "")
            {
                if (counter != 0) { message += " AND "; }

                message += "B.objeto = '" + query.Table + "'";
                counter++;
            }
        }

        return message;
    }
}