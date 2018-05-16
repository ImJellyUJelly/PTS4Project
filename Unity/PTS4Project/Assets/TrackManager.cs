﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    class TrackManager : MonoBehaviour
    {
        private string path;
        public MidiManager ms;
        public Dropdown dropdown = null;

        public void onChange()
        {
            switch (dropdown.value)
            {
                case 0:
                    ms.ChangeTrack(-1);
                    break;
                case 1:
                    ms.ChangeTrack(0);
                    break;
                case 2:
                    ms.ChangeTrack(1);
                    break;
                case 3:
                    ms.ChangeTrack(2);
                    break;
                case 4:
                    ms.ChangeTrack(3);
                    break;
                case 5:
                    ms.ChangeTrack(4);
                    break;
                case 6:
                    ms.ChangeTrack(5);
                    break;
                case 7:
                    ms.ChangeTrack(6);
                    break;
                case 8:
                    ms.ChangeTrack(7);
                    break;
                case 9:
                    ms.ChangeTrack(8);
                    break;
                case 10:
                    ms.ChangeTrack(9);
                    break;
            }
        }
    }
}