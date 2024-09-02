using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public Text dialogueText; // Ссылка на Text элемент для отображения диалогов
    public GameObject dialoguePanel; // Ссылка на панель для диалогов
    public List<string> dialogues; // Список строк с диалогами
    public int currentDialogueIndex = 0; // Индекс текущего диалога}

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
        dialoguePanel.SetActive(true); // Показать панель диалога
        currentDialogueIndex = 0; // Начать с первого диалога
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
            dialogueText.text = dialogues[currentDialogueIndex]; // Отображать текущий диалог
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
