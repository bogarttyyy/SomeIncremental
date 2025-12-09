using System;
using Enums;
using NSBLib.Helpers;
using Interfaces;
using NSBLib.EventChannelSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class Envelope : MonoBehaviour, IClickable
{
    // public static readonly UnityEvent<Sprite> EnvelopeClicked = new UnityEvent<Sprite>();
    

    [SerializeField] private Sprite bigSprite;
    [FormerlySerializedAs("EnvelopeClicked")] 
    [SerializeField] private SpriteEventChannel envelopeClicked;
    [SerializeField] private GameStateEventChannel gameStateChanged;

    public void OnClicked()
    {
        NSBLogger.Log($"Clicked: {gameObject.name}");
        envelopeClicked?.Invoke(bigSprite);
        gameStateChanged?.Invoke(EGameState.WriteAddressState);
    }
}
