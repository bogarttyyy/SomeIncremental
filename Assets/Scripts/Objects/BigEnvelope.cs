using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NSBLib.EventChannelSystem;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Listens for envelope click events and swaps this object's sprite accordingly.
/// Supports either SpriteRenderer (world) or Image (UI) components.
/// </summary>
public class BigEnvelope : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 outPos;
    [SerializeField] private float slideInPos;
    [SerializeField] private float slideSendPos;
    [SerializeField] private float slideDuration=0.5f;
    [SerializeField] private Ease slideInEase = Ease.InOutQuad;
    [SerializeField] private Ease sendEase = Ease.Flash;
    [SerializeField] private Image image;

    private TweenerCore<Vector3, Vector3, VectorOptions> envelopeSlideTween;

    [SerializeField] private EventChannel EnvelopeChanged;
    [SerializeField] private EventChannel EnvelopeSent;

    [SerializeField]
    private List<Stamp> stamps = new();

    private void Awake()
    {
        // Auto-find components if not wired in the inspector.
        spriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
        image ??= GetComponentInChildren<Image>();
    }

    public void Start()
    {
        transform.position = outPos;
    }

    public void OnEnvelopeClicked(Sprite sprite)
    {
        ResetEnvelope();
        if (sprite == null) return;

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }

        if (image != null)
        {
            image.sprite = sprite;
        }

        SlideIn();
    }

    public void OnLetterSend(string address)
    {
        envelopeSlideTween = transform.DOMoveX(slideSendPos, slideDuration).SetEase(sendEase);
        envelopeSlideTween.OnComplete(() =>
        {
            ResetEnvelope();
            EnvelopeSent?.Invoke(new Empty());
        });
    }

    private void ResetEnvelope()
    {
        // Clear Stamps
        ClearStamps();

        // Kill Tween
        envelopeSlideTween?.Kill();
        
        // Reset Address Text
        EnvelopeChanged?.Invoke(new Empty());
        
        // Reset position away from screen
        transform.position = outPos;
    }

    private void SlideIn()
    {
        envelopeSlideTween = transform.DOMoveX(slideInPos, slideDuration).SetEase(slideInEase);
    }

    public void AddStamp(Stamp stamp)
    {
        stamps.Add(stamp);
        stamp.transform.SetParent(transform);
    }

    public void ClearStamps()
    {
        stamps.ForEach(stamp => Destroy(stamp.gameObject));
        stamps.Clear();
    }
}
