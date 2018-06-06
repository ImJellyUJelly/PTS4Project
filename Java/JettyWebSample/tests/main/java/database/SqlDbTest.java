package main.java.database;

import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class SqlDbTest {

    private SqlDb db;

    @Before
    public void SetUp() {
        db = new SqlDb();
    }

    @Test
    public void getMidiStringByIdTest() {
        String midiString = db.getMidiStringById(0);
        Assert.assertTrue(midiString.equals("<xml>Test XML File</xml>"));
    }

    @Test
    public void saveMidiStringTest() {
        String midiString = "<xml>Nieuwe xmlstring for testing.</xml>";
        db.insertMidiString(midiString);
        midiString = db.getAllMidiStrings();
        Assert.assertTrue(midiString.equals("<xml>Nieuwe xmlstring for testing.</xml>"));
    }
}