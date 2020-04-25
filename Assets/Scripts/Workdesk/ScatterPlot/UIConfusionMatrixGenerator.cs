using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    // Start is called before the first frame update
    void Awake()
    {
        //matrixCellPrefab = (GameObject)Resources.Load("Prefabs/MatrixCell", typeof(GameObject));
    }

    public void setMatrixValues(DataVisConfusionMatrix confusionMatrix)
    {
        if (confusionMatrix.data.Length != 0)
        {
            this.confusionMatrixData = confusionMatrix.data;
            this.labels = confusionMatrix.labels;
            ColorMap.setGradient(startColor, endColor, 1, 2);
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

    public void delteCurrentmatrix()
    {
        confusionMatrixData = new float[0][];
        labels.Clear();
        maxValue = 0;
        foreach (Transform child in matrixCanvas.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
