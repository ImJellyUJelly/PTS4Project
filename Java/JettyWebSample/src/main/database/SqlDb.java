package main.database;

public class SqlDb {
    // This class will be used to push statements to the database, using Database-class for: connection, statement and resultset.
    private Database database;
    public SqlDb() {
        this.database = new Database();
    }

    public String loadMidiFile(String id) {
        // TODO: Load the string (of the midifile) from the database
        return "";
    }

    public void saveMidiFile(String file) {
        // TODO: Save the string (of the midifile) to the database
    }
}
