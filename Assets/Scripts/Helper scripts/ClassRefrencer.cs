using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassRefrencer : MonoBehaviour
{
    public static ClassRefrencer instance;

    public GameManager gameManager;
    public UIManager UIManager;
    public Announcement announcementManager;
    public WaveManager waveManager;
    public EnemyManager enemyManager;
    public Board boardManager;
    public BuildManager buildManager;
    public PowerUpManager powerupManager;
    public ObjectPoolingManager objectPoolingManager;

    private void Awake()
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
