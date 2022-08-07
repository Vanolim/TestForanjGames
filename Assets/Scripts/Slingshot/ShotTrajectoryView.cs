using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShotTrajectoryView : MonoBehaviour
{
    [SerializeField] private LineRenderer _main;
    [SerializeField] private LineRenderer _leftAdditional;
    [SerializeField] private LineRenderer _rightAdditional;

    public bool MainIsHide { get; private set; }
    public bool AdditionalIsHide { get; private set; }

    public void SetCountPointAdditionalRenderers(int count)
    {
        _leftAdditional.positionCount = count;
        _rightAdditional.positionCount = count;
    }

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
        _leftAdditional.positionCount = countPoint;
        for (int i = 0; i < _leftAdditional.positionCount; i++)
        {
            _leftAdditional.SetPosition(i, positionPointsLeftRenderer[i]);
        }
    }
    
    public void SetAdditionalRightTrajectory(List<Vector2> positionPointsRightRenderer, int countPoint)
    {
        Debug.Log($"{positionPointsRightRenderer.Count} ___ {countPoint}");
        _rightAdditional.positionCount = countPoint;
        for (int i = 0; i < _rightAdditional.positionCount; i++)
        {
            _rightAdditional.SetPosition(i, positionPointsRightRenderer[i]);
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
        _leftAdditional.enabled = true;
        _rightAdditional.enabled = true;
        AdditionalIsHide = false;
    }
    
    public void HideAdditional()
    {
        _leftAdditional.enabled = false;
        _rightAdditional.enabled = false;
        AdditionalIsHide = true;
    }
}