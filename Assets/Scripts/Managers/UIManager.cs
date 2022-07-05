using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

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

    public RectTransform tempShopRef;
    public RectTransform tempSellRef;
    public RectTransform tempShopRefSub;
    public RectTransform tempSellRefSub;

    private Dictionary<UIScreenTypes, GameObject> screenTypeToObject;
    public Vector3 targetOffset;

    public static bool isUsingUI;
    public bool TEMPisUsingUI;

    public Text highScoreText;

    private void Update()
    {
        TEMPisUsingUI = isUsingUI;
    }

    private void Start()
    {
        highScoreText.text = "0";

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

        UpdateHighScore();
    }

    //public void DisplaySystemMessages()
    //{
    //    DisplaySpecificScreens(new UIScreenTypes[] { UIScreenTypes.SystemMessages, UIScreenTypes.InGameScreen });
    //}

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

    public void SetShopPos(Transform _targetHoverAbove)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(_targetHoverAbove.position);

        tempShopRefSub.position = pos + targetOffset; /// temp here
    }
    public void SetShopSellPos(Transform _targetHoverAbove)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(_targetHoverAbove.position);

        tempSellRefSub.position = pos + targetOffset; /// temp here
    }

    public void UpdateHighScore()
    {
        int highestScoreReached = 0;

        if (PlayerPrefs.HasKey("ScoreSaved"))
        {
            highestScoreReached = GetSetScorePlayerPrefs("ScoreSaved");
        }
        else
        {
            SetScorePlayerPrefs("ScoreSaved", ClassRefrencer.instance.gameManager.playerState.Score);
        }

        if(ClassRefrencer.instance.gameManager.playerState.Score > highestScoreReached)
        {
            highScoreText.text = ClassRefrencer.instance.gameManager.playerState.Score.ToString();

            SetScorePlayerPrefs("ScoreSaved", ClassRefrencer.instance.gameManager.playerState.Score);
        }
        else
        {
            highScoreText.text = highestScoreReached.ToString();
        }
    }


    public void SetScorePlayerPrefs(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }

    public int GetSetScorePlayerPrefs(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }

    public void callSetIsUsingUI(bool _isUsingUI, float _delay)
    {
        StartCoroutine(SetIsUsingUI(_isUsingUI, _delay));
    }

    public static IEnumerator SetIsUsingUI(bool _isUsingUI, float _delay)
    {
        yield return new WaitForSeconds(_delay);

        isUsingUI = _isUsingUI;
    }
}
