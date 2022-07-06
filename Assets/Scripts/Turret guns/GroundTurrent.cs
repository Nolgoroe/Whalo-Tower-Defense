using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTurrent : TurretParent
{
    public override void AddPowerUpValues()
    {
        attackSpeed -= ClassRefrencer.instance.powerupManager.groundAttackSpeedModifier;

        if (attackSpeed < maxAttackSpeed)
        {
            attackSpeed = maxAttackSpeed;
        }

        range += ClassRefrencer.instance.powerupManager.groundRangeModifier;

        if (range > maxRange)
        {
            range = maxRange;
        }
    }
}
