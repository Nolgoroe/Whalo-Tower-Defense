using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingUIDetector : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (ClassRefrencer.instance.UIManager)
        {
            ClassRefrencer.instance.UIManager.callSetIsUsingUI(true, 0);
        }
    }

    private void OnDisable()
    {
        if (ClassRefrencer.instance.UIManager)
        {
            ClassRefrencer.instance.UIManager.callSetIsUsingUI(false, 0.3f);
        }
    }
}
