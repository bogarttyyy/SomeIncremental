using System;
using Enums;
using Generators;
using Helpers;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] TMP_Text letterAddress;
    [SerializeField] TMP_Text orderAddress;
    [SerializeField] TMP_Text orderEnvelope;
    [SerializeField] TMP_Text orderCard;

    public string LastGeneratedName => lastGeneratedName;
    public string LastGeneratedAddress => lastGeneratedAddress;

    [SerializeField] ECardRarity lastGeneratedCard;
    [SerializeField] EEnvelopeType lastGeneratedEnvelope;
    [SerializeField] string lastGeneratedName;
    [SerializeField] string lastGeneratedAddress;

    private void OnEnable()
    {
        BigEnvelope.EnvelopeSent += ShowNextOrder;
    }
    
    private void OnDisable()
    {
        BigEnvelope.EnvelopeSent -= ShowNextOrder;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowNextOrder();
        letterAddress.text = string.Empty;
    }

    public void ShowNextOrder()
    {
        string randomName = GenerateNextName();
        Address generatedAddress = GenerateNextAddress();
        var card = GenerateNextCard(CardManager.Instance.GetRandomCard());
        var envelope = GenerateEnvelopeType();
        
        UpdateTextFields(randomName, generatedAddress, envelope, card);
    }

    private void UpdateTextFields(string randomName, Address generatedAddress, EEnvelopeType envelope, string cardName)
    {
        orderAddress.text = $"{randomName}\n{generatedAddress.StreetLine}\n{generatedAddress.SuburbStateLine}";
        orderEnvelope.text = envelope.ToString();
        orderCard.text = cardName;
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
        SetLastGeneratedName(name);
        return name;
    }

    public Address GenerateNextAddress(bool allowUnit = false)
    {
        Address generatedAddress = RandomAussieAddressGenerator.CreateAddressParts(allowUnit);
        lastGeneratedAddress = $"{generatedAddress.StreetLine}, {generatedAddress.SuburbStateLine}";
        NSBLogger.Log($"Ship to: {lastGeneratedAddress}");
        return generatedAddress;
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
        Address generatedAddress = RandomAussieAddressGenerator.CreateAddressParts(false);
        SetLastGenerateAddress($"{generatedAddress.StreetLine}, {generatedAddress.SuburbStateLine}");
        SetLastGeneratedCard(CardManager.Instance.GetRandomCard());
        // ShowNextOrderAddress(lastGeneratedName, generatedAddress.StreetLine, generatedAddress.SuburbStateLine);
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
}
