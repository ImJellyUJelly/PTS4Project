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
    public boolean login(String username, String password){
        Database sqlConnection = new Database();
        String result = null;
        try{
            sqlConnection.setStatement(sqlConnection.getConnection().createStatement());
            String query = "SELECT * FROM DAWdatabase.dbo.[User] where username = '"+username+"' and password = '"+password+"'";

            PreparedStatement ps = sqlConnection.getConnection().prepareStatement(query);

            sqlConnection.setResult(ps.executeQuery());
            if (sqlConnection.getResult().next()) {
                result = sqlConnection.getResult().getString(1);
            }
        } catch(Exception ex) {
            //Do nothing
            ex.printStackTrace();
        }
        finally{
            sqlConnection.closeAll();
        }
        if(result == null){
            return false;
        }
        else{
            return true;
        }
    }

    public void insertMidiBlob(String midi) {
        Database sqlConnection = new Database();
        try{
            sqlConnection.setStatement(sqlConnection.getConnection().createStatement());
            byte[] theByteArray = midi.getBytes();
            System.out.println(theByteArray.length);
            String query = "INSERT INTO DAWdatabase.dbo.[MidiBLOB] (blob) VALUES (?);";

            PreparedStatement ps = sqlConnection.getConnection().prepareStatement(query);
            ps.setBytes(1, theByteArray);

            ps.executeUpdate();

        } catch(Exception ex) {
            //Do nothing
            ex.printStackTrace();
        }
        finally{
            sqlConnection.closeAll();
        }
    }

    public String readMidiBlob() {
        Database sqlConnection = new Database();
        String result = null;
        try{
            sqlConnection.setStatement(sqlConnection.getConnection().createStatement());
            String query = "SELECT [BLOB] from DAWdatabase.dbo.[MidiBLOB] Where id = (?);";

            PreparedStatement ps = sqlConnection.getConnection().prepareStatement(query);
            ps.setInt(1, 36);
            sqlConnection.setResult(ps.executeQuery());
            if (sqlConnection.getResult().next()) {
                result = sqlConnection.getResult().getString(1);
            }

        } catch(Exception ex) {
            //Do nothing
            ex.printStackTrace();
        }
        finally{
            sqlConnection.closeAll();
        }
        return result;
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
