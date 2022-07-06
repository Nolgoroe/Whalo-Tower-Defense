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
    EndGameScreen,
    ShopBuyScreen,
    ShopSellScreen
}

public class UIManager : MonoBehaviour
{
    public static bool isUsingUI;

    [Header("Lists")]
    public GameObject[] allScreensInGame;
    public SpriteHolder[] allGameSpeedsObjects;

    [Header("Shop Screen data")]
    public Vector3 targetOffsetForShopScreensHover;
    public TowerDisplayData towerDisplayPrefab;

    [Header("End screen Data")]
    public Text highScoreText;


    private Dictionary<UIScreenTypes, GameObject> screenTypeToObject;


    #region public functions
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
    public void DeactivateAllScreens()
    {
        foreach (GameObject screen in allScreensInGame)
        {
            screen.SetActive(false);
        }
    }
    public void callSetIsUsingUI(bool _isUsingUI, float _delay)
    {
        StartCoroutine(SetIsUsingUI(_isUsingUI, _delay));
    }
    public void SetScreenPos(UIScreenTypes _screen, Vector3 _pos)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(_pos);

        screenTypeToObject[_screen].transform.position = pos + targetOffsetForShopScreensHover;
    }
    public void DisplayEndGameScreen()
    {
        UpdateHighScore();

        DisplaySpecificScreensNoDeactivate(new UIScreenTypes[] { UIScreenTypes.EndGameScreen });

        Time.timeScale = 0;
        GameManager.gameRunning = false;
    }
    public void DisplayGameSpeedSprite(int _amount)
    {
        foreach (SpriteHolder spriteHolder in allGameSpeedsObjects)
        {
            spriteHolder.SetDeactivated();
        }

        allGameSpeedsObjects[_amount - 1].SetActivated();
    }
    public void PopulateTowerDisplays()
    {
        BaseBuilding[] alltowers = ClassRefrencer.instance.buildManager.allTowersInGame;

        foreach (Transform child in ParentReferencer.instance.towerDisplayDataParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < alltowers.Length; i++)
        {
            TowerDisplayData display = Instantiate(towerDisplayPrefab, ParentReferencer.instance.towerDisplayDataParent);
            display.SetData(i, alltowers[i].towerName, alltowers[i].BuildCost.ToString(), alltowers[i].UIIcon, alltowers[i]);
        }

        ClassRefrencer.instance.UIManager.SetScreenPos(UIScreenTypes.ShopBuyScreen, ClassRefrencer.instance.buildManager.buildingPivotClicked.transform.position);
    }

    #endregion

    #region private functions
    private void Start()
    {
        highScoreText.text = "0";

        screenTypeToObject = new Dictionary<UIScreenTypes, GameObject>();

        for (int i = 0; i < allScreensInGame.Length; i++)
        {
            screenTypeToObject.Add((UIScreenTypes)i, allScreensInGame[i]);
            allScreensInGame[i].SetActive(false);
        }

        DisplaySpecificScreens(new UIScreenTypes[] { UIScreenTypes.MainMenuScreen });
    }
    private void SetScorePlayerPrefs(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }

    private int GetSetScorePlayerPrefs(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }
    private void UpdateHighScore()
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

        if (ClassRefrencer.instance.gameManager.playerState.Score > highestScoreReached)
        {
            highScoreText.text = ClassRefrencer.instance.gameManager.playerState.Score.ToString();

            SetScorePlayerPrefs("ScoreSaved", ClassRefrencer.instance.gameManager.playerState.Score);
        }
        else
        {
            highScoreText.text = highestScoreReached.ToString();
        }
    }

    #endregion

    #region button Functions
    public void RestartGameButton()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }
    public void MoveToGameButton()
    {
        DisplaySpecificScreens(new UIScreenTypes[] { UIScreenTypes.InGameScreen });
        ClassRefrencer.instance.gameManager.InitGameManagerInGame();
    }

    #endregion

    #region static functions
    public static IEnumerator SetIsUsingUI(bool _isUsingUI, float _delay)
    {
        yield return new WaitForSeconds(_delay);

        isUsingUI = _isUsingUI;
    }

    #endregion
}
