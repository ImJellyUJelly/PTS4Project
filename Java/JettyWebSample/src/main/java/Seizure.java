package main.java;

import main.database.SqlDb;

public class Seizure { // facade
    // Nou succes jongens, bring me code
    private SqlDb sqlDb;

    public Seizure() {
        sqlDb = new SqlDb();
    }

    public String loadMidiString(int id) {
        return sqlDb.getMidiStringById(id);
    }

    public void saveMidiString(String midi) {
        sqlDb.insertMidiString(midi);
    }
}
