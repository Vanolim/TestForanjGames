using System;
using UnityEngine;
using UnityEngine.UI;

public class AboutGameSceneUI : SceneContextUI
{
    [SerializeField] private Button _link;
    [SerializeField] private Button _main;

    public event Action OnChosenMain; 
    public event Action OnChosenLink; 

    private void OnEnable()
    {
        _main.onClick.AddListener(delegate { OnChosenMain?.Invoke(); });
        _link.onClick.AddListener(delegate { OnChosenLink?.Invoke(); });
    }

    private void OnDisable()
    {
        _main.onClick.RemoveListener(delegate { OnChosenMain?.Invoke(); });
        _link.onClick.RemoveListener(delegate { OnChosenLink?.Invoke(); });
    }
}