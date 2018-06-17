package main.java;

import main.java.database.SqlDb;

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

    public void saveMidiByte(String midi){sqlDb.insertMidiBlob(midi);}
    public String readMidiByte(){return sqlDb.readMidiBlob();}
    public boolean login(String username, String password){return sqlDb.login(username,password);}
}
