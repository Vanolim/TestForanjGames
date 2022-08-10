using System;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : SceneContextUI
{
    [SerializeField] private BestResultView _bestResultView;
    [SerializeField] private LoseView _loseView;
    [SerializeField] private WinView _winView;
    [SerializeField] private Button _main;

    public BestResultView BestResultView => _bestResultView;

    public event Action OnChosenMain;

    private void OnEnable() => _main.onClick.AddListener(delegate { OnChosenMain?.Invoke(); });
    
    private void OnDisable() => _main.onClick.RemoveListener(delegate { OnChosenMain?.Invoke(); });

    public void ActivateLoseView() => _loseView.gameObject.SetActive(true);

    public void ActivateWinView() => _winView.gameObject.SetActive(true);
}