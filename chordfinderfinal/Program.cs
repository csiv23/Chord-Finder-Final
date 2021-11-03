using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace chordfinderfinal
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Note Fsharp  = new Note("F#");

            getWholeStep(Fsharp);



           Console.WriteLine("Welcome to the program! Please input the tonic note of the key.\n");

            
          Note tonicNote = new Note(Console.ReadLine(), Degree.Tonic);

           // Note G = new Note("G");

           // Console.WriteLine("You put in " + getFullName(tonicNote) + "\n");


            //getHalfStep(G, tonicNote);

            List<String> notes = printListOfNotes(Generate(tonicNote));

            
            foreach (var item in notes)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Here's everything up a whole step:");



            List<String> wholeSteps = printWholeSteps(Generate(tonicNote));

            foreach (var item in wholeSteps)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();

            

        }


        /// <summary>
        /// Return the full name of a note. Converts the accidental to a String which is appended to the note name.
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public static String getFullName(Note note)
        {
            String result = note.name;
            if (note.accidental == Accidental.Flat)
            {
                result += '♭';
            }
            else if (note.accidental == Accidental.Sharp)
            {
                result += '#';
            }

            if (note.accidental == Accidental.DoubleFlat)
            {
                result += "𝄫";
            }
            else if (note.accidental == Accidental.DoubleSharp)
            {
                result += "𝄪";
            }

            else
            {
                result += '♮';
            }

            return result;
        }

   

        public static Note getHalfStep(Note givenNote, Note tonicNote)
        {
            Note result = null;

            if (givenNote.accidental == Accidental.Natural)
            {
                if (givenNote.name == "B" || givenNote.name == "E")
                {
                    result = new Note(generateNextLetter(givenNote.name));
                }
                else
                {
                    result = new Note(givenNote.name + "#");

                    if(tonicNote.accidental == Accidental.Flat)
                    {
                        result = getEnharmonic(result);
                    }
                }
            }
            else
            {
                if (givenNote.accidental == Accidental.Flat)
                {
                    result = new Note(givenNote.name.Substring(0, 1));

                    if (tonicNote.accidental == Accidental.Sharp)
                    {
                        result = getEnharmonic(result);
                    }
                }
                else if (givenNote.accidental == Accidental.Sharp)
                {
                    result = new Note(generateNextLetter(givenNote.name));

                    if (tonicNote.accidental == Accidental.Flat)
                    {
                        result = getEnharmonic(result);
                    }
                }
            }

            return result;
        }

        public static Note getWholeStep(Note givenNote, [Optional] Note tonicNote )
        {
            Note result = null;

            if (givenNote.accidental == Accidental.Natural)
            {
                if (givenNote.name == "B" || givenNote.name == "E")
                {
                    result = new Note(generateNextLetter(givenNote.name) + "#");
                    if (tonicNote.accidental == Accidental.Flat)
                    {
                        result = getEnharmonic(result);
                    }
                }
                else
                {
                    result = new Note(generateNextLetter(givenNote.name));
                }
            }
            else
            {
                if (givenNote.accidental == Accidental.Flat)
                {

                    if (givenNote.name == "B" || givenNote.name == "E")
                    {
                        result = new Note(generateNextLetter(givenNote.name));
                    }
                    else
                    {
                        result = new Note(generateNextLetter(givenNote.name) + "b");
                    }

                    if (tonicNote.accidental == Accidental.Sharp)
                    {
                        result = getEnharmonic(result);
                    }
                }
                else if (givenNote.accidental == Accidental.Sharp)
                {

                    if (givenNote.name == "A" || givenNote.name == "E")
                    {
                        if (tonicNote.accidental == Accidental.Natural)
                        {
                            result = new Note("C");
                        }
                        else
                        {
                            result = new Note("B sharp");
                        }
                    }
                    result = new Note(generateNextLetter(givenNote.name));

                    if (tonicNote.accidental == Accidental.Flat)
                    {
                        result = getEnharmonic(result);
                    }
                }
            }

            return result;
        }

        public static Note getEnharmonic(Note tonicNote)
        {
            Note newNote = null;

            if (tonicNote.accidental == Accidental.Sharp)
            {
                if (tonicNote.name.StartsWith('B'))
                {
                    newNote = new Note("C");
                }
                else if (tonicNote.name.StartsWith('E'))
                {
                    newNote = new Note("F");
                }
                else
                {
                    newNote = new Note(generateNextLetter(tonicNote.name) + "flat");
                }
            }

            else if (tonicNote.name.EndsWith("♭"))
            {
                if (tonicNote.name.StartsWith('C'))
                {
                    newNote = new Note("B");
                }
                else if (tonicNote.name.StartsWith('F'))
                {
                    newNote = new Note("E");
                }
                else
                {
                    newNote = new Note(generatePreviousLetter(tonicNote.name) + "sharp");
                }
            }
            return newNote;
        }


        public static List<Note> Generate(Note tonicNote)
        {
            List<Note> noteNames = generateMajorScale(tonicNote);
            return noteNames;
        }

        public static List<String> printWholeSteps(List<Note> noteList)
        {
            List<String> result = new List<String>();

            foreach (Note item in noteList)
            {
                Note currentNote = getWholeStep(item, noteList[0]);
                result.Add(getFullName(currentNote));
            }
            return result;
        }

        public static List<String> printListOfNotes(List<Note> notes)
        {
            List<String> result = new List<String>();
            foreach (Note item in notes)
            {
                result.Add(getFullName(item));
            }
            return result;
        }
        public static List<Note> generateMajorScale(Note tonicNote)
        {



            List<Note> scale = new List<Note>();
            scale.Add(tonicNote);
            Note secondNote = getWholeStep(tonicNote, tonicNote);
            scale.Add(secondNote);
            Note thirdNote = getWholeStep(secondNote, tonicNote);
            scale.Add(thirdNote);
            Note fourthNote = getHalfStep(thirdNote, tonicNote);
            scale.Add(fourthNote);
            Note fifthNote = getWholeStep(fourthNote, tonicNote);
            scale.Add(fifthNote);
            Note sixthNote = getWholeStep(fifthNote, tonicNote);
            scale.Add(sixthNote);
            Note seventhNote = getWholeStep(sixthNote, tonicNote);
            scale.Add(seventhNote);


            return scale;
        }

        public static String generateNextLetter(String letter)
        {
            char character = letter.ToCharArray()[0];
            char nextChar;

            if (character == 'G')
                nextChar = 'A';
            else
                nextChar = (char)(((int)character) + 1);

            return Char.ToString(nextChar);
        }

        /// <summary>
        /// Returns the previous letter.
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public static String generatePreviousLetter(String letter)
        {
            char character = letter.ToCharArray()[0];
            char nextChar;

            if (character == 'A')
                nextChar = 'G';
            else
                nextChar = (char)(((int)character) - 1);

            return Char.ToString(nextChar);
        }
    }

    public enum Accidental
        {
            Sharp,
            Flat,
            Natural,
            DoubleSharp,
            DoubleFlat
        }

    public enum Degree
    {
        Tonic,
        Other
    }

        public class Note
        {
            public string name { get; set; }
            public Accidental accidental { get; set; }

            public Degree degree { get; set; }
            public Note(string Name)
            {
                name = Name;
                degree = Degree.Other;

                if (name.EndsWith('♭') || name.EndsWith('b') || name.EndsWith("flat") || name.EndsWith("Flat"))
                {
                    accidental = Accidental.Flat;
                    name = Name.Substring(0, 1);
                }

                else if (name.EndsWith('#') || name.EndsWith("sharp") || name.EndsWith("Sharp"))
                {
                    accidental = Accidental.Sharp;
                    name = Name.Substring(0, 1);

                }
                else if (name.EndsWith('X') | name.EndsWith('x') | name.EndsWith("DoubleSharp") | name.EndsWith("𝄪")) 
            {

                if ((!name.StartsWith('B')) | (!name.StartsWith('C')) | (!name.StartsWith('D')) | (!name.StartsWith('E')) | (!name.StartsWith('F')) | (!name.StartsWith('G')) | (!name.StartsWith('A')))
                {
                    throw new Exception("not a valid double sharp");
                }
                accidental = Accidental.DoubleSharp;
                name = Name.Substring(0, 1);
            }
            else if (name.EndsWith("𝄫") | name.EndsWith("double flat") | name.EndsWith("DoubleFlat") | name.EndsWith("Double Flat"))
            {

                if ((!name.StartsWith('E')) | (!name.StartsWith('F')) | (!name.StartsWith('D')) | (!name.StartsWith('G')) | (!name.StartsWith('A')) | (!name.StartsWith('B')) | (!name.StartsWith('C')))
                {
                    throw new Exception("not a valid double flat");
                }
                accidental = Accidental.DoubleFlat;
                name = Name.Substring(0, 1);
            }
            else
                {
                    accidental = Accidental.Natural;
                    name = Name.Substring(0, 1);

                }
            }
        public Note(String Name, Degree Degree)
        {
            name = Name;
            degree = Degree;


            if (name.EndsWith('♭') || name.EndsWith('b') || name.EndsWith("flat") || name.EndsWith("Flat"))
            {
                accidental = Accidental.Flat;
                name = Name.Substring(0, 1);
            }

            else if (name.EndsWith('#') || name.EndsWith("sharp") || name.EndsWith("Sharp"))
            {
                accidental = Accidental.Sharp;
                name = Name.Substring(0, 1);

            }
            else if (name.EndsWith('X') | name.EndsWith('x') | name.EndsWith("DoubleSharp") | name.EndsWith("𝄪"))
            {

                if ((!name.StartsWith('B')) | (!name.StartsWith('C')) | (!name.StartsWith('D')) | (!name.StartsWith('E')) | (!name.StartsWith('F')) | (!name.StartsWith('G')) | (!name.StartsWith('A')))
                {
                    throw new Exception("not a valid double sharp");
                }
                accidental = Accidental.DoubleSharp;
                name = Name.Substring(0, 1);
            }
            else if (name.EndsWith("𝄫") | name.EndsWith("double flat") | name.EndsWith("DoubleFlat") | name.EndsWith("Double Flat"))
            {

                if ((!name.StartsWith('E')) | (!name.StartsWith('F')) | (!name.StartsWith('D')) | (!name.StartsWith('G')) | (!name.StartsWith('A')) | (!name.StartsWith('B')) | (!name.StartsWith('C')))
                {
                    throw new Exception("not a valid double flat");
                }
                accidental = Accidental.DoubleFlat;
                name = Name.Substring(0, 1);
            }
            else
            {
                accidental = Accidental.Natural;
                name = Name.Substring(0, 1);

            }
        }

        





          




    }
}












