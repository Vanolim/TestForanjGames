using UnityEngine;

public class SlingshotView : MonoBehaviour
{
    [SerializeField] private Band _band;
    [SerializeField] private Rigidbody2D _hook;
    [SerializeField] private CircleCollider2D _ballZone;
    [SerializeField] private ShotTrajectoryView _shotTrajectoryView;
    [SerializeField] private Border _border;
    

    public Band Band => _band;
    public Rigidbody2D Hook => _hook;
    public CircleCollider2D BallZone => _ballZone;
    public ShotTrajectoryView ShotView => _shotTrajectoryView;
    public Border Border => _border;
}