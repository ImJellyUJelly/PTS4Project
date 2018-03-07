using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sanford.Multimedia.Midi;
using System;

public class midiSequencer : MonoBehaviour {

    Sequence song;
    Sequencer sequencer = new Sequencer();
    OutputDevice outDevice = new OutputDevice(0);
    public int channels = 16;

	// Use this for initialization
	void Start () {
        Debug.Log("");
        sequencer.Sequence = new Sequence();

        Reset();

        sequencer.ChannelMessagePlayed += Sequencer_ChannelMessagePlayed;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void LoadMidi()
    {        
        try
        {
            song.Load("town.mid");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        sequencer.Sequence = song;

        Debug.Log("Loaded town.mid");
        
    }

    public void PlaySequence()
    {
        sequencer.Sequence = song;
        sequencer.Start();

        Debug.Log("Playing");
    }

    private void Reset()
    {
        song = new Sequence();
        for (int i = 0; i < channels; i++)
        {
            song.Add(new Track());
        }
    }

    private void Sequencer_ChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
    {
        outDevice.Send(e.Message);
        //do graphics here
    }
}
