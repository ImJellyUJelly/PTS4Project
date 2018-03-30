﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Sanford.Multimedia.Midi;
using System;
using Assets;
using System.Threading;

public class midiSequencer : MonoBehaviour
{

    Sequence song;
    public Sequencer sequencer;
    OutputDevice outDevice;

    public GameObject Slider;
    private Slider ProgressBar;

    public GameObject MidiNote;
    private List<List<GameObject>> MidiNotes;

    public GameObject ContentMidiSong;

    public GameObject MidiScrollBar;
    private Scrollbar TimeBar;

    public Scrollbar PianoScroll;

    public Scrollbar NoteViewScoll;

    public List<GameObject> Octave;

    public initializePiano piano;

    public Camera mainCam;

    public int channels = 16;
    private int currentTrack = -1;

    public bool playing;

    private NoteGrid noteGrid = new NoteGrid();

    // Use this for initialization
    void Start()
    {
        playing = false;
        MidiNotes = new List<List<GameObject>>();
        sequencer = new Sequencer();
        sequencer.Sequence = new Sequence();

        MidiNotes = new List<List<GameObject>>();

        ProgressBar = Slider.GetComponent<Slider>();
        TimeBar = MidiScrollBar.GetComponent<Scrollbar>();

        outDevice = new OutputDevice(0);

        Reset();

        sequencer.ChannelMessagePlayed += Sequencer_ChannelMessagePlayed;
    }

    // Update is called once per frame
    void Update()
    {
        ProgressBar.value = sequencer.Position;
        TimeBar.value = Normalize(sequencer.Position, song.GetLength(), 0);

        PianoScroll.value = NoteViewScoll.value;
    }

    public void LoadMidi(String path)
    {
        StopSequence();

        if (MidiNotes != null)
        {
            for (int track = 0; track < MidiNotes.Count; track++)
            {
                foreach (var note in MidiNotes[track])
                {
                    Destroy(note);
                }
                MidiNotes.Remove(MidiNotes[track]);
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
        GameObject previousNote = null;

        Transform contentMidiSong = GameObject.Find("ContentMidiSong").transform;
        foreach (var track in song)
        {
            MidiNotes.Add(new List<GameObject>());

            foreach (var midiEvent in track.Iterator())
            {
                if (midiEvent.MidiMessage.MessageType == MessageType.Channel)
                {
                    ChannelMessage cm = (ChannelMessage)midiEvent.MidiMessage;
                    if (cm.Command == ChannelCommand.NoteOn && cm.Data2 != 0)
                    {
                        GameObject midiNote = Instantiate(MidiNote, contentMidiSong);
                        GameObject button = (GameObject)piano.KeyMap[cm.Data1];
                        buttonClickTest midiNoteComponent = midiNote.GetComponent<buttonClickTest>();
                        Button pianoButton = button.GetComponent<Button>();

                        midiNoteComponent.button = pianoButton;
                        midiNoteComponent.position = midiEvent.AbsoluteTicks;
                        midiNoteComponent.sequencer = this;
                        midiNoteComponent.colors = pianoButton.colors;
                        MidiNotes[trackNo].Add(midiNote);

                        midiNote.transform.localPosition = new Vector3((midiEvent.AbsoluteTicks), (int)noteGrid.GridNote[cm.Data1]);

                        Color32 noteColor = new Color32(0, 0, 0, 0);
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
                            case 9:
                                noteColor = new Color32(0, 125, 125, 255);
                                break;
                            case 10:
                                noteColor = new Color32(0, 125, 125, 255);
                                break;
                            case 11:
                                noteColor = new Color32(0, 125, 125, 255);
                                break;
                            case 12:
                                noteColor = new Color32(0, 125, 125, 255);
                                break;
                            case 13:
                                noteColor = new Color32(0, 125, 125, 255);
                                break;
                            case 14:
                                noteColor = new Color32(0, 125, 125, 255);
                                break;
                            case 15:
                                noteColor = new Color32(0, 125, 125, 255);
                                break;
                            case 16:
                                noteColor = new Color32(0, 125, 125, 255);
                                break;
                            default:
                                break;
                        }
                        midiNote.GetComponent<Image>().color = noteColor;
                        previousNote = midiNote;

                    } else if (cm.Command == ChannelCommand.NoteOff || cm.Command == ChannelCommand.NoteOn && cm.Data2 == 0) // Check for the end of a note.
                    {
                        // TODO:
                        // Longer notes may disappear, maybe increase camera range
                        // Quickly repeated notes look like a single line, maybe change the graphic to show borders around the note
                        // Keys don't press for the full duration

                        int duration = (int)previousNote.transform.localPosition.x - midiEvent.AbsoluteTicks;
                        RectTransform rec = previousNote.GetComponent<RectTransform>();

                        rec.sizeDelta = new Vector2(rec.rect.width - duration, rec.rect.height);
                        rec.localPosition = new Vector3(rec.localPosition.x - (duration / 2), rec.localPosition.y);
                    }
                }
            }
            trackNo++;
        }
    }

    public void ChangeTrack(int trackNo)
    {
        //int trackIndex = 0;
        outDevice.Reset();
        currentTrack = trackNo;

        for (int tracks = 0; tracks < MidiNotes.Count; tracks++)
        {
            if (tracks == trackNo || currentTrack == -1)
            {
                foreach (var note in MidiNotes[tracks])
                {
                    note.GetComponent<Image>().enabled = true;
                    note.GetComponent<buttonClickTest>().enabled = true;
                }
            }
            else
            {
                foreach (var note in MidiNotes[tracks])
                {
                    note.GetComponent<Image>().enabled = false;
                    note.GetComponent<buttonClickTest>().enabled = false;
                }
            }
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

    private float Normalize(float value, float max, float min)
    {
        return (value - min) / (max - min);
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