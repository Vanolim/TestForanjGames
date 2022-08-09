using System;
using UnityEngine;
using UnityEngine.UI;

public class LearningViewFirst : LearningView, IMovedNext
{
    [SerializeField] private Button _next;
    
    public event Action<LearningView> OnNext;

    private void OnEnable()
    {
        _next.onClick.AddListener(delegate { OnNext?.Invoke(this); });
    }

    private void OnDisable()
    {
        _next.onClick.RemoveListener(delegate { OnNext?.Invoke(this); });
    }
}