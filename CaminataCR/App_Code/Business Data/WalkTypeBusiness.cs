using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de WalkTypeBusiness
/// </summary>
public class WalkTypeBusiness
{
    WalkTypeData walkTypeData = new WalkTypeData();
    public WalkTypeBusiness()
    {
    }

    public string insertWalkType(string newWalkType)
    {
        int insertingError = walkTypeData.InsertWalkType(newWalkType);
        string errorText = "";
        switch (insertingError)
        {
            case 0:
                errorText = null;
                break;
            case 1:
                errorText = "Este tipo de caminata ya existe";
                break;            
            default:
                errorText = "Hay un error no previsto. Contacte a un administrador";
                break;
        }
        return errorText;
    }

    public List<string> getWalkTypes()
    {
        List<string> walkTypes = walkTypeData.getWalkTypes();
        return walkTypes;
    }
}