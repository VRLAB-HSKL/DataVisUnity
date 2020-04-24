using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        //Loads data platform independent
        fileLoader.platformIndependentProtocolLoading(Utilities.generateURLForAPICall(baseurl, port, Utilities.GET_SELECTED_PROTOCOLS_PATH));
        //
        DataVisProtocolLoader.onLoadRdy += this.updateVisAndDropDown;
    }

    //Start will fill the data generated in Awake into the dropdown and will show the first scatterplot in the scene
    void Start()
    {
        updateVisAndDropDown();
    }

    public void reloadSelectedProtocolsfromAPI()
    {
        fileLoader.refreshSelectedProtocolsFromAPI(Utilities.generateURLForAPICall(baseurl, port, Utilities.GET_SELECTED_PROTOCOLS_PATH));
    }

    private void updateVisAndDropDown()
    {
        //gets plotDataList from Singelton DataHolder
        List<PlotData> plotDataList = DataVisProtocolHolder.GetInstance().getPlotDataList();
        //create first Scatterplot to show. always first of the list.
        visualizer.createInitialScatterPlot(plotDataList[0]);
        string url = "localhost:4242/DataVis/protocols/" + plotDataList[0]._id + "/commentsDownload";
        fileLoader.getFileFromUrl(url);
        //fills data for dropdown menu
        whiteboardValueLoader.setClassification(plotDataList[0].Classification);
        whiteboardValueLoader.printValues();
        plotClipBoardController.fillPlotListwithList(plotDataList);
        plotClipBoardController.fillScatterplotAxisDropwdown(visualizer.GetPossibleScattersplots());
    }
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

    public void loadSelectedAxisScatterPlot()
    {
        int value = plotClipBoardController.getScatterPlotAxisValue();
        visualizer.loadSpecificScatterplot(value);
    }

}
