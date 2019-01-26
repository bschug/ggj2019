using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Letter/Letter")]
public class LetterDefinition : ScriptableObject
{
    public string SenderName;
    public string RecipientName;

    public LetterPage[] Pages;
}
