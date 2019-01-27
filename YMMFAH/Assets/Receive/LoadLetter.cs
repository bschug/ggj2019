using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LoadLetter : MonoBehaviour
{
    public StringVariable LetterId;
    public Downloader Downloader;
    public GameObject LoadingScreen;
    public Image LoadingBar;
    public GameObject[] EnableWhenLoaded;

    public bool Download = true;

    TextFromLetterPage[] Pages;

    private void Start ()
    {
        LoadLetterNow();
    }

    private async Task LoadLetterNow () {
        if (Download) {
            LoadingScreen.SetActive( true );
            await Downloader.Download( LetterId.Value, LoadingBar );
        }
        LoadingScreen.SetActive( false );
        foreach (var obj in EnableWhenLoaded) {
            obj.SetActive( true );
        }
    }
}
