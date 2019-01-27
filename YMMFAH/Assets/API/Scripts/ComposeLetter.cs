using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "API/Compose Letter")]
public class ComposeLetter : ScriptableObject
{
    public StringVariable ComposeLetterWebsite;

    public void Open ()
    {
        Application.OpenURL( ComposeLetterWebsite.Value );
    }
}
