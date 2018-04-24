using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Sanford.Multimedia.Midi;

public class ControlMidiTest {

    private midiSequencer ms;
    private MidiManager mm;

    [UnityTest]
    public IEnumerator LoadMidiFileTest()
    {
        Sequence song = new Sequence();
        song.Load("smas11.mid");

        yield return null;

        var midiSequencerTest = GameObject.Find("MidiManager");
        mm = midiSequencerTest.GetComponent<MidiManager>();
        ms = mm.midiSequencer;
        ms.Start();
        mm.Start();

        mm.LoadMidi("../smas11.mid");

        Sequence midiSong = ms.sequencer.Sequence;

        Assert.AreEqual(midiSong.SequenceType, song.SequenceType);
    }

    [UnityTest]
    public IEnumerator ChangeTrackTest()
    {
        yield return null;

        var midiSequencerTest = GameObject.Find("MidiManager");
        mm = midiSequencerTest.GetComponent<MidiManager>();
        ms = mm.midiSequencer;
        ms.Start();
        mm.Start();

        mm.ChangeTrack(2);

        Assert.AreEqual(ms.currentTrack, 2);
    }

    [TearDown]
    public void TearDown()
    {
        if (!ms.outDevice.IsDisposed)
        {
            ms.outDevice.Dispose();
        }
    }
}
