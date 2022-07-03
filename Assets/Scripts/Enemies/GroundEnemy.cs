using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : EnemyParent
{
    public ParticleSystem deathParticle;

    private void Start()
    {
        if (path.Length > 0)
        {
            nextTargetPos = path[0];
        }
        else
        {
            Debug.LogError("No path!");
        }
    }

    private void Update()
    {
        if (!IsAlive)
        {
            return;
        }

        Move();
    }
    public override void Move()
    {
        Vector3 moveDir = nextTargetPos - transform.localPosition;

        transform.Translate(moveDir.normalized * moveSpeed * Board.boardSize * Time.deltaTime, Space.World);

        DoRotation(moveDir);

        if (Vector3.Distance(transform.localPosition, nextTargetPos) <= 0.2f)
        {
            MoveWaypoint();
        }
    }

    private void DoRotation(Vector3 _moveDir)
    {
        Quaternion rotate = Quaternion.LookRotation(_moveDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, 10);
    }

    [ContextMenu("take dmg")]
    public override void TakeDamage()
    {
        health -= 10;

        anim.SetBool("Take Damage", true);

        if (!IsAlive)
        {
            OnDeath();
        }
    }

    public override void OnDeath()
    {
        anim.SetTrigger("Die");
        deathParticle.Play();

        ClassRefrencer.instance.gameManager.playerState.AddScore(scoreToGiveOnDeath);
        ClassRefrencer.instance.gameManager.playerState.AddFunds(fundsToGiveOnDeath);
        ClassRefrencer.instance.waveManager.CheckCanAdvanceWave();

        ClassRefrencer.instance.enemyManager.allEnemiiesInGame.Remove(this);

        moveSpeed = 0;

        Destroy(gameObject, 3);
        Debug.LogError("DEAD!");
    }

}
