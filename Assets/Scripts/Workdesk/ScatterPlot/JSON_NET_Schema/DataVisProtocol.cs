using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class DataVisProtocol
{
    public string name;
    public ExportCsv dataset;
    public DataVisColors colors;
    public DataVisSelection selection;
    public string _id;
    public List<Comment> comments;
    public DataVisClassification classification;
}
