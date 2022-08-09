using UnityEngine;

public class SavingAndLoadBestResult
{
    private const string BES_RESULT_KEY = "BestResult";
    
    public int TryGetBestResult() => PlayerPrefs.GetInt(BES_RESULT_KEY, 0);
    
    public void SetBestResult(int value) => PlayerPrefs.SetInt(BES_RESULT_KEY, value);
}