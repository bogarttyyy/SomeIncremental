using Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class Envelope : MonoBehaviour, IClickable
{
    public static readonly UnityEvent<Sprite> EnvelopeClicked = new UnityEvent<Sprite>();

    [SerializeField] private Sprite bigSprite;

    public void OnClicked()
    {
        Debug.Log($"Clicked: {gameObject.name}");
        EnvelopeClicked.Invoke(bigSprite);
    }
}
