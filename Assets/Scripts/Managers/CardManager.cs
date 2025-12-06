using Enums;
using Generators;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }
    
    public static Sprite commonSprite;
    public static Sprite uncommonSprite;
    public static Sprite rareSprite;
    public static Sprite epicSprite;
    public static Sprite legendarySprite;
    
    public Card selectedCard;
    
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

    public void GetRandomCard()
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

        selectedCard.GetComponent<SpriteRenderer>().sprite = selectedSprite;
    }
}