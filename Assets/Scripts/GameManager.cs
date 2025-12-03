using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    string lastGeneratedName;

    [SerializeField]
    string lastGeneratedAddress;

    public string LastGeneratedName => lastGeneratedName;
    public string LastGeneratedAddress => lastGeneratedAddress;

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

#if UNITY_EDITOR
    [ContextMenu("Generate Random Name")]
    public void GenerateRandomNameInEditor()
    {
        lastGeneratedName = RandomNameGenerator.GetRandomName();
        UnityEditor.EditorUtility.SetDirty(this);
        Debug.Log($"Generated name: {lastGeneratedName}");
    }

    [ContextMenu("Generate Random Name + Address")]
    public void GenerateRandomNameAndAddressInEditor()
    {
        lastGeneratedName = RandomNameGenerator.GetRandomName();
        lastGeneratedAddress = RandomAussieAddressGenerator.GetRandomAddress(false);
        UnityEditor.EditorUtility.SetDirty(this);
        Debug.Log($"Generated name: {lastGeneratedName}");
        Debug.Log($"Generated address: {lastGeneratedAddress}");
    }

    [ContextMenu("Generate Random Address")]
    public void GenerateRandomAddressInEditor()
    {
        lastGeneratedAddress = RandomAussieAddressGenerator.GetRandomAddress(false);
        UnityEditor.EditorUtility.SetDirty(this); // mark scene dirty so it persists
        Debug.Log($"Generated address: {lastGeneratedAddress}");
    }
#endif
}
