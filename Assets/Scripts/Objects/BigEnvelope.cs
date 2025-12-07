using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
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

    public static event Action EnvelopeChanged;
    public static event Action EnvelopeSent;

    private void Awake()
    {
        // Auto-find components if not wired in the inspector.
        spriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
        image ??= GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        Envelope.EnvelopeClicked += OnEnvelopeClicked;
        AddressTyper.LetterSend += OnLetterSend;
    }

    private void OnDisable()
    {
        Envelope.EnvelopeClicked -= OnEnvelopeClicked;
        AddressTyper.LetterSend -= OnLetterSend;
    }

    public void Start()
    {
        transform.position = outPos;
    }

    private void OnEnvelopeClicked(Sprite sprite)
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

    private void OnLetterSend(string address)
    {
        envelopeSlideTween = transform.DOMoveX(slideSendPos, slideDuration).SetEase(sendEase);
        envelopeSlideTween.OnComplete(() =>
        {
            ResetEnvelope();
            EnvelopeSent?.Invoke();
        });
    }

    private void ResetEnvelope()
    {
        // Kill Tween
        envelopeSlideTween?.Kill();
        
        // Reset Address Text
        EnvelopeChanged?.Invoke();
        
        // Reset position away from screen
        transform.position = outPos;
    }

    private void SlideIn()
    {
        envelopeSlideTween = transform.DOMoveX(slideInPos, slideDuration).SetEase(slideInEase);
    }
}
