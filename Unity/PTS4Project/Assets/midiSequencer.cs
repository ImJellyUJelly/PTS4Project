using Assets;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    private float lengthMultiplier = 2;
    public Slider sliderScale;

    private GameObject selectedNote;

    public InputField ifPosition;
    public InputField ifDuration;

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
        sliderScale.onValueChanged.AddListener(Slider_OnValueChanged);

        outDevice = new OutputDevice(0);

        Reset();

        sequencer.ChannelMessagePlayed += Sequencer_ChannelMessagePlayed;
    }

    

    // Update is called once per frame
    void Update()
    {
        ProgressBar.value = ((float)sequencer.Position / lengthMultiplier); //* lengthMultiplier;
        TimeBar.value = Normalize((float)sequencer.Position, ((float)song.GetLength()*lengthMultiplier), 0);

        PianoScroll.value = NoteViewScoll.value;// * lengthMultiplier;

        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].gameObject.layer == LayerMask.NameToLayer("Note"))
                    {
                        Debug.Log("Selecting note: " + results[i].gameObject.transform.localPosition + " duration: " + results[i].gameObject.GetComponent<RectTransform>().rect.width);
                        selectedNote = results[i].gameObject;

                        ifPosition.text = selectedNote.transform.localPosition.x + "";
                        ifDuration.text = selectedNote.GetComponent<RectTransform>().rect.width + "";
                    }

                }

            }
            else
            {
                Debug.Log("no hit");
            }

        }

    }

    private void ResetMidiVisualization()
    {
        foreach (var track in MidiNotes)
        {
            foreach (var note in track)
            {
                Destroy(note);
            }
        }

        MidiNotes = new List<List<GameObject>>();
    }

    public void LoadMidi(String path)
    {
        StopSequence();

        if (MidiNotes != null)
        {
            ResetMidiVisualization();
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
        GameObject[] previousNote; // Circular note buffer
        int bufferSize = 8;
        int previousNoteIndex = 0;

        Transform contentMidiSong = GameObject.Find("ContentMidiSong").transform;
        foreach (var track in song)
        {
            MidiNotes.Add(new List<GameObject>());
            previousNote = new GameObject[bufferSize];

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
                                noteColor = new Color32(0, 62, 125, 255);
                                break;
                            case 9:
                                noteColor = new Color32(62, 125, 125, 255);
                                break;
                            case 10:
                                noteColor = new Color32(0, 125, 62, 255);
                                break;
                            case 11:
                                noteColor = new Color32(62, 62, 62, 255);
                                break;
                            case 12:
                                noteColor = new Color32(0, 125, 200, 255);
                                break;
                            case 13:
                                noteColor = new Color32(200, 200, 125, 255);
                                break;
                            case 14:
                                noteColor = new Color32(200, 200, 200, 255);
                                break;
                            case 15:
                                noteColor = new Color32(0, 0, 200, 255);
                                break;
                            case 16:
                                noteColor = new Color32(0, 125, 125, 255);
                                break;
                            default:
                                break;
                        }
                        midiNote.GetComponent<Image>().color = noteColor;

                        bool isSameNotePresent = false;

                        for (int noteIndex = 0; noteIndex < previousNote.Length; ++noteIndex)
                        {
                            if (previousNote[noteIndex] != null && (int)noteGrid.GridNote[cm.Data1] == previousNote[noteIndex].transform.localPosition.y)
                            {
                                previousNote[noteIndex] = midiNote;
                                isSameNotePresent = true;
                                break;
                            }
                        }

                        if (!isSameNotePresent)
                        {
                            previousNote[previousNoteIndex] = midiNote;
                        }
                        
                        previousNoteIndex++;
                        if (previousNoteIndex > bufferSize - 1)
                        {
                            previousNoteIndex = 0;
                        }

                    } else if (cm.Command == ChannelCommand.NoteOff || cm.Command == ChannelCommand.NoteOn && cm.Data2 == 0) // Check for the end of a note.
                    {
                        // TODO:
                        // Longer notes may disappear, maybe increase camera range
                        // Quickly repeated notes look like a single line, maybe change the graphic to show borders around the note
                        // Keys don't press for the full duration

                        foreach (var note in previousNote)
                        {
                            if (note != null && (int)noteGrid.GridNote[cm.Data1] == (int)note.transform.localPosition.y)
                            {

                                float duration = (Math.Abs((int)note.transform.localPosition.x - midiEvent.AbsoluteTicks)) / lengthMultiplier * -1;
                                RectTransform rec = note.GetComponent<RectTransform>();

                                if (duration < -5000)
                                {
                                    Debug.Log("duration: " + duration + " lpX: " + note.transform.localPosition.x + " at: " + midiEvent.AbsoluteTicks + " actual size: " + (rec.rect.width - duration));
                                    continue;
                                }
                                rec.sizeDelta = new Vector2(rec.rect.width - duration, rec.rect.height);


                                rec.localPosition = new Vector3((rec.localPosition.x/ lengthMultiplier) - (duration / 2), rec.localPosition.y);
                                //rec.anchoredPosition = new Vector2((rec.localPosition.x / lengthMultiplier) - (duration / 2), rec.localPosition.y);
                                //rec.localPosition.Set(((rec.localPosition.x / lengthMultiplier) - (duration / 2)), rec.localPosition.y, rec.localPosition.z);

                            } else
                            {
                                //Debug.Log("Error! Previous note on was not valid.");
                            }
                        }
                        
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

    private void Slider_OnValueChanged(float value)
    {
        lengthMultiplier = value;
        ResetMidiVisualization();

        //ProgressBar.maxValue = song.GetLength();
        //ContentMidiSong.GetComponent<RectTransform>().offsetMax = new Vector2(song.GetLength() / lengthMultiplier, ContentMidiSong.GetComponent<RectTransform>().offsetMax.y);


        //sequencer.Sequence = song;
        //ProgressBar.maxValue = song.GetLength();
        //ContentMidiSong.GetComponent<RectTransform>().offsetMax = new Vector2(song.GetLength(), ContentMidiSong.GetComponent<RectTransform>().offsetMax.y);
        ReadSong();
        /*
        for (int tracks = 0; tracks < MidiNotes.Count; tracks++)
        {
            foreach (var note in MidiNotes[tracks])
            {
                RectTransform rec = note.GetComponent<RectTransform>();
                //rec.sizeDelta = new Vector2((rec.rect.width * lengthMultiplier), rec.rect.height);


                //rec.localPosition = new Vector3((rec.localPosition.x / lengthMultiplier) - (duration / 2), rec.localPosition.y);
            }
        }
        */
    }

    private void OnApplicationQuit()
    {
        outDevice.Dispose();
    }
}