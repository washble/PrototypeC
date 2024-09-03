using UnityEngine;

public class PlayerRifleWeapon : Weapon
{
    [Header("[Fire]")]
    [SerializeField] private Transform fireTransform;
    
    protected override void Start()
    {
        base.Start();
        
        InitSettings();
    }

    private void InitSettings()
    {
        if (!fireTransform)
        {
            fireTransform = transform;
        }
        ChangeCurWeaponAttackType(0);
    }

    protected override void Attack(int attackCount)
    {
        GameObject bullet = WeaponSpwaner.Instance.GetWeapon(weaponType);
        bullet.transform.SetPositionAndRotation(
            fireTransform.position, fireTransform.rotation
        );
        bullet.SetActive(true);
    }

    public override void AttackStop()
    {
        curAttackState = AttackState.Idle;
    }
}
