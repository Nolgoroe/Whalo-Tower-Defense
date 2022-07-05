using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public BaseBuilding[] allTowersInGame;
    public TowerDisplayData towerDisplayPrefab;

    public BaseBuildingPivot buildingPivotClicked;

    public LayerMask layerToHit;

    public void PopulateTowerDisplays()
    {
        foreach (Transform child in ParentReferencer.instance.towerDisplayDataParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < allTowersInGame.Length; i++)
        {
            TowerDisplayData display = Instantiate(towerDisplayPrefab, ParentReferencer.instance.towerDisplayDataParent);
            display.SetData(i, allTowersInGame[i].towerName, allTowersInGame[i].BuildCost.ToString(), allTowersInGame[i].UIIcon, allTowersInGame[i]);
        }

        ClassRefrencer.instance.UIManager.SetShopPos(buildingPivotClicked.transform);
    }

    public void BuildTowerOnPivot(int _indexInAllTowerArray)
    {
        bool canBuyTower = ClassRefrencer.instance.gameManager.playerState.TryTakeFunds(allTowersInGame[_indexInAllTowerArray].BuildCost);

        if (canBuyTower)
        {
            BaseBuilding building = Instantiate(allTowersInGame[_indexInAllTowerArray], buildingPivotClicked.transform.position, Quaternion.identity);

            buildingPivotClicked.SetBuilding(building);

            building.GetComponent<TurretParent>().AddPowerUpValues();

            ClassRefrencer.instance.UIManager.tempShopRef.gameObject.SetActive(false);

            buildingPivotClicked = null;
        }
        else
        {
            ClassRefrencer.instance.announcementManager.Show("Need more funds!", 1, ClassRefrencer.instance.announcementManager._fundstext);
            ClassRefrencer.instance.UIManager.tempShopRef.gameObject.SetActive(false);
            buildingPivotClicked = null;
            return;
        }

    }

    private void Update()
    {
        if (!UIManager.isUsingUI)
        {
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerToHit))
                {
                    BaseBuildingPivot pivotHit = hit.transform.GetComponent<BaseBuildingPivot>();
                    pivotHit.OnChosenPivot();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Transform cameraTransform = Camera.main.transform;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Gizmos.DrawRay(ray);

        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 raycastDir = mousePos - cameraTransform.position;

        ////if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 100.0f))
        //Debug.DrawRay(cameraTransform.position, raycastDir * 100.0f, Color.yellow);
    }

    public void SellTower()
    {
        if (buildingPivotClicked.CurrentBuilding)
        {
            ClassRefrencer.instance.gameManager.playerState.AddFunds(buildingPivotClicked.CurrentBuilding.BuildCost / 2);



            buildingPivotClicked.RemoveBuilding();

            ClassRefrencer.instance.UIManager.tempSellRef.gameObject.SetActive(false);

        }
    }
}
