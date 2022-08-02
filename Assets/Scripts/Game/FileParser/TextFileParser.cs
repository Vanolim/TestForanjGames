using System.Collections.Generic;
using System.IO;

public class TextFileParser
{
    public char[,] GetFileData()
    {
        List<List<char>> data = new List<List<char>>();
        
        foreach (string line in File.ReadLines(AssetPath.FileData))
        {
            List<char> charsLine = new List<char>();

            for (int i = 0; i < line.Length; i++)
            {
                charsLine.Add(line[i]);
            }
            
            data.Add(charsLine);
        }

        return FormArrayData(data);
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