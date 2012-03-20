using System;

namespace ArrayFormat
{
    internal class ArrayMethods
    {
        public void Fill(string[] inputstrings, int wrapcolumn, Page page)
        {
            bool startingaNewLine = true;
            int stringCounter = 0;
            int offset = 0; //offset
            int linelength = 0;

            for (int j = 0; j <= (inputstrings.Length - 1); j++) //for the amount of values in the array - 1
            {
                if (startingaNewLine)
                {
                    stringCounter = 0;
                    linelength = inputstrings[offset].Length + 1;
                }
                //check if the last one 

                #region Last Element

                if (j == (inputstrings.Length - 1) && startingaNewLine == false) //
                {
                    var array = new string[stringCounter + 1];
                    for (int i = 0; i <= stringCounter; i++)
                    {
                        array[i] = inputstrings[i + offset];
                    }
                    page.Add(array);
                    break;
                }
                if (j == (inputstrings.Length - 1) && startingaNewLine) //
                {
                    var array = new string[stringCounter + 1];
                    for (int i = 0; i <= stringCounter; i++)
                    {
                        array[i] = inputstrings[i + offset];
                    }
                    page.Add(array);
                    break;
                }

                #endregion

                if (inputstrings[(j + 1)].Length + (linelength + 1) <= wrapcolumn) //if we can fit it on the same line
                {
                    stringCounter++;
                    linelength += inputstrings[(j + 1)].Length + 1;
                    startingaNewLine = false;
                }
                else
                {
                    var array = new string[stringCounter + 1];
                    for (int i = 0; i <= stringCounter; i++)
                    {
                        array[i] = inputstrings[i + offset];
                    }
                    offset += stringCounter + 1; //Update Offset
                    page.Add(array);
                    startingaNewLine = true;
                }
            }
        }

        private void FillMod(string[] inputstrings, int wrapcolumn, Page page, int maxelements)
        {
            bool startingaNewLine = true;
            int stringCounter = 0;
            int offset = 0; //offset
            int linelength = 0;
            int elementlimit = 0;

            for (int j = 0; j <= (inputstrings.Length - 1); j++) //for the amount of values in the array - 1
            {
                if (startingaNewLine)
                {
                    stringCounter = 0;
                    linelength = inputstrings[offset].Length + 1;
                    elementlimit = 1;
                }
                //check if the last one 

                #region Last Element

                if (j == (inputstrings.Length - 1) && startingaNewLine == false) //
                {
                    var array = new string[stringCounter + 1];
                    for (int i = 0; i <= stringCounter; i++)
                    {
                        array[i] = inputstrings[i + offset];
                    }
                    page.Add(array);
                    break;
                }
                if (j == (inputstrings.Length - 1) && startingaNewLine) //
                {
                    var array = new string[stringCounter + 1];
                    for (int i = 0; i <= stringCounter; i++)
                    {
                        array[i] = inputstrings[i + offset];
                    }
                    page.Add(array);
                    break;
                }

                #endregion

                if (inputstrings[(j + 1)].Length + (linelength + 1) <= wrapcolumn && elementlimit < maxelements)
                    //if we can fit it on the same line
                {
                    stringCounter++;
                    elementlimit++;
                    linelength += inputstrings[(j + 1)].Length + 1;
                    startingaNewLine = false;
                }
                else
                {
                    var array = new string[stringCounter + 1];
                    for (int i = 0; i <= stringCounter; i++)
                    {
                        array[i] = inputstrings[i + offset];
                    }
                    offset += stringCounter + 1; //Update Offset
                    page.Add(array);
                    startingaNewLine = true;
                }
            }
        }

        public int UniformCount(string[] inputstrings, int wrapcolumn, Page page, bool strict)
        {
            int strictoffset = 0;
            if (strict == false)
                strictoffset = 1;
            int maxPossibleColumn = 0;
            for (int h = 1; h < wrapcolumn; h++)
            {
                Page temp = new Text(wrapcolumn);
                FillMod(inputstrings, wrapcolumn, temp, h);
                int maxcolumns = 0;
                int elementsperline = temp.Lines[0].Count();
                //will break if there is one line
                bool alltheSame = false;
                for (int i = 1; i < temp.Lines.Count - strictoffset; i++) //Get the total amount of elements
                {
                    if (elementsperline == temp.Lines[i].Count())
                    {
                        alltheSame = true;
                        maxcolumns = elementsperline;
                    }
                    else
                    {
                        alltheSame = false;
                        break;
                    }

                }
                if (alltheSame)
                {
                    maxPossibleColumn = maxcolumns;
                }
            }
            FillMod(inputstrings, wrapcolumn, page, maxPossibleColumn);
            return maxPossibleColumn;
        }

        public void Column(string[] inputstrings, int wrapcolumn, Page page, bool strict)
        {
            int totalcolumns = UniformCount(inputstrings, wrapcolumn, page, strict);
            if (totalcolumns != 1)
            {
                //Efficency at its finest LOL
                for (int column = 0; column < totalcolumns; column++)
                {
                    int longestline = 0;
                    for (int i = 0; i < page.Lines.Count; i++) //Find the Longest String
                    {
                        if (page.Lines[i].Count() > column) //Wont Break If Its the Last Column
                            if (longestline < page.Lines[i].At(column).Length) //If Current Line is Longer than Previous
                                longestline = page.Lines[i].At(column).Length; //Set as New Longest Line
                    }

                    foreach (Line t in page.Lines)
                    {
                        if (t.Count() > column)
                        {
                            int num = t.At(column).Length; //Get Length of the String
                            for (int h = num; h < longestline; h++) //Repeat for the amount of spaces to add
                            {
                                if (t.Count() > column)
                                    t.At(column, " "); //Add a Space
                            }
                        }
                    }
                }
            }
        }

