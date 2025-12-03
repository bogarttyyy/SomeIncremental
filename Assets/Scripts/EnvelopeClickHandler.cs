using UnityEngine;
using UnityEngine.InputSystem;

public enum EnvelopeType { Untracked, Tracked, Express }

/// <summary>
/// Raycasts from the main camera on click to detect this envelope (or its children) via collider.
/// Requires a Collider on this GameObject or its children. Works even if nested under parents.
/// </summary>
public class EnvelopeClickHandler : MonoBehaviour
{
    [SerializeField] EnvelopeSelect envelopeSelect;
    [SerializeField] EnvelopeType type;
    [SerializeField] LayerMask raycastLayers = ~0; // defaults to everything
    [SerializeField] float maxRayDistance = 1000f;

    void Update()
    {
        // New Input System mouse check
        var mouse = Mouse.current;
        if (mouse == null || !mouse.leftButton.wasPressedThisFrame) return;
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, raycastLayers, QueryTriggerInteraction.Collide))
        {
            // If the hit is this object or any of its children
            if (hit.transform == transform || hit.transform.IsChildOf(transform))
            {
                NotifySelection();
            }
        }
    }

    void NotifySelection()
    {
        if (envelopeSelect != null)
        {
            envelopeSelect.Select(type);
            Debug.Log(gameObject.name + " was selected");
        }
    }
}
