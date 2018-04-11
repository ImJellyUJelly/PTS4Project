
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
