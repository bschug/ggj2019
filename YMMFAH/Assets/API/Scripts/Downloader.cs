using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class DownloadError : System.Exception
{
    public string error;
}

[CreateAssetMenu(menuName = "API/Downloader")]
public class Downloader : ScriptableObject
{
    public LetterDefinition Target;
    public string BaseUrl = "https://s3.amazonaws.com/ymmfah/";

    public string DebugLetterId = "0a6fa2e5207c4a3eb3216edd5c8fec82";

    [ContextMenu("Download with Debug Id")]
    public void DownloadDebug ()
    {
        Download( DebugLetterId );
    }

    public async Task Download (string letterId)
    {
        var request = UnityWebRequest.Get( BaseUrl + letterId + "/letter.json" );
        Debug.Log( "Downloading " + BaseUrl + letterId + "/letter.json" );
        await request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError) {
            Debug.LogError( "Download failed: " + request.error );
            throw new DownloadError() { error = request.error };
        }

        Debug.Log( "Download complete" );
        var json = request.downloadHandler.text;
        JsonUtility.FromJsonOverwrite( json, Target );
    }
}
