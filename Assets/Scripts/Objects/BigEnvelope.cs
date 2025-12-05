using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Listens for envelope click events and swaps this object's sprite accordingly.
/// Supports either SpriteRenderer (world) or Image (UI) components.
/// </summary>
public class BigEnvelope : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Image image;

    private void Awake()
    {
        // Auto-find components if not wired in the inspector.
        spriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
        image ??= GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        Envelope.EnvelopeClicked.AddListener(OnEnvelopeClicked);
    }

    private void OnDisable()
    {
        Envelope.EnvelopeClicked.RemoveListener(OnEnvelopeClicked);
    }

    private void OnEnvelopeClicked(Sprite sprite)
    {
        if (sprite == null) return;

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }

        if (image != null)
        {
            image.sprite = sprite;
        }
    }
}
