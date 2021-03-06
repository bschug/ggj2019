﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLayout : PageDependentBinding
{
    public GameObject TextOnly;
    public GameObject ImageOnly;
    public GameObject TextThenImage;
    public GameObject ImageThenText;

    PageLayout currentLayout = PageLayout.None;

    public void Update ()
    {
        if (currentLayout != Page?.PageLayout) {
            ForceUpdate();
        }
    }

    [ContextMenu("Apply Now")]
    public void ForceUpdate ()
    {
        currentLayout = Page.PageLayout;
        TextOnly.SetActive( currentLayout == PageLayout.TextOnly );
        ImageOnly.SetActive( currentLayout == PageLayout.ImageOnly );
        TextThenImage.SetActive( currentLayout == PageLayout.TextThenImage );
        ImageThenText.SetActive( currentLayout == PageLayout.ImageThenText );
    }
}
