﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework; //test classes need to have the using statement

///     REDDIT DAILY PROGRAMMER SOLUTION TEMPLATE 
///                             http://www.reddit.com/r/dailyprogrammer
///     Your Name: Andrii Pelekh
///     Challenge Name:[Intermediate] Rail Fence Cipher
///     Challenge #: #196
///     Challenge URL: http://www.reddit.com/r/dailyprogrammer/comments/2rnwzf/20150107_challenge_196_intermediate_rail_fence/
///     Brief Description of Challenge:
///Program encrypts and decrypts an input string by breaking it into specified amount of strings using a zigzag method (example below "REDDITCOMRDAILYPROGRAMMER"). After that, it returns a single string that contains all concatenated encrypted strings (e.g RIMIRAREDTORALPORMEDCDYGM). Decryption happens by the same zig zag way. User can input different amount of string for zigzaging.
/// 
///R   I   M   I   R   A   R
/// E D T O R A L P O R M E
///  D   C   D   Y   G   M
///
///     What was difficult about this challenge?
/// 1)Figure out how to write a code that would be flexible to number of string for zigzaging.
/// 2)Figure out how to add values(but not remove) into elements in the list. I spent the most of time for it.
/// 3)Figure out the maximun flexibility of number of string for zigzaging (I did for only five and technically cound do 100, but it would be just copypasting. I cound't figure out how to make it like a functions with input parameter of number of string for zigzaging).
/// 4)Decripting part was much harder than I was expected.
/// 5)My first version had lots of copy paste parts, which I replaced later with only few lines by Dustin's advice.
///
///     
///
///     What was easier than expected about this challenge?
///It was easier than I expected to create an algorithm for encryption with static amount of strings for zigzaging. After that, I just used that algorithm's framework to make the initial algorithm more complex.
///     
///
///     BE SURE TO CREATE AT LEAST TWO TESTS FOR YOUR CODE IN THE TEST CLASS
///     One test for a valid entry, one test for an invalid entry.

namespace RailFenceCypher
{
    class Program
    {
        static void Main(string[] args)
        {
            RailFenceEnc test = new RailFenceEnc(3);
            string output = test.Encrypt("REddITCOMRDAILYPROGRAMMER");
            Console.WriteLine(output);
            RailFenceDecrypt test1 = new RailFenceDecrypt(3);
            string output1 = test1.Decrypt("RImiRAREDTORALPORMEDCDYGM");
            Console.WriteLine(output1);
            RailFenceEnc newRail = new RailFenceEnc(4);
            string text = newRail.Encrypt("THEQUIcKBrOWNFOXJUMPSOVERTHELAZYDOG");
            Console.WriteLine(text);
            RailFenceDecrypt newRailDec = new RailFenceDecrypt(4);
            string text2 = newRailDec.Decrypt("TCNMRzHIkWFUPETAYEUBOOJSVHLDGQRXOEO");
            Console.WriteLine(text2);


            Console.ReadKey();
        }

        /// <summary>
        /// Simple function to illustrate how to use tests
        /// </summary>
        /// <param name="inputInteger"></param>
        /// <returns></returns>
        public static int MyTestFunction(int inputInteger)
        {
            return inputInteger;
        }
    }

    /// <summary>
    /// Class that is responsible for encryption of a string
    /// </summary>
    class RailFenceEnc
    {
        //creating a list of strings that will hold encrypted letters. Strings are corresponding to rows for encryption
        private List<string> _listOfStrings = new List<string>();
        public List<string> ListOfStrings
        {
            get { return _listOfStrings; }
            set { _listOfStrings = value; }
        }

