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

    public string insertWalkType(string newWalkType, int state)
    {
        int insertingError = walkTypeData.InsertWalkType(newWalkType, state);
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

    public string editWalkType(string oldWalkType,string newWalkType, int state)
    {
        deleteWalkType(oldWalkType);
        string errorText = insertWalkType(newWalkType, state);
        return errorText;
    }

    public void deleteWalkType(string walkType)
    {
        walkTypeData.deleteWalkType(walkType);
    }

    public List<string> getWalkTypes()
    {
        List<string> walkTypes = walkTypeData.getWalkTypes();
        return walkTypes;
    }
}