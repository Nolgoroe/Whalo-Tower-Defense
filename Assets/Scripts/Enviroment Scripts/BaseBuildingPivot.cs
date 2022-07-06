using UnityEngine;

public class BaseBuildingPivot : MonoBehaviour
{
    [SerializeField]
    private GameObject _placementIndicator = null;

    //public BaseBuilding CurrentBuilding { get; private set; }
    public BaseBuilding CurrentBuilding;

    public Quaternion Rotation => transform.rotation;
    public Vector3 WorldPosition => transform.position;

    public void SetBuilding(BaseBuilding building)
    {
        if (CurrentBuilding != null)
        {
            Debug.LogError("Current building already set, cannot add another building to pivot", gameObject);
            return;
        }

            _placementIndicator.SetActive(false);
            CurrentBuilding = building;
            CurrentBuilding.SetParentPivot(transform);
    }

    public void RemoveBuilding()
    {
        if (CurrentBuilding == null)
        {
            Debug.LogError("There is no current building to remove on this pivot", gameObject);
            return;
        }
        var building = CurrentBuilding;
        CurrentBuilding = null;
        building.OnRemoval();
        _placementIndicator.SetActive(true);
    }

    public void OnChosenPivot()
    {
        ClassRefrencer.instance.buildManager.buildingPivotClicked = this;

        if (CurrentBuilding)
        {
            ClassRefrencer.instance.UIManager.DisplaySpecificScreensNoDeactivate(new UIScreenTypes[] { UIScreenTypes.ShopSellScreen });

            ClassRefrencer.instance.UIManager.SetScreenPos(UIScreenTypes.ShopSellScreen, transform.position);
        }
        else
        {
            ClassRefrencer.instance.UIManager.DisplaySpecificScreensNoDeactivate(new UIScreenTypes[] { UIScreenTypes.ShopBuyScreen });

            ClassRefrencer.instance.UIManager.PopulateTowerDisplays();
        }

    }

    private void OnMouseEnter()
    {
        //change color here
    }
}
