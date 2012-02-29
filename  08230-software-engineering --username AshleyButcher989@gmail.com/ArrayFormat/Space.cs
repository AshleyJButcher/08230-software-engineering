using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrayFormat {
  public class Space : IComparable {

      internal int position = -1;

      public int CompareTo(Object x)
      {
          return 0;
      }

      public override string ToString()
      {
          return " ";
      }
  }
}
