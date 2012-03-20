using System;

namespace ArrayFormat {
  public class Space : IComparable {

      internal int Position = -1;

      public int CompareTo(Object x)
      {
          return 0;
      }

      public override string ToString()
      {
          return "#";
      }
  }
}
