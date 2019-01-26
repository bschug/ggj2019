using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour
{
    public StringVariable LetterId;

    private void Start ()
    {
        var url = new Uri(Application.absoluteURL);
        if (!string.IsNullOrEmpty(url.Fragment)) {
            LetterId.Value = url.Fragment.Trim(new char[] { '#' } );
            SceneManager.LoadScene( "Receive" );
        }
        else {
            SceneManager.LoadScene( "Send" );
        }
    }
}
