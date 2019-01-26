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
    string currentUrl;
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
        LoadImage( currentUrl );
    }

    public async Task LoadImage(string url)
    {
        var request = UnityWebRequest.Get( url );
        await request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError) {
            Debug.LogError( "Failed to load " + url );
            return;
        }
        texture = new Texture2D( 1, 1 );
        texture.LoadImage( request.downloadHandler.data, true );
        Debug.LogFormat( "Loaded {0}x{1} texture", texture.width, texture.height );
        image.texture = texture;

        image.SizeToParent();
        frame.rectTransform.offsetMin = image.rectTransform.offsetMin - new Vector2(70,70);
        frame.rectTransform.offsetMax = image.rectTransform.offsetMax + new Vector2(70,70);
    }
}
