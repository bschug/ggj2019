using UnityEngine;
using System.Collections;
using System;

public enum PageLayout
{
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
