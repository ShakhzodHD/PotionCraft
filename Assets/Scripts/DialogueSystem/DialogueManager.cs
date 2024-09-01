using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText; // ������ �� Text ������� ��� ����������� ��������
    public GameObject dialoguePanel; // ������ �� ������ ��� ��������
    public List<string> dialogues; // ������ ����� � ���������
    public int currentDialogueIndex = 0; // ������ �������� �������}

    [SerializeField] private CameraController cameraController;

    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Transform pos3;

    private Transform previousCameraTarget;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            ContinueDualogue();
        }
    }
    private void Start()
    {
        StartDialogue();
        previousCameraTarget = cameraController.target;
    }
    public void StartDialogue()
    {
        dialoguePanel.SetActive(true); // �������� ������ �������
        currentDialogueIndex = 0; // ������ � ������� �������
        ShowDialogue();
    }
    public void ContinueDualogue()
    {
        dialoguePanel.SetActive(true);
    }
    public void ShowDialogue()
    {
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
        }
        if (currentDialogueIndex == 5)
        {
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
            dialogueText.text = dialogues[currentDialogueIndex]; // ���������� ������� ������
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
    }
}
