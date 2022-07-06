using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static bool gameRunning;
    public PlayerState playerState = new PlayerState();

    [Header("In game setup settings")]
    public int startFundAmount;

    [Header("In game timer settings")]
    public int amountOfScorePerSec;
    public int amontOfFundsPerSec;


    public void InitGameManagerInGame()
    {
        gameRunning = true;
        playerState.AddFunds(startFundAmount);

        StartCoroutine(addScoreAndFunds());

        StartCoroutine(ClassRefrencer.instance.waveManager.CountdownNextWave(ClassRefrencer.instance.waveManager.waveSpawnDelay));
    }

    private IEnumerator addScoreAndFunds()
    {
        while (gameRunning)
        {
            yield return new WaitForSeconds(1);
            playerState.AddFunds(amontOfFundsPerSec);
            playerState.AddScore(amountOfScorePerSec);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
