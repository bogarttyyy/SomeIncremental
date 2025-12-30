using System.Collections.Generic;
using Enums;
using Interfaces;
using NSBLib.Helpers;
using UnityEngine;

public class Stamp : Draggable
{
    private Collider2D collider;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private EStampState eStampState;
    
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }
    
    public override void OnDragged(Vector3 position)
    {
        if (isDraggable)
        {
            transform.position = position;
        }
    }
    
    public override void OnDragReleased()
    {
        // NSBLogger.Log("Stamp released");
        
        List<Collider2D> results = new List<Collider2D>();
        collider.Overlap(ContactFilter2D.noFilter, results);

        foreach (var col in results)
        {
            if (col.TryGetComponent(out BigEnvelope bigEnvelope))
            {
                // NSBLogger.Log("Stamp hit envelope");
                SetIsDraggable(false);
                bigEnvelope.AddStamp(this);
                return;
            }
        }
        
        Destroy(gameObject);
    }

    public void SetStampSprite(Sprite stamp)
    {
        renderer.sprite = stamp;
    }
    
    public void SetStampState(EStampState state)
    {
        eStampState = state;
    }

    public EStampState GetState()
    {
        return eStampState;
    }
}
