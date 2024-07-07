using UnityEngine;
using System;
using System.Collections;

namespace Alf.Utils
{

public class CoroutineTimer
{

    public Action Timeout;

	private CoroutineTimer(){}

    public static CoroutineTimer Init(float seconds = 1)
    {
        var ret = new CoroutineTimer();
        Coroutines.StartCoroutine(ret.CountDown(seconds));
        return ret;
    }

    public static CoroutineTimer Init(float seconds, bool workWhilePaused)
    {
        if(!workWhilePaused)
            return Init(seconds);

        var ret = new CoroutineTimer();
        Coroutines.StartCoroutine(ret.CountDownRealtime(seconds));
        return ret;
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
