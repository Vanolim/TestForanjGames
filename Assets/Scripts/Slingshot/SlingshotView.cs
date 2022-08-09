using UnityEngine;

public class SlingshotView : MonoBehaviour
{
    [SerializeField] private BandView _bandView;
    [SerializeField] private Transform _hook;
    [SerializeField] private CircleCollider2D _ballZone;
    [SerializeField] private ShotTrajectoryView _shotTrajectoryView;
    [SerializeField] private Border _border;
    

    public BandView BandView => _bandView;
    public Transform Hook => _hook;
    public CircleCollider2D BallZone => _ballZone;
    public ShotTrajectoryView ShotView => _shotTrajectoryView;
    public Border Border => _border;
}