using System.IO;
using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Container for misc methods that can be usefull on any place in the application.
/// </summary>
public static class AmcUtilities
{
    /// <summary>
    /// Reads an (configuration?) file and searches its content for a match
    /// </summary>
    /// <param name="identifier">identifier to search the file for</param>
    /// <param name="path">relative path to the file</param>
    /// <returns></returns>
    public static string ReadFileItem(string identifier, string path)
    {
        StreamReader reader = new StreamReader(path);
        string line;
        
        //Read the file line by line
        while ((line = reader.ReadLine()) != null)
        {                   
            //split the line between identifier[0] and value[1]
            string[] value = line.Split('=');      
            if (value[0] == identifier)
            {
                reader.Close();
                //only the value needs to be returned
                return value[1];
            }            
        }
        reader.Close();
        return null;
    }
}