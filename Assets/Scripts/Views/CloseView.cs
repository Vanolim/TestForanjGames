using System;
using UnityEngine;
using UnityEngine.UI;

public class CloseView : MonoBehaviour
{
    [SerializeField] private Button _exit;
    [SerializeField] private Button _closeView;

    public event Action OnExit; 

    public void ActivateView()
    {
        _exit.onClick.AddListener(delegate { OnExit?.Invoke();});
        _closeView.onClick.AddListener(DeactivateView);
        
        gameObject.SetActive(true);
    }

    public void DeactivateView()
    {
        _exit.onClick.RemoveListener(delegate { OnExit?.Invoke(); });
        _closeView.onClick.RemoveListener(DeactivateView);
        
        gameObject.SetActive(false);
    }
}