using System;
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
        public midiSequencer ms;
        public Dropdown dropdown;

        public void onChange()
        {
            switch (dropdown.value)
            {
                case 0:
                    ms.ChangeTrack(0);
                    break;
                case 1:
                    ms.ChangeTrack(1);
                    break;
                case 2:
                    ms.ChangeTrack(2);
                    break;
                case 3:
                    ms.ChangeTrack(3);
                    break;
                case 4:
                    ms.ChangeTrack(4);
                    break;
            }
        }
    }
}
