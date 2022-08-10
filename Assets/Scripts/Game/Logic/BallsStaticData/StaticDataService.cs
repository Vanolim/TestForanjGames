using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private Dictionary<BallType, BallStaticData> _balls;
    private SlingshotView _slingshotView;
    private LoadTextData _loadTextData;
    
    public void LoadBalls() =>
        _balls = Resources.LoadAll<BallStaticData>(AssetPath.BallsStaticDataPath)
            .ToDictionary(x => x.Type, x => x);
    
    public void LoadTextData() =>
        _loadTextData = Resources.Load<LoadTextData>(AssetPath.FileData);


    public BallStaticData ForBalls(BallType typeId) => 
        _balls.TryGetValue(typeId, out BallStaticData staticData) ? staticData : null;
    

    public SlingshotView ForSlingshotView() => _slingshotView;
    public LoadTextData ForLoadTextData() => _loadTextData;
}