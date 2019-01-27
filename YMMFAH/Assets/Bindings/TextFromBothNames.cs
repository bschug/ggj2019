using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextFromBothNames : MonoBehaviour
{
    public LetterDefinition Letter;

    [Tooltip("Use {0} for recipient, {1} for sender")]
    public string Format = "Hi, {0}! I have a message from {1} for you!";

    TextMeshProUGUI textField;
    string currentSender;
    string currentRecipient;

    public void Update ()
    {
        if (currentSender != Letter.SenderName || currentRecipient != Letter.RecipientName) {
            ForceUpdate();
        }
    }

    [ContextMenu("Apply Now")]
    public void ForceUpdate ()
    {
        textField = textField ?? GetComponent<TextMeshProUGUI>();
        currentSender = Letter.SenderName;
        currentRecipient = Letter.RecipientName;

        textField.text = string.Format( Format, currentRecipient, currentSender );
    }
}
