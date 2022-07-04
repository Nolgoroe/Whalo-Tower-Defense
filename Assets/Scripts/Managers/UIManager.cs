using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIScreenTypes
{
    MainMenuScreen,
    InGameScreen,
    LoseScreen,
    SystemMessages,
}

public class UIManager : MonoBehaviour
{
    public GameObject[] allScreensInGame; // used to reset all screens to false on start so that we don't accidentaly forget a screen as active.

    public TowerDisplayData towerDisplayPrefab;
    public GameObject tempShopRef;

    private Dictionary<UIScreenTypes, GameObject> screenTypeToObject;

    private void Start()
    {
        screenTypeToObject = new Dictionary<UIScreenTypes, GameObject>();

        for (int i = 0; i < allScreensInGame.Length; i++)
        {
            screenTypeToObject.Add((UIScreenTypes)i, allScreensInGame[i]);
        }

        foreach (GameObject screen in allScreensInGame)
        {
            screen.SetActive(false);
        }

        DisplaySpecificScreens(new UIScreenTypes[] { UIScreenTypes.MainMenuScreen });
    }

    public void DisplaySpecificScreens(UIScreenTypes[] screens)
    {
        DeactivateAllScreens();

        foreach (UIScreenTypes type in screens)
        {
            screenTypeToObject[type].SetActive(true);
        }
    }

    public void DisplaySpecificScreensNoDeactivate(UIScreenTypes[] screens)
    {
        foreach (UIScreenTypes type in screens)
        {
            screenTypeToObject[type].SetActive(true);
        }
    }

    public void DeactivateSpecificScreens(UIScreenTypes[] screens)
    {
        foreach (UIScreenTypes type in screens)
        {
            screenTypeToObject[type].SetActive(false);
        }
    }

    public void MoveToGame()
    {
        DisplaySpecificScreens(new UIScreenTypes[] { UIScreenTypes.InGameScreen });
        ClassRefrencer.instance.gameManager.InitGameManagerInGame();
    }

    public void DisplayEndGameScreen()
    {
        DisplaySpecificScreens(new UIScreenTypes[] { UIScreenTypes.LoseScreen });
    }

    public void DisplaySystemMessages()
    {
        DisplaySpecificScreens(new UIScreenTypes[] { UIScreenTypes.SystemMessages, UIScreenTypes.InGameScreen });
    }

    public void RestartGameButton()
    {
        SceneManager.LoadScene(0);
    }

    private void DeactivateAllScreens()
    {
        foreach (GameObject screen in allScreensInGame)
        {
            screen.SetActive(false);
        }
    }
}
