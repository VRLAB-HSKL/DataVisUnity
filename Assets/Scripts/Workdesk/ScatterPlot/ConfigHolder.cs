using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;


//Singelton Pattern
[Serializable]
public class ConfigHolder
{

    private string defaultFolder;
    private string defaultFile;
    private string fileToShow;
    private string folderToShow;

    public string FolderToShow { get => folderToShow; set => folderToShow = value; }
    public string FileToShow { get => fileToShow; set => fileToShow = value; }
    public string DefaultFile { get => defaultFile; set => defaultFile = value; }
    public string DefaultFolder { get => defaultFolder; set => defaultFolder = value; }
    
    public string getFileToShow()
    {
        if(fileToShow == "")
        {
            return defaultFile;
        }
        else
        {
            return fileToShow;
        }
    }

    public string getFolderToShow()
    {
        if (folderToShow == "")
        {
            Debug.LogError(defaultFolder);
            return defaultFolder;
        }
        else
        {
            Debug.LogError(folderToShow);
            return folderToShow;
        }
    }
}
