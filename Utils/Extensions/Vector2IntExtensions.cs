using UnityEngine;
using System.Collections.Generic;

public static class Vector2IntExtensions
{
    public static Vector2Int sign(this Vector2Int v)
    {
        var ret = Vector2Int.zero;
        if(v.x > 0)
            ret.x = 1;
        if(v.x < 0)
            ret.x = -1;
        if(v.y > 0)
            ret.y = 1;
        if(v.y < 0)
            ret.y = -1;
        return ret;
    }

    public static List<Vector2Int> AdjacentCoords(this Vector2Int v, bool includeDiagonal = false)
    {
        var directions = new List<Vector2Int>() {
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.up,
        };
        if(includeDiagonal)
        {
            directions.AddRange(new List<Vector2Int>(){
                new Vector2Int( 1, 1),
                new Vector2Int( 1,-1),
                new Vector2Int(-1,-1),
                new Vector2Int(-1, 1)
            });
        }
        var ret = new List<Vector2Int>();
        foreach(Vector2Int direction in directions)
            ret.Add(v + direction);
        return ret;
    }

    public static Vector3Int ToVector3Int(this Vector2Int v)
    {
        return new Vector3Int(v.x, v.y, 0);
    }

    public static Vector3Int ToVector3Int(this Vector2Int v, int z)
    {
        return new Vector3Int(v.x, v.y, z);
    }
}
