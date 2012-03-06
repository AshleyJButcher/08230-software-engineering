using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrayFormat {
  public abstract class Line {
      public Page Page { get; set; }

      internal List<object> Text = new List<object>();
      internal int WrapColumn;

      internal Line(int wrap, Page page) {
          Page = page;
          if (wrap < 0) {
        throw new ArgumentException("Wrap column cannot be negative: " + wrap);
      }
      WrapColumn = wrap;
      }

    /// <summary>
    /// Number of strings on line.
    /// </summary>
    /// <returns></returns>
    internal int Count() {
        int templength = 0;
        Space spaceobject = new Space();
        foreach (object obj in Text)
        {
            if (obj.GetType() != spaceobject.GetType())
            {
                templength++;
            }
        }
        return templength;
    }

    internal abstract int Length();

    /// <summary>
    /// True if line violates line condition.
    /// </summary>
    /// <returns></returns>
    internal abstract bool Overflow(string add);

    /// <summary>
    /// String at supplied index
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    internal String At(int i){
        Space spaceobject = new Space();
        var stringList = Text.Where(obj => obj.GetType() != spaceobject.GetType()).ToList(); //Without Spaces
        return stringList[i].ToString();
        //return Text[i].ToString();
    }

      /// <summary>
      /// Set supplied string at supplied index
      /// </summary>
      /// <param name="i"></param>
      /// <param name="s"> </param>
      /// <returns></returns>
      internal void At(int i, String s) {
        Space newspaceobject = new Space();
        int counter = 0;
        int incrementer = 0;
        while (true)
        {
            while (Text[incrementer].GetType() == newspaceobject.GetType()) //Find Only the Strings
            {
                incrementer++; //If position is a space, increment til its a number
            }
            //We Will Get Here When We Have a String
            if (i == counter)
            {
                i = incrementer; //Location of the String
                break;
            }
            counter++; //Next String
            incrementer++; //Move Along One
        }
        object currentobject;
        if (s != " ")
        {
            currentobject = new TextString(s);
        }
        else
        {
           currentobject = new Space();

        }
        Text.Insert(i+1, currentobject);
    }

    /// <summary>
    /// Add string to this line subject to overflow constraint.
    /// True if added and hence no new line required.
    /// False if not added to line.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    internal bool Add(String s) {
        try
        {
            if (Overflow(s))
            {
                return false;
            }
            TextString input = new TextString(s); //Create a Custom String Object with the string
            Text.Add(input); //Add it to the Object Collection
            Space space = new Space();
            Text.Add(space);
            return true;
        }
        catch (Exception)
        {
            return false; //If We Could Not Add It
        }
    }


    /// <summary>
    /// Place strings into supplied buffer
    /// </summary>
    /// <param name="text"></param>
    internal abstract void IntoText(StringBuilder text);

      /// <summary>
      /// Overrides the Lines ToString Method
      /// </summary>
      /// <returns></returns>
    public override string ToString()
    {
        string temp = "";
        foreach (object obj in Text)
        {
            temp += obj.ToString();
        }
        return temp + ((char) 10);
    }
  }
}
