using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PriceLevelBusiness
/// </summary>
public class PriceLevelBusiness
{
    PriceLevelData priceData = new PriceLevelData();
    public PriceLevelBusiness()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }  
  
    public List<string> getPriceLevels()
    {
        List<string> priceLevels = priceData.getPriceLevels();
        return priceLevels;
    }

    public string insertPriceLevel(string newPriceLevel, int state)
    {
        int insertingError = priceData.InsertPriceLevel(newPriceLevel, state);
        string errorText = "";
        switch (insertingError)
        {
            case 0:
                errorText = null;
                break;
            case 1:
                errorText = "Este nivel de precio ya existe";
                break;
            default:
                errorText = "Hay un error no previsto. Contacte a un administrador";
                break;
        }
        return errorText;
    }

    public void deletePriceLevel(string pricelevel)
    {
        priceData.deletePriceLevel(pricelevel);
    }

    public string editPriceLevel(string oldPriceLevel, string newPriceLevel, int state)
    {
        priceData.deletePriceLevel(oldPriceLevel);
        string error = insertPriceLevel(newPriceLevel, state);
        return error;
    }
}