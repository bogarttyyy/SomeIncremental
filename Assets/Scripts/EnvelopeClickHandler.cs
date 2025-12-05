using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum EnvelopeType { Untracked, Tracked, Express }

/// <summary>
/// Raycasts from the main camera on click to detect this envelope (or its children) via collider.
/// Requires a Collider on this GameObject or its children. Works even if nested under parents.
/// </summary>
public class EnvelopeClickHandler : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + " was selected");
    }
}
