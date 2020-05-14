using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Diese Singelton Klasse hält alle ausgewählten Protokolle
/// </summary>
public class DataVisProtocolHolder
{
    private List<DataVisProtocol> dataVisProtocolList = new List<DataVisProtocol>();

    private static DataVisProtocolHolder _instance;
    private DataVisProtocolHolder(){ }
    public static DataVisProtocolHolder GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DataVisProtocolHolder();
        }
        return _instance;
    }



    public void addProtocolToList(DataVisProtocol protocol)
    {
        dataVisProtocolList.Add(protocol);
    }
    public List<DataVisProtocol> getDataVisProtocolList()
    {
        return dataVisProtocolList;
    }
    public void fillDataVisProtocolList(List<DataVisProtocol> dataVisProtocolList)
    {
        this.dataVisProtocolList = dataVisProtocolList;
    }

    /// <summary>
    /// Methode gibt eine Liste von PlotDaten zurück welche aus der Liste aller Protokolle erstellt wurde.
    /// </summary>
    public List<PlotData> getPlotDataList()
    {
        List <PlotData> plotDataList = new List<PlotData>();
        foreach(DataVisProtocol protocol in dataVisProtocolList)
        {
            PlotData plotData = new PlotData();
            plotData.Name = protocol.name;
            plotData._id = protocol._id;
            TextAsset csvAsset = new TextAsset(protocol.dataset.ToString());
            Debug.LogError(csvAsset);
            plotData.CsvAsset = csvAsset;
            if(!(protocol.colors is null))
            {
                plotData.CategorieColors = protocol.colors.colorList;
                plotData.CategorieColumn = protocol.colors.column;
            }
            if(!(protocol.selection is null))
            {
                plotData.Selection = protocol.selection;
            }
            if (!(protocol.classification))
            {
                plotData.Classification = protocol.classification;
            }
            plotDataList.Add(plotData);
        }
        return plotDataList;
    }

    public void clearProtocols()
    {
        dataVisProtocolList.Clear();
    }
}
