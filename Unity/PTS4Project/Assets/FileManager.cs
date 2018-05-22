using UnityEngine;
using UnityEngine.UI;
using Assets;
using SFB;
using Sanford.Multimedia.Midi;

public class FileManager : MonoBehaviour
{

    private string path;
    public MidiManager ms;
    public Dropdown dropdown;
    public ExtensionFilter[] filters;
    public Dropdown track;

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
