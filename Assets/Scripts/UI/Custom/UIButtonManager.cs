using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] playerEffects;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private YGRewardedManager rewardedManager; 
    private Button activeButton;
    private Dictionary<Button, IButtonAction> buttonActions;

    private bool[] isEffectUnlocked;

    public int GetButtonCount()
    {
        return buttons.Count;
    }
    public bool[] SetUnlockEffect
    {
        get { return isEffectUnlocked; }
        set
        {
            if (value.Length != playerEffects.Length)
            {
                return;
            }

            CreateUnlockedEffects();
            isEffectUnlocked = value;

            for (int i = 0; i < buttons.Count; i++)
            {
                Button button = buttons[i];
                bool isUnlocked = isEffectUnlocked[i];

                Transform firstChild = button.transform.GetChild(0);
                firstChild.gameObject.SetActive(!isUnlocked);
            }
        }
    }
    private void Start()
    {
        foreach (var effect in playerEffects)
        {
            effect.Stop();
        }

        buttonActions = new Dictionary<Button, IButtonAction>
        {
            { buttons[0], new ButtonActionSparksEffect() },
            { buttons[1], new ButtonActionHeartEffect() },
            { buttons[2], new ButtonActionCurseEffect() }
        };
    }

    public void OnButtonClicked(Button clickedButton)
    {
        int buttonIndex = buttons.IndexOf(clickedButton); 

        if (clickedButton == activeButton)
        {
            DisableActiveButton();
            return;
        }

        if (isEffectUnlocked[buttonIndex])
        {
            SetActiveButton(clickedButton);
        }
        else
        {
            rewardedManager.PlayRewardAdForButtonEffect(buttonIndex);
        }
    }

    private void SetActiveButton(Button newActiveButton)
    {
        if (activeButton != null)
        {
            activeButton.GetComponent<Image>().color = Color.white;
        }

        activeButton = newActiveButton;

        activeButton.GetComponent<Image>().color = Color.green;

        ExecuteButtonAction(newActiveButton);

        int buttonIndex = buttons.IndexOf(newActiveButton);
        SaveManager.instance.SaveActiveEffect(buttonIndex);
    }
    private void DisableActiveButton()
    {
        if (activeButton != null)
        {
            StopButtonEffect(activeButton);

            activeButton.GetComponent<Image>().color = Color.white;
            activeButton = null;
            SaveManager.instance.SaveActiveEffect(-1); // Сохраняем значение -1 или другой индикатор, что нет активной кнопки
        }
    }
    private void StopButtonEffect(Button button)
    {
        int buttonIndex = buttons.IndexOf(button);
        if (buttonIndex >= 0 && buttonIndex < playerEffects.Length)
        {
            playerEffects[buttonIndex].Stop();
        }
    }
    private void ExecuteButtonAction(Button button)
    {
        foreach (var effect in playerEffects)
        {
            effect.Stop();
        }

        if (buttonActions.TryGetValue(button, out IButtonAction action))
        {
            int buttonIndex = buttons.IndexOf(button);
            action.Execute(playerEffects[buttonIndex]);
        }
    }
    private void CreateUnlockedEffects()
    {
        isEffectUnlocked = new bool[playerEffects.Length];
    }
    public void UnlockEffect(int buttonIndex)
    {
        isEffectUnlocked[buttonIndex] = true;
        SetActiveButton(buttons[buttonIndex]);

        SaveManager.instance.SaveUnlockEffecs(buttonIndex);
    }
    public void SetActiveButton(int buttonIndex)
    {
        if (buttonIndex <= -1) return;

        SetActiveButton(buttons[buttonIndex]);
    }
}
