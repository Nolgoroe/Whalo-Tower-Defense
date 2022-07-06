using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletParent : MonoBehaviour
{
    [SerializeField] private Transform targetAttack;
    [SerializeField] internal float moveSpeed;
    [SerializeField] internal float damageToEnemy;

    public float maxMoveSpeed;

    private Vector3 bulletDir;

    #region public functions

    public void SetTarget(Transform _targetToAttack)
    {
        targetAttack = _targetToAttack;

        bulletDir = targetAttack.transform.position - transform.position;
    }


    public abstract void AddPowerUpValues();

    #endregion

    #region private functions
    private void OnEnable()
    {
        StartCoroutine(DeactivateBulletAfterDelay());
    }


    private void Update()
    {
        if(targetAttack == null)
        {
            ClassRefrencer.instance.objectPoolingManager.AddObjectBackToQueue(transform.tag, gameObject);
            return;
        }


        transform.Translate(bulletDir.normalized * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GroundEnemy") || other.CompareTag("AirEnemy"))
        {
            EnemyParent enemy = other.GetComponent<EnemyParent>();
            enemy.TakeDamage(damageToEnemy);

            ClassRefrencer.instance.objectPoolingManager.AddObjectBackToQueue(transform.tag, gameObject);
        }
    }

    private IEnumerator DeactivateBulletAfterDelay()
    {
        yield return new WaitForSeconds(2);
        ClassRefrencer.instance.objectPoolingManager.AddObjectBackToQueue(transform.tag, gameObject);

    }

    #endregion

}
