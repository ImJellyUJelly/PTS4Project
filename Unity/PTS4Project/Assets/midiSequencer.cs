﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Sanford.Multimedia.Midi;
using System;

public class midiSequencer : MonoBehaviour {

    Sequence song;
    Sequencer sequencer;
    OutputDevice outDevice;

    public GameObject Slider;
    private Slider ProgressBar;

    public GameObject MidiNote;

    public GameObject ContentMidiSong;

    public GameObject MidiScrollBar;
    private Scrollbar TimeBar;

    public int channels = 16;

	// Use this for initialization
	void Start () {
        Debug.Log("");
        sequencer = new Sequencer();
        sequencer.Sequence = new Sequence();

        ProgressBar = Slider.GetComponent<Slider>();
        TimeBar = MidiScrollBar.GetComponent<Scrollbar>();

        outDevice = new OutputDevice(0);

        Reset();

        sequencer.ChannelMessagePlayed += Sequencer_ChannelMessagePlayed;
    }

    // Update is called once per frame
    void Update () {
        ProgressBar.value = sequencer.Position;
        TimeBar.value = Normalize(sequencer.Position, song.GetLength(), 0);

        //Debug.Log(sequencer.Position);
    }

    public void LoadMidi()
    {        
        try
        {
            song.Load("newbark.mid");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        sequencer.Sequence = song;
        ProgressBar.maxValue = song.GetLength();
        ContentMidiSong.GetComponent<RectTransform>().offsetMax = new Vector2(song.GetLength(), ContentMidiSong.GetComponent<RectTransform>().offsetMax.y);
        

        Debug.Log(song.GetLength());
        

        ReadSong();

        Debug.Log("Loaded newbark.mid");
        
    }

    public void PlaySequence()
    {
        sequencer.Sequence = song;
        sequencer.Start();

        Debug.Log("Playing");
    }

    public void ReadSong()
    {
        int trackNo = 0;

        foreach (var track in song)
        {
            foreach (var midiEvent in track.Iterator())
            {
                if (midiEvent.MidiMessage.MessageType == MessageType.Channel)
                {
                    ChannelMessage cm = (ChannelMessage)midiEvent.MidiMessage;
                    if (cm.Command == ChannelCommand.NoteOn && cm.Data2 != 0)
                    {
                        GameObject midiNote = Instantiate(MidiNote, GameObject.Find("ContentMidiSong").transform);

                        midiNote.transform.localPosition = new Vector3((midiEvent.AbsoluteTicks), (cm.Data1 * 7) - 800);
                        midiNote.GetComponent<Image>().color = new Color32((byte)(trackNo * 100), (byte)(trackNo * 100), (byte)(trackNo * 100), 255);
                        Debug.Log(midiEvent.AbsoluteTicks);
                    }
                    //Console.WriteLine("Track:" + trackNo + " " + midiEvent.AbsoluteTicks + ": " + cm.Command + " :" + cm.Data1 + " :" + cm.Data2);                   
                }
            }
            trackNo++;
        }
    }

    private void Reset()
    {
        song = new Sequence();
        for (int i = 0; i < channels; i++)
        {
            song.Add(new Track());
        }
    }

    private float Normalize(float value, float max, float min) {
        return (value - min) / (max - min);
    }


    private void Sequencer_ChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
    {
        outDevice.Send(e.Message);
    }

    private void OnApplicationQuit()
    {
        outDevice.Dispose();
    }
}
