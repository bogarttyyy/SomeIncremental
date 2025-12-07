using System;
using Helpers;
using Interfaces;
using UnityEngine;

public class Envelope : MonoBehaviour, IClickable
{
    // public static readonly UnityEvent<Sprite> EnvelopeClicked = new UnityEvent<Sprite>();
    
    public static event Action<Sprite> EnvelopeClicked; 

    [SerializeField] private Sprite bigSprite;

    public void OnClicked()
    {
        NSBLogger.Log($"Clicked: {gameObject.name}");
        EnvelopeClicked?.Invoke(bigSprite);
    }
}
