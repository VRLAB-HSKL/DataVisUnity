using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class HTMLShower : MonoBehaviour
{
    public Texture2D tex;
    public UnityWebRequest w;

    void Start()
    {
        LoadTex();
    }

    void LoadTex()
    {
        w = new UnityWebRequest("C:/Users/basti/Desktop/ReactApp.html");
    }

    void Update()
    {
        if (w.isDone)
        {
            Debug.Log("done");
                }
    }
}
