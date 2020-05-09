using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Utilities : MonoBehaviour
{

    public static string GET_SELECTED_PROTOCOLS_PATH = "/DataVis//protocols/selected";
    public static string GET_ALL_PROTOCOLS_PATH = "/DataVis/protocols";

    /// <summary>
    /// Methode generiert die aus der baseurl, port und endpointPath eine URL und gibt diese als string zurück
    /// </summary>
    public static string generateURLForAPICall(string baseurl, string port, string endpointPath)
    {
        string url = baseurl + ":" + port + endpointPath;
        return url;
    }
    /// <summary>
    /// Methode konvertiert Liste in ein Array
    /// </summary>
    public static object[] convertListToArray(List<object> list)
    {
        object[] array;
        array = list.ToArray();
        return array;
    }
    /// <summary>
    /// Methode gibt den Szenennamen zurück
    /// </summary>
    public static string getSceneAsString()
    {
        return SceneManager.GetActiveScene().name;
    }
    /// <summary>
    /// Methode gibt Inahlt einer Datei im StreamingAsset ordner als string zurück
    /// </summary>
    public static string readFileFromStreaminAssets(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            return jsonString;
        }
        else
        {
            Debug.Log("readFileFromStreaminAssets(..): File does not exist");
            return null;
        }
    }
    /// <summary>
    /// Methode konvertiert einen Hex-Wert in eine Color und gibt diese zurück-
    /// </summary>
    public static Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }

    /// <summary>
    /// Methode liefert CommandLineBefehle zurück, die nach einem Bestimmt Command folgen
    /// </summary>
    public static string getCommandLineArgs(string name)
    {
        var args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }
    /// <summary>
    /// Methode berechnet Prozentsatz
    /// </summary>
    public static float prozentsatz(float grundwert, float prozentwert)
    {
        return (100 / grundwert) * prozentwert;
    }

}
