using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Language
{
    public abstract class Lang
    {

        static Lang currentLang;
        static System.Random rnd = new System.Random();

        public static Lang Get
        {
            get
            {
                if (currentLang == null)
                {
                    switch (rnd.Next(0, 3))
                    {
                        case 0:
                            currentLang = new LangJP();
                            break;
                        case 1:
                            currentLang = new LangEN();
                            break;
                        case 2:
                            currentLang = new LangNL();
                            break;
                        default:
                            currentLang = new LangNL();
                            break;
                    }
                }
                     /*
                     switch (Application.systemLanguage)
                     {
                         case SystemLanguage.Dutch:
                             currentLang = new LangNL();
                             break;
                         default:
                         case SystemLanguage.English:
                             currentLang = new LangEN();
                             break;
                     }
                     */

                return currentLang;
            }
        }

        public abstract string Position { get; }
        public abstract string Duration { get; }
        public abstract string NoteNumber { get; }
        public abstract string Velocity { get; }
        public abstract string AddNote { get; }
        public abstract string AllTracks { get; }

    }
}
