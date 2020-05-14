using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using SimpleWebBrowser;
using System;

/// <summary>
/// Diese Klasse ist für das Laden eines Protokolls über die Schnittstelle oder einen Kommandozeilenbefehl zuständig
/// </summary>
public class DataVisProtocolLoader : MonoBehaviour
{
    APIController aPIController;
    public delegate void LoadRdy();
    public static event LoadRdy onLoadRdy;
    public void Awake()
    {
        aPIController = (new GameObject("APIController")).AddComponent<APIController>();
    }

    /// <summary>
    /// Diese Methode überprüft ob die anwendung mit einem Befehl über die CMD gestartet wurde. Falls nicht werden Protokolle von der Schnittstelle geladen
    /// </summary>
    public void platformIndependentProtocolLoading(string url)
    {
#if UNITY_Android
        DOING STUFF WITH API
        getSelectedProtocolsFromAPI(url);
#endif
#if UNITY_STANDALONE_WIN
        string outputFile = Utilities.getCommandLineArgs("--file");
        string outputDir = Utilities.getCommandLineArgs("--dir");
        if (!(outputFile is null))
        {
            //DOING STUFF WITH ARGS
            getProtocolFromPath(outputFile);
        }

        if (!(outputDir is null))
        {
            //DOING STUFF WITH ARGS
            getProtocolsFromPath(outputFile);
        }
        else
        {
            getSelectedProtocolsFromAPI(url);
        }

#endif
    }

    private void getProtocolsFromPath(string Path)
    {

    }
    /// <summary>
    /// Diese Methode liest jenach Dateiendung die Daten aus und speichert diese ab.
    /// </summary>
    private void getProtocolFromPath(string path)
    {
        if (!(path is null))
        {
            //get Filename
            string fileName = Path.GetFileName(path);
            // read text from file
            string fileContent = File.ReadAllText(path);

            // case simple csv
            if (Path.GetExtension(path).Equals(".csv"))
            {    
                // create TextAsset with content form file
                TextAsset asset = new TextAsset(fileContent);
                //create plotData object for holding the csvdata
                PlotData plotData = new PlotData(fileName, asset);
                
                //TODO

            }
            // case own json structure
            else if (Path.GetExtension(path).Equals(".json"))
            {
                DataVisProtocol protocol = JsonConvert.DeserializeObject<DataVisProtocol>(fileContent);
                DataVisProtocolHolder.GetInstance().addProtocolToList(protocol);
            }
            //default if no condition is true
            else
            {

            }
        }
        else
        {
  
        } 
    }
    /// <summary>
    /// Methode speichert den Response der Schnittstelle in DataVisProtocol Objekte und legt diese im DataVisProtocolHolder ab
    /// </summary>
    private void getSelectedProtocolsFromAPI(string url)
    {
        //DOING STUFF WITH API
        StartCoroutine(aPIController.GetRequest(url, (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            }
            else
            {
                //Debug.Log(req.downloadHandler.text);
                //JUST PUT DATA SOMEWHERE
                JArray protocols = JArray.Parse(req.downloadHandler.text);
                foreach(JObject obj in protocols)
                {
                    DataVisProtocol protocol = JsonConvert.DeserializeObject<DataVisProtocol>(obj.ToString());
                    DataVisProtocolHolder.GetInstance().addProtocolToList(protocol);
                }       
            }
        }));
    }
    /// <summary>
    /// Methode erhält die HTML-Kommentardatei von der Schnittstelle und zeigt diese im Browserfenster an
    /// </summary>
    public void getFileFromUrl(string url)
    {
        //DOING STUFF WITH API
        StartCoroutine(aPIController.GetRequest(url, (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            }
            else
            {
                //Debug.Log(req.downloadHandler.text);
                //JUST PUT DATA SOMEWHERE
                //string foldername = "Kommentare";
                string name = "test";
                string extension = ".html";
                Debug.Log(Application.persistentDataPath);
                var fileurl = Application.persistentDataPath + "/" + name + extension;
                var path = "file:///" + fileurl;
                //FileStream file = File.Open(urltest, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                File.WriteAllText(fileurl, String.Empty);
                TextWriter tw = new StreamWriter(fileurl, true);
                tw.WriteLine(req.downloadHandler.text);
                tw.Close();
                WebBrowser browser = GameObject.Find("InworldBrowser").GetComponent<WebBrowser>();
                browser.OpenCommentFile(path);
            }
        }));
    }

    /// <summary>
    /// Methode verarbeitet Response der Schnittstelle und speichert diese in DataVisProtokoll Objekte
    /// </summary>
    public void refreshSelectedProtocolsFromAPI(string url)
    {
        //DOING STUFF WITH API
        StartCoroutine(aPIController.GetRequest(url, (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            }
            else
            {
                DataVisProtocolHolder.GetInstance().clearProtocols();
                //JUST PUT DATA SOMEWHERE
                JArray protocols = JArray.Parse(req.downloadHandler.text);
                foreach (JObject obj in protocols)
                {
                    DataVisProtocol protocol = JsonConvert.DeserializeObject<DataVisProtocol>(obj.ToString());
                    DataVisProtocolHolder.GetInstance().addProtocolToList(protocol);
                }
                onLoadRdy?.Invoke();
            }
        }));
    }

}
