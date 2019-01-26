using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour
{
    public StringVariable LetterId;
    public StringVariable ComposeLetterWebsite;

    private void Start ()
    {
        var url = new Uri(Application.absoluteURL);
        if (!string.IsNullOrEmpty(url.Fragment)) {
            LetterId.Value = url.Fragment.Trim(new char[] { '#' } );
            SceneManager.LoadScene( "Receive" );
        }
        else {
            Application.OpenURL( ComposeLetterWebsite.Value );
        }
    }
}
