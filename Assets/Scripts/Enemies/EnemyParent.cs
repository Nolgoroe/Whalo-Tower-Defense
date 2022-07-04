using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyParent: MonoBehaviour
{
    [Header("Path data")]
    public Vector3[] path;
    public Vector3 nextTargetPos;
    public int waypointIndex;

    [Header("Enemy stats data")]
    public int moveSpeed;
    public float health;
    public int DamageToCore;
    public EnemyTypes typeOfEnemy;

    [Header("On death data")]
    public int scoreToGiveOnDeath;
    public int fundsToGiveOnDeath;

    [Header("Enemy components")]
    public Animator anim;
    public Transform hitPoint;
    public Collider enemyCollider;


    public bool IsAlive => health > 0;

    public virtual EnemyParent Init(int _moveSpeed, int _health, Vector3[] _path)
    {
        moveSpeed = _moveSpeed;
        health = _health;
        path = _path;

        transform.localPosition = path[0];

        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider>();
        return this;
    }

    public abstract void Move();
    public virtual void MoveWaypoint()
    {
        if (waypointIndex >= path.Length - 1)
        {
            ClassRefrencer.instance.enemyManager.playerCore.TakeDamage(DamageToCore);
            ClassRefrencer.instance.enemyManager.allEnemiiesInGame.Remove(this);
            ClassRefrencer.instance.waveManager.CheckCanAdvanceWave();

            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        nextTargetPos = path[waypointIndex];
    }

    public abstract void TakeDamage(float amount);
    public abstract void OnDeath();

}
