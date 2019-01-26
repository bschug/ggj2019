using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ImageFromLetterPage : PageDependentBinding
{
    string currentUrl;
    RawImage image;
    Texture2D texture;

    public void Update ()
    {
        if (currentUrl != Page.ImageUrl) {
            ForceUpdate();
        }
    }

    public void ForceUpdate ()
    {
        image = image ?? GetComponent<RawImage>();
        currentUrl = Page.ImageUrl;
        
    }
}
