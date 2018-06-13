package main.java.database;

import java.sql.Statement;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;

public class Database {
    private Connection connection;
    private Statement statement;
    private ResultSet result;

    public Database(){
        connection = setConnection();
    }

    public Connection getConnection() {
        return connection;
    }

    public static Connection setConnection(){
        try{
            String dbDriver = "com.microsoft.sqlserver.jdbc.SQLServerDriver";
            Class.forName(dbDriver).newInstance();

            return DriverManager.getConnection("jdbc:sqlserver://daw.database.windows.net;" + "databaseName=DAWdatabase", "pts42", "Password1");
        }
        catch(Exception exception){
            return null;
        }
    }

    public Statement getStatement() {
        return statement;
    }

    public void setStatement(Statement statement) {
        this.statement = statement;
    }

    public ResultSet getResult() {
        return result;
    }

    public void setResult(ResultSet result) {
        this.result = result;
    }

    private void closeStatement(){
        if (statement != null) {
            try {
                statement.close();
            } catch (SQLException e) {
                //Do nothing
            }
        }
    }

    private void closeResultSet(){
        if (result != null) {
            try {
                result.close();
            } catch (SQLException e) {
                //Do nothing
            }
        }
    }

    private void closeConnection(){
        if (connection != null) {
            try {
                connection.close();
            } catch (SQLException e) {
                //Do nothing
            }
        }
    }

    public void closeAll(){
        closeResultSet();
        closeStatement();
        closeConnection();
    }
}
