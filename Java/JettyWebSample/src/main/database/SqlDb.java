package main.database;

import java.sql.PreparedStatement;

public class SqlDb {
    // This class will be used to push statements to the database, using Database-class for: connection, statement and resultset.

    public String getMidiStringById(int id) {
        Database sqlConnection = new Database();
        try{
            sqlConnection.setStatement(sqlConnection.getConnection().createStatement());
            String query = ""; // TODO: Make the query

            PreparedStatement ps = sqlConnection.getConnection().prepareStatement(query);
            ps.setInt(1, id);

            sqlConnection.setResult(ps.executeQuery());

            String result = null;
            while(sqlConnection.getResult().next()) {
                result = sqlConnection.getResult().getString(1); // TODO: Find the right row to return from the Id.
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
            String query = ""; // TODO: Make the query

            PreparedStatement ps = sqlConnection.getConnection().prepareStatement(query);

            sqlConnection.setResult(ps.executeQuery());

            // TODO: input the String into the database
        } catch(Exception ex) {
            //Do nothing
            ex.printStackTrace();
        }
        finally{
            sqlConnection.closeAll();
        }
    }
}