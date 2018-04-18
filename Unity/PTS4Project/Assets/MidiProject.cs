using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class MidiProject : MonoBehaviour
    {
        public  int channels;
        private Sequence sequence;

        public MidiManager ms;

        private void Start()
        {

            sequence = new Sequence();

            for (int i = 0; i < channels; i++)
            {
                sequence.Add(new Track());
            }

            Debug.Log("Created new project with Name: " + name + " channels: " + channels);
        }

        public void AddNote(int track, int position, int duration, int note, int velocity)
        {
            ChannelMessage cmOn = new ChannelMessage(ChannelCommand.NoteOn, track, note, velocity);
            ChannelMessage cmOff = new ChannelMessage(ChannelCommand.NoteOff, track, note, velocity);

            sequence[track].Insert(position, cmOn);
            sequence[track].Insert(position + duration, cmOff);

            Debug.Log("Inserted note at: track: " + track + " position: " + position + " duration: " + duration + " note: " + note + " velocity: " + velocity);

            Refresh();
        }

        public void Refresh()
        { 
            sequence.Save(name + ".midi"); // hacci?
            ms.LoadMidi(name + ".midi"); // make load accept raw sequence ? and not load the entire file each time

            Debug.Log("Refreshed note GameObject edits.");
        }
    }
}
