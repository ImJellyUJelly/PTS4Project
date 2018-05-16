using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Language
{
    public class LangNL : Lang
    {
        public override string Position
        {
            get
            {
                return "Plaats";
            }
        }

        public override string Duration
        {
            get
            {
                return "Lengte";
            }
        }

        public override string NoteNumber
        {
            get
            {
                return "Noot";
            }
        }

        public override string Velocity
        {
            get
            {
                return "Snelheid";
            }
        }

        public override string AddNote
        {
            get
            {
                return "Noot toevoegen";
            }
        }

        public override string AllTracks
        {
            get
            {
                return "Alle Tracks";
            }
        }
    }
}
