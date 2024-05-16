using UnityEngine;

namespace Alf.Utils
{
public static class BoxCollider2DExtensions
{
    public static bool Contains(this BoxCollider2D bigCollider, BoxCollider2D smallCollider)
    {
        var smallBounds = smallCollider.bounds;
        smallBounds.min = smallBounds.min.ZeroZ();
        smallBounds.max = smallBounds.max.ZeroZ();

        var bigBounds = bigCollider.bounds;
        bigBounds.min = bigBounds.min.ZeroZ();
        bigBounds.max = bigBounds.max.ZeroZ();
        var checkA = bigBounds.Contains(smallBounds.min);
        var checkB = bigBounds.Contains(smallBounds.max);
        return bigBounds.Contains(smallBounds.min) && bigBounds.Contains(smallBounds.max);
    }
}
}