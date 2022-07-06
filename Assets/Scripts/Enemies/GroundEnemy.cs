using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : EnemyParent
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

        anim.SetBool("Take Damage", true);

        if (!IsAlive)
        {
            OnDeath();
        }
    }

    [ContextMenu("Kill enemy manually")]
    public override void OnDeath()
    {
        enemyCollider.enabled = false;

        anim.SetTrigger("Die");
        deathParticle.Play();

        ClassRefrencer.instance.gameManager.playerState.AddScore(scoreToGiveOnDeath);
        ClassRefrencer.instance.gameManager.playerState.AddFunds(fundsToGiveOnDeath);

        ClassRefrencer.instance.enemyManager.allEnemiesSpawned.Remove(this);

        ClassRefrencer.instance.waveManager.CheckCanAdvanceWave();


        moveSpeed = 0;

        CallDeactivateObjectWithDelay();

        Debug.LogError("DEAD!");
    }

    public override void ResetDataOnSummon()
    {
        base.ResetDataOnSummon();
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
