using System;

namespace ArrayFormat
{
    class TextString : IComparable {

      internal int Position = -1;
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
