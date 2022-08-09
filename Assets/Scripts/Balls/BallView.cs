using TMPro;
using UnityEngine;

public class BallView : MonoBehaviour
{
    [SerializeField] private ShootButtonEvent _shootButtonEvent;
    [SerializeField] private TMP_Text _text;

    public ShootButtonEvent ShootButtonEvent => _shootButtonEvent;

    public void EnableViewText() => _text.enabled = true;

    public void DisableViewText() => _text.enabled = false;

    public void SetValueTest(string value) => _text.text = value;
}