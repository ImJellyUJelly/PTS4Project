using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Sanford.Multimedia.Midi;
using System;
using Assets;
using System.Threading;

public class midiSequencer : MonoBehaviour {

    Sequence song;
    public Sequencer sequencer;
    OutputDevice outDevice;

    public GameObject Slider;
    private Slider ProgressBar;

    public GameObject MidiNote;
    private List<GameObject> MidiNotes;

    public GameObject ContentMidiSong;

    public GameObject MidiScrollBar;
    private Scrollbar TimeBar;

    public Scrollbar PianoScroll;

    public Scrollbar NoteViewScoll;

    public List<GameObject> Octave;

    public initializePiano piano;

    public int channels = 16;

    public bool playing;

    private NoteGrid noteGrid = new NoteGrid();

	// Use this for initialization
	void Start () {
        playing = false;
        MidiNotes = new List<GameObject>();
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

        PianoScroll.value = NoteViewScoll.value;
    }

    public void LoadMidi(String path)
    {        
        if (MidiNotes.Count > 1)
        {
            foreach (var note in MidiNotes)
            {
                Destroy(note);
            }
        }

        try
        {
            song.Load(path);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        sequencer.Sequence = song;
        ProgressBar.maxValue = song.GetLength();
        ContentMidiSong.GetComponent<RectTransform>().offsetMax = new Vector2(song.GetLength(), ContentMidiSong.GetComponent<RectTransform>().offsetMax.y);

        Thread.CurrentThread.IsBackground = true;
        ReadSong();

        Debug.Log("Loaded " + path);
        
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
                        Debug.Log(cm.Data1);
                        GameObject button = (GameObject)piano.KeyMap[cm.Data1];
                        midiNote.GetComponent<buttonClickTest>().button = button.GetComponent<Button>();
                        midiNote.GetComponent<buttonClickTest>().position = midiEvent.AbsoluteTicks;
                        midiNote.GetComponent<buttonClickTest>().sequencer = this;
                        midiNote.GetComponent<buttonClickTest>().colors = button.GetComponent<Button>().colors;
                        MidiNotes.Add(midiNote);

                        midiNote.transform.localPosition = new Vector3((midiEvent.AbsoluteTicks), (int)noteGrid.GridNote[cm.Data1]);

                        Color32 noteColor = new Color32(0, 0, 0,0);
                        switch (trackNo)
                        {
                            case 0:
                                noteColor = new Color32(255, 0, 0, 255);
                                break;
                            case 1:
                                noteColor = new Color32(0, 255, 0, 255);
                                break;
                            case 2:
                                noteColor = new Color32(0, 0, 255, 255);
                                break;
                            case 3:
                                noteColor = new Color32(255, 255, 0, 255);
                                break;
                            case 4:
                                noteColor = new Color32(255, 0, 255, 255);
                                break;
                            case 5:
                                noteColor = new Color32(0, 255, 255, 255);
                                break;
                            case 6:
                                noteColor = new Color32(125, 125, 0, 255);
                                break;
                            case 7:
                                noteColor = new Color32(125, 0, 125, 255);
                                break;
                            case 8:
                                noteColor = new Color32(0, 125, 125, 255);
                                break;
                            default:
                                break;
                        }
                        midiNote.GetComponent<Image>().color = noteColor;
                    }
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
