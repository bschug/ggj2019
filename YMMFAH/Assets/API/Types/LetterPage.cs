using UnityEngine;
using System.Collections;
using System;

public enum PageLayout
{
    None,
    TextOnly,
    ImageOnly,
    TextThenImage,
    ImageThenText
}

[Serializable]
public class LetterPage 
{
    public string Text;
    public string ImageUrl;
    public PageLayout Layout;
}
