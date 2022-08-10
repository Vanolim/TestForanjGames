using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextFileParser
{
    private LoadTextData _loadTextData;

    public TextFileParser(LoadTextData loadTextData)
    {
        _loadTextData = loadTextData;
    }
    
    public char[,] GetFileData()
    {
        List<List<char>> data = new List<List<char>>();


        var split_arr = SplitFileIntoLines();
        
        foreach (string line in split_arr)
        {
            List<char> charsLine = new List<char>();
        
            for (int i = 0; i < line.Length; i++)
            {
                if(line[i] != ';')
                    charsLine.Add(line[i]);
            }
            
            data.Add(charsLine);
        }
        
        return FormArrayData(data);
    }

    private string[] SplitFileIntoLines()
    {
        string[] split_arr = _loadTextData.LevelTextData.text.Split(';');
        return split_arr;
    }

    private char[,] FormArrayData( List<List<char>> data)
    {
        char[,] chars = new char[data.Count, data[0].Count];
        
        int countRows = chars.GetUpperBound(0) + 1;  
        int countColumns = chars.Length / countRows; 

        for (int i = 0; i < countRows; i++)
        {
            for (int j = 0; j < countColumns; j++)
            {
                chars[i, j] = data[i][j];
            }
        }

        return chars;
    }
}