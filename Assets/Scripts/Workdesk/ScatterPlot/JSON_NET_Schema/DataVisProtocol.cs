using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class DataVisProtocol
{
    public string _id;
    public string name;
    public DataVisDataSet dataset;
    public DataVisColors colors;
    public DataVisSelection selection;
    public List<DataVisCommet> comments;
    public DataVisClassification classification;
}
