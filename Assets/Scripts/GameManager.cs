using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        var address = RandomAussieAddressGenerator.GetRandomAddress();
        Debug.Log($"Ship to: {address}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowNextOrderAddress(string name, string addressLine1, string addressLine2)
    {
        orderAddress.text = $"{name}\n{addressLine1}\n{addressLine2}";
    }

#if UNITY_EDITOR
    [ContextMenu("Generate Random Name")]
    public void GenerateRandomNameInEditor()
    {
        lastGeneratedName = RandomNameGenerator.GetRandomName();
        UnityEditor.EditorUtility.SetDirty(this);
        Debug.Log($"Generated name: {lastGeneratedName}");
    }

    [ContextMenu("Generate Random Address")]
    public void GenerateRandomAddressInEditor()
    {
        lastGeneratedAddress = RandomAussieAddressGenerator.GetRandomAddress(false);
        UnityEditor.EditorUtility.SetDirty(this); // mark scene dirty so it persists
        Debug.Log($"Generated address: {lastGeneratedAddress}");
    }

    [ContextMenu("Generate Random Name + Address")]
    public void GenerateRandomNameAndAddressInEditor()
    {
        lastGeneratedName = RandomNameGenerator.GetRandomName();
        RandomAussieAddressGenerator.AddressParts generatedAddress = RandomAussieAddressGenerator.CreateAddressParts(null, false);
        lastGeneratedAddress = $"{generatedAddress.StreetLine}, {generatedAddress.SuburbStateLine}";
        ShowNextOrderAddress(lastGeneratedName, generatedAddress.StreetLine, generatedAddress.SuburbStateLine);
        UnityEditor.EditorUtility.SetDirty(this);
        Debug.Log($"Generated name: {lastGeneratedName}");
        Debug.Log($"Generated address: {lastGeneratedAddress}");
    }
#endif
}
