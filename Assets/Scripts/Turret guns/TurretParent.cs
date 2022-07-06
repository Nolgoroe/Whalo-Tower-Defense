using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretParent : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] internal float range;
    [SerializeField] internal float maxRange;
    [SerializeField] internal float rotationSpeed;
    [SerializeField] internal float attackSpeed;
    [SerializeField] internal float maxAttackSpeed;

    [SerializeField] internal EnemyTypes typeToAttack;

    [Header("Parts")]
    [SerializeField] internal GameObject baseToRotate;
    [SerializeField] internal GameObject gunsToRotate;
    [SerializeField] internal Transform shootPivot;

    [Header("Shoot data")]
    [SerializeField] internal GameObject bulletPrefab;
    [SerializeField] internal string bulletObjectTag;
    [SerializeField] internal bool canShoot = true;
    [SerializeField] internal LayerMask layerToHit;

    [Header("Debugging data")]
    [SerializeField] internal EnemyParent target;

    #region public functions

    public abstract void AddPowerUpValues();

    #endregion

    #region private functions
    private void Start()
    {
        StartCoroutine(CallLookingForTarget());
        StartCoroutine(Shoot());
        bulletObjectTag = bulletPrefab.transform.tag;
    }
    private IEnumerator Shoot()
    {
        while (GameManager.gameRunning)
        {
            yield return new WaitForSeconds(attackSpeed);

            if (target && canShoot)
            {
                GameObject bulletObject = ClassRefrencer.instance.objectPoolingManager.GetFromPool(bulletObjectTag, shootPivot.position, shootPivot.rotation);

                BulletParent summonedBullet = bulletObject.GetComponent<BulletParent>();
                summonedBullet.SetTarget(target.hitPoint.transform);
                summonedBullet.AddPowerUpValues();
            }
        }
    }

    private IEnumerator CallLookingForTarget()
    {
        while (GameManager.gameRunning)
        {
            yield return new WaitForSeconds(0.5f);
            LookForTarget(null);
        }
    }
    private void LookForTarget(EnemyParent toIgnore)
    {
        float minimumDistanceToEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (EnemyParent enemy in ClassRefrencer.instance.enemyManager.allEnemiesSpawned)
        {
            if (enemy != toIgnore)
            {
                if (enemy.typeOfEnemy == typeToAttack)
                {
                    float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);

                    if (enemyDistance < minimumDistanceToEnemy)
                    {
                        minimumDistanceToEnemy = enemyDistance;
                        closestEnemy = enemy.gameObject;
                    }
                }
            }
        }

        if (closestEnemy != null && minimumDistanceToEnemy <= range)
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

        RaycastHit hit;

        Vector3 raycastDir = target.hitPoint.position - shootPivot.position;

        if (Physics.Raycast(shootPivot.position, raycastDir, out hit, Mathf.Infinity, layerToHit))
        {
            if (hit.transform == target.transform && hit.transform != transform)
            {
                Vector3 raycastDirToShoot = shootPivot.transform.forward * 100 - shootPivot.position;

                if (Physics.Raycast(shootPivot.position, raycastDirToShoot, out hit, Mathf.Infinity, layerToHit))
                {
                    canShoot = true;
                }
                else
                {
                    canShoot = false;
                }
            }
            else
            {
                canShoot = false;
                LookForTarget(target);
                return;
            }
        }

        Vector3 enemyDirHead = target.hitPoint.position - baseToRotate.transform.position;
        Vector3 enemyDirGuns = target.hitPoint.position - gunsToRotate.transform.position;

        Quaternion rotationCalcHead = Quaternion.LookRotation(enemyDirHead);
        Quaternion rotationCalcGuns = Quaternion.LookRotation(enemyDirGuns);

        Vector3 endRotationHead = Quaternion.Lerp(baseToRotate.transform.rotation, rotationCalcHead, (rotationSpeed * Time.deltaTime) / 360).eulerAngles;
        Vector3 endRotationGuns = Quaternion.Lerp(gunsToRotate.transform.localRotation, rotationCalcGuns, (rotationSpeed * Time.deltaTime) / 360).eulerAngles;
        baseToRotate.transform.rotation = Quaternion.Euler(0, endRotationHead.y, 0);
        gunsToRotate.transform.localRotation = Quaternion.Euler(endRotationGuns.x, 0, 0);

        gunsToRotate.transform.localRotation = Quaternion.Euler(endRotationGuns.x, 0, 0);

    }

    #endregion

    #region debugging region
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 raycastDir = Vector3.zero;
        Vector3 raycastDirToShoot = Vector3.zero;

        if (target)
        {
            raycastDir = target.hitPoint.position - shootPivot.position;
            raycastDirToShoot = shootPivot.transform.forward * 100 - shootPivot.position;

        }

        Gizmos.DrawRay(shootPivot.position, raycastDir * 100);
        Gizmos.DrawRay(shootPivot.position, raycastDirToShoot);
    }

    #endregion
}
