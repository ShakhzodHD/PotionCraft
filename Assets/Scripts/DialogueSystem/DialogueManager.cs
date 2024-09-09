using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using YG;

public class DialogueManager : MonoBehaviour
{
    //Код полное дерьмо в падлу рефакторить
    public static DialogueManager instance;
    public Text dialogueText; 
    public GameObject dialoguePanel; 
    [SerializeField] private List<string> dialogues;
    [SerializeField] private List<string> dialoguesEn;
    [SerializeField] private List<string> dialoguesTr;
    [SerializeField] private List<string> dialoguesKk;
    public int currentDialogueIndex = 0; 

    [SerializeField] private CameraController cameraController;

    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Transform pos3;
    [SerializeField] private Transform pos4;

    [SerializeField] private GameObject buttonForPulse;

    private Transform previousCameraTarget;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartDialogue();
        previousCameraTarget = cameraController.target;
    }
    public void StartDialogue()
    {
        dialoguePanel.SetActive(true); 
        currentDialogueIndex = 0; 
        ShowDialogue();
    }
    public void ContinueDualogue()
    {
        PauseSystem.Instance.SetPause();
        dialoguePanel.SetActive(true);
    }
    public void ShowDialogue()
    {
        PauseSystem.Instance.SetPause();

        if (currentDialogueIndex == 2)
        {
            cameraController.target = pos1.transform;
        }
        if (currentDialogueIndex == 3)
        {
            cameraController.target = pos2.transform;
        }
        if (currentDialogueIndex == 4)
        {
            cameraController.target = previousCameraTarget.transform;
            buttonForPulse.AddComponent<ButtonPulseColor>();

        }
        if (currentDialogueIndex == 5)
        {
            Destroy(buttonForPulse.GetComponent<ButtonPulseColor>());
            EndDialogue();
        }
        if (currentDialogueIndex == 6)
        {
            EndDialogue();
        }
        if (currentDialogueIndex == 8)
        {
            cameraController.target = pos3.transform;
        }
        if (currentDialogueIndex == 9)
        {
            cameraController.target = previousCameraTarget.transform;
        }
        if (currentDialogueIndex < dialogues.Count)
        {
            switch (YandexGame.lang)
            {
                case "ru":
                    dialogueText.text = dialogues[currentDialogueIndex];
                    break;
                case "en":
                    dialogueText.text = dialoguesEn[currentDialogueIndex];
                    break;
                case "tr":
                    dialogueText.text = dialoguesTr[currentDialogueIndex];
                    break;
                case "kk":
                    dialogueText.text = dialoguesKk[currentDialogueIndex];
                    break;
                default:
                    dialogueText.text = dialoguesEn[currentDialogueIndex];
                    break;
            }
        }
        else
        {
            EndDialogue();
        }
    }

    public void NextDialogue()
    {
        currentDialogueIndex++;
        ShowDialogue();
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        PauseSystem.Instance.RemovePause();
    }
}
