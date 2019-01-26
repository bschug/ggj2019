using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text;
using System.Threading.Tasks;

public class UploadError : System.Exception
{
    public string error;
}


[CreateAssetMenu(menuName = "API/Uploader")]
public class Uploader : ScriptableObject
{
    public LetterDefinition Letter;
    public string Url;

    public string UploadedLetterId;

    [ContextMenu("Upload Now")]
    public async void DebugUpload ()
    {
        try {
            string uploadedLetterId = await Upload();
            Debug.Log( "Uploaded letter " + uploadedLetterId );
        }
        catch {
            Debug.LogError( "Upload failed!" );
        }
    }

    public async Task<string> Upload ()
    {
        var definitionJson = JsonUtility.ToJson( Letter );
        var formData = new WWWForm();
        formData.AddBinaryData( "letter.json", Encoding.UTF8.GetBytes( definitionJson ) );
        var request = UnityWebRequest.Post( Url, formData );
        Debug.Log( "Uploading..." );
        await request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError) {
            Debug.LogError( "Upload failed: " + request.error );
            UploadedLetterId = null;
            throw new UploadError() { error = request.error };
        }
        Debug.Log( "Upload complete: " + request.downloadHandler.text );
        UploadedLetterId = request.downloadHandler.text;
        return UploadedLetterId;
    }
}
