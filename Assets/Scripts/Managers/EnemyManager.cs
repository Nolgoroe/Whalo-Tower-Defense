using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyTypes
{
    GroundEnemy,
    FlyingEnemy
}

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy prefabs")]
    public EnemyParent groundEnemyPrefab;
    public EnemyParent flyingEnemyPrefab;

    [Header("PlayerCore core")]
    public PlayerCore playerCore;

    [Header("Lists")]
    public List<EnemyParent> allEnemiesSpawned;

    public void SpawnEnemy(EnemyTypes enemyType, int _moveSpeed, int _health)
    {
        EnemyParent EP = null;
        GameObject enemy = null;

        switch (enemyType)
        {
            case EnemyTypes.GroundEnemy:
                Vector3[] groundPath = ClassRefrencer.instance.boardManager.GetGroundPath();
                enemy = ClassRefrencer.instance.objectPoolingManager.GetFromPool(groundEnemyPrefab.transform.tag, Vector3.zero, groundEnemyPrefab.transform.rotation);
                EP = enemy.GetComponent<EnemyParent>();
                EP.Init(_moveSpeed, _health, groundPath);
                allEnemiesSpawned.Add(EP);
                break;
            case EnemyTypes.FlyingEnemy:
                Vector3[] airPath = ClassRefrencer.instance.boardManager.GetAirPath();
                enemy = ClassRefrencer.instance.objectPoolingManager.GetFromPool(flyingEnemyPrefab.transform.tag, Vector3.zero, flyingEnemyPrefab.transform.rotation);
                EP = enemy.GetComponent<EnemyParent>();
                EP.Init(_moveSpeed, _health, airPath);
                allEnemiesSpawned.Add(EP);
                break;
            default:
                break;
        }
    }
}
