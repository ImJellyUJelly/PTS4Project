package main.java;

import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class SeizureTest {
    private Seizure seizuer;

    @Before
    public void setUp() throws Exception {
        seizuer = new Seizure();
    }

    @Test
    public void loadMidiString() {
        String testString = "<xml>Test XML File</xml>";
        assertEquals(testString, seizuer.loadMidiString(0));
    }

    @Test
    public void saveMidiString() {
        String midiString = "<xml>Nieuwe xmlstring for testing.</xml>";
        seizuer.saveMidiString(midiString);
        Assert.assertTrue(midiString.equals("<xml>Nieuwe xmlstring for testing.</xml>"));
    }
}