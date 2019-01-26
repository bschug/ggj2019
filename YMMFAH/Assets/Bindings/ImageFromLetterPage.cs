using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageFromLetterPage : PageDependentBinding
{
    [SerializeField] Image frame;
    [SerializeField] RawImage image;
    Texture2D currentTexture;

    public void Update ()
    {
        if (currentTexture != Page.ImageTexture) {
            ForceUpdate();
        }
    }

    public void ForceUpdate ()
    {
        image = image ?? GetComponent<RawImage>();
        currentTexture = Page.ImageTexture;

        image.texture = currentTexture;
        image.SizeToParent();
        frame.rectTransform.offsetMin = image.rectTransform.offsetMin - new Vector2( 70, 70 );
        frame.rectTransform.offsetMax = image.rectTransform.offsetMax + new Vector2( 70, 70 );
    }

}
