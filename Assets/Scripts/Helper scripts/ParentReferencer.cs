using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentReferencer : MonoBehaviour
{
    public static ParentReferencer instance;

    public Transform towerDisplayDataParent;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
