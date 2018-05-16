using Assets;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Threading;

namespace Assets
{
    class UI : MonoBehaviour
    {
        public GameObject txtPos;
        public GameObject txtDur;
        public GameObject txtNoteNumber;
        public GameObject txtVelocity;
        public GameObject btnAddNote;
        public GameObject dropdownTrack;

        public void Start()
        {
            txtPos.GetComponent<Text>().text = Language.Lang.Get.Position;
            txtDur.GetComponent<Text>().text = Language.Lang.Get.Duration;
            txtNoteNumber.GetComponent<Text>().text = Language.Lang.Get.NoteNumber;
            txtVelocity.GetComponent<Text>().text = Language.Lang.Get.Velocity;
            btnAddNote.GetComponentInChildren<Text>().text = Language.Lang.Get.AddNote;

            dropdownTrack.GetComponent<Dropdown>().options[0].text = Language.Lang.Get.AllTracks;

        }

        void Update()
        {
        }
    }
}
