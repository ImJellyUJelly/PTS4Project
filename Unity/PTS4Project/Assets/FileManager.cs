
﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sanford.Multimedia.Midi;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Assets;

public class FileManager : MonoBehaviour
{

    private MidiProject mp;

    private string path;
    public MidiManager ms;
    public Dropdown dropdown;

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
        path = EditorUtility.OpenFilePanel("", "../", "mid");
        if (path.Length != 0)
        {
            var fileContent = path;
            Debug.Log(fileContent);
            ms.LoadMidi(fileContent);
        }
    }

    public void SaveFile()
    {
        path = EditorUtility.SaveFilePanel("", "../", "", "mid");
        if (path.Length != 0)
        {
            var fileContent = path;
            Debug.Log(fileContent);
        }
    }
}
