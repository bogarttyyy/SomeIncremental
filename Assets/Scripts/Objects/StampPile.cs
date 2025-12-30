using System;
using Enums;
using Interfaces;
using NSBLib.Helpers;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;

public class StampPile : Draggable
{
    public Stamp stamp;
    private Stamp instancedStamp;
    [SerializeField] EStampState eStampState;
    
    [SerializeField] private Sprite stampSprite;

    private void Start()
    {
        stampSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public override void OnInitialClick(Vector3 position)
    {
        NSBLogger.Log("Initial click");
        instancedStamp = Instantiate(stamp, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
        instancedStamp.SetStampSprite(stampSprite);
        instancedStamp.SetStampState(eStampState);
        
    }

    public override void OnDragged(Vector3 position)
    {
        instancedStamp.OnDragged(position);
    }

    public override void OnDragReleased()
    {
        instancedStamp.OnDragReleased();
        instancedStamp = null;
    }
}
