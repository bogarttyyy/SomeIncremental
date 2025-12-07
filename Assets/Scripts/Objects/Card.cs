using System;
using System.Collections.Generic;
using Enums;
using Helpers;
using Interfaces;
using UnityEngine;

public class Card : MonoBehaviour, IClickable
{
    [SerializeField] private ECardRarity rarity;
    private Sprite sprite;
    
    public static event Action<Card> CardClicked;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void OnClicked()
    {
        NSBLogger.Log($"Clicked: {gameObject.name}");
        CardClicked?.Invoke(this);
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }
    
    public ECardRarity GetRarity()
    {
        return rarity;
    }

    public void SetRarity(ECardRarity rarity)
    {
        this.rarity = rarity;
    }

    public void SetRarity(Card card)
    {
        SetRarity(card.GetRarity());
    }
    
    public void SetSprite(Card card)
    {
        SetSprite(card.GetSprite());
    }
    
}
