using UnityEngine;
using UnityEngine.UI;
using Assets;
using SFB;

public class FileManager : MonoBehaviour
{

    private MidiProject mp;

    private string path;
    public MidiManager ms;
    public Dropdown dropdown;
    public ExtensionFilter[] filters;

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
                mp = new MidiProject("test", 16);
                mp.ms = this.ms;
                mp.AddNote(1, 100, 100, 60, 127);
                mp.AddNote(1, 200, 200, 61, 127);
                mp.AddNote(1, 300, 300, 62, 127);
                break;
            case 2:
                dropdown.value = 0;
                OpenExplorer();
                break;
            case 3:
                dropdown.value = 0;
                SaveFile();
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
            Debug.Log(fileContent);
        }
    }
}
