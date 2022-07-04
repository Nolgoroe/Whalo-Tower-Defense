using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDisplayData : MonoBehaviour
{
    public Text towerNameText, towerCostText;
    public Image towerImage;

    public Vector3 targetOffset;

    public void SetData(string _towerName, string _towerCost, Sprite _towerSprite, GameObject _target)
    {
        towerNameText.text = _towerName;
        towerCostText.text = _towerName;

        towerImage.sprite = _towerSprite;


        Vector3 pos = Camera.main.WorldToScreenPoint(_target.transform.position);

        transform.position = pos + targetOffset;
    }
}
