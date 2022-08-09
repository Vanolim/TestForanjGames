using TMPro;
using UnityEngine;

public class BestResultView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void ActivateView() => gameObject.SetActive(true);
    public void DeactivateView() => gameObject.SetActive(false);

    public void SetValue(int value) => _text.text = "Лучший результат: " + value.ToString();
}