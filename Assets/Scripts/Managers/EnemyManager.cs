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
    public Transform enemiesParentTransform;

    public EnemyParent groundEnemyPrefab;
    public EnemyParent flyingEnemyPrefab;

    public PlayerCore playerCore;

    public List<EnemyParent> allEnemiiesInGame;

    public void SpawnEnemy(EnemyTypes enemyType, int _moveSpeed, int _health)
    {
        EnemyParent EP = null;

        switch (enemyType)
        {
            case EnemyTypes.GroundEnemy:
                Vector3[] groundPath = ClassRefrencer.instance.boardManager.GetGroundPath();
                EP = Instantiate(groundEnemyPrefab, Vector3.zero, groundEnemyPrefab.transform.rotation, enemiesParentTransform).Init(_moveSpeed, _health, groundPath);
                allEnemiiesInGame.Add(EP);
                break;
            case EnemyTypes.FlyingEnemy:
                Vector3[] airPath = ClassRefrencer.instance.boardManager.GetAirPath();
                EP = Instantiate(flyingEnemyPrefab, Vector3.zero, flyingEnemyPrefab.transform.rotation, enemiesParentTransform).Init(_moveSpeed, _health, airPath);
                allEnemiiesInGame.Add(EP);
                break;
            default:
                break;
        }
    }
}
