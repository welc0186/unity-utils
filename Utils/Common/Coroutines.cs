using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alf.Utils
{

public static class Coroutines
{
    private static MonoBehaviour s_CoroutineRunner;

    public static bool IsInitialized => s_CoroutineRunner != null;

    public static void SetRunner(MonoBehaviour runner)
    {
        s_CoroutineRunner = runner;
    }

    public static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        if (s_CoroutineRunner == null)
        {
            s_CoroutineRunner = new GameObject("Coroutine runner", typeof(CoroutineRunner)).GetComponent<CoroutineRunner>();
            s_CoroutineRunner.GetComponent<CoroutineRunner>().MyCoroutines = new List<Coroutine>();
        }

        var ret = s_CoroutineRunner.StartCoroutine(coroutine);
        s_CoroutineRunner.GetComponent<CoroutineRunner>().MyCoroutines.Add(ret);
        return ret;
    }

    public static void StopCoroutine(Coroutine coroutine)
    {
        if (s_CoroutineRunner != null && coroutine != null)
        {
            s_CoroutineRunner.StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    public static void StopCoroutine(ref Coroutine coroutine)
    {
        if (s_CoroutineRunner != null && coroutine != null)
        {
            s_CoroutineRunner.StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}

public class CoroutineRunner : MonoBehaviour
{

    public List<Coroutine> MyCoroutines;
    
    void OnDestroy()
    {
        foreach(var coroutine in MyCoroutines)
        {
            if (coroutine != null)
                Coroutines.StopCoroutine(coroutine);
        }
    }

}
}

