using Assets;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MidiManager : MonoBehaviour
{
    public MidiProject midiProject;
    public GameObject Slider = null;
    private Slider ProgressBar = null;

    public GameObject MidiNote = null;
    private List<List<GameObject>> MidiNotes;

    public GameObject ContentMidiSong = null;

    public GameObject MidiScrollBar = null;
    private Scrollbar TimeBar = null;

    public Scrollbar PianoScroll = null;

    public Scrollbar NoteViewScoll = null;

    public List<GameObject> Octave = null;

    public initializePiano piano = null;

    public Camera mainCam = null;

    private NoteGrid noteGrid = new NoteGrid();
    private float lengthMultiplier = 1;
    public Slider sliderScale = null;

    private GameObject selectedNote;

    public InputField ifPosition = null;
    public InputField ifDuration = null;
    public InputField ifNoteNumber = null;
    public InputField ifVelocity = null;

    public midiSequencer midiSequencer = null;
    private System.Diagnostics.Stopwatch stopwatch = null;
    public string path;

    public void Start()
    {
        path = null;
        MidiNotes = new List<List<GameObject>>();
        ProgressBar = Slider.GetComponent<Slider>();
        TimeBar = MidiScrollBar.GetComponent<Scrollbar>();
        sliderScale.onValueChanged.AddListener(Slider_OnValueChanged);
        ifPosition.onValueChanged.AddListener(InputFieldPosition_OnValueChanged);
    }

    void Update()
    {
        if (midiSequencer != null)
        {
            TimeBar.value = Normalize((float)midiSequencer.sequencer.Position, ((float)midiSequencer.song.GetLength() * lengthMultiplier), 0);
        }

        PianoScroll.value = NoteViewScoll.value;

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


                        Debug.Log("Selecting note: " + results[i].gameObject.transform.position + " duration: " + results[i].gameObject.GetComponent<RectTransform>().rect.width + " NOTE: " + results[i].gameObject.GetComponent<MidiNote>().NoteIndex);
                        if (selectedNote != null)
                        {
                            selectedNote.GetComponent<Outline>().effectDistance = new Vector2(1, -1);
                        }

                        selectedNote = results[i].gameObject;
                        selectedNote.GetComponent<Outline>().effectDistance = new Vector2(6, -6);



                        ifPosition.text = selectedNote.transform.localPosition.x + "";
                        ifDuration.text = selectedNote.GetComponent<RectTransform>().rect.width + "";
                        ifNoteNumber.text = selectedNote.GetComponent<MidiNote>().NoteNumber.ToString();
                        ifVelocity.text = selectedNote.GetComponent<MidiNote>().NoteVelocity.ToString();
                    }

                }

            }
            else
            {
                Debug.Log("no hit");
            }

            if (midiSequencer.sequencer.Position > (int)ProgressBar.value + 50 ||
                midiSequencer.sequencer.Position < (int)ProgressBar.value - 50)
            {
                Debug.Log(midiSequencer.sequencer.Position + " " + ProgressBar.value);
                midiSequencer.sequencer.Position = (int)ProgressBar.value;

                midiSequencer.outDevice.Reset();
            }

        }
        else
        {
            if (midiSequencer != null)
            {
                ProgressBar.value = midiSequencer.sequencer.Position;
            }
        }

        if (selectedNote != null && Input.GetKeyDown(KeyCode.Delete))
        {
            DeleteNote();

        }

    }

    public void DeleteNote()
    {
        MidiNote note = selectedNote.GetComponent<MidiNote>();
        int index = note.NoteIndex;
        midiSequencer.song[note.NoteTrack].RemoveAt(note.NoteIndex - 1);
        midiProject.sequence[note.NoteTrack].RemoveAt(note.NoteIndex - 1);

        List<GameObject> notes = MidiNotes[note.NoteTrack];

        for (int i = 0; i < notes.Count; i++)
        {
            MidiNote n = notes[i].GetComponent<MidiNote>();
            if (n.NoteIndex > index)
            {
                n.NoteIndex--;
            }
        }

        MidiNotes[note.NoteTrack].Remove(selectedNote);

        Destroy(selectedNote);
        selectedNote = null;
    }


    private float Normalize(float value, float max, float min)
    {
        return (value - min) / (max - min);
    }

    private void InputFieldPosition_OnValueChanged(string arg0)
    {
        float position;

        if (arg0 == null ||
            float.TryParse(arg0, out position) ||
            selectedNote == null ||
            position == selectedNote.transform.localPosition.x
            )
        {
            return;
        }

        MidiNote midiNoteComponent = selectedNote.GetComponent<MidiNote>();
        selectedNote.transform.localPosition = new Vector3(float.Parse(arg0), selectedNote.transform.localPosition.y);
        Debug.Log("Setting position of: " + selectedNote.name + " to: " + arg0);
        //IEnumerator<Track> enumerator = midiSequencer.sequencer.Sequence.GetEnumerator();

        midiSequencer.sequencer.Sequence[midiNoteComponent.NoteTrack].Move(midiSequencer.sequencer.Sequence[midiNoteComponent.NoteTrack].GetMidiEvent(midiNoteComponent.NoteIndex), int.Parse(arg0));
    }

    public void CheckNoteSelected()
    {
        if (selectedNote == null)
        {
            AddNote();
        }
        else
        {
            EditNote();
        }
    }

    public void EditNote()
    {

        int position;
        int duration;
        int notenumber;
        int velocity;

        if (int.TryParse(ifPosition.text, out position) &&
            int.TryParse(ifDuration.text, out duration) &&
            int.TryParse(ifNoteNumber.text, out notenumber) &&
            int.TryParse(ifVelocity.text, out velocity))
        {
            DeleteNote();
            AddNote();
        }
        else
        {
            Debug.Log("Could not parse all values.");
        }

    }

    public void AddNote()
    {
        if (midiProject != null)
        {
            int position;
            int duration;
            int notenumber;
            int velocity;

            if (int.TryParse(ifPosition.text, out position) &&
                int.TryParse(ifDuration.text, out duration) &&
                int.TryParse(ifNoteNumber.text, out notenumber) &&
                int.TryParse(ifVelocity.text, out velocity))
            {
                midiProject.AddNote(1, position, duration, notenumber, velocity);
                ResetMidiVisualization();
                ReadSong();
            }
            else
            {
                Debug.Log("Could not parse all values.");
            }
        }
        else
        {
            Debug.Log("Midi project is null");
        }
    }

    public void LoadMidi(String path)
    {
        stopwatch = System.Diagnostics.Stopwatch.StartNew();
        midiSequencer.StopSequence();
        midiSequencer.Reset();

        try
        {
            midiSequencer.song.Load(path);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        ProgressBar.maxValue = midiSequencer.song.GetLength();
        ContentMidiSong.GetComponent<RectTransform>().offsetMax = new Vector2(midiSequencer.song.GetLength(), ContentMidiSong.GetComponent<RectTransform>().offsetMax.y);

        ReadSong();
        stopwatch.Stop();

        Debug.Log("Loaded " + path + " in " + stopwatch.ElapsedMilliseconds / 1000 + " seconds.");
        this.path = path;
    }

    public void ReadSong()
    {
        int trackNo = 0;
        int midiEventIndex = 0;
        GameObject[] previousNote;
        int bufferSize = 7;
        int previousNoteIndex = 0;

        Transform contentMidiSong = GameObject.Find("ContentMidiSong").transform;
        foreach (var track in midiSequencer.song)
        {
            MidiNotes.Add(new List<GameObject>());
            previousNote = new GameObject[bufferSize];

            foreach (var midiEvent in track.Iterator())
            {
                midiEventIndex++;

                if (midiEvent.MidiMessage.MessageType == MessageType.Channel)
                {
                    ChannelMessage cm = (ChannelMessage)midiEvent.MidiMessage;
                    if (cm.Command == ChannelCommand.NoteOn && cm.Data2 != 0)
                    {
                        GameObject midiNote = Instantiate(MidiNote, contentMidiSong);
                        GameObject button = (GameObject)piano.KeyMap[cm.Data1];
                        MidiNote midiNoteComponent = midiNote.GetComponent<MidiNote>();
                        Button pianoButton = button.GetComponent<Button>();

                        midiNoteComponent.NoteNumber = cm.Data1;
                        midiNoteComponent.NoteVelocity = cm.Data2;
                        midiNoteComponent.button = pianoButton;
                        midiNoteComponent.position = midiEvent.AbsoluteTicks;
                        midiNoteComponent.sequencer = midiSequencer;
                        midiNoteComponent.colors = pianoButton.colors;
                        midiNoteComponent.NoteTrack = trackNo;
                        MidiNotes[trackNo].Add(midiNote);
                        midiNoteComponent.NoteIndex = midiEventIndex;

                        midiNote.transform.localPosition = new Vector3((midiEvent.AbsoluteTicks), (int)noteGrid.GridNote[cm.Data1]);

                        Color32 noteColor = new Color32(0, 0, 0, 255);
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
                        midiNoteComponent.noteColor = noteColor;
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

                    }
                    else if (cm.Command == ChannelCommand.NoteOff || cm.Command == ChannelCommand.NoteOn && cm.Data2 == 0)
                    {

                        foreach (var note in previousNote)
                        {
                            if (note != null && (int)noteGrid.GridNote[cm.Data1] == (int)note.transform.localPosition.y)
                            {

                                float duration = (Math.Abs((int)note.transform.localPosition.x - midiEvent.AbsoluteTicks)) / lengthMultiplier * -1;
                                RectTransform rec = note.GetComponent<RectTransform>();
                                note.GetComponent<MidiNote>().NoteOffIndex = midiEventIndex;
                                note.GetComponent<MidiNote>().duration = duration;

                                if (duration < -5000)
                                {
                                    Debug.Log("duration: " + duration + " lpX: " + note.transform.localPosition.x + " at: " + midiEvent.AbsoluteTicks + " actual size: " + (rec.rect.width - duration));
                                    continue;
                                }
                                rec.sizeDelta = new Vector2(rec.rect.width - duration, rec.rect.height);


                                rec.localPosition = new Vector3((rec.localPosition.x / lengthMultiplier) - (duration / 2), rec.localPosition.y);

                            }
                            else
                            {
                                //Debug.Log("Error! Previous note on was not valid.");
                            }
                        }

                    }
                }
            }
            trackNo++;
            midiEventIndex = 0;
        }

        midiProject.sequence = midiSequencer.song;
    }

    public void ChangeTrack(int trackNo)
    {
        //int trackIndex = 0;
        midiSequencer.outDevice.Reset();
        midiSequencer.currentTrack = trackNo;

        for (int tracks = 0; tracks < MidiNotes.Count; tracks++)
        {
            if (tracks == trackNo || midiSequencer.currentTrack == -1)
            {
                foreach (var note in MidiNotes[tracks])
                {
                    note.GetComponent<Image>().enabled = true;
                    note.GetComponent<MidiNote>().enabled = true;
                }
            }
            else
            {
                foreach (var note in MidiNotes[tracks])
                {
                    note.GetComponent<Image>().enabled = false;
                    note.GetComponent<MidiNote>().enabled = false;
                }
            }
        }
    }

    private void Slider_OnValueChanged(float value)
    {
        lengthMultiplier = value;
        ResetMidiVisualization();
        ReadSong();
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
}
