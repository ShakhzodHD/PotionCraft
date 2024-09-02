using UnityEngine;

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
        if (!isCrafted) 
        {
            HandleDialogue();
            isCrafted = true; 
        }
    }
    public void Sell()
    {
        if (!isSold)
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
