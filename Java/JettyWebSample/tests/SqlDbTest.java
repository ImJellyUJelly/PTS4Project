package tests;

import main.database.SqlDb;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

public class SqlDbTest {
    private SqlDb db;

    @Before
    public void SetUp() {
        db = new SqlDb();
    }

    @Test
    public void getMidiStringById() {
        // TODO: Create test
        String midiString = db.getMidiStringById(0);
        Assert.assertFalse(midiString.length() < 1);
    }
}