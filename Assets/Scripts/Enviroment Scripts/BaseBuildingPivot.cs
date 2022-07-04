using UnityEngine;

public class BaseBuildingPivot : MonoBehaviour
{
    [SerializeField]
    private GameObject _placementIndicator = null;

    public BaseBuilding CurrentBuilding { get; private set; }

    public Quaternion Rotation => transform.rotation;
    public Vector3 WorldPosition => transform.position;

    public Sprite tempSprite;

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
        CurrentBuilding.OnPlacement();
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

    private void OnMouseDown()
    {
        Debug.Log("Clicked");
        //sRefrencer.instance.UIManager.DisplaySpecificScreensNoDeactivate(new UIScreenTypes[] { UIScreenTypes.ShopMenu });
        ClassRefrencer.instance.UIManager.tempShopRef.SetActive(true);
        ClassRefrencer.instance.UIManager.towerDisplayPrefab.SetData("this name", "this cose", tempSprite, gameObject);
    }
}
