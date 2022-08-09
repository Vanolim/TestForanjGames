using System;
using UnityEngine;
using UnityEngine.UI;

public class LearningViewUsual : LearningView, IMovedBack, IMovedNext
{
    [SerializeField] private Button _next;
    [SerializeField] private Button _back;

    public event Action<LearningView> OnBack;
    public event Action<LearningView> OnNext;
    
    private void OnEnable()
    {
        _back.onClick.AddListener(delegate { OnBack?.Invoke(this); });
        _next.onClick.AddListener(delegate { OnNext?.Invoke(this); });
    }

    private void OnDisable()
    {
        _back.onClick.RemoveListener(delegate { OnBack?.Invoke(this); });
        _next.onClick.RemoveListener(delegate { OnNext?.Invoke(this); });
    }
}