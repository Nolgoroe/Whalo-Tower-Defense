using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTurrent : TurretParent
{
    public override void AddPowerUpValues()
    {
        attackSpeed -= ClassRefrencer.instance.powerupManager.airAttackSpeedModifier;

        if(attackSpeed < maxAttackSpeed)
        {
            attackSpeed = maxAttackSpeed;
        }

        range += ClassRefrencer.instance.powerupManager.airRangeModifier;

        if (range > maxRange)
        {
            range = maxRange;
        }
    }
}
