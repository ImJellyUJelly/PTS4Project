using System.Collections;
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

    public int channels = 16;

	// Use this for initialization
	void Start () {
        Debug.Log("");
        sequencer = new Sequencer();
        sequencer.Sequence = new Sequence();

        ProgressBar = Slider.GetComponent<Slider>();

        outDevice = new OutputDevice(0);

        Reset();

        sequencer.ChannelMessagePlayed += Sequencer_ChannelMessagePlayed;
    }

    // Update is called once per frame
    void Update () {
        ProgressBar.value = sequencer.Position;
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
        ProgressBar.maxValue = song.GetLength();

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
    }

    private void OnApplicationQuit()
    {
        outDevice.Dispose();
    }
}
