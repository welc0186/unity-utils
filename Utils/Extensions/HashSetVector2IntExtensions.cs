using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Alf.Utils
{
public static class HashSetVector2IntExtensions
{
    public static Vector2Int FindExtremeOnAxisFromPoint(this HashSet<Vector2Int> input, Vector2Int point, bool findMaximum, char axis)
    {
        IEnumerable<Vector2Int> filteredKeys = input.Where(key =>
        {
            if (axis == 'x')
                return key.y == point.y;
            else if (axis == 'y')
                return key.x == point.x;
            else
                return false;
        });

        if (findMaximum)
            return axis == 'x' ? filteredKeys.OrderByDescending(key => key.x).FirstOrDefault() :
                                  filteredKeys.OrderByDescending(key => key.y).FirstOrDefault();
        else
            return axis == 'x' ? filteredKeys.OrderBy(key => key.x).FirstOrDefault() :
                                  filteredKeys.OrderBy(key => key.y).FirstOrDefault();
    }

    public static void FindExtremes(this HashSet<Vector2Int> input, ref int minX, ref int maxX, ref int minY, ref int maxY)
    {
        minX = input.FirstOrDefault<Vector2Int>().x;
        maxX = input.FirstOrDefault<Vector2Int>().x;
        minY = input.FirstOrDefault<Vector2Int>().y;
        maxY = input.FirstOrDefault<Vector2Int>().y;

        foreach(Vector2Int v in input)
        {
            if(v.x < minX)
                minX = v.x;
            if(v.x > maxX)
                maxX = v.x;
            if(v.y < minY)
                minY = v.y;
            if(v.y > maxY)
                maxY = v.y;
        }
    }
}
}