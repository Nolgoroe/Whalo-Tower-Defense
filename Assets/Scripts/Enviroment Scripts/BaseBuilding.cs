using UnityEngine;

public class BaseBuilding : MonoBehaviour
{
    [SerializeField]
    private int _buildCost = 40;

    [SerializeField]
    private string _name = null;

    [SerializeField]
    private Sprite _uiIcon = null;

    public string towerName => _name;

    public Sprite UIIcon => _uiIcon;

    public int BuildCost => _buildCost;

    public void SetParentPivot(Transform pivotTransform)
    {
        transform.SetParent(pivotTransform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public virtual void OnRemoval() { Destroy(gameObject); }
}
