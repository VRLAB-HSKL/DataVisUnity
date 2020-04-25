using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using SimpleWebBrowser;
using System;

public class DataVisProtocolLoader : MonoBehaviour
{
    APIController aPIController;
    public delegate void LoadRdy();
    public static event LoadRdy onLoadRdy;
    public void Awake()
    {
        aPIController = (new GameObject("APIController")).AddComponent<APIController>();
    }

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
                //returns created plot
 
            }
            // case own json structure
            else if (Path.GetExtension(path).Equals(".json"))
            {
                DataVisProtocol protocol = JsonConvert.DeserializeObject<DataVisProtocol>(fileContent);
                TextAsset csvAsset = new TextAsset(protocol.dataset.ToString());
                DataVisSelection selection = protocol.selection;
                PlotData plotData = new PlotData(fileName,csvAsset,selection);
                plotData.CategorieColors = protocol.colors.colorList;
                plotData.CategorieColumn = protocol.colors.column;
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
                string foldername = "Kommentare";
                string name = "test";
                string extension = ".html";
                var urltest = Application.persistentDataPath + "/" + foldername + "/" + name + extension;
                var fileurl = "file:///" + urltest;
                //FileStream file = File.Open(urltest, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                File.WriteAllText(urltest, String.Empty);
                TextWriter tw = new StreamWriter(urltest, true);
                tw.WriteLine(req.downloadHandler.text);
                tw.Close();
                WebBrowser browser = GameObject.Find("InworldBrowser").GetComponent<WebBrowser>();
                browser.OpenCommentFile(fileurl);
            }
        }));
    }

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
