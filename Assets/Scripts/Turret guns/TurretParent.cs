using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretParent : MonoBehaviour
{
    public EnemyParent target;
    public float range;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LookForTarget", 0, 0.5f);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range * Board.boardSize);
    }
}
