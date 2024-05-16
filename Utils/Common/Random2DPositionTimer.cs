using UnityEngine;
using Random = UnityEngine.Random;

namespace Alf.Utils
{

public class Random2DPositionTimer
{
    
    RandomTimer _timer;
    Bounds _area;

    public Random2DPositionTimer(float minSeconds, float maxSeconds, Bounds area)
    {
        _timer = new RandomTimer(minSeconds, maxSeconds);
        _area = area;
    }

    public bool GetPosition(ref Vector3 position, float deltaTime)
    {
        if(!_timer.Elapsed(deltaTime))
            return false;
        
        position = RandomPosition();
        return true;
    }

    private Vector3 RandomPosition()
    {
        var x = Random.Range(_area.min.x, _area.max.x);
        var y = Random.Range(_area.min.y, _area.max.y);
        return new Vector3(x, y, 0);
    }
}
}
