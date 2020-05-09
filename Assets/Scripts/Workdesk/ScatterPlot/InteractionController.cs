using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Vive;
using UnityEngine.Events;

/// <summary>
/// Diese Klasse wird dem Scatterplot Prefab angeheftet. 
/// Sie Überprüft ob sich ein Controller innerhalb des Scatterplots befindet und verwaltet die Interaktionsmöglichkeiten mit dem Plot
/// </summary>
public class InteractionController : MonoBehaviour
    , IColliderEventHoverEnterHandler
    , IColliderEventHoverExitHandler

{
    public UnityEvent onToggleSelection;
    public UnityEvent onToggleError;
    bool inCollider = false;

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
        if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Pad) && inCollider)
        {
            Debug.Log("DPadRight onToggleSelection");
            onToggleSelection.Invoke();
        }
        if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Pad) && inCollider)
        {
            Debug.Log("DPadLeft onToggleError");
            onToggleError.Invoke();
        }
    }
}
