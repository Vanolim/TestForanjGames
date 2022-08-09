using System.Collections.Generic;
using UnityEngine;

public class ShotTrajectory
{
     private readonly ShotTrajectoryView _shotTrajectoryView;
     private readonly ParabolicTrajectory _parabolicTrajectory;
     private readonly SlingshotViewBall _slingshotViewBall;
     private float _divergenceAdditionalTrajectoryAngle;

     private const float MAXIMUM_LINE_TRAJECTORY_VIEW = 10f;

     public ShotTrajectory(ShotTrajectoryView view, Border border, SlingshotViewBall slingshotViewBall)
     {
          _shotTrajectoryView = view;
          _slingshotViewBall = slingshotViewBall;
          _parabolicTrajectory = new ParabolicTrajectory(border);
     }

     public List<Vector2> GetTrajectory(float force) => 
          _parabolicTrajectory.GetTrajectory(_slingshotViewBall.GetBallPosition, _slingshotViewBall.GetBallDirection(), force);
     
     public List<Vector2> GetTrajectory(float force, float maxLength) =>
          _parabolicTrajectory.GetTrajectory(_slingshotViewBall.GetBallPosition, _slingshotViewBall.GetBallDirection(), force, maxLength);
     
     public List<Vector2> GetTrajectory(Vector2 direction, float force, float maxLength) =>
          _parabolicTrajectory.GetTrajectory(_slingshotViewBall.GetBallPosition, direction, force, maxLength);

     public List<Vector2> GetRandomAngleTrajectory(float force)
     {
          float randomAngle = Random.Range(_divergenceAdditionalTrajectoryAngle * -1,
               _divergenceAdditionalTrajectoryAngle);
          
          return _parabolicTrajectory.GetTrajectory(_slingshotViewBall.GetBallPosition,
               _slingshotViewBall.GetBallDirectionAngle(randomAngle), force);
     }

     public void SetMainTrajectoryView(float force)
     {
          List<Vector2> trajectory = GetTrajectory(force, MAXIMUM_LINE_TRAJECTORY_VIEW);
          _shotTrajectoryView.SetTrajectoryMainLineRenderer(trajectory, trajectory.Count);
     }

     public void SetAdditionalTrajectoryView(float force)
     {
          List<Vector2> trajectory;
          trajectory = GetTrajectory(GetDirectionLeftAdditionalTrajectory(), force, MAXIMUM_LINE_TRAJECTORY_VIEW);
          _shotTrajectoryView.SetAdditionalLeftTrajectory(trajectory, trajectory.Count);

          trajectory = GetTrajectory(GetDirectionRightAdditionalTrajectory(), force, MAXIMUM_LINE_TRAJECTORY_VIEW);
          _shotTrajectoryView.SetAdditionalRightTrajectory(trajectory, trajectory.Count);
     }
     
     private Vector2 GetDirectionLeftAdditionalTrajectory() => 
          _slingshotViewBall.GetBallDirectionAngle(_divergenceAdditionalTrajectoryAngle);
     
     private Vector2 GetDirectionRightAdditionalTrajectory() => 
          _slingshotViewBall.GetBallDirectionAngle(_divergenceAdditionalTrajectoryAngle * -1);

     public void SeemMainTrajectoryView()
     {
          if(_shotTrajectoryView.MainIsHide)
               _shotTrajectoryView.SeemMain();
     }

     public void SeemAdditionalTrajectoryView()
     {
          if(_shotTrajectoryView.AdditionalIsHide)
               _shotTrajectoryView.SeemAdditional();
     }

     public void HideView()
     {
          HideMainTrajectoryView();
          HideAdditionalTrajectoryView();
     }

     public void HideAdditionalTrajectoryView() => _shotTrajectoryView.HideAdditional();
     public void HideMainTrajectoryView() => _shotTrajectoryView.HideMain();
     
     public void IncreaseDivergenceAngle(float dt)
     {
          int maxDivergenceAngle = 15;
          float divergenceRate = 10f;
        
          if (_divergenceAdditionalTrajectoryAngle < maxDivergenceAngle)
               _divergenceAdditionalTrajectoryAngle += dt * divergenceRate;
     }

     public void ResetIncreaseDivergenceAngle() => _divergenceAdditionalTrajectoryAngle = 0;
}