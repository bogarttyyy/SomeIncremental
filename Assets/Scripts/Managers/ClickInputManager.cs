using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Managers
{
    
    public class ClickInputManager : MonoBehaviour
    {
        
        [SerializeField] private Camera cam;
        [SerializeField] private float maxDistance = 1000f;
        [SerializeField] private LayerMask clickableLayers = ~0;
        [SerializeField] private bool ignoreUI = true;

        private void Awake()
        {
            cam ??= Camera.main;
        }
        
        public void Update()
        {
            // New Input System mouse
            var mouse = Mouse.current;
            if (mouse == null) 
                return; // e.g. on touch-only device
            
            if (!mouse.leftButton.wasPressedThisFrame) 
                return;
            
            // Optional: don't click through UI
            if (ignoreUI && EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) 
                return;

            Debug.Log($"Click detected!");
            
            Vector2 screenPos = mouse.position.ReadValue();
            Ray ray = cam.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out var hit3D, maxDistance, clickableLayers))
            {
                Debug.Log($"Hit3D: {hit3D.collider.gameObject.name}");
                
                var clickable = hit3D.collider.GetComponentInParent<IClickable>();
                clickable?.OnClicked();
                return;
            }
            
            RaycastHit2D hit2D = Physics2D.Raycast(cam.ScreenToWorldPoint(screenPos), Vector2.zero, maxDistance, clickableLayers);
            if (hit2D.collider != null)
            {
                Debug.Log($"Hit2D: {hit2D.collider.gameObject.name}");
                var clickable = hit2D.collider.GetComponentInParent<IClickable>();
                clickable?.OnClicked();
            }
            else
            {
                Debug.Log($"Null Hit: {hit2D.collider}");
            }

        }
    }
}