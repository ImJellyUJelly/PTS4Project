package main.java.database;

import java.sql.PreparedStatement;

public class SqlDb {
    // This class will be used to push statements to the main.java.database, using Database-class for: connection, statement and resultset.

    public String getMidiStringById(int id) {
        Database sqlConnection = new Database();
        try{
            sqlConnection.setStatement(sqlConnection.getConnection().createStatement());
            String query = "SELECT XmlString FROM DAWdatabase.dbo.[Midi-Project] WHERE MPID = ?;";

            PreparedStatement ps = sqlConnection.getConnection().prepareStatement(query);
            ps.setInt(1, id);

            sqlConnection.setResult(ps.executeQuery());

            String result = null;
            while(sqlConnection.getResult().next()) {
                result = sqlConnection.getResult().getString(1);
            }
            return result;
        } catch(Exception ex) {
            //Do nothing
            ex.printStackTrace();
        }
        finally{
            sqlConnection.closeAll();
        }
        return null;
    }

    public String getAllMidiStrings() {
        Database sqlConnection = new Database();
        try{
            sqlConnection.setStatement(sqlConnection.getConnection().createStatement());
            String query = "SELECT XmlString FROM DAWdatabase.dbo.[Midi-Project] ORDER BY MPID DESC;";

            PreparedStatement ps = sqlConnection.getConnection().prepareStatement(query);

            sqlConnection.setResult(ps.executeQuery());

            String result = null;
            while(sqlConnection.getResult().next()) {
                result = sqlConnection.getResult().getString(1);
                break;
            }
            return result;
        } catch(Exception ex) {
            //Do nothing
            ex.printStackTrace();
        }
        finally{
            sqlConnection.closeAll();
        }
        return null;
    }

    public void insertMidiString(String midiFile) {
        Database sqlConnection = new Database();
        try{
            sqlConnection.setStatement(sqlConnection.getConnection().createStatement());
            String query = "INSERT INTO DAWdatabase.dbo.[Midi-Project] VALUES (?, 'random name', 'random artist', NULL, NULL);";

            PreparedStatement ps = sqlConnection.getConnection().prepareStatement(query);
            ps.setString(1, midiFile);

            ps.executeUpdate();

        } catch(Exception ex) {
            //Do nothing
            ex.printStackTrace();
        }
        finally{
            sqlConnection.closeAll();
        }
    }
}
