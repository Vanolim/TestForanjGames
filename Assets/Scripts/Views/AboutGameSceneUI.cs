using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutGameSceneUI : SceneContextUI
{
    [SerializeField] private Button _link;
    [SerializeField] private Button _main;
    [SerializeField] private List<LearningView> _learningViews;

    public event Action OnChosenMain; 
    public event Action OnChosenLink; 

    private void OnEnable()
    {
        _main.onClick.AddListener(delegate { OnChosenMain?.Invoke(); });
        _link.onClick.AddListener(delegate { OnChosenLink?.Invoke(); });

        SubscribeLearningViews();
        ActivateFirstLearningView();
    }

    private void ActivateFirstLearningView()
    {
        _learningViews[0].Activate();
    }

    private void OnDisable()
    {
        _main.onClick.RemoveListener(delegate { OnChosenMain?.Invoke(); });
        _link.onClick.RemoveListener(delegate { OnChosenLink?.Invoke(); });

        UnsubscribeLearningViews();
    }

    private void SubscribeLearningViews()
    {
        foreach (var learningView in _learningViews)
        {
            if (learningView.TryGetComponent(out IMovedNext movedNext))
                movedNext.OnNext += SetNextLearningView;

            if (learningView.TryGetComponent(out IMovedBack movedBack))
                movedBack.OnBack += SetBackLearningView;
        }
    }

    private void SetNextLearningView(LearningView currentLearningView)
    {
        currentLearningView.Deactivate();
        int indexCurrentView = _learningViews.IndexOf(currentLearningView);
        _learningViews[indexCurrentView + 1].Activate();
    }
    
    private void SetBackLearningView(LearningView currentLearningView)
    {
        currentLearningView.Deactivate();
        int indexCurrentView = _learningViews.IndexOf(currentLearningView);
        _learningViews[indexCurrentView - 1].Activate();
    }

    private void UnsubscribeLearningViews()
    {
        foreach (var learningView in _learningViews)
        {
            if (learningView.TryGetComponent(out IMovedNext movedNext))
                movedNext.OnNext -= SetNextLearningView;

            if (learningView.TryGetComponent(out IMovedBack movedBack))
                movedBack.OnBack -= SetBackLearningView;
        }
    }
}