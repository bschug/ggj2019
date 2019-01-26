using UnityEngine;
using System.Collections;
using TMPro;

public class TextFromLetterPage : MonoBehaviour
{
    public LetterPage Page;
    public TextMeshProUGUI Target;

    private string currentText;

    public void Update ()
    {
        if (currentText != Page.Text) {
            ForceUpdate();
        }
    }

    [ContextMenu("Apply Now")]
    public void ForceUpdate ()
    {
        currentText = Page.Text;
        Target.text = currentText;
    }
}
