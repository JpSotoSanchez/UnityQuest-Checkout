using System;
using System.IO;
using System.Text;
using UnityEngine;

public class fileReader : MonoBehaviour
{
    struct savedData
    {
        bool available;
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateKits()
    {
        //FILES
        try
        {
            // Open the text file using a stream reader.
            using StreamReader reader = new("TestFile.txt");

            // Read the stream as a string.
            string text = reader.ReadToEnd();

            // Write the text to the console.
            Console.WriteLine(text);
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }


        //STRIGNS
        string str = "Some number of characters";
        char[] b = new char[str.Length];

        using (StringReader sr = new StringReader(str))
        {
            // Read 13 characters from the string into the array.
            sr.Read(b, 0, 13);
            Console.WriteLine(b);

            // Read the rest of the string starting at the current string position.
            // Put in the array starting at the 6th array member.
            sr.Read(b, 5, str.Length - 13);
            Console.WriteLine(b);
        }

        //CHARWRITER
        StringBuilder sb = new StringBuilder("Start with a string and add from ");
        char[] c = { 'c', 'h', 'a', 'r', '.', ' ', 'B', 'u', 't', ' ', 'n', 'o', 't', ' ', 'a', 'l', 'l' };

        using (StringWriter sw = new StringWriter(sb))
        {
            // Write five characters from the array into the StringBuilder.
            sw.Write(c, 0, 5);
            Console.WriteLine(sb);
        }
    }
}
