using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Language
{
    public class LangEN : Lang
    {
        public override string Position
        {
            get
            {
                return "Position";
            }
        }

        public override string Duration
        {
            get
            {
                return "Duration";
            }
        }

        public override string NoteNumber
        {
            get
            {
                return "Note Number";
            }
        }

        public override string Velocity
        {
            get
            {
                return "Velocity";
            }
        }

        public override string AddNote
        {
            get
            {
                return "Add note";
            }
        }

        public override string AllTracks
        {
            get
            {
                return "All Tracks";
            }
        }
    }
}
