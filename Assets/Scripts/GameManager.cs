using UnityEngine;

public class GameManager : MonoBehaviour
{
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
}
