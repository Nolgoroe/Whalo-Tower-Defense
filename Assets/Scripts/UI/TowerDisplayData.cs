using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDisplayData : MonoBehaviour
{
    public int indexInAllTowersArray;

    public Text towerNameText, towerCostText;
    public Image towerImage;

    public Vector3 targetOffset;
    public BaseBuilding towerPrefab;

    public Button connectedButton;

    public void SetData(int _indexInAllTowersArray ,string _towerName, string _towerCost, Sprite _towerSprite, BaseBuilding _towerToSummon, GameObject _targetHoverAbove)
    {
        indexInAllTowersArray = _indexInAllTowersArray;

        towerNameText.text = _towerName;
        towerCostText.text = _towerName;

        towerImage.sprite = _towerSprite;

        towerPrefab = _towerToSummon;

        Vector3 pos = Camera.main.WorldToScreenPoint(_targetHoverAbove.transform.position);

        transform.position = pos + targetOffset;


        connectedButton.onClick.AddListener(delegate { ClassRefrencer.instance.buildManager.BuildTowerOnPivot(indexInAllTowersArray); });
    }
}
