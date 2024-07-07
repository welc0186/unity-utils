using UnityEngine;
using System;
using System.Collections;

namespace Alf.Utils
{

public class CoroutineTimer
{

    public Action Timeout;
    Coroutine _countdownCoroutine;

	private CoroutineTimer(){}

    public static CoroutineTimer Init(float seconds = 1)
    {
        var ret = new CoroutineTimer();
        var coroutine = Coroutines.StartCoroutine(ret.CountDown(seconds));
        ret._countdownCoroutine = coroutine;
        return ret;
    }

    public static CoroutineTimer Init(float seconds, bool workWhilePaused)
    {
        if(!workWhilePaused)
            return Init(seconds);

        var ret = new CoroutineTimer();
        var coroutine = Coroutines.StartCoroutine(ret.CountDownRealtime(seconds));
        ret._countdownCoroutine = coroutine;
        return ret;
    }

    public void Stop()
    {
        if (_countdownCoroutine != null)
            Coroutines.StopCoroutine(_countdownCoroutine);
    }

    IEnumerator CountDownRealtime(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Timeout?.Invoke();
    }

    IEnumerator CountDown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Timeout?.Invoke();
    }

}
}
