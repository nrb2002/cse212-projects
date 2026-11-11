// Importing namespace

/** This tells C# that we want to use classes and methods available in the System namespace */

using System;
using System.Collections.Generic;

// Class declaration

class D
{
    // This class has 3 methods: Main, R, and C

    // The Main is the entry point of the program; it executes the other two methods and prints a sorted list of 5 random numbers, and their total. 
    stating void Main()
    {
        int[] d = R(5);
        Array.Sort(d);
        Console.WriteLine("Values: " + string.Join(", ", d));

        int s = C(d);
        Console.WriteLine("Total: " +s);
    }

    /* This function receives a number and returns an array of 
    random numbers with same amount of index as the numbers received. 
    static int[] R(int n) */
    {
        Random r = new Random(); // create an instance of the Random class to generate random numbers

        int[] d = new int[n]; // create an array of integers d
        // d = [0, 0, 0, 0, 0]

        // Generate and store random numbers from 1 to 6 in d. 
        for (int i = 0; i < n; i++)
        {
            d[i] = r.Next(1, 7);
        }

        return d; // return generated array d so it can be used elsewhere in the class
    }

    /*
        This method receives the array d, counts the occurrences of numbers, and calculates the final score.    
        
        Finally, it returns an integer. 
    */
    static int C(int[] d)
    {
        int s = 0; // Initialize the score

        Dictionary<int, int> c = new Dictionary<int, int>(); // create an empty dictionary

        // Iterate through array d
        foreach (int x in d)
        {
            // check if the number exists at the current index
            if (c.ContainsKey(x))
            {
                c[x]++; // if the number exists, increment it by 1
            }
            else
            {
                c[x] = 1; // if the number does not exist create a new key and assign 1 as its value.
            }
        }

        // This loop examines how many times each number appears in the dictionary and awards points according to its frequency.
        foreach (int v in c.Values)
        {
            switch (v)
            {
                case 2:
                    s += 10;
                    break;
                case 3:
                    s += 20;
                    break;
                case 4:
                    s += 30;
                    break;
                case 5:
                    s += 40;
                    break;
            }
        }
        return s;
    }
}