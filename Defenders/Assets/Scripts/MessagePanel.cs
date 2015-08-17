using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class MessagePanel : MonoBehaviour
{
    RectTransform rt;

    public Color CurrentColor
    {
        set { backgroundColor.color = value; }
        get { return backgroundColor.color; }
    }
    public Text text;
    Image backgroundColor;

    public void SetMessageText(string message)
    {
        text.text = message;
    }

    void Awake()
    {
        text = GetComponentInChildren<Text>(); //If there is more than one text child object, use GetComponentsInChildren<Text>()[i] where i is the index of the child (remember if the current gameobject has a text component, it will be the first in the list followed by all the children components
        rt = gameObject.GetComponent<RectTransform>();
        backgroundColor = GetComponent<Image>();
            
    }

    #region Helper Functions
    /// <summary>
    /// Both versions of each helper function keeps the panel more versatile. They don't need to be altered.
    /// </summary>
    /// <param name="startPositionPercent"></param>
    /// <param name="endPositionPercent"></param>
    /// <returns></returns>
    public bool NeedToResize(Vector2 startPositionPercent, Vector2 endPositionPercent)
    {
        return ((rt.anchorMin != startPositionPercent) || (rt.anchorMax != endPositionPercent));
    }
    public bool NeedToResize(Vector2 startPositionPercent, float width, float height)
    {
        return (
            rt.anchorMin != startPositionPercent ||
            Mathf.Abs(rt.anchorMax.x - rt.anchorMin.x) != width || 
            Mathf.Abs(rt.anchorMax.y - rt.anchorMin.y) != height
            );
    }
    public void Resize(Vector2 startPositionPercent, Vector2 endPositionPercent)
    {
        //iF your anchors are changing when the panel is loaded but you still can't see it. The rect transform positions are probably off. ...SetParent(canvas.transform, false) : using false makes sure that when anchors are changed, the position is affacted (otherwise the position is changed to keep the ui looking the same on screen - we don't want that here)
        rt.anchorMin = new Vector2(startPositionPercent.x, startPositionPercent.y);
        rt.anchorMax = new Vector2(endPositionPercent.x, endPositionPercent.y);
    }
    public void Resize(Vector2 startPositionPercent, float width, float height)
    {
        rt.anchorMin = new Vector2(startPositionPercent.x, startPositionPercent.y);
        rt.anchorMax = new Vector2(Mathf.Min(1, startPositionPercent.x + width), Mathf.Min(1, startPositionPercent.y + height));
    }
    #endregion

}
