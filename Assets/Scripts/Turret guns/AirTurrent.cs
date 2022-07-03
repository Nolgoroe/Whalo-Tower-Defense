using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTurrent : TurretParent
{
    public GameObject baseToRotate;
    public GameObject gunsToRotate;

    private void LookForTarget()
    {
        float minimumDistanceToEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (EnemyParent enemy in ClassRefrencer.instance.enemyManager.allEnemiiesInGame)
        {
            if (enemy.typeOfEnemy == EnemyTypes.FlyingEnemy)
            {
                float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);

                if (enemyDistance < minimumDistanceToEnemy)
                {
                    minimumDistanceToEnemy = enemyDistance;
                    closestEnemy = enemy.gameObject;
                }
            }
        }


        if (closestEnemy != null && minimumDistanceToEnemy <= range * Board.boardSize)
        {
            target = closestEnemy.transform.GetComponent<EnemyParent>();
        }
        else
        {
            target = null;
        }
    }
    private void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 enemyDirHead = target.hitPoint.position - baseToRotate.transform.position;
        Vector3 enemyDirGuns = target.hitPoint.position - gunsToRotate.transform.position;

        Quaternion rotationCalcHead = Quaternion.LookRotation(enemyDirHead);
        Quaternion rotationCalcGuns = Quaternion.LookRotation(enemyDirGuns);

        Vector3 endRotationHead = Quaternion.Lerp(baseToRotate.transform.rotation, rotationCalcHead, rotationSpeed * Time.deltaTime).eulerAngles;
        Vector3 endRotationGuns = Quaternion.Lerp(gunsToRotate.transform.localRotation, rotationCalcGuns, rotationSpeed * Time.deltaTime).eulerAngles;

        baseToRotate.transform.rotation = Quaternion.Euler(0, endRotationHead.y, 0);
        gunsToRotate.transform.localRotation = Quaternion.Euler(endRotationGuns.x, 0, 0);
    }

}
