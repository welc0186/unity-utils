using UnityEngine;

public static class Vector3Extenstions
{
    public static Vector2 ToVector2(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static Vector3 ZeroZ(this Vector3 v)
    {
        return new Vector3(v.x, v.y, 0);
    }

    public static float DistanceTo(this Vector3 v1, Vector3 v2)
    {
        return (v2 - v1).magnitude;
    }
}
