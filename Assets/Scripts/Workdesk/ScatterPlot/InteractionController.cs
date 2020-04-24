using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Vive;
using UnityEngine.Events;

public class InteractionController : MonoBehaviour
    , IColliderEventHoverEnterHandler
    , IColliderEventHoverExitHandler

{

    public UnityEvent onToggleSelection;
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
        if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Menu) && inCollider)
        {
            Debug.Log("Menu");
            onToggleSelection.Invoke();
        }
    }
}
