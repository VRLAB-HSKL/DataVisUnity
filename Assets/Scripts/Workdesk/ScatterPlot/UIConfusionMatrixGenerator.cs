using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Diese Klasse generiert die im Protokoll mitgegebene ConfusionMatrix
/// </summary>
public class UIConfusionMatrixGenerator : MonoBehaviour
{

    public GameObject matrixCellPrefab;
    public GameObject confusionMatrixLabelYPrefab;
    public GameObject confusionMatrixLabelXPrefab;
    public GameObject matrixCanvas;
    private float[][] confusionMatrixData;
    private List<string> labels = new List<string>();
    public Color32 startColor;
    public Color32 endColor;
    private float maxValue;
    private float minValue;

    /// <summary>
    /// Diese Methode speichert die ConfusionMatrix Daten in die Klassenvariablen
    /// </summary>
    public void setMatrixValues(DataVisConfusionMatrix confusionMatrix)
    {
        if (confusionMatrix.data.Length != 0)
        {
            Debug.Log(confusionMatrix.data.Length);
            this.confusionMatrixData = confusionMatrix.data;
            this.labels = confusionMatrix.labels;
            ColorMap.setGradient(startColor, endColor);
            float[] flattConfusionMatrix = confusionMatrix.data.SelectMany(x => x).ToArray();
            maxValue = flattConfusionMatrix.Max();
            //maxValue = confusionMatrix.Cast<float>().Max();
            // minValue = confusionMatrix.Cast<float>().Min();
        }
        else
        {
            Debug.LogError("confusionMatrix is null");
        }
    }

    /// <summary>
    /// Diese Methode erstellt mit den Daten der ConfusionMatrix die ConfusionMatrix für den Canvas des Whiteboards
    /// </summary>
    public void printMatrixToCanvas()
    {
        int height = 0;
        for(int i = 0; i < confusionMatrixData.Length; i++)
        {
            GameObject confusionMatrixLabelY = Instantiate(confusionMatrixLabelYPrefab, new Vector3(0, height, 0.0f), Quaternion.identity);
            confusionMatrixLabelY.transform.SetParent(matrixCanvas.transform, false);
            confusionMatrixLabelY.GetComponent<Text>().text = labels[i];
            int width = 150;
            for(int j = 0; j < confusionMatrixData[i].Length; j++)
            {
                if (i == confusionMatrixData.Length - 1)
                {
                    GameObject confusionMatrixLabelX = Instantiate(confusionMatrixLabelXPrefab, new Vector3(width, height - 70, 0.0f), Quaternion.identity);
                    confusionMatrixLabelX.transform.Rotate(0.0f, 0.0f, 90.0f);
                    confusionMatrixLabelX.transform.SetParent(matrixCanvas.transform, false);
                    confusionMatrixLabelX.GetComponent<Text>().text = labels[j];
                }
                GameObject matrixCell = Instantiate(matrixCellPrefab, new Vector3(width, height, 0.0f), Quaternion.identity);
                matrixCell.transform.SetParent(matrixCanvas.transform, false);
                matrixCell.GetComponent<Image>().color = ColorMap.getColorForValue(Utilities.prozentsatz(maxValue, confusionMatrixData[i][j])/100);
                for (int k = 0; k < matrixCell.transform.childCount; ++k)
                {
                    Transform currentItem = matrixCell.transform.GetChild(k);
                    if (currentItem.name.Equals("MatrixCellValue"))
                    {
                        Text matrixCellValue = currentItem.GetComponent<Text>();
                        matrixCellValue.text = confusionMatrixData[i][j].ToString();
                        if (Utilities.prozentsatz(maxValue, confusionMatrixData[i][j]) >= 80)
                        {
                            matrixCellValue.color = Color.white;
                        }

                    }
                }
                    width += 50;
            }
            height -= 50;
        }
    }

    /// <summary>
    /// Diese Methode löscht alle Daten der ConfusionMatrix und löscht alle erstellten UI Elemente
    /// </summary>
    public void delteCurrentmatrix()
    {
        confusionMatrixData = new float[0][];
        labels = new List<string>();
        maxValue = 0;
        foreach (Transform child in matrixCanvas.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
