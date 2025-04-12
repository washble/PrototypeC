

public class SwordWeapon : WeaponBase
{
    protected override void Start()
    {
        base.Start();

        InitSettings();
    }
    
    private void InitSettings()
    {
        ChangeCurWeaponAttackType(0);
    }

    protected override void Attack(int attackCount)
    {
        
    }
}
