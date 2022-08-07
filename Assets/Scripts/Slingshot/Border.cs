using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider;

    public BoxCollider2D Box => _boxCollider;
}