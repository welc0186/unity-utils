using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alf.Utils
{

public static class PixelClamper
{
    
    public static void ClampTransform(Transform input, int pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(
            Mathf.RoundToInt(input.position.x * pixelsPerUnit),
            Mathf.RoundToInt(input.position.y * pixelsPerUnit),
            0
        );

        input.position = vectorInPixels / pixelsPerUnit;
    }

    public static Vector3 ClampVector(Vector3 input, int pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(
            Mathf.RoundToInt(input.x * pixelsPerUnit),
            Mathf.RoundToInt(input.y * pixelsPerUnit),
            0
        );

        return vectorInPixels / pixelsPerUnit;
    }

}
}