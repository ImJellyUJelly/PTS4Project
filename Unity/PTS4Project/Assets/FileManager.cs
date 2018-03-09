using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FileManager : MonoBehaviour {

    private string path;
    public midiSequencer ms;
    public Dropdown dropdown;

    public void onChange()
    {
        switch (dropdown.value)
        {
            case 0:
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
            ms.SaveFile(fileContent);
        }
    }
}
