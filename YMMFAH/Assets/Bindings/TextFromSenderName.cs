using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFromSenderName : MonoBehaviour
{
    public LetterDefinition Letter;
    public string Format = "Yours,\n{0}";
    TextMeshProUGUI textField;

    string currentName;

    public void Update ()
    {
        if (currentName != Letter.SenderName) {
            ForceUpdate();
        }
    }

    public void ForceUpdate ()
    {
        textField = textField ?? GetComponent<TextMeshProUGUI>();
        currentName = Letter.SenderName;
        textField.text = string.Format( Format, currentName );
    }
}
