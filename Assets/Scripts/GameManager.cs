using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text letterAddress;
    [SerializeField]
    TMP_Text orderAddress;

    public string LastGeneratedName => lastGeneratedName;
    public string LastGeneratedAddress => lastGeneratedAddress;

    [SerializeField]
    string lastGeneratedName;

    [SerializeField]
    string lastGeneratedAddress;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowNextOrderAddress();
        letterAddress.text = string.Empty;
    }

    private void ShowNextOrderAddress()
    {
        string randomName = RandomNameGenerator.GetRandomName();
        EAddressParts generatedEAddress = GenerateNextAddress();
        orderAddress.text = $"{randomName}\n{generatedEAddress.StreetLine}\n{generatedEAddress.SuburbStateLine}";
    }

    public EAddressParts GenerateNextAddress(bool allowUnit = false)
    {
        EAddressParts generatedEAddress = RandomAussieAddressGenerator.CreateAddressParts(null, allowUnit);
        lastGeneratedAddress = $"{generatedEAddress.StreetLine}, {generatedEAddress.SuburbStateLine}";
        Debug.Log($"Ship to: {lastGeneratedAddress}");
        return generatedEAddress;
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

#if UNITY_EDITOR
    [ContextMenu("Generate Random Name")]
    public void GenerateRandomNameInEditor()
    {
        UnityEditor.EditorUtility.SetDirty(this);
    }

    [ContextMenu("Generate Random Address")]
    public void GenerateRandomAddressInEditor()
    {
        SetLastGenerateAddress(RandomAussieAddressGenerator.GetRandomAddress(false));
        UnityEditor.EditorUtility.SetDirty(this); // mark scene dirty so it persists
    }

    [ContextMenu("Generate Random Name + Address")]
    public void GenerateRandomNameAndAddressInEditor()
    {
        SetLastGeneratedName(RandomNameGenerator.GetRandomName());
        EAddressParts generatedEAddress = RandomAussieAddressGenerator.CreateAddressParts(null, false);
        SetLastGenerateAddress($"{generatedEAddress.StreetLine}, {generatedEAddress.SuburbStateLine}");
        // ShowNextOrderAddress(lastGeneratedName, generatedAddress.StreetLine, generatedAddress.SuburbStateLine);
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
}
