using System;
using System.Text;

namespace ArrayFormat
{
    class NewLine : Line
    {

        public NewLine(int wrap, Page page)
            : base(wrap, page)
        {
        }

        internal override void IntoText(StringBuilder text)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// will return the length of the string without spaces
        /// </summary>
        /// <returns></returns>
        internal override int Length()
        {
            int templength = 0;
            Space spaceobject = new Space();
            foreach (object obj in Text)
            {
                if (obj.GetType() != spaceobject.GetType())
                {
                    templength += obj.ToString().Length;
                }
            }
            return templength;
        }

        internal override bool Overflow(string s)
        {
            //if (s.Length + Length() <= WrapColumn)
                return false;
           // else
              //  return true;
        }

    }
}
