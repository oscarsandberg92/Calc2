using System.Collections.Generic;
using System;

namespace Calc2
{
    internal class Program
    {  
        static decimal Calculate(List<string> stringList)
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
                catch
                {
                    opList.Add(Convert.ToChar(s));
                }
            }

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
                        result /= numList[i + 1];
                        break;
                        default:
                        break;
                }
            }
            return result;


        }//This method does the calculations

        static List<string> InputToList(char[] charArray)
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
                    list.Add(numString);
                    numString = "";
                    list.Add(c.ToString());
                }
                else
                //If something is wrong with the input, display this message. Also sets the list to empty and return.
                {
                    Console.WriteLine("Error. Not valid input");
                    list.Clear();
                    return list;
                }
            }
            //Adds the last number to the list.
            list.Add(numString);

            return list;
        }//This method take the input as a char[] and converts it to a
         //list of numbers and operators
        static void Main(string[] args)
        {
            while (true)
            { 
            Console.Write("Enter problem to calculate: ");
            string input = Console.ReadLine();

            //Removing potential spaces and replacing commas with dots.
            input = input.Replace(" ", "").Replace(".", ",");

            char[] charArray = input.ToCharArray();
            string num = "";
            List<string> list = InputToList(charArray);

            Console.WriteLine(Calculate(list));
            }





            //------------------------------
            /*
            foreach (string s in list)
            {
                Console.WriteLine(s);
            }
            */
            /*
            foreach (string s in list)
            {
                try
                {
                    double number = Convert.ToDouble(s);
                    Console.WriteLine($" Added {number} to numList");
                }
                catch
                {
                    char ope = Convert.ToChar(s);
                    Console.WriteLine($"Added {ope} to opList");
                }
            }
            Console.ReadKey();
            */


        }
    }
}