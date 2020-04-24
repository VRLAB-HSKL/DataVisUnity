using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataVisClassification : MonoBehaviour
{
    public string name { get; set; }
    public DataVisConfusionMatrix confusionMatrix { get; set; }
    public string[] result { get; set; }
    public string accuracy { get; set; }
    public string resultErrorColor { get; set; }

    public string getResultAsString()
    {
        string resultAsString = "[";
        foreach(string value in result)
        {
            resultAsString = resultAsString + "\"" + value + "\", ";
        }
        resultAsString = resultAsString + "]";
        return resultAsString;
    }


}
