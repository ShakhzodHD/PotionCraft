using UnityEngine;
using YG;

public class ActivatorDialogue : MonoBehaviour
{
    public static ActivatorDialogue instanse;

    private bool isCrafted = false;
    private bool isSold = false;
    private void Start()
    {
        instanse = this;
    }
    public void Craft()
    {
        if (!isCrafted && YandexGame.savesData._completeTutorial == false) 
        {
            HandleDialogue();
            isCrafted = true; 
        }
    }
    public void Sell()
    {
        if (!isSold && YandexGame.savesData._completeTutorial == false)
        {
            HandleDialogue();
            isSold = true;
        }
    }
    private void HandleDialogue()
    {
        DialogueManager.instance.ContinueDualogue();
    }
}
