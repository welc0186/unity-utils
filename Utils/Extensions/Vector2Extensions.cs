using UnityEngine;
using Random = UnityEngine.Random;

namespace Alf.Utils
{
public static class Vector2Extensions
{
    public static Vector2 rotate(this Vector2 v, float delta) {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    public static Vector2 RandOffset(this Vector2 v, float minOffset, float maxOffset)
    {
        var offsetDir = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        );
        offsetDir.Normalize();
        var offsetMag = Random.Range(minOffset, maxOffset);
        return v + offsetDir*offsetMag;
    }

    public static Vector2Int ToVector2Int(this Vector2 v)
    {
        int x = Mathf.RoundToInt(v.x);
        int y = Mathf.RoundToInt(v.y);
        return new Vector2Int(x,y);
    }
}
}