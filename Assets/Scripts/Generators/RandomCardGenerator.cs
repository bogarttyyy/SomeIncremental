using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Generators
{
    public static class RandomCardGenerator
    {
        public static Sprite commonSprite;
        public static Sprite uncommonSprite;
        public static Sprite rareSprite;
        public static Sprite epicSprite;
        public static Sprite legendarySprite;

        private static readonly IReadOnlyDictionary<ECardRarity, Sprite> cardRarities = new Dictionary<ECardRarity, Sprite>()
        {
            { ECardRarity.Common, commonSprite},
            { ECardRarity.Uncommon, uncommonSprite},
            { ECardRarity.Rare, rareSprite},
            { ECardRarity.Epic, epicSprite},
            { ECardRarity.Legendary, legendarySprite}
        };

        public static ECardRarity GenerateRandomCard()
        {
            return (ECardRarity)Random.Range(0, cardRarities.Count);
        }
    }
}