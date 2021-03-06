﻿using UnityEngine;
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
    public Texture2D ImageTexture;

    [SerializeField] string Layout;
    public PageLayout PageLayout => (PageLayout)PageLayout.Parse(typeof(PageLayout), Layout );
}
