using System;
using Enums;
using NSBLib.EventChannelSystem;
using Generators;
using NSBLib.Helpers;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Score;
    [SerializeField] private float duration = 60f;
    
    [SerializeField] private float currentTime;
    [Header( "Events")]
    [SerializeField] IntEventChannel updateScore;
    [SerializeField] private StringEventChannel AddressGenerated;
    [SerializeField] private StringEventChannel NameGenerated;
    [SerializeField] private GameStateEventChannel GameStateChanged;
    [SerializeField] private FloatEventChannel timeChanged;
    
    
    [Header("UI")]
    [SerializeField] TMP_Text letterAddress;
    [SerializeField] TMP_Text orderAddress;
    [SerializeField] TMP_Text orderEnvelope;
    [SerializeField] TMP_Text orderCard;

    [Header( "Last Generated Data" )]
    public string LastGeneratedName => lastGeneratedName;
    public string LastGeneratedAddress => lastGeneratedAddress;
    public EGameState CurrentGameState;

    [Header( "Generated Data")]
    [SerializeField] ECardRarity lastGeneratedCard;
    [SerializeField] EEnvelopeType lastGeneratedEnvelope;
    [SerializeField] string lastGeneratedName;
    [SerializeField] string lastGeneratedAddress;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = duration;
        ShowNextOrder();
        letterAddress.text = string.Empty;
    }

    private void Update()
    {
        UpdateTimer();
    }

    public void AddScore(int points)
    {
        Score += points;
        updateScore?.Invoke(Score);
    }

    public void AddTimer(float additionalTime)
    {
        currentTime += additionalTime;
    }

    private void UpdateTimer()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timeChanged?.Invoke(currentTime / duration);

            if (currentTime <= 0)
            {
                currentTime = 0;
                timeChanged?.Invoke(0f);
                // Optional: Trigger Game Over or Time Up logic here
            }
        }
    }

    public void ShowNextOrder()
    {
        GameStateChanged?.Invoke(EGameState.SelectEnvelopeState);
        string randomName = GenerateNextName();
        Address generatedAddress = GenerateNextAddress();
        var card = GenerateNextCard(CardManager.Instance.GetRandomCard());
        var envelope = GenerateEnvelopeType();
        
        UpdateTextFields(randomName, generatedAddress, envelope, card);
    }

    public void SetGameState(EGameState state)
    {
        CurrentGameState = state;
    }

    private void UpdateTextFields(string randomName, Address generatedAddress, EEnvelopeType envelope, string cardName)
    {
        orderAddress.text = $"{randomName}\n{generatedAddress.StreetLine}\n{generatedAddress.SuburbStateLine}";
        // orderEnvelope.text = ;
        orderCard.text = $"{cardName} - {envelope.ToString()}";
    }
    
    public EEnvelopeType GenerateEnvelopeType() => LastGeneratedEnvelope((EEnvelopeType)Random.Range(0, 3));

    public string GenerateNextCard(ECardRarity rarity)
    {
        SetLastGeneratedCard(rarity);
        return rarity.ToString();
    }

    public string GenerateNextName()
    {
        var name = RandomNameGenerator.GetRandomName();
        NameGenerated?.Invoke(name);
        SetLastGeneratedName(name);
        return name;
    }

    public Address GenerateNextAddress(bool allowUnit = false)
    {
        Address generatedAddress = GenerateAddress(allowUnit);
        lastGeneratedAddress = $"{generatedAddress.StreetLine}, {generatedAddress.SuburbStateLine}";
        NSBLogger.Log($"Ship to: {lastGeneratedAddress}");
        return generatedAddress;
    }

    private Address GenerateAddress(bool allowUnit)
    {
        var address = RandomAussieAddressGenerator.CreateAddressParts(allowUnit);
        AddressGenerated?.Invoke(address.CompleteAddress);
        return address;
    }

    public void SetLetterAddress(string address)
    {
        letterAddress.text = address;
    }

    public void ClearText()
    {
        letterAddress.text = string.Empty;
    }

#if UNITY_EDITOR
    [ContextMenu("Generate Random Name")]
    public void GenerateRandomNameInEditor()
    {
        SetLastGeneratedName(RandomNameGenerator.GetRandomName());
        UnityEditor.EditorUtility.SetDirty(this);
    }

    [ContextMenu("Generate Random Address")]
    public void GenerateRandomAddressInEditor()
    {
        SetLastGenerateAddress(RandomAussieAddressGenerator.GetRandomAddress(false));
        UnityEditor.EditorUtility.SetDirty(this); // mark scene dirty so it persists
    }

    [ContextMenu("Generate Random Card")]
    public void GenerateRandomCardInEditor()
    {
        SetLastGeneratedCard(RandomCardGenerator.GenerateRandomCard());
        UnityEditor.EditorUtility.SetDirty(this); // mark scene dirty so it persists
    }

    [ContextMenu("Generate Random Name + Address")]
    public void GenerateRandomeOrderInEditor()
    {
        SetLastGeneratedName(RandomNameGenerator.GetRandomName());
        Address generatedAddress = GenerateAddress(false);
        SetLastGenerateAddress($"{generatedAddress.StreetLine}, {generatedAddress.SuburbStateLine}");
        SetLastGeneratedCard(CardManager.Instance.GetRandomCard());
        // ShowNextOrderAddress(lastGeneratedName, generatedAddress.StreetLine, generatedAddress.SuburbStateLine);
        UnityEditor.EditorUtility.SetDirty(this);
    }

    private void SetLastGeneratedName(string personName)
    {
        lastGeneratedName = personName;
        NSBLogger.Log($"Generated name: {lastGeneratedName}");
    }

    private void SetLastGenerateAddress(string address)
    {
        lastGeneratedAddress = address;
        NSBLogger.Log($"Generated address: {lastGeneratedAddress}");
    }

    private void SetLastGeneratedCard(ECardRarity rarity)
    {
        lastGeneratedCard = rarity;
        NSBLogger.Log($"Generated card: {rarity}");
    }

    private EEnvelopeType LastGeneratedEnvelope(EEnvelopeType envelopeType)
    {
        lastGeneratedEnvelope = envelopeType;
        NSBLogger.Log($"Generated envelope: {envelopeType}");
        return envelopeType;
    }
#endif
}
