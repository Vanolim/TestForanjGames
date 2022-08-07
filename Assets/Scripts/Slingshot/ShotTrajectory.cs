using System.Collections.Generic;
using UnityEngine;

public class ShotTrajectory
{
     private readonly ShotTrajectoryView _shotTrajectory;
     private readonly ParabolicTrajectory _parabolicTrajectory;
     private List<Vector2> _trajectory;

     private const float MAXIMUM_LINE_TRAJECTORY_VIEW = 8f;

     public IReadOnlyList<Vector2> Trajectory => _trajectory;

     public ShotTrajectory(ShotTrajectoryView view, Border border)
     {
          _shotTrajectory = view;
          _parabolicTrajectory = new ParabolicTrajectory(border);
     }

     public void SetTrajectory(Vector2 ballPosition, Vector2 direction, float force)
     {
          _trajectory = _parabolicTrajectory.GetTrajectory(ballPosition, direction, force, MAXIMUM_LINE_TRAJECTORY_VIEW);

          if (_parabolicTrajectory.LineLength >= MAXIMUM_LINE_TRAJECTORY_VIEW)
          {
               int numberLastPointConsideredLineLenght = _parabolicTrajectory.NumberLastPointConsideredLineLenght;
               var trajectoryView = _trajectory.GetRange(0, numberLastPointConsideredLineLenght);
               SetTrajectoryView(trajectoryView, numberLastPointConsideredLineLenght);
               return;
          }
          SetTrajectoryView(_trajectory, _trajectory.Count);
     }

     public void SetAdditionalTrajectory(Vector2 ballPosition, Vector2 directionLeft, Vector2 directionRight, float force)
     {
          Vector2 vec1;
          Vector2 vec2;
          _trajectory = _parabolicTrajectory.GetTrajectory(ballPosition, directionLeft, force, MAXIMUM_LINE_TRAJECTORY_VIEW);
          vec1 = _trajectory[0];
          if (_parabolicTrajectory.LineLength >= MAXIMUM_LINE_TRAJECTORY_VIEW)
          {
               _trajectory = _trajectory.GetRange(0, _parabolicTrajectory.NumberLastPointConsideredLineLenght);
               _shotTrajectory.SetAdditionalLeftTrajectory(_trajectory, _parabolicTrajectory.NumberLastPointConsideredLineLenght);
          }
          else
          {
               _shotTrajectory.SetAdditionalLeftTrajectory(_trajectory, _trajectory.Count);
          }

          _trajectory = _parabolicTrajectory.GetTrajectory(ballPosition, directionRight, force, MAXIMUM_LINE_TRAJECTORY_VIEW);
          vec2 = _trajectory[0];
          if (_parabolicTrajectory.LineLength >= MAXIMUM_LINE_TRAJECTORY_VIEW)
          {
               _trajectory = _trajectory.GetRange(0, _parabolicTrajectory.NumberLastPointConsideredLineLenght);
               _shotTrajectory.SetAdditionalRightTrajectory(_trajectory, _parabolicTrajectory.NumberLastPointConsideredLineLenght);
          }
          else
          {
               _shotTrajectory.SetAdditionalRightTrajectory(_trajectory, _trajectory.Count);
          }
     }

     public void SeemView()
     {
          if(_shotTrajectory.MainIsHide)
               _shotTrajectory.SeemMain();
     }

     public void HideView() => _shotTrajectory.HideMain();

     private void SetTrajectoryView(List<Vector2> trajectory, int trajectoryCountDataPoint) => 
          _shotTrajectory.SetTrajectoryMainLineRenderer(trajectory, trajectoryCountDataPoint);

     public void SeemAdditionalTrajectoryView()
     {
          if(_shotTrajectory.AdditionalIsHide)
               _shotTrajectory.SeemAdditional();
     }

     public void HideAdditionalTrajectoryView() => _shotTrajectory.HideAdditional();
}