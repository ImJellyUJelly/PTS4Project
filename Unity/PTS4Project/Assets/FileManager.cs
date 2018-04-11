<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sanford.Multimedia.Midi;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
=======
﻿using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FileManager : MonoBehaviour {

    private MidiProject mp;

>>>>>>> a98661afe4944d6de79ba1209fb4e712e2d9a258
    private string path;
    public midiSequencer ms;
    public Dropdown dropdown;

    public void onChange()
    {
        switch (dropdown.value)
        {
            case 0:
<<<<<<< HEAD
                break;
            case 1:
                OpenExplorer();
                break;
            case 2:
=======
                // make dialog
                dropdown.value = 0; // dont set stuff to 0 plz
                //mp = new MidiProject("test", 16);
                //mp.ms = this.ms;
                //mp.AddNote(1, 100, 100, 60, 127);
                break;
            case 1:
                dropdown.value = 0;
                OpenExplorer();
                break;
            case 2:
                dropdown.value = 0;
>>>>>>> a98661afe4944d6de79ba1209fb4e712e2d9a258
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

<<<<<<< HEAD
    public void safeFile()
=======
    public void safeFile() //save?
>>>>>>> a98661afe4944d6de79ba1209fb4e712e2d9a258
    {
        path = EditorUtility.SaveFilePanel("", "../", "", "mid");
        if (path.Length != 0)
        {
<<<<<<< HEAD
        }
    }

=======
            var fileContent = path;
            Debug.Log(fileContent);
            //ms.SaveFile(fileContent);
        }
    }
>>>>>>> a98661afe4944d6de79ba1209fb4e712e2d9a258
}
