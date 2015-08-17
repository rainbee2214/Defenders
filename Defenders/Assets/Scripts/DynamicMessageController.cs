using UnityEngine;
using System.Collections;

public class DynamicMessageController : MessageController
{
    public Vector2 endPositionPercent = new Vector2(0.8f, 0.9f);
    [Tooltip("Dynamic will resize the panel based on the vector percentages. Non dynamic will start at the startPositiona nd resize the panel using the width and height (clamping the panel to the screen)")]
    public bool dynamic = true;
    [Range(1,10)]
    public float closeDelay = 1f;

    [Range(0, 1)]
    public float fadeSpeed = 0.5f;

    public override void DisplayMessage(string message)
    {
        //base.DisplayMessage();
        StartCoroutine(DisplayMessagePanel(message, closeDelay));
    }

    IEnumerator DisplayMessagePanel(string message, float delay = 1f)
    {
        float noAlpha = 0f;
        float lowAlpha = 10 / 255f; //this is an alpha level that we want to fade to before turning off
        float highAlpha = 255 / 255f; // = 1 is full, but you may not want your panel at full alpha all the time, so adjust accordingly
        Color targetColor = messagePanel.CurrentColor;

        //Display the message panel 
        //Will check for changes in the public position variables - Right now these are being manipulated in the inspector (they will soon be manipulated through code)
        if (dynamic && messagePanel.NeedToResize(startPositionPercent, endPositionPercent))
            ResizePanel();

        messagePanel.SetMessageText(message);
        messagePanel.gameObject.SetActive(true);

        //Wait for the delay -  the co routine will stop here and only continue executing after the delay
        yield return new WaitForSeconds(delay);

        //Don't turn off until it's faded out
        targetColor.a = noAlpha;
        while (messagePanel.CurrentColor.a > lowAlpha)
        {
            messagePanel.CurrentColor = Color.Lerp(messagePanel.CurrentColor, targetColor, fadeSpeed);
            //increase the fadeSpeed over time
            yield return null;
        }

        messagePanel.gameObject.SetActive(false);

        //Set the panel back to original color 
        targetColor.a = highAlpha;
        messagePanel.CurrentColor = targetColor;

        yield return null;
    }

    public override void ResizePanel()
    {
        messagePanel.Resize(startPositionPercent, endPositionPercent);
    }
}
