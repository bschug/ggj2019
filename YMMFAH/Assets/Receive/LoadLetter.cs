using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LoadLetter : MonoBehaviour
{
    public StringVariable LetterId;
    public Downloader Downloader;
    public GameObject LoadingScreen;
    public GameObject[] EnableWhenLoaded;

    TextFromLetterPage[] Pages;

    private void Start ()
    {
        LoadLetterNow();
    }

    private async Task LoadLetterNow () { 
        LoadingScreen.SetActive( true );
        await Downloader.Download( LetterId.Value );
        LoadingScreen.SetActive( false );
        foreach (var obj in EnableWhenLoaded) {
            obj.SetActive( true );
        }
    }
}
