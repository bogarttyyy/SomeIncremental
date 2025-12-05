using System;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

public class Clickable :  MonoBehaviour, IClickable
{
    public static event Action<Clickable> OnAnyClicked;
    
    public void OnClicked()
    {
        OnAnyClicked?.Invoke(this);
    }
}