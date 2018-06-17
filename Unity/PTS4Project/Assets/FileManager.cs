using UnityEngine;
using UnityEngine.UI;
using Assets;
using SFB;
using Sanford.Multimedia.Midi;
using System.IO;
using System.Collections;
using UnityEngine.Networking;

public class FileManager : MonoBehaviour
{

    private string path;
    public MidiManager ms;
    public Dropdown dropdown;
    public ExtensionFilter[] filters;
    public Dropdown track;
    string returnMessage = "";
    byte[] b;

    private void Awake()
    {
        filters = new ExtensionFilter[]
        {
            new ExtensionFilter("Midi Files", "mid")
        };
    }

    public void onChange()
    {
        switch (dropdown.value)
        {
            case 1:
                Debug.Log("Making new project");
                break;
            case 2:
                dropdown.value = 0;
                OpenExplorer();
                break;
            case 3:
                dropdown.value = 0;
                SaveFile();
                break;
            case 4:
                dropdown.value = 0;
                DeleteFile();
                break;
        }
    }

    public void OpenExplorer()
    {
        path = StandaloneFileBrowser.OpenFilePanel("Open MIDI", "", filters, false)[0];
        if (path.Length != 0)
        {
            var fileContent = path;
            Debug.Log(fileContent);
            ms.LoadMidi(fileContent);
            
        }
    }

    public void SaveFile()
    {
        path = StandaloneFileBrowser.SaveFilePanel("Save MIDI", "", "New Midi", filters);
        if (path.Length != 0)
        {
            var fileContent = path;
            ms.midiSequencer.song.Save(fileContent);
            Debug.Log(fileContent);
            b = File.ReadAllBytes(path);
            //int i = 0;
            //foreach (byte by in b)
            //{
            //    Debug.Log(by.ToString()+ " " + i);
            //    i++;
            //}
            //string convert = "This is the string to be converted";

            // From byte array to string
            string s = System.Text.Encoding.UTF8.GetString(b, 0, b.Length);
            StartCoroutine(Authenticate(s));
            //// From string to byte array
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(returnMessage);
            path = StandaloneFileBrowser.SaveFilePanel("Save MIDI", "", "New Midi", filters);
            if (path.Length != 0)
            {
                var fileContent2 = path;
                File.WriteAllBytes(fileContent2, buffer);
            }
        }
    }
    public IEnumerator Authenticate(string s)
    {
        string method = "testSongSave";
        StartCoroutine(getRequest("http://localhost:8080/SimpleMavenWebApp/HelloWorld?method=" + method + "&payload=" + s));
        Debug.Log("Authenticate: " + returnMessage);
        yield return new WaitForSeconds(10);
    }
    public IEnumerator getRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();
        Debug.Log(returnMessage);
        returnMessage = uwr.downloadHandler.text;
        Debug.Log(returnMessage);
        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Message: " + returnMessage);
        }
        yield return new WaitForSeconds(10);
    }

    public void DeleteFile()
    {
        int trackNumber = track.value;
        Sequence s = ms.midiSequencer.sequencer.Sequence;

        Track t = s[trackNumber - 1];
        
        Debug.Log(trackNumber);

        if (trackNumber > 0)
        {
            ms.midiSequencer.song.Remove(t);
        }

    }
}
