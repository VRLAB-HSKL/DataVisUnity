using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Diese Klasse befüllt und verwaltet das Clipboard
/// </summary>
public class PlotClipBoardController : MonoBehaviour
{

    private Dropdown scatterplotDropdown;
    private Dropdown scatterplotAxisDropdown;
    private List<PlotData> dataFiles = new List<PlotData>();
    TextAsset[] data;

    // Start is called before the first frame update
    void Awake()
    {
        scatterplotDropdown = GameObject.FindGameObjectWithTag("ScatterplotDropdown").GetComponent<Dropdown>();
        scatterplotAxisDropdown = GameObject.FindGameObjectWithTag("DataDropdown").GetComponent<Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Diese Methode löscht alle Optionen des Scatterplotdropdowns
    /// </summary>
    private void clearScatterPlotDropdown()
    {
        if (scatterplotDropdown.options.Count != 0) scatterplotDropdown.ClearOptions();
    }
    /// <summary>
    /// Diese Methode löscht alle Optionen des ScatterplotAxisdropdowns
    /// </summary>
    private void clearScatterPlotAxisDropdown()
    {
        if (scatterplotAxisDropdown.options.Count != 0) scatterplotAxisDropdown.ClearOptions();
    }
    /// <summary>
    /// Diese Methode befüllt die PlotData Liste mit einem PlotDaten Objekt
    /// </summary>
    public void fillPlotList(PlotData plotData)
    {
        dataFiles.Add(plotData);
    }
    /// <summary>
    /// Diese Methode befüllt die PlotData Liste mit einer PlotData Liste
    /// </summary>
    public void fillPlotListwithList(List<PlotData> list)
    {
        dataFiles.Clear();
        foreach(PlotData plotData in list)
        {
            dataFiles.Add(plotData);
        }
        fillScatterPlotDropwdown();
    }
    /// <summary>
    /// Diese Methode befüllt die Options des ScatterplotDropdown mit der PlotData Liste
    /// </summary>
    public void fillScatterPlotDropwdown()
    {
        //Clear Dropdown
        clearScatterPlotDropdown();
      
        foreach(PlotData plotData in dataFiles)
        {
            scatterplotDropdown.options.Add(new Dropdown.OptionData() { text = plotData.Name});
        }
        scatterplotDropdown.RefreshShownValue();
    }
    /// <summary>
    /// Diese Methode befüllt die Options des ScatterplotAxisDropdown mit der PlotData Liste
    /// </summary>
    public void fillScatterplotAxisDropwdown(string[] possibleAxisPlots)
    {
        //Clear Axis Dropdown
        clearScatterPlotAxisDropdown();
        for(int i = 0; i < possibleAxisPlots.Length; i++)
        {
            scatterplotAxisDropdown.options.Add(new Dropdown.OptionData() { text = possibleAxisPlots[i] });
        }
        scatterplotAxisDropdown.value = 0;
        scatterplotAxisDropdown.RefreshShownValue();
    }
    /// <summary>
    /// Diese Methode gibt den Value des ScatterplotDropdowns zurück
    /// </summary>
    public int getScatterPlotValue()
    {
            Debug.Log("switchScatterplot():  " + scatterplotDropdown.value);
            return scatterplotDropdown.value;
    }
    /// <summary>
    /// Diese Methode gibt den Value des ScatterplotAxisDropdowns zurück
    /// </summary>
    public int getScatterPlotAxisValue()
    {
        Debug.Log("switchScatterplot():  " + scatterplotAxisDropdown.value);
        return scatterplotAxisDropdown.value;
    }

    /// <summary>
    /// Gibt für den Value der Option das jeweilige PlotData Objekt zurück.
    /// </summary>
    public PlotData getPlotDataByDropwDownValue(int value)
    {
        PlotData data = dataFiles[value];
        return data;
    }  
}
