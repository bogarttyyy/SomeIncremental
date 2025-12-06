using Enums;
using Generators;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    TMP_Text letterAddress;
    [SerializeField]
    TMP_Text orderAddress;

    public string LastGeneratedName => lastGeneratedName;
    public string LastGeneratedAddress => lastGeneratedAddress;

    [SerializeField] ECardRarity lastGeneratedCard;
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
        ShowNextOrderAddress();
        letterAddress.text = string.Empty;
    }

    public void ShowNextOrderAddress()
    {
        string randomName = RandomNameGenerator.GetRandomName();
        Address generatedAddress = GenerateNextAddress();
        orderAddress.text = $"{randomName}\n{generatedAddress.StreetLine}\n{generatedAddress.SuburbStateLine}";
    }

    public Address GenerateNextAddress(bool allowUnit = false)
    {
        Address generatedAddress = RandomAussieAddressGenerator.CreateAddressParts(allowUnit);
        lastGeneratedAddress = $"{generatedAddress.StreetLine}, {generatedAddress.SuburbStateLine}";
        Debug.Log($"Ship to: {lastGeneratedAddress}");
        return generatedAddress;
    }

    private void SetLastGeneratedName(string personName)
    {
        lastGeneratedName = personName;
        Debug.Log($"Generated name: {lastGeneratedName}");
    }

    private void SetLastGenerateAddress(string address)
    {
        lastGeneratedAddress = address;
        Debug.Log($"Generated address: {lastGeneratedAddress}");
    }

    private void SetLastGeneratedCard(ECardRarity rarity)
    {
        lastGeneratedCard = rarity;
        Debug.Log($"Generated card: {rarity}");
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
        SetLastGeneratedCard(RandomCardGenerator.GenerateRandomCard());
        // ShowNextOrderAddress(lastGeneratedName, generatedAddress.StreetLine, generatedAddress.SuburbStateLine);
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
}
