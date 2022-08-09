using UnityEditor;
using UnityEngine;

public class ClearBestResult
{
    [MenuItem("Tools/Clear prefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}