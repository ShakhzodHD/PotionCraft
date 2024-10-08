using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using YG;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    //��� ������ ������ � ����� �����������
    public static DialogueManager instance;

    public Text dialogueText; 
    public GameObject dialoguePanel;
    public bool isCompete;

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
    [SerializeField] private RectTransform buttonCustom;

    private Transform previousCameraTarget;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Debug.Log(isCompete);
        if (isCompete == false)
        {
            StartDialogue();
        }
        else
        {
            buttonCustom.gameObject.SetActive(true);
        }
    }
    public void StartDialogue()
    {
        previousCameraTarget = cameraController.target;
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
        if (currentDialogueIndex >= dialogues.Count)
        {
            YandexGame.savesData._completeTutorial = true;
            isCompete = true;
            
            Destroy(gameObject);
            buttonCustom.gameObject.SetActive(true);
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
