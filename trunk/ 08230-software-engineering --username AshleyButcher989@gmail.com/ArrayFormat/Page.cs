using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ArrayFormat {

  public abstract class Page {

    internal int wrap;
    internal List<Line> Lines = new List<Line>();

    internal Page(int wrap) {
      if (wrap < 0) {
        throw new ArgumentException("Line count cannot be less than zero: " + wrap);
      }
      this.wrap = wrap;
    }

    /// <summary>
    /// Add strings to this page maintaining the format
    /// </summary>
    /// <param name="s"></param>
    internal void Add(String[] sarray) {
        Line newline = new NewLine(wrap, this);
        for (int i = 0; i < sarray.Length; i++)
        {

            if (newline.Add(sarray[i].ToString()) == false)
                break;
        }
        Lines.Add(newline);
    }

    /// <summary>
    /// Place strings into supplied buffer
    /// </summary>
    /// <param name="text"></param>
    internal void IntoText(StringBuilder text)
    {
    }

    internal void ToFile(String fileName) {
      StringBuilder outText = new StringBuilder(9999);
      IntoText(outText);
      try {
        using (StreamWriter sw = new StreamWriter(fileName)) {
          sw.Write(outText.ToString());
        }
      }
      catch (Exception e) {
        String message = "Failed to write output file: " + e.Message;
        Console.WriteLine(message);
        throw new Exception(message);
      }
    }

    public override String ToString() {
        string tempstring = "";
        foreach (Line node in Lines)
        {
            tempstring += node.ToString();
        }
        return tempstring;
    }

  }
}
