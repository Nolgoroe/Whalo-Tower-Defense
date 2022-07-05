using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedChanger : MonoBehaviour
{
    public void SetGameSpeedNormal()
    {
        Time.timeScale = 1;
    }
    public void SetGameSpeedDouble()
    {
        Time.timeScale = 2;
    }
    public void SetGameSpeedTriple()
    {
        Time.timeScale = 3;
    }
}
