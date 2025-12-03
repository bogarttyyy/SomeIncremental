using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    string lastGeneratedAddress;

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
    [ContextMenu("Generate Random Address")]
    public void GenerateRandomAddressInEditor()
    {
        lastGeneratedAddress = RandomAussieAddressGenerator.GetRandomAddress();
        UnityEditor.EditorUtility.SetDirty(this); // mark scene dirty so it persists
        Debug.Log($"Generated address: {lastGeneratedAddress}");
    }
#endif
}
