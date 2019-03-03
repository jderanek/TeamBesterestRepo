using System.Collections;
using System.Collections.Generic;
using System.Text;

public static class Scissors {
    public static string CamelToSentence(string input)
    {
        string output = "";

        string[] sentences = input.Split('.');

        bool first = true;

        foreach (string sentence in sentences)
        {
            if (sentence.Length == 0)
                continue;
            StringBuilder toAdd = new StringBuilder();
            toAdd.Append(System.Text.RegularExpressions.Regex.Replace(sentence, "([A-Z])", " $1").Trim().ToLower());
            toAdd[0] = char.ToUpper(toAdd[0]);
            toAdd.Append(".");

            if (!first)
                output += " ";
            first = false;

            output += toAdd.ToString();
        }

        return output;
    }

    public static string UnderScoresToSentence(string input)
    {
        return input.Replace('_', ' ');
    }
}
