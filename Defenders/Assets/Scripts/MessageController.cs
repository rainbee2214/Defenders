using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//If a message call is made and the messaging panel doesn't exist, create one, reset it and then display the message

//If the messaging controller is a prefab in the game, a message can be dispalyed to screen at any time

public class MessageController : MonoBehaviour
{
    public static MessageController messageController;

    public Vector2 startPositionPercent = new Vector2(0.2f, 0.2f);
    [Range(0, 1)] protected float width = 0.5f;
    [Range(0, 1)] protected float height = 0.5f;
    protected MessagePanel messagePanel;

    protected Canvas canvas;
    void Awake()
    {
        if (messageController == null)
        {
            DontDestroyOnLoad(gameObject);
            messageController = this;
        }
        else if (messageController != this)
        {
            Destroy(gameObject);
        }
        LoadNewMessagingPanel();
    }

    public virtual void DisplayMessage(string message)
    {
        //Will check for changes in the public position variables - Manipulate the variables with other game objects and scripts
        if (messagePanel.NeedToResize(startPositionPercent, width, height))
            ResizePanel();

        messagePanel.SetMessageText(message);
        messagePanel.gameObject.SetActive(true);
    }

    #region Helper Functions
    public void LoadNewMessagingPanel()
    {
        messagePanel = Instantiate(Resources.Load<GameObject>("Prefabs/MessagePanel")).GetComponent<MessagePanel>();
        //Find the main canvas (if more than one) or specific canvas to attach message panel to 
        canvas = (GameObject.Find("Canvas") != null) ? GameObject.Find("Canvas").GetComponent<Canvas>() : CreateCanvas().GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        messagePanel.name = "Message Panel - " + this.name;
        messagePanel.transform.SetParent(canvas.transform, false); //don't forget the second parameter!
        messagePanel.transform.SetAsLastSibling();
        //If the panel has a local scale of (0,0,0) [it happens] and the ui won't display and you'll be confused
        messagePanel.transform.localScale = Vector3.one;
        messagePanel.gameObject.SetActive(false);
        ResizePanel();
    }

    public GameObject CreateCanvas()
    {
        GameObject c = new GameObject("Canvas");
        c.AddComponent<Canvas>();
        c.AddComponent<CanvasScaler>();
        c.AddComponent<GraphicRaycaster>();
        return c;
    }

    public virtual void ResizePanel()
    {
        messagePanel.Resize(startPositionPercent, width, height);
    }
    #endregion

}
