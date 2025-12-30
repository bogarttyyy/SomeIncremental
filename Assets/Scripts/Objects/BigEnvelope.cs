using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Interfaces;
using NSBLib.EventChannelSystem;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Listens for envelope click events and swaps this object's sprite accordingly.
/// Supports either SpriteRenderer (world) or Image (UI) components.
/// </summary>
public class BigEnvelope : MonoBehaviour, IClickable
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private string currentName;
    [FormerlySerializedAs("currentAddress")] [SerializeField] private string currentAddressString;
    private Address envelopeAddress;
    [SerializeField] private float timerAdd = 1f;
    [SerializeField] private Sprite bigEnvelopeSprite;
    [SerializeField] private Vector2 outPos;
    [SerializeField] private Vector2 slideInPos;
    [SerializeField] private Vector2 slideSendPos;
    [SerializeField] private float slideDuration=0.5f;
    [SerializeField] private Ease slideInEase = Ease.InOutQuad;
    [SerializeField] private Ease sendEase = Ease.Flash;
    [SerializeField] private Image image;

    private TweenerCore<Vector3, Vector3, VectorOptions> envelopeSlideTween;

    [SerializeField] private EventChannel EnvelopeChanged;
    [SerializeField] private BigEnvelopeEventChannel EnvelopeSent;
    [SerializeField] private FloatEventChannel addTimer;
    [SerializeField] private StringEventChannel envelopeAddressChanged;

    [SerializeField] private List<Stamp> stamps = new();

    private void Awake()
    {
        // Auto-find components if not wired in the inspector.
        spriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
        image ??= GetComponentInChildren<Image>();
    }

    public void Start()
    {
        transform.position = outPos;
        OnEnvelopeClicked(bigEnvelopeSprite);
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
        GameManager.Instance.ClearText();
        envelopeSlideTween = transform.DOMove(slideSendPos, slideDuration).SetEase(sendEase);
        envelopeSlideTween.OnComplete(() =>
        {
            ResetEnvelope();
            EnvelopeSent?.Invoke(this);
            OnEnvelopeClicked(bigEnvelopeSprite);
            addTimer.Invoke(timerAdd);
            ClearStamps();
        });
    }

    private void ResetEnvelope()
    {
        // Clear Stamps
        // ClearStamps();

        // Kill Tween
        envelopeSlideTween?.Kill();
        
        // Reset Address Text
        EnvelopeChanged?.Invoke(new Empty());
        
        // Reset position away from screen
        transform.position = outPos;

    }

    private void SlideIn()
    {
        envelopeSlideTween = transform.DOMove(slideInPos, slideDuration).SetEase(slideInEase);
        envelopeSlideTween.OnComplete(SetEnvelopeAddress);
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

    public void OnClicked()
    {
        OnLetterSend(string.Empty);
    }

    private void SetEnvelopeAddress()
    {
        var address = $"{currentName}\n{currentAddressString}";
        envelopeAddressChanged.Invoke(address.ToUpperInvariant());
    }

    public void SetCurrentAddress(Address address)
    {
        envelopeAddress = address;
        currentAddressString = address.CompleteAddress;
        SetEnvelopeAddress();
    }

    public void SetCurrentName(string name)
    {
        currentName = name;
    }

    public Address GetAddress()
    {
        return envelopeAddress;
    }
    
    public List<Stamp> GetStamps()
    {
        return stamps;
    }
}