        public void Set(string[] inputstrings, int wrapcolumn, Page page)
        {
            SortArray(inputstrings);
            var used = new bool[inputstrings.Length]; //Create a Boolean Array
            var organisedStrings = new string[inputstrings.Length];
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
                    organisedStrings[counter] = inputstrings[g];
                    counter++;
                    int diff = wrapcolumn - inputstrings[g].Length;
                    for (int i = 0; i < inputstrings.Length; i++)
                    {
                        if (inputstrings[i].Length == diff && used[i] == false)
                        {
                            used[i] = true;
                            organisedStrings[counter] = inputstrings[i];
                            counter++;
                            //Console.WriteLine(inputstrings[i]);
                        }
                    }
                }
            }

            for (int i = 0; i < used.Length; i++) //Add Ones That Wouldn't Fit
            {
                if (used[i] == false)
                {
                    organisedStrings[counter] = inputstrings[i];
                    counter++;
                }
            }
            Fill(organisedStrings, wrapcolumn, page);
        }

        private void SortArray(string[] stringsort)
        {
            for (int i = (stringsort.Length - 1); i >= 0; i--)
            {
                int j;
                for (j = 1; j <= i; j++)
                {
                    if (stringsort[j - 1].Length < stringsort[j].Length)
                    {
                        string temp = stringsort[j - 1];
                        stringsort[j - 1] = stringsort[j];
                        stringsort[j] = temp;
                    }
                }
            }
        }

        public void FillAdjust(string[] inputstrings, int wrapcolumn, Page page)
        {
            Fill(inputstrings, wrapcolumn, page);
            foreach (Line t in page.Lines)
            {
                int spacestoadd = (wrapcolumn - t.Length());
                if (spacestoadd < 0) //If there are no spaces to add
                    spacestoadd = 0;
                int divide = t.Count()%2;
                bool divisable = Convert.ToBoolean(divide); //Spaces are Evenly Divisable
                if (divisable)
                {
                    #region Even Spaces
                    //case divisable by 2
                    bool evenspaces = spacestoadd%2 == 0;
                    if (evenspaces)
                    {
                        int equalspaces = (spacestoadd/2);
                        for (int g = 0; g < t.Count(); g++) //For Each Gap
                        {
                            for (int h = 0; h < equalspaces; h++) //For each of the spaces to add
                            {
                                t.At(g, " "); //Add space
                            }
                        }
                    }
                    else
                    {
                        int remainder = spacestoadd % 2;
                        int leftToAdd = (spacestoadd - remainder) / 2;
                        for (int g = 0; g < t.Count(); g++) //For Each Gap
                        {
                            for (int h = 0; h < leftToAdd; h++) //For each of the spaces to add
                            {
                                t.At(g, " "); //Add space
                            }
                        }
                        int gapdiff = -100; //set it very high to start
                        int marker = 0; //this will tell us where it was
                        int previous = 0; //will set the previous one
                        //Now the Remaining One
                        for (int g = 0; g < t.Count(); g++) //For Each Gap
                        {
                            if(previous + t.At(g).Length > gapdiff)
                            {
                                marker = g - 1;
                                gapdiff = previous - t.At(g).Length;
                                previous = t.At(g).Length;
                            }
                        }
                        t.At(marker, " ");
                    }
                    #endregion
                }
                else
                {
                    int loopreps = spacestoadd/t.Count();
                    bool[] usedarray = new bool[t.Count()];
                    //Add As Many even spaces as possible
                    for (int g = 0; g < t.Count(); g++) //For Each Gap
                    {
                        for (int h = 0; h < loopreps; h++) //For each of the spaces to add
                        {
                            t.At(g, " "); //Add space
                        }
                    }

                    int oddspacestoadd = spacestoadd - (loopreps*t.Count());

                    Console.WriteLine(oddspacestoadd.ToString());
                    //get biggest elements and add space
                    for (int f = 0; f < oddspacestoadd; f++) //Spaces to Add
                    {
                        int gapdiff = -100; //set it very high to start
                        int marker = 0; //this will tell us where it was
                        int previous = 0; //will set the previous one
                        //Now the Remaining One

                        for (int g = 0; g < t.Count(); g++) //For Each Gap
                        {
                            if (previous + t.At(g).Length > gapdiff && !usedarray[g])
                            {
                                marker = g - 1;
                                gapdiff = previous + t.At(g).Length;
                                previous = t.At(g).Length;
                            }
                        }

                        for (int h = 0; h < t.Count(); h++)
                        {
                            if (!usedarray[h])
                            {
                                t.At(marker, " ");
                                usedarray[marker] = true;
                                break;
                            }
                        }
                    }
                }
                //move end spaces to the start
                t.Adjust();
            }
        }



    }
}
