using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Net;

/*
Pseudo kod
[X] Välkomnande meddelande
[X] En lista för att spara historik för räkningar
[X] Användaren matar in tal och matematiska operation
[X] OBS! Användaren måsta mata in ett tal för att kunna ta sig vidare i programmet! // Ifall användaren skulle dela 0 med 0 visa Ogiltig inmatning!
[X] Lägga resultat till listan
[X] Visa resultat
[X] Fråga användaren om den vill visa tidigare resultat.
    Frågan ställs inte direkt, men alternativet finns i main menu.
[X] Visa tidigare resultat
[X] Fråga användaren om den vill avsluta eller fortsätta.
    Frågan ställs inte direkt, men alternativet finns i main menu.
 */

namespace Calc2
{

    internal class Program
    {
        //----Methods-----

        static string RunCalculation()
        {
            // This list is used to store the numbers and operators of the current calculation.
            List<string> currentCalculation = new();
            
            // Creating a string to store the entire calculation + result in.
            string resultString = "";

            // This bool will be be false until we get a valid calculation.                        
            bool validResult = false;

            // The program will keep asking for valid input if user enter something that is not valid.
            while (!validResult)
            {
                // Clear the list from previous calculation
                currentCalculation.Clear();                

                // Save the user input as an array of type char.
                char[] inputArray = GetUserInput();

                // Convert inputArray to a list of valid numbers and operators
                currentCalculation = InputToList(inputArray);

                // Call "Calculate" with the list of numbers and operators. Store the result in resultString.
                resultString = Calculate(currentCalculation);

                // If resulstring does not contain "Error", we add it to calcHistory and break out of this loop.
                if (!resultString.Contains("Error"))
                {
                    WriteWithColor(resultString, ConsoleColor.Gray);
                    
                    validResult = true;
                }
                // If resultString contain "Error", we run this loop again.
                else
                {
                    WriteWithColor(resultString, ConsoleColor.Red);
                    WriteWithColor("\nPress any key to try again...", ConsoleColor.Yellow);
                    Console.ReadKey();
                }
            }

            WriteWithColor("\n\nPress any key to continue..", ConsoleColor.Yellow);
            Console.ReadKey();
            return resultString;
            
        }//This method handles everything from user input up to the calculation.
        static string Calculate(List<string> stringList)
        {
            // List of numbers
            List<decimal> numList = new();

            // List of operators
            List<char> opList = new();

            decimal result;

            // Converts the numbers in stringList to doubles, and operators to chars
            foreach (string s in stringList)
            {

                try
                {
                    numList.Add(Convert.ToDecimal(s));
                }
                catch (FormatException)
                {
                    opList.Add(Convert.ToChar(s));
                }
                catch
                {
                    return "Error. Invalid input";
                }
            }

            // Return "Invalid input" if for some reason numList doesnt contain 1 more element than opList.
            if (numList.Count != opList.Count + 1) return "Error. Invalid input.";
            
            
            // This while loop checks for high priority operators ( * and /) and perform the calculations until no priority operator remains.
            // This is my way of implementing operator precedence            
            while((opList.Contains('*') || opList.Contains('/')) && opList.Count>1)
            {                
                // When we find a * or /, we will perform the calculation on the number to the left and right of the operator
                // and store it in calculated number
                decimal calculatedNumber;
                
                // Find index of prio operator(s)
                for (int i = 0; i < opList.Count; i++)
                {
                    //If i is *
                    if (opList[i] == '*')
                    {
                       //index of *
                        int index = i;
                        // Multiply the number to the left with the number to the right
                        calculatedNumber = numList[index] * numList[index+1];
                        // Store the new value at the left numbers index.
                        numList[index] = calculatedNumber;
                        // Remove the number to the right
                        numList.RemoveAt(index + 1);
                        // Remove the operator from the list
                        opList.RemoveAt(index);
                        break;
                    }
                    //If i is /
                    if (opList[i] == '/')
                    {
                        //index of '/'
                        int index = i;
                        // Check for division by zero
                        if (numList[index + 1] == 0) return "Error. Divide by zero attempted.";
                        // Divide the number to the left with the number to the right
                        calculatedNumber = numList[index] * numList[index + 1];
                        // Store the new value at the left numbers index.
                        numList[index] = calculatedNumber;
                        // Remove the number to the right
                        numList.RemoveAt(index + 1);
                        // Remove the operator from the list
                        opList.RemoveAt(index);
                        break;
                    }                    
                }
                
            }
            // Setting the result to the first number of numList
            result = numList[0];

            // Looping through the operators list and performing calculations based on operator
            for (int i = 0; i < opList.Count; i++)
            {
                switch (opList[i])
                {
                    case '+':
                        result += numList[i + 1];
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
            
            return FormatResult(stringList, result);


        }// This method does the calculations and returns the result as a string
        static void DisplayResultHistory(List<string> list)
        {
            Console.Clear();
            //If no calculation are med yet, let the user know that
            if (list.Count == 0)
            {
                Console.WriteLine("You did not make any calculations yet.");
            }
            //Print every calculation that was made.
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
        }// Displays  calculation history
        static void ExitProgram() // Shuts down the program and prints a thank you msg to the user
        {
            Console.Clear();
            WriteWithColor("Thank you for using MyCalc!", ConsoleColor.Yellow);
            WriteWithColor("Press any key to exit..", ConsoleColor.DarkGray);
            Console.ReadKey(true);
            Environment.Exit(0);

        }
        static string FormatResult(List<string> list, decimal result)// Formatting the result string
        {
            string resultString = "";
            // Add every number and operator from the list, separated with a space
            foreach (string str in list)
            {                
                resultString += str + " ";
            }
            // After everything in the list is added, end with "=" and the result.
            resultString += $"= {result}";

            return resultString;
        }
        static char[] GetUserInput()
        {
            Console.Clear();
            // Ask user for a problem to calculate
            Console.Write("Enter problem to calculate: ");
            string input = Console.ReadLine();
            
            //If for some reason the user inputs a null-value (like ctrl+z), an empty array is returned instead.
            if(input==null) return new char[] {};
            
            // Removing potential spaces and replacing dots with commas.
            input = input.Replace(" ", "").Replace(".", ",");

            char[] charArray = input.ToCharArray();

            return charArray;
        }// Takes input from the user and returns it as a char[]. Also removes possible space and replace dots with commas
        static void Instructions() // Displays instructions
        {
            Console.Clear();
            WriteWithColor("   Instructions:\n", ConsoleColor.White);
            Console.WriteLine("1. To calculate, enter your numbers and operator(s) on a single line and then press Enter.\n");
            Console.WriteLine("2. The calculator supports multiple numbers and operators.\n\n"+
                              "3. The calculator handles all the basic operators(+, -, *, /). It also handles negative\n" +
                              "   and decimal values.\n\n");
            WriteWithColor("   Press any key to contiune..", ConsoleColor.Yellow);
            Console.ReadKey(true);
        }
        static List<string> InputToList(char[] charArray)
        {
            // List to hold numbers and operators in the right order (N OP N OP N OP N etc)
            List<string> list = new List<string>();

            // List of valid numbers
            char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            // List of valid operators
            char[] op = { '+', '-', '*', '/' };

            // Since we are working with a char array we will build our numbers in this string variable
            string numString = "";

            // Now we will form a list of numbers and operators.
            foreach (char c in charArray)
            {
                // If numString is empty, it's possible to start the string with a '-' or '+'. (Negative/positive indicators)
                if (numString == string.Empty && (c == '-' || c == '+'))
                {
                    numString += c;
                }
                // If the string is NOT empty, we can add a comma IF there is not one already.
                else if (c == ',' && numString != string.Empty && !numString.Contains(","))
                {
                    numString += c;
                }
                // If c is a number, add it to num.
                else if (numbers.Contains(c))
                {
                    numString += c;
                }
                // If c is an operator, add current num to list, reset num back to empty, and add the operator to list.
                else if (op.Contains(c))
                {
                    // I added this if-statement to make sure empty strings wasn't being added to the list if user entered something like 4//4.
                    if (numString != String.Empty) list.Add(numString);

                    numString = string.Empty;
                    list.Add(c.ToString());
                }
                else
                // If something is wrong with the input, display this message. Also sets the list to empty and return.
                {
                    list.Clear();
                    return list;
                }
            }
            // Adds the last number to the list (unless it's empty)
            if (numString!= string.Empty) list.Add(numString);

            return list;
        }// This method takes the input as a char[] and converts it to a
         //list of numbers and operators        
        static void PrintMainMenu()
        {
            Console.WriteLine("|        Main menu.       |\n" +
                              "---------------------------\n" +
                              "Calculate          Press 1\n" +
                              "Show history       Press 2\n" +
                              "Instructions       Press 3\n" +
                              "\n" +
                              "----------------------------\n");
            WriteWithColor("Exit program       Press ESC", ConsoleColor.DarkGray);

        }// Prints the main menu
        static void Welcome()
        {
            WriteWithColor(@" ____      ____             ________                                                   ", ConsoleColor.Yellow);
            WriteWithColor(@"|    \    /    |           /        \         ___                            ", ConsoleColor.Yellow);
            WriteWithColor(@"|     \  /     |          /   ____   \       |   |                         ", ConsoleColor.Yellow);
            WriteWithColor(@"|      \/      |         |   /    \__| ______|   |______                                ", ConsoleColor.Yellow);
            WriteWithColor(@"|   |\    /|   |__    ___|   |        |___   |   |  ____|                                    ", ConsoleColor.Yellow);
            WriteWithColor(@"|   | \  / |   |  \  /  /|   |    ____ ___|  |   |  |                                     ", ConsoleColor.Yellow);
            WriteWithColor(@"|   |  \/  |   |\  \/  / |   \___/   ||      |   |  |                                   ", ConsoleColor.Yellow);
            WriteWithColor(@"|   |      |   | \    /   \          /|  X   |   |  |___                             ", ConsoleColor.Yellow);
            WriteWithColor(@"|___|      |___|  /  /     \________/ |______|___|______|                                                  ", ConsoleColor.Yellow);
            WriteWithColor(@"                 /  /                                                           ", ConsoleColor.Yellow);
            WriteWithColor(@"                /__/                                                                ", ConsoleColor.Yellow);
            WriteWithColor(@"", ConsoleColor.Yellow);

            WriteWithColor("Welcome to MyCalc!\nThis is a calculator made by: Oscar Sandberg.", ConsoleColor.White);
            WriteWithColor("Press any key to continue...", ConsoleColor.Yellow);

        }// Welcome message
        static void WriteWithColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;

        } // Writes a line with given color

        //------------------------------------------
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            // We will be saving every calculation as a string eg. 1 + 2 + 3 = 6
            List<string> calcHistory = new();
                        
            Welcome();
            Console.ReadKey(true);

            // The program will keep running until the user decides to exit the program.
            while (true)
            {
                Console.Clear();
                PrintMainMenu();
                // This var is used to keep track if the users menu choice
                var keyPress = Console.ReadKey(true);

                //Execute the selected menuchoice.
                switch (keyPress.Key)
                {
                    case ConsoleKey.D1:
                        calcHistory.Add(RunCalculation());
                        break;
                        
                    case ConsoleKey.D2:
                        DisplayResultHistory(calcHistory);
                        break;

                    case ConsoleKey.D3:
                        Instructions();
                        break;

                    case ConsoleKey.Escape:
                        ExitProgram();
                        break;
                }
            }
        }
    }
}