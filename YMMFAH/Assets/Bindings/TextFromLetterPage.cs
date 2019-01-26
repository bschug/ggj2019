using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextFromLetterPage : MonoBehaviour
{
    public LetterDefinition Letter;
    public int PageNr;
    TextMeshProUGUI Target;

    private string currentText;

    private LetterPage Page => Letter.Pages.Length > PageNr ? Letter.Pages[PageNr] : null;

    public void Update ()
    {
        if (currentText != Page?.Text) {
            ForceUpdate();
        }
    }

    [ContextMenu("Apply Now")]
    public void ForceUpdate ()
    {
        Target = Target ?? GetComponent<TextMeshProUGUI>();
        currentText = Page != null ? Page.Text : "";
        Target.text = currentText;
    }
}
