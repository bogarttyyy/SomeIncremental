using TMPro;
using UnityEngine;

/// <summary>
/// Handles freeform typing (letters, numbers, symbols) into a TMP_Text,
/// adding a newline on Enter, or submitting/advancing on Ctrl+Enter.
/// </summary>
public class AddressTyper : MonoBehaviour
{
    [SerializeField] TMP_Text targetText;
    [SerializeField] GameManager gameManager;

    string currentText = string.Empty;

    void Awake()
    {
        if (targetText == null)
        {
            targetText = GetComponent<TMP_Text>();
        }
    }

    void Update()
    {
        if (targetText == null || gameManager == null) return;

        HandleTyping();
        HandleEnterKeys();
    }

    void HandleTyping()
    {
        foreach (char c in Input.inputString)
        {
            switch (c)
            {
                case '\b': // backspace
                    if (currentText.Length > 0)
                        currentText = currentText.Substring(0, currentText.Length - 1);
                    break;
                case '\n':
                case '\r':
                    // Enter handling is done separately so we can detect modifiers.
                    break;
                default:
                    currentText += c;
                    break;
            }
        }

        targetText.text = currentText;
    }

    void HandleEnterKeys()
    {
        bool enterDown = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter);
        if (!enterDown) return;

        bool ctrlHeld = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        if (ctrlHeld)
        {
            SubmitAndAdvance();
        }
        else
        {
            InsertNewLine();
        }
    }

    // Ctrl+Enter: advance to next order address and clear current input.
    public void SubmitAndAdvance()
    {
        currentText = string.Empty;
        targetText.text = string.Empty;
        gameManager.ShowNextOrderAddress();
    }

    // Enter: append a newline to the current input.
    public void InsertNewLine()
    {
        currentText += "\n";
        targetText.text = currentText;
    }
}
