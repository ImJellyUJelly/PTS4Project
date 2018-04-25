using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class NoteGrid
    {
        public Hashtable GridNote { get; set; }

        public NoteGrid()
        {
            GridNote = new Hashtable();
            FillTable();
        }

        private void FillTable() // NEVER SHOW THIS
        {
            for (int i = 0; i < 11; i++)
            {
                int noteMultiplier = i * 12;
                int extraY = 0;
                //int noteMultiplier2 = 36;

                if (i > 0)
                {
                    extraY = 36 * i;
                }
                GridNote.Add(0 + noteMultiplier, ((-20 - (0 + noteMultiplier) * 18)) - extraY);
                GridNote.Add(1 + noteMultiplier, (-20 - (1 + noteMultiplier) * 18) - extraY);
                GridNote.Add(2 + noteMultiplier, (-20 - (2 + noteMultiplier) * 18) - extraY);
                GridNote.Add(3 + noteMultiplier, (-20 - (3 + noteMultiplier) * 18) - extraY);
                GridNote.Add(4 + noteMultiplier, (-20 - (4 + noteMultiplier) * 18) - extraY);
                GridNote.Add(5 + noteMultiplier, ((-20 - (5 + noteMultiplier) * 18) - 18) - extraY);
                GridNote.Add(6 + noteMultiplier, ((-20 - (6 + noteMultiplier) * 18) - 18) - extraY);
                GridNote.Add(7 + noteMultiplier, ((-20 - (7 + noteMultiplier) * 18) - 18) - extraY);
                GridNote.Add(8 + noteMultiplier, ((-20 - (8 + noteMultiplier) * 18) -18) - extraY);
                GridNote.Add(9 + noteMultiplier, ((-20 - (9 + noteMultiplier) * 18) - 18) - extraY);
                GridNote.Add(10 + noteMultiplier, ((-20 - (10 + noteMultiplier) * 18)- 18) - extraY);
                GridNote.Add(11 + noteMultiplier, ((-20 - (11 + noteMultiplier) * 18)- 18) - extraY);
            }
        }

    }
}
