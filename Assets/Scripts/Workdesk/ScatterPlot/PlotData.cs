using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Datatype containing the main csv and selectiondata
public class PlotData
{
    //contains csv as TextAsset (string)
    private TextAsset csvAsset;
    //contains csv of selected as TextAsset
    private DataVisSelection selection;
    //name of file for dropdownmenu
    private string name;
    private List<List<string>> categorieColors;
    private string categorieColumn;
    private DataVisClassification classification;
    public string _id;

    //Getter Setter
    public string Name { get => name; set => name = value; }
    public TextAsset CsvAsset { get => csvAsset; set => csvAsset = value; }
    public DataVisSelection Selection { get => selection; set => selection = value; }
    public List<List<string>> CategorieColors { get => categorieColors; set => categorieColors = value; }
    public string CategorieColumn { get => categorieColumn; set => categorieColumn = value; }
    
    public DataVisClassification Classification { get => classification; set => classification = value; }

    //contructors
    public PlotData()
    {

    }
    public PlotData(TextAsset csvAsset)
    {
        this.csvAsset = csvAsset;
    }

    public PlotData(string name, TextAsset csvAsset)
    {
        this.name = name;
        this.csvAsset = csvAsset;
        this.csvAsset.name = name;
    }

    public PlotData(string name, TextAsset csvAsset, DataVisSelection selection)
    {
        this.name = name;
        this.csvAsset = csvAsset;
        this.csvAsset.name = name;
        this.selection = selection;
    }

}
