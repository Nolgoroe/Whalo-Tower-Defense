using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDisplayData : MonoBehaviour
{
    [Header("General data")]
    public int indexInAllTowersArray;

    [Header("Texts")]
    public Text towerNameText;
    public Text towerCostText;

    [Header("Prefabs")]
    public BaseBuilding towerPrefab;

    [Header("Connected componenets")]
    public Image towerImage;
    public Button connectedButton;

    public void SetData(int _indexInAllTowersArray ,string _towerName, string _towerCost, Sprite _towerSprite, BaseBuilding _towerToSummon)
    {
        indexInAllTowersArray = _indexInAllTowersArray;

        towerNameText.text = _towerName;
        towerCostText.text = _towerCost + "$";

        towerImage.sprite = _towerSprite;

        towerPrefab = _towerToSummon;

        connectedButton.onClick.AddListener(delegate { ClassRefrencer.instance.buildManager.BuildTowerOnPivot(indexInAllTowersArray); });
    }
}
