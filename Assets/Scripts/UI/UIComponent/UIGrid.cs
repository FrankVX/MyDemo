using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGrid : GridLayoutGroup
{
    public override void CalculateLayoutInputVertical()
    {
        base.CalculateLayoutInputVertical();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, preferredHeight);
    }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        if (rectChildren.Count == 0) return;
        RectTransform first = rectChildren[0];
        RectTransform last = rectChildren[rectChildren.Count - 1];
        rectTransform.sizeDelta = new Vector2(Mathf.Abs(first.position.x - last.position.x), rectTransform.sizeDelta.y);
    }

}
