using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Vive;
using UnityEngine.Events;


/// <summary>
/// Diese Klasse ist für das Betätigen des Buttons zum aktualisieren der ausgewählten Protokolle zuständig.
/// </summary>
public class ButtonClick : MonoBehaviour
    , IColliderEventHoverEnterHandler
    , IColliderEventHoverExitHandler

{

    public UnityEvent onClick;
    bool inCollider = false;
    private GameObject fileLoader;

    // Da das GameObject "protocolLoader" erst zur Laufzeit erstellt wird. Kann man es im Editor nicht mit diesem File verknüpfen. Deswegen
    // wird hier in der Start nach diesem Objekt gesucht. Achtung falls der Name verändert wird muss auch dieser verändert werden! 
    public void Start()
    {
        fileLoader = GameObject.Find("ProtocolLoader");
    }

    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        inCollider = true;
    }

    public void OnColliderEventHoverExit(ColliderHoverEventData eventData)
    {
        inCollider = false;
    }

    void Update()
    {
        if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Trigger) && inCollider)
        {
            Debug.Log("onClick");
            onClick.Invoke();
        }
    }
}