        //prorepty for the amount of rows for encryption
        private int _numberOfRows;
        public int NumberOfRows
        {
            get { return _numberOfRows; }
            set { _numberOfRows = value; }
        }
        /// <summary>
        /// Constructor that initializing the list of strings accourding to the amount of rows indicated
        /// </summary>
        /// <param name="numberOfRows">Number of rows for encryption</param>
        public RailFenceEnc(int numberOfRows)
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                this.ListOfStrings.Add(string.Empty);
            }
            this.NumberOfRows = numberOfRows;
        }
        /// <summary>
        /// Method that encrypts the input string
        /// </summary>
        /// <param name="inputString">String to be encrypted</param>
        /// <returns>Returns an encrypted string</returns>
        public string Encrypt(string inputString)
        {
            //keeping all captial letters in input string
            inputString = inputString.ToUpper();
            //decraring a counter that indicates the number of row during the main loop
            int counter = 0;
            //declaring a counter that indicates the number of row that we are at during the loop
            string direction = "up";
            //looping through each letter in the input string
            for (int i = 0; i < inputString.Length; i++)
            {
                //checking for direction
                if (direction == "up")
                {
                    //adding a letter to apropriate string in the list of strings according to counter value
                    this.ListOfStrings[counter] += inputString[i];
                    //checking if we reached the last row
                    if (counter == this.NumberOfRows - 1)
                    {
                        //if we reached the last row, then changing direction and starting decrementing the counter
                        direction = "down";
                        counter--;
                    }
                    else
                    {
                        //if it's not the last row, then just keed incrementing the counter
                        counter++;
                    }
                }
                //checking for direction
                else if (direction == "down")
                {
                    //adding a letter to apropriate string in the list of strings according to counter value
                    this.ListOfStrings[counter] += inputString[i];
                    //checking if we reached the first row
                    if (counter == 0)
                    {
                        //if we reached the first row, then changing direction and starting incrementing the counter
                        direction = "up";
                        counter++;
                    }
                    else
                    {
                        //if it's not the first row, then just keed decrementing the counter
                        counter--;
                    }
                }

            }
            //joining all strings from the list and returning a full encrypted string
            return string.Join("", this.ListOfStrings.Select(x => x));

            //OLD VERSION FOR RECORD
            //    //add letter to the the row of counter 1
            //    //check if direction is up, are we at the first row?
            //    //check if the direction is down, are we at the bottom row?
            //    //then increment depending
            //    //ListOfStrings[0] += "l";
            //for (int i = 0; i < inputString.Length; i++)
            //{
            //    //if (counter1 == _numberOfRows)
            //    //{
            //    //    counter2 = "down";
            //    //    counter1--;
            //    //}
            //    if (counter2 == "up")
            //    {
            //        if (counter1 == 1)
            //        {
            //            string storage = ListOfStrings[0] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(0);
            //            ListOfStrings.Insert(0, storage);
            //            counter1++;
            //        }
            //        else if (counter1 == 2)
            //        {
            //            string storage = ListOfStrings[1] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(1);
            //            ListOfStrings.Insert(1, storage);
            //            if (counter1 == this.NumberOfRows)
            //            {
            //                counter2 = "down";
            //                counter1--;
            //            }
            //            else
            //            {
            //                counter1++;
            //            }
            //        }
            //        else if (counter1 == 3)
            //        {
            //            string storage = ListOfStrings[2] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(2);
            //            ListOfStrings.Insert(2, storage);
            //            if (counter1 == this.NumberOfRows)
            //            {
            //                counter2 = "down";
            //                counter1--;
            //            }
            //            else
            //            {
            //                counter1++;
            //            }
            //        }
            //        else if (counter1 == 4)
            //        {
            //            string storage = ListOfStrings[3] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(3);
            //            ListOfStrings.Insert(3, storage);
            //            if (counter1 == this.NumberOfRows)
            //            {
            //                counter2 = "down";
            //                counter1--;
            //            }
            //            else
            //            {
            //                counter1++;
            //            }
            //        }
            //        else if (counter1 == 5)
            //        {
            //            string storage = ListOfStrings[4] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(4);
            //            ListOfStrings.Insert(4, storage);
            //            if (counter1 == this.NumberOfRows)
            //            {
            //                counter2 = "down";
            //                counter1--;
            //            }
            //            //else
            //            //{
            //            //    counter1++;
            //            //}
            //        }
            //    }
            //    else if (counter2 == "down")
            //    {
            //        if (counter1 == 4)
            //        {
            //            string storage = ListOfStrings[3] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(3);
            //            ListOfStrings.Insert(3, storage);
            //            counter1--;
            //        }
            //        else if (counter1 == 3)
            //        {
            //            string storage = ListOfStrings[2] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(2);
            //            ListOfStrings.Insert(2, storage);
            //            counter1--;
            //        }
            //        else if (counter1 == 2)
            //        {
            //            string storage = ListOfStrings[1] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(1);
            //            ListOfStrings.Insert(1, storage);
            //            counter1--;
            //        }
            //        else if (counter1 == 1)
            //        {
            //            string storage = ListOfStrings[0] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(0);
            //            ListOfStrings.Insert(0, storage);
            //            counter1++;
            //            counter2 = "up";
            //        }
            //    }
            //}
        }
    }

    public class RailFenceDecrypt
    {
        //creating a list of strings that will hold the length of encrypted strings
        private List<string> _listOfStrings = new List<string>();
        //creating a list of strings that will hold decrypted letters
        private List<string> _listForDecrypt = new List<string>();
        public List<string> ListOfStrings
        {
            get { return _listOfStrings; }
            set { _listOfStrings = value; }
        }
        public List<string> ListForDecrypt
        {
            get { return _listForDecrypt; }
            set { _listForDecrypt = value; }
        }

        //prorepty for the amount of rows for decryption
        private int _numberOfRows;
        public int NumberOfRows
        {
            get { return _numberOfRows; }
            set { _numberOfRows = value; }
        }
        /// <summary>
        /// Constructor that initializing the list of strings accourding to the amount of rows indicated
        /// </summary>
        /// <param name="numberOfRows">Number of rows for decryption</param>
        public RailFenceDecrypt(int numberOfRows)
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                this.ListOfStrings.Add(string.Empty);
            }
            this.NumberOfRows = numberOfRows;
        }

        /// <summary>
        /// Method that decrypts the input string
        /// </summary>
        /// <param name="inputString">String to be decrypted</param>
        /// <returns>Decrypted string</returns>
        public string Decrypt(string inputString)
        {
            //saving original input string before modifying it
            string originalInputString = inputString;
            //keeping all captial letters in input string
            inputString = inputString.ToUpper();
            //declaring a counter that indicates the number of row that we are at during the loop
            int counter = 0;
            //declaring a counter that indicates the direction that we are moving at during the main loop
            string direction = "up";
            //this loop is responsible for getting correct lengths of each string to be decrypted
            //looping through each letter in the input string
            for (int i = 0; i < inputString.Length; i++)
            {
                //checking for direction
                if (direction == "up")
                {
                    //adding a letter to apropriate string in the list of strings according to counter value
                    this.ListOfStrings[counter] += inputString[i];
                    //checking if we reached the last row
                    if (counter == this.NumberOfRows - 1)
                    {
                        //if we reached the last row, then changing direction and starting decrementing the counter
                        direction = "down";
                        counter--;
                    }
                    else
                    {
                        //if it's not the last row, then just keed incrementing the counter
                        counter++;
                    }
                }
                //checking for direction
                else if (direction == "down")
                {
                    //adding a letter to apropriate string in the list of strings according to counter value
                    this.ListOfStrings[counter] += inputString[i];
                    //checking if we reached the first row
                    if (counter == 0)
                    {
                        //if we reached the first row, then changing direction and starting incrementing the counter
                        direction = "up";
                        counter++;
                    }
                    else
                    {
                        //if it's not the first row, then just keed decrementing the counter
                        counter--;
                    }
                }
            }

            //going through each string from list of strings, getting the length of each and adding the same number of letters into respective string in my decryption list
            for (int i = 0; i < ListOfStrings.Count; i++)
            {
                this.ListForDecrypt.Add(inputString.Substring(0, ListOfStrings[i].Length));
                //removing letters from input string that were added into decryption list
                inputString = inputString.Replace(ListForDecrypt[i], "");
            }

            //declaring a string that will have a dectypted text
            string returnString = string.Empty;
            //reseting my counters for another loop
            counter = 0;
            direction = "up";
            //adding letters from my decryption list into return string until the length of return string is equal to the original input string
            while (returnString.Length != originalInputString.Length)
            {
                //checking for direction
                if (direction == "up")
                {
                    //adding a letter from apropriate string from the decryption list according to counter value
                    returnString += ListForDecrypt[counter][0].ToString();
                    //removing the letter that was added from decryption list
                    ListForDecrypt[counter] = ListForDecrypt[counter].Remove(0, 1);
                    //checking if we reached the last row
                    if (counter == this.NumberOfRows - 1)
                    {
                        //if we reached the last row, then changing direction and starting decrementing the counter
                        direction = "down";
                        counter--;
                    }
                    else
                    {
                        //if it's not the last row, then just keed incrementing the counter
                        counter++;
                    }
                }
                //checking for direction
                else if (direction == "down")
                {
                    //adding a letter to my return string from apropriate string from the decryption list according to counter value
                    returnString += ListForDecrypt[counter][0].ToString();
                    //removing the letter that was added from decryption list
                    ListForDecrypt[counter] = ListForDecrypt[counter].Remove(0, 1);
                    //checking if we reached the first row
                    if (counter == 0)
                    {
                        //if we reached the first row, then changing direction and starting incrementing the counter
                        direction = "up";
                        counter++;
                    }
                    else
                    {
                        //if it's not the first row, then just keed decrementing the counter
                        counter--;
                    }
                }
            }
            return returnString;

            //OLD VERSION FOR RECORD (Getting the right length of strings)
            //int counter1 = 1;
            //string counter2 = "up";
            //for (int i = 0; i < inputString.Length; i++)
            //{
            //    if (counter2 == "up")
            //    {
            //        if (counter1 == 1)
            //        {
            //            string storage = ListOfStrings[0] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(0);
            //            ListOfStrings.Insert(0, storage);
            //            counter1++;
            //        }
            //        else if (counter1 == 2)
            //        {
            //            string storage = ListOfStrings[1] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(1);
            //            ListOfStrings.Insert(1, storage);
            //            if (counter1 == this.NumberOfRows)
            //            {
            //                counter2 = "down";
            //                counter1--;
            //            }
            //            else
            //            {
            //                counter1++;
            //            }
            //        }
            //        else if (counter1 == 3)
            //        {
            //            string storage = ListOfStrings[2] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(2);
            //            ListOfStrings.Insert(2, storage);
            //            if (counter1 == this.NumberOfRows)
            //            {
            //                counter2 = "down";
            //                counter1--;
            //            }
            //            else
            //            {
            //                counter1++;
            //            }
            //        }
            //        else if (counter1 == 4)
            //        {
            //            string storage = ListOfStrings[3] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(3);
            //            ListOfStrings.Insert(3, storage);
            //            if (counter1 == this.NumberOfRows)
            //            {
            //                counter2 = "down";
            //                counter1--;
            //            }
            //            else
            //            {
            //                counter1++;
            //            }
            //        }
            //        else if (counter1 == 5)
            //        {
            //            string storage = ListOfStrings[4] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(4);
            //            ListOfStrings.Insert(4, storage);
            //            if (counter1 == this.NumberOfRows)
            //            {
            //                counter2 = "down";
            //                counter1--;
            //            }
            //            //else
            //            //{
            //            //    counter1++;
            //            //}
            //        }
            //    }
            //    else if (counter2 == "down")
            //    {
            //        if (counter1 == 4)
            //        {
            //            string storage = ListOfStrings[3] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(3);
            //            ListOfStrings.Insert(3, storage);
            //            counter1--;
            //        }
            //        else if (counter1 == 3)
            //        {
            //            string storage = ListOfStrings[2] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(2);
            //            ListOfStrings.Insert(2, storage);
            //            counter1--;
            //        }
            //        else if (counter1 == 2)
            //        {
            //            string storage = ListOfStrings[1] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(1);
            //            ListOfStrings.Insert(1, storage);
            //            counter1--;
            //        }
            //        else if (counter1 == 1)
            //        {
            //            string storage = ListOfStrings[0] + inputString[i].ToString();
            //            ListOfStrings.RemoveAt(0);
            //            ListOfStrings.Insert(0, storage);
            //            counter1++;
            //            counter2 = "up";
            //        }
            //    }
            //}

            //OLD VERSION FOR RECORD (Getting the decrypted word)
            //List<string> returnString = new List<string>();
            //int counter1Dec = 1;
            //string counter2Dec = "up";
            //while (returnString.Count != originalInputString.Length)
            //{
            //    if (counter2Dec == "up")
            //    {
            //        if (counter1Dec == 1)
            //        {
            //            returnString.Add(ListForDecrypt[0][0].ToString());
            //            ListForDecrypt[0] = ListForDecrypt[0].Remove(0, 1);
            //            counter1Dec++;
            //        }
            //        else if (counter1Dec == 2)
            //        {
            //            returnString.Add(ListForDecrypt[1][0].ToString());
            //            ListForDecrypt[1] = ListForDecrypt[1].Remove(0, 1);
            //            if (counter1Dec == this.NumberOfRows)
            //            {
            //                counter2Dec = "down";
            //                counter1Dec--;
            //            }
            //            else
            //            {
            //                counter1Dec++;
            //            }
            //        }
            //        else if (counter1Dec == 3)
            //        {
            //            returnString.Add(ListForDecrypt[2][0].ToString());
            //            ListForDecrypt[2] = ListForDecrypt[2].Remove(0, 1);
            //            if (counter1Dec == this.NumberOfRows)
            //            {
            //                counter2Dec = "down";
            //                counter1Dec--;
            //            }
            //            else
            //            {
            //                counter1Dec++;
            //            }
            //        }
            //        else if (counter1Dec == 4)
            //        {
            //            returnString.Add(ListForDecrypt[3][0].ToString());
            //            ListForDecrypt[3] = ListForDecrypt[3].Remove(0, 1);
            //            if (counter1Dec == this.NumberOfRows)
            //            {
            //                counter2Dec = "down";
            //                counter1Dec--;
            //            }
            //            else
            //            {
            //                counter1Dec++;
            //            }
            //        }
            //        else if (counter1Dec == 5)
            //        {
            //            returnString.Add(ListForDecrypt[4][0].ToString());
            //            ListForDecrypt[4] = ListForDecrypt[4].Remove(0, 1);
            //            if (counter1Dec == this.NumberOfRows)
            //            {
            //                counter2Dec = "down";
            //                counter1Dec--;
            //            }
            //        }
            //    }
            //    else if (counter2Dec == "down")
            //    {
            //        if (counter1Dec == 4)
            //        {
            //            returnString.Add(ListForDecrypt[3][0].ToString());
            //            ListForDecrypt[3] = ListForDecrypt[3].Remove(0, 1);
            //            counter1Dec--;
            //        }
            //        else if (counter1Dec == 3)
            //        {
            //            returnString.Add(ListForDecrypt[2][0].ToString());
            //            ListForDecrypt[2] = ListForDecrypt[2].Remove(0, 1);
            //            counter1Dec--;
            //        }
            //        else if (counter1Dec == 2)
            //        {
            //            returnString.Add(ListForDecrypt[1][0].ToString());
            //            ListForDecrypt[1] = ListForDecrypt[1].Remove(0, 1);
            //            counter1Dec--;
            //        }
            //        else if (counter1Dec == 1)
            //        {
            //            returnString.Add(ListForDecrypt[0][0].ToString());
            //            ListForDecrypt[0] = ListForDecrypt[0].Remove(0, 1);
            //            counter1Dec++;
            //            counter2Dec = "up";
            //        }
            //    }
            //}
            //return string.Join("", returnString.Select(x => x));
        }
    }

    //MY ORIGINAL ALGORITHM  
    /// <summary>
    /// I created this function just as initial algorithm for railfence encryption
    /// </summary>
    /// <param name="input">String to be encrypted</param>
    //public static void RailFenceEnc(string input)
    //{
    //    string one = string.Empty;
    //    string two = string.Empty;
    //    string three = string.Empty;
    //    int counter1 = 1;
    //    string counter2 = "up";
    //    for (int i = 0; i < input.Length; i++)
    //    {
    //        if (counter2 == "up")
    //        {
    //            if (counter1 == 1)
    //            {
    //                one = one + input[i];
    //                //counter1 = 2;
    //                counter1++;
    //            }
    //            else if (counter1 == 2)
    //            {
    //                two = two + input[i];
    //                //counter1 = 3;
    //                counter1++;
    //            }
    //            else if (counter1 == 3)
    //            {
    //                three = three + input[i];
    //                //counter1 = 2;
    //                counter1--;
    //                counter2 = "down";
    //            }
    //        }
    //        else if (counter2 == "down")
    //        {
    //            if (counter1 == 2)
    //            {
    //                two = two + input[i];
    //                //counter1 = 1;
    //                counter1--;
    //            }
    //            else if (counter1 == 1)
    //            {
    //                one = one + input[i];
    //                //counter1 = 2;
    //                counter1++;
    //                counter2 = "up";
    //            }
    //        }
    //    }
    //    Console.WriteLine(string.Join("", one, two, three));

    //}


    //class String
    //{
    //    private string _string;
    //    public string StringValue
    //    {
    //        get { return _string; }
    //        set { _string = value; }
    //    }
    //    public String()
    //    {
    //        _string = string.Empty;
    //    }
    //}

    #region " TEST CLASS "

    //We need to use a Data Annotation [ ] to declare that this class is a Test class
    [TestFixture]
    class Test
    {
        //Test classes are declared with a return type of void.  Test classes also need a data annotation to mark them as a Test function
        [Test]
        public void MyValidTest()
        {
            //inside of the test, we can declare any variables that we'll need to test.  Typically, we will reference a function in your main program to test.
            int result = Program.MyTestFunction(15);  // this function should return 15 if it is working correctly
            //now we test for the result.
            Assert.IsTrue(result == 15, "This is the message that displays if it does not pass");
            // The format is:
            // Assert.IsTrue(some boolean condition, "failure message");
        }

        [Test]
        public void MyInvalidTest()
        {
            int result = Program.MyTestFunction(15);
            Assert.IsFalse(result == 14);
        }

        [Test]
        public void ValidTest()
        {
            RailFenceEnc ite = new RailFenceEnc(3);
            Assert.IsTrue(ite.Encrypt("REDDITCOMRDAILYPROGRAMMER") == "RIMIRAREDTORALPORMEDCDYGM");
            RailFenceDecrypt iteTwo = new RailFenceDecrypt(4);
            Assert.IsTrue(iteTwo.Decrypt("TCNMRZHIKWFUPETAYEUBOOJSVHLDGQRXOEO") == "THEQUICKBROWNFOXJUMPSOVERTHELAZYDOG");
        }
        [Test]
        public void InvalidTest()
        {
            RailFenceEnc ite = new RailFenceEnc(3);
            Assert.IsFalse(ite.Encrypt("REDDITCOMRDAILYPROGRAMMER") == "REDDITCOMRDAILYPROGRAMMER");
        }

    }
    #endregion
}
