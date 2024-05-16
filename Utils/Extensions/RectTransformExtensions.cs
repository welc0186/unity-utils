using UnityEngine;

public static class RectTransformExtensions
{
    public enum Anchor
    {
        UPPER_LEFT,
        UPPER_MIDDLE,
        UPPER_RIGHT,
        MIDDLE_LEFT,
        MIDDLE_MIDDLE,
        MIDDLE_RIGHT,
        LOWER_LEFT,
        LOWER_MIDDLE,
        LOWER_RIGHT
    }

    public static RectTransform SetAnchor(this RectTransform self, Anchor anchor)
    {
        Vector2 anchorMax;
        Vector2 anchorMin;
        switch(anchor)
        {
            case(Anchor.UPPER_RIGHT):
                anchorMin = new Vector2(1,1);
                anchorMax = new Vector2(1,1);
                break;
            default:
                anchorMin = new Vector2(0,0);
                anchorMax = new Vector2(0,0);
                Debug.LogWarning("Anchor preset not implemented!");
                break;
        }
        self.anchorMin = anchorMin;
        self.anchorMax = anchorMax;
        return self;
    }

}