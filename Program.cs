using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Net;

namespace Calc2
{
    /*TODO:
     * Få kontroll över felmeddelande
     * Om historiken är tom, skriv ut tex "You didn't make any calculations yet!
     * Snygga till allt visuellt
     * Lägga till ett alternativ i main menu, "Info/Future improvements". Ge instruktion om bla att använda * & / innan + & -
     *
     *
     */
    internal class Program
    {  
        static void PrintMainMenu()
        {
            Console.WriteLine("Main menu.\n" +
                              "Calculate        Press 1\n" +
                              "Show history     Press 2\n" +
                              "\n" +
                              "Exit application Press ESC");
        }
        static string Calculate(List<string> stringList)
        {
            //List of numbers
            List<decimal> numList = new();

            //List of operators
            List<char> opList = new();            

            //Converts the numbers in stringList to doubles, and operators to chars
            foreach (string s in stringList)
            {
                try
                {
                    numList.Add(Convert.ToDecimal(s));
                }
                catch (FormatException)
                {
                    try
                    {
                        opList.Add(Convert.ToChar(s));
                    }
                    catch
                    {
                        return "Error, invalid input. please try again";
                    }
                }
            }

            //Return "Invalid input" if for some reason numList.Count == opList.Count + are not true.
            
            if (numList.Count != opList.Count + 1) return "Error,invalid input.";
            //Setting the result to the first number of numList
            decimal result = numList[0];
         
            //Looping through the operators list and performing calculations based on operator
            for (int i = 0;i<opList.Count;i++)
            {
                switch(opList[i])
                {
                    case '+': result += numList[i +1]; 
                        break;
                    case '-':
                        result -= numList[i + 1];
                        break;
                    case '*':
                        result *= numList[i + 1];
                        break;
                    case '/':
                        try
                        {
                            result /= numList[i + 1];
                        }
                        catch (DivideByZeroException)
                        {
                            return "Error. Divide by zero attempted.";
                        }
                        break;
                        default:
                        break;
                }
            }
            return ListToString(stringList, result);


        }//This method does the calculations
        static void DisplayResultHistory(List<string> list)
        {
            Console.Clear();
            if (list.Count == 0)
            {
                Console.WriteLine("You didn't make any calculations yet.");
            }
            else
            {
                Console.WriteLine("This is your recent history:\n");
                
                foreach (string s in list)
                {
                    WriteWithColor(s, ConsoleColor.Blue);
                }                
            }

            WriteWithColor("\n\nPress any key to return to the menu..", ConsoleColor.Yellow);
            Console.ReadKey(true);
        }
        static char[] GetUserInput()
        {           
            Console.Clear();
            //Ask user for a problem to calculate
            Console.Write("Enter problem to calculate: ");
            string input = Console.ReadLine();

            //Removing potential spaces and replacing dots with commas.
            input = input.Replace(" ", "").Replace(".", ",");

            char[] charArray = input.ToCharArray();

            return charArray;
        }
        static List<string> ArrayToList(char[] charArray)
        {
            //List to hold numbers and operators in the right order (N OP N OP N OP N etc)
            List<string> list = new List<string>();
            
            //List of valid 
            char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            
            //List of valid operators
            char[] op = { '+', '-', '*', '/' };
            
            //Since we are working with a char array we will build our numbers in this string variable
            string numString = "";
            
            //Now we will form a list of numbers and operators.
            foreach (char c in charArray)
            {
                //If string is empty, it's possible to start the string with a '-' or '+'. (Negative/positive indicators)
                if (numString == string.Empty && (c == '-' || c == '+'))
                {
                    numString += c;
                }
                //If the string is NOT empty, we can add a comma
                else if (c == ',' && numString != string.Empty)
                {
                    numString += c;
                }
                //If c is a number, add it to num.
                else if (numbers.Contains(c))
                {
                    numString += c;
                }
                //If c is an operator, add current num to list, reset num back to empty, and add the operator to list.
                else if (op.Contains(c))
                {
                    //I addes this if-statement to make sure empty strings wasnt being added to the list if user entered something like 4//4.
                    if(numString !=String.Empty)list.Add(numString);
                    
                    numString = string.Empty;
                    list.Add(c.ToString());
                }
                else
                //If something is wrong with the input, display this message. Also sets the list to empty and return.
                {
                    //Console.WriteLine("Error. Not valid input");
                    list.Clear();
                    return list;
                }
            }
            //Adds the last number to the list.
            list.Add(numString);

            return list;
        }//This method take the input as a char[] and converts it to a
         //list of numbers and operators
        static string ListToString(List<string> list, decimal result)//Formatting the result string
        {
            string resultString = "";
            foreach (string str in list)
            {
                resultString += str + " ";
            }
            
            resultString += $"= {result}";
            
            return resultString;
        }

        static void WriteWithColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;

        }
        static void Main(string[] args)
        {
            //We will be every calculation as a string eg. 1 + 2 + 3 = 6
            List<string> calcHistory = new();
            List<string> stringList = new();

            //The program will keep running until the user decides to exit the program.
            while (true)
            {
                Console.Clear();
                PrintMainMenu();
                var keyPress = Console.ReadKey(true);

                switch (keyPress.Key)
                {                    
                    case ConsoleKey.D1:
                        stringList.Clear();
                        //Creating a string to store the entire calcultion + result in.
                        string resultString = "";
                        //Save the user input as an array of typ char.
                        char[] chars = GetUserInput();
                        
                        //Convert to char array to a list with valid numbers and operators
                        stringList = ArrayToList(chars);
                        //Call "Calculate" with the list of numbers and operators. Store the result in result.
                        

                        resultString = Calculate(stringList);
                        
                        //Console.WriteLine(resultString);

                        if (!resultString.Contains("Error"))
                        {
                            WriteWithColor(resultString, ConsoleColor.Gray);
                            calcHistory.Add(resultString);
                        }
                        else
                        {
                            WriteWithColor(resultString, ConsoleColor.Red);
                        }

                        WriteWithColor("\n\nPress any key to continue..", ConsoleColor.Yellow);
                        Console.ReadKey();
                        break;
                        case ConsoleKey.D2: DisplayResultHistory(calcHistory);
                        break;
                    case ConsoleKey.Escape: Environment.Exit(0);
                        break;

                }                                               
            }
        }
    }
}