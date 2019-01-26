using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextFromLetterPage : PageDependentBinding
{
    TextMeshProUGUI Target;
    private string currentText;

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
