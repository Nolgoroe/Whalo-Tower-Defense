using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBullet : BulletParent
{
    public override void AddPowerUpValues()
    {
        moveSpeed += ClassRefrencer.instance.powerupManager.missileSpeedModifier;

        if(moveSpeed > maxMoveSpeed)
        {
            moveSpeed = maxMoveSpeed;
        }

        damageToEnemy += ClassRefrencer.instance.powerupManager.airDMGModifier;
    }
}
