using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de DificultyLevelBusiness
/// </summary>
public class DificultyLevelBusiness
{
    DificultyLevelData dificultyData = new DificultyLevelData();
    public DificultyLevelBusiness()
    {

    }

    public List<string> getDificultyLevels()
    {
        List<string> dificultyLevels = dificultyData.getDificultyLevels();
        return dificultyLevels;
    }

    public string insertDificultyLevel(string newDificultyLevel, int state)
    {
        int insertingError = dificultyData.InsertDificultyLevel(newDificultyLevel, state);
        string errorText = "";
        switch (insertingError)
        {
            case 0:
                errorText = null;
                break;
            case 1:
                errorText = "Este nivel de dificultad ya existe";
                break;
            default:
                errorText = "Hay un error no previsto. Contacte a un administrador";
                break;
        }
        return errorText;
    }

    public void deleteDificultyLevel(string dificultylevel)
    {
        dificultyData.deleteDificultyLevel(dificultylevel);
    }

    public string editDificultyLevel(string oldDificultyLevel, string newDificultyLevel, int state)
    {
        dificultyData.deleteDificultyLevel(oldDificultyLevel);
        string error = insertDificultyLevel(newDificultyLevel, state);
        return error;
    }
}