using Assets;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class midiSequencer : MonoBehaviour
{
    public OutputDevice outDevice;
    public Sequence song;
    public Sequencer sequencer;

    public int channels = 16;
    public int currentTrack = -1;

    public bool playing;

    
    private System.Diagnostics.Stopwatch stopwatch;

    public void Start()
    {
        playing = false;
        
        sequencer = new Sequencer();
        sequencer.Sequence = new Sequence();

        Reset();
        sequencer.ChannelMessagePlayed += Sequencer_ChannelMessagePlayed;
        outDevice = new OutputDevice(0);
    }

    void Update()
    {
    }

    public void PlaySequence()
    {
        sequencer.Sequence = song;
        sequencer.Continue();
        playing = true;

        Debug.Log("Playing");
    }

    public void PauseSequence()
    {
        sequencer.Stop();
        playing = false;

        outDevice.Reset();

        Debug.Log("Pausing");
    }

    public void StopSequence()
    {
        sequencer.Stop();
        sequencer.Position = 0;
        playing = false;

        outDevice.Reset();

        Debug.Log("Stopping.");
    }

    public void Reset()
    {
        song = new Sequence();
        for (int i = 0; i < channels; i++)
        {
            song.Add(new Track());
        }
    }


    private void Sequencer_ChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
    {
        if (currentTrack == -1)
        {
            outDevice.Send(e.Message);
        } else if ((e.Message.MidiChannel) == currentTrack - 1)
        {
            outDevice.Send(e.Message);
        }
    }

    private void OnApplicationQuit()
    {
        outDevice.Dispose();
    }
}