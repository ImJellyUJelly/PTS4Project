package main.database;

public class SqlDb {
    // This class will be used to push statements to the database, using Database-class for: connection, statement and resultset.
    private Database database;
    public SqlDb() {
        this.database = new Database();
    }


}
