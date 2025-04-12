
public class PlayerSwordWeapon : WeaponBase
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
        Debug.Log($"Sword Attack!!!!");
    }

    public override void AttackStop()
    {
        CurAttackState = AttackState.Idle;
    }
}
