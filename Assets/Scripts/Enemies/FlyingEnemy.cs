using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyParent
{
    #region public functions
    public override void Start()
    {
        base.Start();
    }
    public override void Move()
    {
        Vector3 moveDir = nextTargetPos - transform.localPosition;

        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.World);

        DoRotation(moveDir);

        if (Vector3.Distance(transform.localPosition, nextTargetPos) <= 0.2f)
        {
            MoveWaypoint();
        }
    }
    public override void TakeDamage(float amount)
    {
        health -= amount;

        if (!IsAlive)
        {
            OnDeath();
        }
    }
    [ContextMenu("Kill enemy manually")]
    public override void OnDeath()
    {
        anim.SetTrigger("Die");

        if (!deathParticle.isPlaying)
        {
            deathParticle.Play();
        }

        ClassRefrencer.instance.gameManager.playerState.AddScore(scoreToGiveOnDeath);
        ClassRefrencer.instance.gameManager.playerState.AddFunds(fundsToGiveOnDeath);

        ClassRefrencer.instance.enemyManager.allEnemiesSpawned.Remove(this);

        ClassRefrencer.instance.waveManager.CheckCanAdvanceWave();


        moveSpeed = 0;

        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;

        CallDeactivateObjectWithDelay();
        Debug.LogError("DEAD!");
    }
    public override void ResetDataOnSummon()
    {
        base.ResetDataOnSummon();
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
    }

    #endregion

    #region private functions
    private void Update()
    {
        if (!IsAlive)
        {
            return;
        }

        Move();
    }
    private void DoRotation(Vector3 _moveDir)
    {
        Quaternion rotate = Quaternion.LookRotation(_moveDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, 10);
    }

    #endregion
}
