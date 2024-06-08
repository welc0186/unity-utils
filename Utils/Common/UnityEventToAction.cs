using System;
using UnityEngine.Events;

namespace Alf.Utils
{
public class UnityEventToAction<T>
{
    public Action Action;

    public UnityEventToAction(UnityEvent<T> unityEvent)
    {
        unityEvent.AddListener(OnUnityActionInvoked);
    }

    private void OnUnityActionInvoked(T arg0)
    {
        Action?.Invoke();
    }

}
}