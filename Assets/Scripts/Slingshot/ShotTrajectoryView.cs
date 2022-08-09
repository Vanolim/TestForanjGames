using System.Collections.Generic;
using UnityEngine;

public class ShotTrajectoryView : MonoBehaviour
{
    [SerializeField] private LineRenderer _main;
    [SerializeField] private LineRenderer _left;
    [SerializeField] private LineRenderer _right;

    public bool MainIsHide { get; private set; }
    public bool AdditionalIsHide { get; private set; }

    public void SetTrajectoryMainLineRenderer(List<Vector2> positionPoints, int countPoint)
    {
        _main.positionCount = countPoint;
        for (int i = 0; i < _main.positionCount; i++)
        {
            _main.SetPosition(i, positionPoints[i]);
        }
    }

    public void SetAdditionalLeftTrajectory(List<Vector2> positionPointsLeftRenderer, int countPoint)
    {
        _left.positionCount = countPoint;
        for (int i = 0; i < _left.positionCount; i++)
        {
            _left.SetPosition(i, positionPointsLeftRenderer[i]);
        }
    }
    
    public void SetAdditionalRightTrajectory(List<Vector2> positionPointsRightRenderer, int countPoint)
    {
        _right.positionCount = countPoint;
        for (int i = 0; i < _right.positionCount; i++)
        {
            _right.SetPosition(i, positionPointsRightRenderer[i]);
        }
    }

    public void SeemMain()
    {
        _main.enabled = true;
        MainIsHide = false;
    }

    public void HideMain()
    {
        _main.enabled = false;
        MainIsHide = true;
    }

    public void SeemAdditional()
    {
        _left.enabled = true;
        _right.enabled = true;
        AdditionalIsHide = false;
    }
    
    public void HideAdditional()
    {
        _left.enabled = false;
        _right.enabled = false;
        AdditionalIsHide = true;
    }
}