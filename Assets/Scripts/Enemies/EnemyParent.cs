using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyParent: MonoBehaviour
{
    [Header("Path data")]
    [SerializeField] private Vector3[] path;
    [SerializeField] private int waypointIndex;

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
    public Rigidbody rigidBody;
    public ParticleSystem deathParticle;


    public bool IsAlive => health > 0;

    internal Vector3 nextTargetPos;


    #region public functions
    public virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    public virtual EnemyParent Init(int _moveSpeed, int _health, Vector3[] _path)
    {
        moveSpeed = _moveSpeed;
        health = _health;
        path = _path;

        ResetDataOnSummon();

        return this;
    }

    public virtual void ResetDataOnSummon()
    {
        enemyCollider.enabled = true;
        transform.localPosition = path[0];
        waypointIndex = 0;
        nextTargetPos = path[0];
    }

    public abstract void Move();
    public abstract void TakeDamage(float amount);
    public abstract void OnDeath();

    #endregion

    #region private fundtions
    internal virtual void MoveWaypoint()
    {
        if (waypointIndex >= path.Length - 1)
        {
            ClassRefrencer.instance.enemyManager.playerCore.TakeDamage(DamageToCore);
            ClassRefrencer.instance.enemyManager.allEnemiesSpawned.Remove(this);
            ClassRefrencer.instance.waveManager.CheckCanAdvanceWave();

            ClassRefrencer.instance.objectPoolingManager.AddObjectBackToQueue(transform.tag, gameObject);
            return;
        }

        waypointIndex++;
        nextTargetPos = path[waypointIndex];
    }
    internal void CallDeactivateObjectWithDelay()
    {
        Invoke(nameof(DeactivateObjectWithDelay), 1.5f);
    }
    private void DeactivateObjectWithDelay()
    {
        ClassRefrencer.instance.objectPoolingManager.AddObjectBackToQueue(transform.tag, gameObject);
    }

    #endregion
}
