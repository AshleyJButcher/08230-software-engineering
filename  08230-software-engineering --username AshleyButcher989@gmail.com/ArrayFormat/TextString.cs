using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrayFormat
{
    class TextString : IComparable {

      internal int position = -1;
      internal string Text = "";

       public TextString(string input)
       {
           Text = input;
       }

      public override string ToString()
      {
 	        return Text;
      }

      public int CompareTo(Object x)
      {
          return 0;
      }

    }
}
