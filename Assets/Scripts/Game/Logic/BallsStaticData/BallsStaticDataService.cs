using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsStaticDataService : IBallsStaticDataService
{
    private Dictionary<BallType, BallStaticData> _balls;    
    
    public void LoadBalls()
    {
        _balls = Resources.LoadAll<BallStaticData>(AssetPath.BallsStaticDataPath)
            .ToDictionary(x => x.Type, x => x);
    }
    
    public BallStaticData ForBalls(BallType typeId) => 
        _balls.TryGetValue(typeId, out BallStaticData staticData) ? staticData : null;
}