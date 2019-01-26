using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class ImageUpload : MonoBehaviour
{
    [DllImport( "__Internal" )]
    private static extern void ImageUploaderCaptureClick ();

    public RawImage Image;

    IEnumerator LoadTexture (string url)
    {
        WWW image = new WWW( url );
        yield return image;
        Texture2D texture = new Texture2D( 1, 1 );
        image.LoadImageIntoTexture( texture );
        Debug.Log( "Loaded image size: " + texture.width + "x" + texture.height );
        Image.texture = texture;
        Image.SizeToParent();
    }

    void FileSelected (string url)
    {
        StartCoroutine( LoadTexture( url ) );
    }

    public void OnButtonPointerDown ()
    {
        ImageUploaderCaptureClick();
    }


}