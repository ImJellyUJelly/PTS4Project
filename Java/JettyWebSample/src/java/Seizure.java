package java;

import database.SqlDb;

public class Seizure { // facade
    private SqlDb database;

    public Seizure() {
        database = new SqlDb();
    }

    // Nou succes jongens, bring me code
    public boolean login(String username, String password) {
        // TODO: Login fixen
        if (true) {
            return true;
        } else if (false) {
            return false;
        } else {
            return false;
        }
    }

    public void saveMidiFile(String midiFile) {
        database.saveMidiFile(midiFile);
    }

    public String loadMidiFile(String id) {
        return database.loadMidiFile(id);
    }
}
