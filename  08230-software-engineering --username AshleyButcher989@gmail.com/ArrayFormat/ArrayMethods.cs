using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrayFormat
{
    class ArrayMethods
    {
        public void Fill(string[] inputstrings, int wrapcolumn, Page page)
        {
            string[] stringarray = new string[inputstrings.Length];
            bool StartingaNewLine = true;
            int StringCounter = 0;
            int offset = 0; //offset
            int linelength = 0;

            for (int j = 0; j <= (inputstrings.Length - 1); j++) //for the amount of values in the array - 1
            {
                if (StartingaNewLine == true)
                {
                    StringCounter = 0;
                    linelength = inputstrings[offset].Length;
                }
                //check if the last one 
                #region Last Element
                if (j == (inputstrings.Length - 1) && StartingaNewLine == false) //
                {
                    string[] array = new string[StringCounter + 1];
                    for (int i = 0; i <= StringCounter; i++)
                    {
                        array[i] = inputstrings[i + offset];
                    }
                    page.Add(array);
                    break;
                }
                else if (j == (inputstrings.Length - 1) && StartingaNewLine == true) //
                {
                    string[] array = new string[StringCounter + 1];
                    for (int i = 0; i <= StringCounter; i++)
                    {
                        array[i] = inputstrings[i + offset];
                    }
                    page.Add(array);
                    break;
                }
                #endregion
                if (inputstrings[(j + 1)].Length + linelength <= wrapcolumn) //if we can fit it on the same line
                {
                    StringCounter++;
                    linelength += inputstrings[(j + 1)].Length;
                    StartingaNewLine = false;
                }
                else
                {
                    string[] array = new string[StringCounter + 1];
                    for (int i = 0; i <= StringCounter; i++)
                    {
                        array[i] = inputstrings[i + offset]; 
                    }
                    offset += StringCounter + 1; //Update Offset
                    page.Add(array);
                    StartingaNewLine = true;
                }
            }
        }

        public void FillMod(string[] inputstrings, int wrapcolumn, Page page, int maxelements)
        {
            string[] stringarray = new string[inputstrings.Length];
            bool StartingaNewLine = true;
            int StringCounter = 0;
            int offset = 0; //offset
            int linelength = 0;
            int elementlimit = 0;

            for (int j = 0; j <= (inputstrings.Length - 1); j++) //for the amount of values in the array - 1
            {
                if (StartingaNewLine == true)
                {
                    StringCounter = 0;
                    linelength = inputstrings[offset].Length;
                    elementlimit = 1;
                }
                //check if the last one 
                #region Last Element
                if (j == (inputstrings.Length - 1) && StartingaNewLine == false) //
                {
                    string[] array = new string[StringCounter + 1];
                    for (int i = 0; i <= StringCounter; i++)
                    {
                        array[i] = inputstrings[i + offset];
                    }
                    page.Add(array);
                    break;
                }
                else if (j == (inputstrings.Length - 1) && StartingaNewLine == true) //
                {
                    string[] array = new string[StringCounter + 1];
                    for (int i = 0; i <= StringCounter; i++)
                    {
                        array[i] = inputstrings[i + offset];
                    }
                    page.Add(array);
                    break;
                }
                #endregion
                if (inputstrings[(j + 1)].Length + linelength <= wrapcolumn && elementlimit < maxelements) //if we can fit it on the same line
                {
                    StringCounter++;
                    elementlimit++;
                    linelength += inputstrings[(j + 1)].Length;
                    StartingaNewLine = false;
                }
                else
                {
                    string[] array = new string[StringCounter + 1];
                    for (int i = 0; i <= StringCounter; i++)
                    {
                        array[i] = inputstrings[i + offset];
                    }
                    offset += StringCounter + 1; //Update Offset
                    page.Add(array);
                    StartingaNewLine = true;
                }
            }
        }

        public int UniformCount(string[] inputstrings, int wrapcolumn, Page page, bool strict)
        {
            int strictoffset = 0;
            if (strict == false)
                strictoffset = 1;
            int MaxPossibleColumn = 0;
            for (int h = 1; h < wrapcolumn; h++)
            {
                Page temp = new Text(wrapcolumn);
                FillMod(inputstrings, wrapcolumn, temp, h);
                int maxcolumns = 0;
                int elementsperline = temp.Lines[0].Count();
                //will break if there is one line
                bool AlltheSame = false;
                for (int i = 1; i < temp.Lines.Count - strictoffset; i++) //Get the total amount of elements
                {
                    if (elementsperline == temp.Lines[i].Count())
                    {
                        AlltheSame = true;
                        maxcolumns = elementsperline;
                    }
                    else
                    {
                        AlltheSame = false;
                        break;
                    }

                }
                if (AlltheSame == true)
                {
                    MaxPossibleColumn = maxcolumns;
                }
            }
            FillMod(inputstrings, wrapcolumn, page, MaxPossibleColumn);
            return MaxPossibleColumn;
        }

        public void Column(string[] inputstrings, int wrapcolumn, Page page, bool strict)
        {
            int Totalcolumns = UniformCount(inputstrings, wrapcolumn, page, strict);
            if (Totalcolumns != 1)
            {
                //Efficency at its finest LOL
                for (int Column = 0; Column < Totalcolumns; Column++)
                {
                    int longestline = 0;
                    for (int i = 0; i < page.Lines.Count; i++) //Find the Longest String
                    {
                        if (page.Lines[i].Count() > Column) //Wont Break If Its the Last Column
                            if (longestline < page.Lines[i].At(Column).Length) //If Current Line is Longer than Previous
                                longestline = page.Lines[i].At(Column).Length; //Set as New Longest Line
                    }

                    for (int i = 0; i < page.Lines.Count; i++) //Go Through Each String Adding More Spaces
                    {
                        if (page.Lines[i].Count() > Column)
                        {
                            int num = page.Lines[i].At(Column).Length; //Get Length of the String
                            for (int h = num; h < longestline; h++) //Repeat for the amount of spaces to add
                            {
                                if (page.Lines[i].Count() > Column)
                                    page.Lines[i].At(Column, " "); //Add a Space
                            }
                        }
                    }
                }
            }
            }

        public void Set(string[] inputstrings, int wrapcolumn, Page page)
        {
            sortArray(inputstrings);
            bool[] used = new bool[inputstrings.Length]; //Create a Boolean Array
            string[] OrganisedStrings = new string[inputstrings.Length];
            int counter = 0;
            for (int i = 0; i < inputstrings.Length; i++)
            { 
                used[i] = false; //Initialise them all as true
            }


            for (int g = 0; g < inputstrings.Length; g++)
            {
                if (inputstrings[g].Length != wrapcolumn && used[g] == false) //If we are not already at the limit
                {
                    used[g] = true;
                    OrganisedStrings[counter] = inputstrings[g];
                    counter++;
                    int diff = wrapcolumn - inputstrings[g].Length;
                    for (int i = 0; i < inputstrings.Length; i++)
                    {
                        if (inputstrings[i].Length == diff && used[i] == false)
                        {
                            used[i] = true;
                            OrganisedStrings[counter] = inputstrings[i];
                            counter++;
                            //Console.WriteLine(inputstrings[i]);
                        }
                    }
                }
            }

            for(int i = 0 ; i < used.Length; i++) //Add Ones That Wouldn't Fit
            {
                if (used[i] == false)
                {
                    OrganisedStrings[counter] = inputstrings[i];
                    counter++;
                }
            }
            Fill(OrganisedStrings, wrapcolumn, page);
        }

        public void sortArray(string[] stringsort)
        {
            int i;
            int j;
            string temp;

            for (i = (stringsort.Length - 1); i >= 0; i--)
            {
                for (j = 1; j <= i; j++)
                {
                    if (stringsort[j - 1].Length < stringsort[j].Length)
                    {
                        temp = stringsort[j - 1];
                        stringsort[j - 1] = stringsort[j];
                        stringsort[j] = temp;
                    }
                }
            }
        }

        public void FillAdjust(string[] inputstrings, int wrapcolumn, Page page)
        {
            Fill(inputstrings, wrapcolumn, page);
            for(int i = 0; i < page.Lines.Count; i++)
            {
                int spacestoadd = (wrapcolumn - page.Lines[i].Length());
                if (spacestoadd < 0) //If there are no spaces to add
                    spacestoadd = 0;
                int divide = page.Lines[i].Count() % 2;
                bool Divisable = Convert.ToBoolean(divide);
                Console.WriteLine("Danny likes kids");
                //case divisable by 2
                int equalspaces = (spacestoadd / 2);
                for (int g = 0; g < page.Lines[0].Count(); g++) //For Each Gap
                {
                    for (int h = 0; h < equalspaces; h++) //For each of the spaces to add
                    {
                        page.Lines[i].At(g, " "); //Add space
                    }
                }
            }
        }
    
    }
}
