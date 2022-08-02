using System;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : SceneContextUI
{
    [SerializeField] private Button _newGame; 
    [SerializeField] private Button _aboutGame;
    [SerializeField] private Button _exit;
    [SerializeField] private CloseView _closeView;

    public event Action OnChosenExit;
    public event Action OnChosenNewGame;
    public event Action OnChosenAboutGame;

    private void OnEnable()
    {
        _newGame.onClick.AddListener(delegate { OnChosenNewGame?.Invoke();});
        _aboutGame.onClick.AddListener(delegate { OnChosenAboutGame?.Invoke();});
        _exit.onClick.AddListener(delegate { _closeView.ActivateView(); });
    }

    private void Exit()
    {
        _closeView.DeactivateView();
        OnChosenExit?.Invoke();
    }

    private void OnDisable()
    {
        _newGame.onClick.RemoveListener(delegate { OnChosenNewGame?.Invoke();});
        _aboutGame.onClick.RemoveListener(delegate { OnChosenAboutGame?.Invoke();});
        _exit.onClick.RemoveListener(delegate { _closeView.ActivateView(); });
        
        _closeView.OnExit -= Exit;
    }
}