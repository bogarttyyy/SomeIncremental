using System;
using Enums;
using NSBLib.EventChannelSystem;
using NSBLib.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// Handles freeform typing (letters, numbers, symbols) into a TMP_Text,
/// adding a newline on Enter, or submitting/advancing on Ctrl+Enter.
/// </summary>
public class AddressTyper : MonoBehaviour
{
    [SerializeField] TMP_Text targetText;
    [SerializeField] IntEventChannel addScore;
    [SerializeField] StringEventChannel LetterSend;
    [SerializeField] private GameStateEventChannel gamestateChanged;
    

    [SerializeField] bool enableWriting;
    string currentText = string.Empty;
    bool backspaceCameFromTextEvent;

    void Awake()
    {
        if (targetText == null)
        {
            targetText = GetComponent<TMP_Text>();
        }
    }

    void OnEnable()
    {
        if (Keyboard.current != null)
        {
            Keyboard.current.onTextInput += OnTextInput;
        }
        NSBLogger.Log("AddressTyper OnEnable()");
    }

    void OnDisable()
    {
        if (Keyboard.current != null)
        {
            Keyboard.current.onTextInput -= OnTextInput;
        }
        NSBLogger.Log("AddressTyper OnDisable()");
    }

    void Update()
    {
        if (!targetText) return;
        var keyboard = Keyboard.current;
        if (keyboard == null) return; // no keyboard available

        HandleControlKeys(keyboard);
        if (!enableWriting) return;
        targetText.text = currentText;

        // reset per-frame flags
        backspaceCameFromTextEvent = false;
    }

    // Shift+Enter: advance to next order address and clear current input.
    public void SubmitAndAdvance()
    {
        LetterSend?.Invoke(currentText);
        gamestateChanged?.Invoke(EGameState.EnvelopeSendState);
        addScore.Invoke(1);
        ClearText();
    }

    public void OnEnvelopeChanged()
    {
        NSBLogger.Log("OnChangeEnvelope");
        ClearText();
    }

    public void EnableWriting(EGameState state) => enableWriting = state == EGameState.WriteAddressState;

    void OnTextInput(char c)
    {
        // New Input System text event; includes letters, numbers, symbols.
        if (c == '\n' || c == '\r') return; // Enter handled separately
        if (c == '\b')
        {
            // Some platforms send backspace as text input; treat it as delete-previous-char.
            DeletePreviousChar();
            backspaceCameFromTextEvent = true;
            return;
        }
        // Allow only letters, digits, symbols/punctuation, and space.
        if (char.IsLetterOrDigit(c) || char.IsSymbol(c) || char.IsPunctuation(c) || c == ' ')
        {
            currentText += char.ToUpperInvariant(c);
        }
    }

    void HandleControlKeys(Keyboard keyboard)
    {
        if (WasPressed(keyboard.backspaceKey) && !backspaceCameFromTextEvent)
        {
            if (CtrlHeld(keyboard))
            {
                DeletePreviousWord();
            }
            else
            {
                DeletePreviousChar();
            }
        }

        bool enterDown = WasPressed(keyboard.enterKey) || WasPressed(keyboard.numpadEnterKey);
        if (!enterDown) return;

        if (CtrlHeld(keyboard))
        {
            SubmitAndAdvance();
        }
        else
        {
            InsertNewLine();
        }
    }

    bool CtrlHeld(Keyboard keyboard) => keyboard.shiftKey != null && keyboard.shiftKey.isPressed;

    bool WasPressed(KeyControl key) => key != null && key.wasPressedThisFrame;

    void DeletePreviousChar()
    {
        if (currentText.Length > 0)
            currentText = currentText.Substring(0, currentText.Length - 1);
    }

    void DeletePreviousWord()
    {
        if (string.IsNullOrEmpty(currentText)) return;

        int i = currentText.Length;
        // skip trailing whitespace
        while (i > 0 && char.IsWhiteSpace(currentText[i - 1])) i--;
        // skip word characters
        while (i > 0 && !char.IsWhiteSpace(currentText[i - 1])) i--;

        currentText = currentText.Substring(0, i);
    }

    // Enter: append a newline to the current input.
    public void InsertNewLine()
    {
        currentText += "\n";
        targetText.text = currentText;
    }

    private void ClearText()
    {
        currentText = string.Empty;
        targetText.text = string.Empty;
    }
}