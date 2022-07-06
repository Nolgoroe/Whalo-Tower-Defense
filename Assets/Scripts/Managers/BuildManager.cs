using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [Header("Lists")]
    public BaseBuilding[] allTowersInGame;

    [Header("Setup data")]
    public LayerMask layerToHit;

    [Header("Debug data")]
    public BaseBuildingPivot buildingPivotClicked;

    #region public functions
    public void BuildTowerOnPivot(int _indexInAllTowerArray)
    {
        bool canBuyTower = ClassRefrencer.instance.gameManager.playerState.TryTakeFunds(allTowersInGame[_indexInAllTowerArray].BuildCost);

        if (canBuyTower)
        {
            BaseBuilding building = Instantiate(allTowersInGame[_indexInAllTowerArray], buildingPivotClicked.transform.position, Quaternion.identity);

            buildingPivotClicked.SetBuilding(building);

            building.GetComponent<TurretParent>().AddPowerUpValues();

            ClassRefrencer.instance.UIManager.DeactivateSpecificScreens(new UIScreenTypes[] { UIScreenTypes.ShopBuyScreen });

            buildingPivotClicked = null;
        }
        else
        {
            ClassRefrencer.instance.announcementManager.Show("Need more funds!", 1, ClassRefrencer.instance.announcementManager._fundstext);
            ClassRefrencer.instance.UIManager.DeactivateSpecificScreens(new UIScreenTypes[] { UIScreenTypes.ShopBuyScreen });
            buildingPivotClicked = null;
            return;
        }

    }

    #endregion

    #region private functions
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

    #endregion

    #region Button functions
    public void SellTower()
    {
        if (buildingPivotClicked.CurrentBuilding)
        {
            ClassRefrencer.instance.gameManager.playerState.AddFunds(buildingPivotClicked.CurrentBuilding.BuildCost / 2);



            buildingPivotClicked.RemoveBuilding();

            ClassRefrencer.instance.UIManager.DeactivateSpecificScreens(new UIScreenTypes[] { UIScreenTypes.ShopSellScreen });
        }
    }

    #endregion

    #region debugging region
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Transform cameraTransform = Camera.main.transform;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Gizmos.DrawRay(ray);
    }
    #endregion
}
