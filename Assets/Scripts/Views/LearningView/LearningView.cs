using UnityEngine;

public abstract class LearningView : MonoBehaviour
{
    public void Activate() => gameObject.SetActive(true);
    public void Deactivate() => gameObject.SetActive(false);
}