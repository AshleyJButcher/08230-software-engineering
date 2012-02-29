using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ArrayFormat {
  public class Program {

    const String outputFileSuffix = ".out";

    const String whiteSpaceChars = @" \n\t\r";

    public enum ArrayFormat : int { Fill, UniformCount, UniformCountStrict, Column, ColumnStrict, Adjust, Set };

    internal static ArrayFormat format;

    public static void Main(string[] args) {
      if (args.Length != 3) {
        Console.WriteLine("Incorrect number of args, usage: arrayFormat.exe filename wrapColumn formatSpec");
        return;
      }
      String inputFile = args[0];
      // read wrap column
      int wrap;
      try {
        wrap = Int32.Parse(args[1]);
      }
      catch (Exception e) {
        Console.WriteLine("Bad input, cannot read wrap column: " + args[1]);
        Console.WriteLine(e.Message);
        return;
      }
      switch (args[2]) {
        case "Fill":
          format = ArrayFormat.Fill;
          break;
        case "UniformCount":
          format = ArrayFormat.UniformCount;
          break;
        case "UniformCountStrict":
          format = ArrayFormat.UniformCountStrict;
          break;
        case "Column":
          format = ArrayFormat.Column;
          break;
        case "ColumnStrict":
          format = ArrayFormat.ColumnStrict;
          break;
        case "Adjust":
          format = ArrayFormat.Adjust;
          break;
        case "Set":
          format = ArrayFormat.Set;
          break;
        default:
          throw new Exception("Unknown format specification: " + args[2]);
      }

      String[] sarray = InputStrings(inputFile);
      Page page = null;
      switch (format) {
        case ArrayFormat.Fill:

              page = new Text(wrap);
             ArrayMethods hi = new ArrayMethods();
             hi.FillAdjust(sarray,wrap,page);
              //Console.WriteLine(page.ToString());
              Console.ReadLine();
          break;
        case ArrayFormat.UniformCount:
          break;
        case ArrayFormat.UniformCountStrict:
          break;
        case ArrayFormat.Column:
          break;
        case ArrayFormat.ColumnStrict:
          break;
        case ArrayFormat.Adjust:
          break;
        case ArrayFormat.Set:
           break;
        default:
          throw new Exception("Unknown format specification: " + args[2]);
      }
      page.ToFile(inputFile + outputFileSuffix);
      //Console.ReadLine();
    }

    /// <summary>
    /// Input strings from supplied file and return as array.
    /// </summary>
    /// <param name="fileName"></param>
    private static String[] InputStrings(String fileName) {
      // open input
      FileInfo fileInfo = new FileInfo(fileName);
      if (!fileInfo.Exists) {
        Console.WriteLine("Input file not found : " + fileInfo.Directory.FullName + @"\" + fileName);
        return new String[0]; ;
      }
      List<String> sarray = new List<String>();
      using (FileStream inStream = fileInfo.OpenRead()) {
        using (StreamReader reader = new StreamReader(inStream)) {
          String line = reader.ReadLine();
          while (line != null) {
            String[] lineParts = line.Split(whiteSpaceChars.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries); // line.Split(' ');  in student version
            foreach (String s in lineParts) {
              sarray.Add(s);
            }
            line = reader.ReadLine();
          }
        }
      }
      return sarray.ToArray();
    }

  }
}
