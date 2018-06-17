using UnityEngine;
using UnityEngine.UI;
using Assets;
using SFB;
using Sanford.Multimedia.Midi;
using System.IO;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Globalization;
using System.Text;

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
                SaveFileOnline();
                break;
            case 5:
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
        }
    }

    public void SaveFileOnline()
    {
        string tempPath = Directory.GetCurrentDirectory() + "\\temp.mid";
        ms.midiSequencer.song.Save(tempPath);
        b = File.ReadAllBytes(tempPath);
        string s = ByteArrayToString(b);
        Authenticate(s);
    }

    public void Authenticate(string s)
    {
        string method = "testSongSave";
        StartCoroutine(getRequest("http://localhost:8080/SimpleMavenWebApp/HelloWorld?method=" + method + "&payload=" + s, saveMidiByteArray));
        Debug.Log("Authenticate: " + returnMessage);
    }

    public void saveMidiByteArray()
    {
        Debug.Log("do after " + returnMessage);
        byte[] buffer = ConvertHexStringToByteArray(returnMessage);
        Debug.Log(buffer.ToString());
        path = Directory.GetCurrentDirectory();
        path += "/tempMidi.mid";
        File.WriteAllBytes(path, buffer);
        Debug.Log("Size after database: " + buffer.Length);
        
    }

    public static byte[] ConvertHexStringToByteArray(string hexString)
    {
        if (hexString.Length % 2 != 0)
        {
            throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
        }

        byte[] HexAsBytes = new byte[hexString.Length / 2];
        for (int index = 0; index < HexAsBytes.Length; index++)
        {
            string byteValue = hexString.Substring(index * 2, 2);
            HexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        }

        return HexAsBytes;
    }

    public static string ByteArrayToString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }

    public IEnumerator getRequest(string uri, Action doAfter)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();
        returnMessage = uwr.downloadHandler.text;
        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Message: " + returnMessage);
            doAfter();
        }
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
