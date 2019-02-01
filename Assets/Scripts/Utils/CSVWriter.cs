using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CSVWriter : MonoBehaviour
{
    public string[] firstRowData;
    public string filenameNoExtension;

    public void AddNewEntry(string[] values)
    {
        if (firstRowData.Length != values.Length) {
            Debug.LogError("trying to write a row with a different column number than the first row! Should have "
                           + firstRowData.Length + " columns but has " + values.Length);
        }

        StreamWriter outStream = null;
            
        //Check if file exists, if it doesn't create it and add first line
        if (!System.IO.File.Exists(GetPath())) {
            outStream = CreateCSVFile();
        } else {
            //Else open the existing file and add the new entry
            outStream = new StreamWriter(GetPath(),true);
        }

        string line = CreateLineEntry(values);
        
        outStream.WriteLine(line);
        outStream.Close();
    }

    private string CreateLineEntry(string[] entry) {
        string delimiter = ",";
        string line = "";
        for (int index = 0; index < entry.Length; index++)
            line += delimiter + entry[index];

        return line;
    }

    private StreamWriter CreateCSVFile() {
        StreamWriter outStream = System.IO.File.CreateText(GetPath());

        string line = CreateLineEntry(firstRowData);
        outStream.WriteLine(line);

        return outStream;
    }

    // Following method is used to retrive the relative path as device platform
    public string GetPath()
    {
#if UNITY_EDITOR
        return Application.dataPath+"/" + filenameNoExtension + ".csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath + filenameNoExtension + ".csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+ filenameNoExtension + ".csv";
#else
        return Application.dataPath +"/"+ filenameNoExtension + ".csv";
#endif
    }
}