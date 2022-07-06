using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [Header("Funds per Power")]
    [SerializeField] private int missileAttackSpeedCost;
    [SerializeField] private int groundAttackSpeedCost;
    [SerializeField] private int missileSpeedCost;
    [SerializeField] private int groundDMGCost;
    [SerializeField] private int airDMGCost;
    [SerializeField] private int groundRangeCost;
    [SerializeField] private int airRangeCost;

    [Header("powerups to add per buy")]
    [SerializeField] private float missileAttackSpeedAmountAdd;
    [SerializeField] private float groundAttackSpeedAmountAdd;
    [SerializeField] private float missileSpeedAmountAdd;
    [SerializeField] private float groundDMGAmountAdd;
    [SerializeField] private float airDMGAmountAdd;
    [SerializeField] private float groundRangeAmountAdd;
    [SerializeField] private float airRangeAmountAdd;

    [Header("Added powerups")]
    public float airAttackSpeedModifier;
    public float groundAttackSpeedModifier;
    public float missileSpeedModifier;
    public float groundDMGModifier;
    public float airDMGModifier;
    public float groundRangeModifier;
    public float airRangeModifier;



    private void Start()
    {
        airAttackSpeedModifier = 0;
        groundAttackSpeedModifier = 0;
        missileSpeedModifier = 0;
        groundDMGModifier = 0;
        airDMGModifier = 0;
        groundRangeModifier = 0;
        airRangeModifier = 0;
    }

    #region button functions
    public void AddAirAttackSpeed()
    {
        bool canBuyTower = ClassRefrencer.instance.gameManager.playerState.TryTakeFunds(missileAttackSpeedCost);

        if (canBuyTower)
        {
            airAttackSpeedModifier += missileAttackSpeedAmountAdd;
        }
        else
        {
            ClassRefrencer.instance.announcementManager.Show("Need more funds!", 1, ClassRefrencer.instance.announcementManager._fundstext);
        }
    }
    public void AddGroundAttackSpeed()
    {
        bool canBuyTower = ClassRefrencer.instance.gameManager.playerState.TryTakeFunds(groundAttackSpeedCost);

        if (canBuyTower)
        {
            groundAttackSpeedModifier += groundAttackSpeedAmountAdd;
        }
        else
        {
            ClassRefrencer.instance.announcementManager.Show("Need more funds!", 1, ClassRefrencer.instance.announcementManager._fundstext);
        }
    }
    public void AddMissileSpeed()
    {
        bool canBuyTower = ClassRefrencer.instance.gameManager.playerState.TryTakeFunds(missileSpeedCost);

        if (canBuyTower)
        {
            missileSpeedModifier += missileSpeedAmountAdd;
        }
        else
        {
            ClassRefrencer.instance.announcementManager.Show("Need more funds!", 1, ClassRefrencer.instance.announcementManager._fundstext);
        }
    }
    public void AddGroundDMG()
    {
        bool canBuyTower = ClassRefrencer.instance.gameManager.playerState.TryTakeFunds(groundDMGCost);

        if (canBuyTower)
        {
            groundDMGModifier += groundDMGAmountAdd;
        }
        else
        {
            ClassRefrencer.instance.announcementManager.Show("Need more funds!", 1, ClassRefrencer.instance.announcementManager._fundstext);
        }
    }
    public void AddAirDMG()
    {
        bool canBuyTower = ClassRefrencer.instance.gameManager.playerState.TryTakeFunds(airDMGCost);

        if (canBuyTower)
        {
            airDMGModifier += airDMGAmountAdd;
        }
        else
        {
            ClassRefrencer.instance.announcementManager.Show("Need more funds!", 1, ClassRefrencer.instance.announcementManager._fundstext);
        }
    }
    public void AddGroundRange()
    {
        bool canBuyTower = ClassRefrencer.instance.gameManager.playerState.TryTakeFunds(groundRangeCost);

        if (canBuyTower)
        {
            groundRangeModifier += groundRangeAmountAdd;
        }
        else
        {
            ClassRefrencer.instance.announcementManager.Show("Need more funds!", 1, ClassRefrencer.instance.announcementManager._fundstext);
        }
    }
    public void AddAirRange()
    {
        bool canBuyTower = ClassRefrencer.instance.gameManager.playerState.TryTakeFunds(airRangeCost);

        if (canBuyTower)
        {
            airRangeModifier += airRangeAmountAdd;
        }
        else
        {
            ClassRefrencer.instance.announcementManager.Show("Need more funds!", 1, ClassRefrencer.instance.announcementManager._fundstext);
        }
    }
    #endregion
}
