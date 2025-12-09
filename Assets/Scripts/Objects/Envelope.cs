using System;
using NSBLib.Helpers;
using Interfaces;
using NSBLib.EventChannelSystem;
using UnityEngine;

public class Envelope : MonoBehaviour, IClickable
{
    // public static readonly UnityEvent<Sprite> EnvelopeClicked = new UnityEvent<Sprite>();
    

    [SerializeField] private Sprite bigSprite;
    [SerializeField] private SpriteEventChannel EnvelopeClicked;

    public void OnClicked()
    {
        NSBLogger.Log($"Clicked: {gameObject.name}");
        EnvelopeClicked?.Invoke(bigSprite);
    }
}
