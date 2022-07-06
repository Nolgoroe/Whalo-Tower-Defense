using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedChanger : MonoBehaviour
{
    public void SetGameSpeed(int _amount)
    {
        Time.timeScale = _amount;

        ClassRefrencer.instance.UIManager.DisplayGameSpeedSprite(_amount);
    }
}
