using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeactivateScreenOnTouch : MonoBehaviour, IPointerClickHandler
{
    public GameObject toDeactivate;

    public void OnPointerClick(PointerEventData eventData)
    {
        toDeactivate.SetActive(false);
    }
}
