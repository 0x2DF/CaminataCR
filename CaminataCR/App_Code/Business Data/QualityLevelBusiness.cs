using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de QualityLevelBusiness
/// </summary>
public class QualityLevelBusiness
{
    QualityLevelData qualityData = new QualityLevelData();
    public QualityLevelBusiness()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public List<string> getQualityLevels()
    {
        List<string> qualityLevels = qualityData.getQualityLevels();
        return qualityLevels;
    }

    public string insertQualityLevel(string newQualityLevel, int state)
    {
        int insertingError = qualityData.InsertQualityLevel(newQualityLevel, state);
        string errorText = "";
        switch (insertingError)
        {
            case 0:
                errorText = null;
                break;
            case 1:
                errorText = "Este nivel de calidad ya existe";
                break;
            default:
                errorText = "Hay un error no previsto. Contacte a un administrador";
                break;
        }
        return errorText;
    }

    public void deleteQualityLevel(string qualitylevel)
    {
        qualityData.deleteQualityLevel(qualitylevel);
    }

    public string editQualityLevel(string oldQualityLevel, string newQualityLevel, int state)
    {
        qualityData.deleteQualityLevel(oldQualityLevel);
        string error = insertQualityLevel(newQualityLevel, state);
        return error;
    }
}