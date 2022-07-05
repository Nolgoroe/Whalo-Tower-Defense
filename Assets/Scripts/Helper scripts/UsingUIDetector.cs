using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingUIDetector : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        ClassRefrencer.instance.UIManager.callSetIsUsingUI(true, 0);
    }

    private void OnDisable()
    {
        ClassRefrencer.instance.UIManager.callSetIsUsingUI(false, 0.1f);
    }
}
