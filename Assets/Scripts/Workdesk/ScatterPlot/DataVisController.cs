using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Diese Klasse ist der Start der Anwendung. Sie erstellt Scatterplots, befüllt die Anzigen und das Clipboard.
/// </summary>
public class DataVisController : MonoBehaviour
{
    [SerializeField] public string baseurl = "localhost";
    [SerializeField] public string port = "4242";
    private DataVisProtocolLoader fileLoader;
    private PlotClipBoardController plotClipBoardController;
    private Visualizer visualizer;
    private WhiteboardValueLoader whiteboardValueLoader;

    // Awake will controll the protocolloading depending on plattform 
    void Awake()
    {
        fileLoader = (new GameObject("ProtocolLoader")).AddComponent<DataVisProtocolLoader>();
        plotClipBoardController = (new GameObject("PlotClipBoardController")).AddComponent<PlotClipBoardController>();
        whiteboardValueLoader = GameObject.Find("WhiteBoard").GetComponent<WhiteboardValueLoader>();
        visualizer = GameObject.FindGameObjectWithTag("Visualizer").GetComponent<Visualizer>();
        // Startet die Anfrage zur Schnittstelle
        fileLoader.platformIndependentProtocolLoading(Utilities.generateURLForAPICall(baseurl, port, Utilities.GET_SELECTED_PROTOCOLS_PATH));
        DataVisProtocolLoader.onLoadRdy += this.updateVisAndDisplays;
    }

    
    void Start()
    {
        updateVisAndDisplays();
    }

    /// <summary>
    /// Methode lädt neu ausgewählte Protokolle von der Schnittstelle
    /// Wird vom UnityEvent Event des ButtonClick Skripts aufgerufen.
    /// </summary>
    public void reloadSelectedProtocolsfromAPI()
    {
        fileLoader.refreshSelectedProtocolsFromAPI(Utilities.generateURLForAPICall(baseurl, port, Utilities.GET_SELECTED_PROTOCOLS_PATH));
    }

    /// <summary>
    /// Methode erstellt den ersten Scatterplot und befüllt anschließend die Anzeigen und das Clipboard
    /// </summary>
    private void updateVisAndDisplays()
    {
        //Erhält die PlotData Liste über den DataVisProtocolHolder
        List<PlotData> plotDataList = DataVisProtocolHolder.GetInstance().getPlotDataList();
        Debug.Log(plotDataList.Count);
        //Erstellt ersten Scatterplot in der Szene
        visualizer.createInitialScatterPlot(plotDataList[0]);
        // Anfrage zur Schnittstelle für die HTML-Kommentardatei und anschließendes Anzeigen der Datei im Browserfenster
        string url = "localhost:4242/DataVis/protocols/" + plotDataList[0]._id + "/commentsDownload";
        fileLoader.getFileFromUrl(url);
        // Befüllen des Whiteboardes
        whiteboardValueLoader.setClassification(plotDataList[0].Classification);
        whiteboardValueLoader.printValues();
        // Befüllen des Clipboards mit allen Protokollen und deren Anzeigemöglichkeiten
        plotClipBoardController.fillPlotListwithList(plotDataList);
        plotClipBoardController.fillScatterplotAxisDropwdown(visualizer.GetPossibleScattersplots());
    }

    /// <summary>
    /// Methode zeigt den ersten Plot des im Clipboard selektierten Protokolls an. Anschließend werden alle Anzeigen befüllt.
    /// Wird im ScatterplotDropDown UnityEvent aufgerufen.
    /// </summary>
    public void loadSelectedScatterPlot()
    {
        int value = plotClipBoardController.getScatterPlotValue();
        PlotData data = plotClipBoardController.getPlotDataByDropwDownValue(value);
        string url = "localhost:4242/DataVis/protocols/" + data._id + "/commentsDownload";
        fileLoader.getFileFromUrl(url);
        whiteboardValueLoader.setClassification(data.Classification);
        whiteboardValueLoader.printValues();
        visualizer.createInitialScatterPlot(data);
        plotClipBoardController.fillScatterplotAxisDropwdown(visualizer.GetPossibleScattersplots());
    }

    /// <summary>
    /// Methode ändert den Scatterplot jenach Auswahl des Benutzers im DataDropDown
    /// Wird im DataDropDown UnityEvent aufgerufen.
    /// </summary>
    public void loadSelectedAxisScatterPlot()
    {
        int value = plotClipBoardController.getScatterPlotAxisValue();
        visualizer.loadSpecificScatterplot(value);
    }

}
