using Random = UnityEngine.Random;

namespace Alf.Utils
{
public class RandomTimer
{
    
    float _minSeconds;
    float _maxSeconds;
    float _secondsElapsed;
    float _timerSeconds;

    public RandomTimer(float minSeconds, float maxSeconds)
    {
        _minSeconds = minSeconds;
        _maxSeconds = maxSeconds;
        _secondsElapsed = 0;
        SetTimer();
    }

    private void SetTimer()
    {
        _timerSeconds = Random.Range(_minSeconds, _maxSeconds);
    }

    public bool Elapsed(float deltaTime)
    {
        _secondsElapsed += deltaTime;
        
        if(_secondsElapsed < _timerSeconds)
            return false;
        
        _secondsElapsed = 0;
        SetTimer();
        return true;
    }
}
}
