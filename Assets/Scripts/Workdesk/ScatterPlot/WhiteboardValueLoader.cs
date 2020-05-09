using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Diese Klasse befüllt das Whiteboard mit den Klassifikationsattributen des Protokolls.
/// </summary>
public class WhiteboardValueLoader : MonoBehaviour
{
    private DataVisClassification classificationValues;
    public Text nameValue;
    public Text accuracyValue;
    public Text resultValue;
    public UIConfusionMatrixGenerator matrixGenerator;

    /// <summary>
    /// Methode befüllt die Klassenvariablen
    /// </summary>
    public void  setClassification(DataVisClassification classificationValues)
    {
        this.classificationValues = classificationValues;
    }

    /// <summary>
    /// Methode befüllt die UI Elemente des Canvases auf dem Whiteboard 
    /// </summary>
    public void printValues()
    {
        matrixGenerator.delteCurrentmatrix();
        nameValue.text = classificationValues.name;
        accuracyValue.text = classificationValues.accuracy;
        resultValue.text = classificationValues.getResultAsString();
        if (matrixGenerator == null) { Debug.LogError("Error matrixGenerator Null"); }
        // Generiert die Confusion Matrix
        matrixGenerator.setMatrixValues(classificationValues.confusionMatrix);
        matrixGenerator.printMatrixToCanvas();
    }

}
