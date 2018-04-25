using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MidiNoteTest {

    [Test]
    public void MidiNoteTestSimplePasses()
    {

    }

    [UnityTest]
    public IEnumerator InstantiateMidiNote()
    {
        var midiNotePrefab = Resources.Load("../midiNote");

        yield return null;

        var spawnedMidiNote = GameObject.Find("MidiNote");
        var prefabSpawned = PrefabUtility.GetPrefabParent(spawnedMidiNote);

        Assert.AreEqual(midiNotePrefab, prefabSpawned);
    }
}
