using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

/// <summary>
/// Diese Klasse dient zum Senden eines Requests zur Schnittstelle
/// </summary>
public class APIController : MonoBehaviour
{
    /// <summary>
    /// Erhält die URL zum Endpunkt der Schinttstelle und sendet einen HTTP GET-Befehl zum übergebenen Endpunkt
    /// </summary>
    public IEnumerator GetRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Send the request and wait for a response
            yield return request.SendWebRequest();
            callback(request);
        }
    }
    /// <summary>
    /// Wird aktuell nicht genutzt.
    /// Soll in späteren Verlauf änderungen am Protokoll durch ein HTTP POST-Befehl vornehmen
    /// </summary>
    public IEnumerator PostRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Send the request and wait for a response
            yield return request.SendWebRequest();
            callback(request);
        }
    }
}
