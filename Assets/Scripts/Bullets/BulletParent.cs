using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletParent : MonoBehaviour
{
    public Transform targetAttack;
    public float moveSpeed;
    public float damageToEnemy;

    public Vector3 bulletDir;

    public float maxMoveSpeed;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    public void SetTarget(Transform _targetToAttack)
    {
        targetAttack = _targetToAttack;

        bulletDir = targetAttack.transform.position - transform.position;
    }

    private void Update()
    {
        if(targetAttack == null)
        {
            Destroy(gameObject);
            return;
        }


        transform.Translate(bulletDir.normalized * moveSpeed * Time.deltaTime, Space.World);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyParent enemy = other.GetComponent<EnemyParent>();
            enemy.TakeDamage(damageToEnemy);

            Destroy(gameObject);
        }
    }


    public abstract void AddPowerUpValues();
}
