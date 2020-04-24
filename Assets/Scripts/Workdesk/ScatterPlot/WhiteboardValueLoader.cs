using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteboardValueLoader : MonoBehaviour
{
    private DataVisClassification classificationValues;
    public Text nameValue;
    public Text accuracyValue;
    public Text resultValue;
    public UIConfusionMatrixGenerator matrixGenerator;

    public void  setClassification(DataVisClassification classificationValues)
    {
        this.classificationValues = classificationValues;
    }
    // Start is called before the first frame update
    public void printValues()
    {
        matrixGenerator.delteCurrentmatrix();
        nameValue.text = classificationValues.name;
        accuracyValue.text = classificationValues.accuracy;
        resultValue.text = classificationValues.getResultAsString();
        if (matrixGenerator == null) { Debug.LogError("Error matrixGenerator Null"); }
        matrixGenerator.setMatrixValues(classificationValues.confusionMatrix);
        matrixGenerator.printMatrixToCanvas();
    }

}
