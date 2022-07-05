using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public BaseBuilding[] allTowersInGame;
    public TowerDisplayData towerDisplayPrefab;

    public BaseBuildingPivot buildingPivotClicked;

    public void PopulateTowerDisplays(BaseBuildingPivot _buildingPivotClicked)
    {
        for (int i = 0; i < allTowersInGame.Length; i++)
        {
            TowerDisplayData display = Instantiate(towerDisplayPrefab, ParentReferencer.instance.towerDisplayDataParent);
            display.SetData(i, allTowersInGame[i].towerName, allTowersInGame[i].BuildCost.ToString(), allTowersInGame[i].UIIcon, allTowersInGame[i], _buildingPivotClicked.gameObject);

            buildingPivotClicked = _buildingPivotClicked;

        }
    }

    public void BuildTowerOnPivot(int _indexInAllTowerArray)
    {
        BaseBuilding building = Instantiate(allTowersInGame[_indexInAllTowerArray], buildingPivotClicked.transform.position, Quaternion.identity);

        buildingPivotClicked.SetBuilding(building);

        ClassRefrencer.instance.UIManager.tempShopRef.SetActive(false);

        buildingPivotClicked = null;
    }
}
