using System;
using Enums;
using Generators;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }
    
    public Sprite commonSprite;
    public Sprite uncommonSprite;
    public Sprite rareSprite;
    public Sprite epicSprite;
    public Sprite legendarySprite;
    
    public Card selectedCard;
    
    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        Card.CardClicked += OnCardClickedHandler;
    }

    private void OnDisable()
    {
        Card.CardClicked -= OnCardClickedHandler;
    }

    private void OnCardClickedHandler(Card card)
    {
        SetCardSelected(card);
    }

    private void SetCardSelected(Card card)
    {
        selectedCard.SetRarity(card);
        selectedCard.SetSprite(card);
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        spriteRenderer = selectedCard.GetComponent<SpriteRenderer>();
    }

    public ECardRarity GetRandomCard()
    {
        var cardType = RandomCardGenerator.GenerateRandomCard();
        var selectedSprite = cardType switch
        {
            ECardRarity.Common => commonSprite,
            ECardRarity.Uncommon => uncommonSprite,
            ECardRarity.Rare => rareSprite,
            ECardRarity.Epic => epicSprite,
            ECardRarity.Legendary => legendarySprite,
            _ => commonSprite
        };

        spriteRenderer.sprite = selectedSprite;
        
        return cardType;
    }
}