public class BestResult : IDisposable
{
    private readonly SavingAndLoadBestResult _savingAndLoadBestResult;
    private readonly Score _score;
    private readonly BestResultView _bestResultView;

    public BestResult(Score score, BestResultView bestResultView)
    {
        _savingAndLoadBestResult = new SavingAndLoadBestResult();
        
        _score = score;
        _score.OnScoreIncreased += CheckBestResultUpdate;

        _bestResultView = bestResultView;
        InitView();
    }

    private void InitView()
    {
        _bestResultView.ActivateView();
        _bestResultView.SetValue(GetBestResult());
    }

    private void CheckBestResultUpdate(int value)
    {
        if(GetBestResult() > value)
            return;
        
        _savingAndLoadBestResult.SetBestResult(value);
        _bestResultView.SetValue(value);
    }
    
    private int GetBestResult() => _savingAndLoadBestResult.TryGetBestResult();
    
    public void Dispose() => _score.OnScoreIncreased -= CheckBestResultUpdate;
}