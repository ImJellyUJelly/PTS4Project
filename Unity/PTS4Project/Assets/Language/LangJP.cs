using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Language
{
    public class LangJP : Lang
    {
        public override string Position
        {
            get
            {
                return "位置";
            }
        }

        public override string Duration
        {
            get
            {
                return "延長";
            }
        }

        public override string NoteNumber
        {
            get
            {
                return "ノートナンバー";
            }
        }

        public override string Velocity
        {
            get
            {
                return "速度";
            }
        }

        public override string AddNote
        {
            get
            {
                return "追加";
            }
        }

        public override string AllTracks
        {
            get
            {
                return "すべてのトラック";
            }
        }
    }
}
