using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Assets;

public class NoteGridTest {

    [Test]
    public void NoteGridTestSimplePasses()
    {
        //test if method correctly fills the hashtable
        NoteGrid notegrid = new NoteGrid();
        Hashtable gridnotetest = new Hashtable();
        gridnotetest = notegrid.GridNote;
        Assert.Greater(gridnotetest.Count, 0);

        //test if the value correspondents with the correct key
        object value = -20;
        Assert.AreEqual(notegrid.GridNote[0], value);
    }
}
