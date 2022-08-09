using System;
using UnityEngine;
using UnityEngine.UI;

public class LearningViewLast : LearningView, IMovedBack
{
    [SerializeField] private Button _back;
    
    public event Action<LearningView> OnBack;
    
    private void OnEnable()
    {
        _back.onClick.AddListener(delegate { OnBack?.Invoke(this); });
    }

    private void OnDisable()
    {
        _back.onClick.RemoveListener(delegate { OnBack?.Invoke(this); });
    }
}