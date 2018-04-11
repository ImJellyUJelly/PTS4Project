using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class MidiProject
    {
        private String name;
        private int channels;
        private Sequence sequence;

        public midiSequencer ms;

        public MidiProject(String name, int channels)
        {
            this.name = name;
            this.channels = channels;

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

            sequence[track].Insert(position, cmOff);
            sequence[track].Insert(position + duration, cmOn);

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
