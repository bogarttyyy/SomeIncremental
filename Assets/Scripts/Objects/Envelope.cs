using Interfaces;
using UnityEngine;

public class Envelope : MonoBehaviour,IClickable
{
    public BigEnvelope bigEnvelope;
    
    public void OnClicked()
    {
        Debug.Log($"Clicked: {gameObject.name}");
    }
}
