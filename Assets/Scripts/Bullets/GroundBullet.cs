using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBullet : BulletParent
{
    public override void AddPowerUpValues()
    {
        damageToEnemy += ClassRefrencer.instance.powerupManager.groundDMGModifier;
    }
}
