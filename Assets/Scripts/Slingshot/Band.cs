using UnityEngine;

public class Band : MonoBehaviour
{
    [SerializeField] private Transform _leftArchor;
    [SerializeField] private Transform _rightArchor;
    [SerializeField] private LineRenderer _band;

    private readonly Vector3[] _positions = new Vector3[COUNT_POSITION_POINT];

    private const int COUNT_POSITION_POINT = 3;
    private const int SET_POINT_NUMBER = 2;

    public void Init()
    {
        _band.positionCount = COUNT_POSITION_POINT;
        
        _positions[0] = _leftArchor.position;
        _positions[SET_POINT_NUMBER - 1] = Vector2.zero;
        _positions[2] = _rightArchor.position;
    }

    public void SetPosition(Vector2 position)
    {
        _positions[SET_POINT_NUMBER - 1] = position;
       _band.SetPositions(_positions);
    }

    public void Activate() => _band.enabled = true;

    public void Deactivate() => _band.enabled = false;
}
