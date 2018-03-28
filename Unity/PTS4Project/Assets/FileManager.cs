using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FileManager : MonoBehaviour {

    private MidiProject mp;

    private string path;
    public midiSequencer ms;
    public Dropdown dropdown;

    public void onChange()
    {
        switch (dropdown.value)
        {
            case 0:
                // make dialog
                mp = new MidiProject("test", 16);
                mp.ms = this.ms;
                mp.AddNote(1, 100, 100, 60, 127);
                break;
            case 1:
                dropdown.value = 0;
                OpenExplorer();
                break;
            case 2:
                safeFile();
                break;
        }
    }

    public void OpenExplorer()
    {
        path = EditorUtility.OpenFilePanel("", "../", "mid");
        if (path.Length != 0)
        {
            var fileContent = path;
            Debug.Log(fileContent);
            ms.LoadMidi(fileContent);
        }
    }

    public void safeFile() //save?
    {
        path = EditorUtility.SaveFilePanel("", "../", "", "mid");
        if (path.Length != 0)
        {
            var fileContent = path;
            Debug.Log(fileContent);
            //ms.SaveFile(fileContent);
        }
    }
}
