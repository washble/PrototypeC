using UnityEngine;

[CreateAssetMenu(fileName = "BulletAttackType", menuName = "Weapon/Bullet Attack Type", order = 0)]
public class BulletAttackTypeSO : ScriptableObject
{
    public WeaponType weaponType;
    
    [Space(10)]
    public float speed = 10f;
    
    [Space(10)]
    public bool useLifeTime = true;
    public ushort lifeTime = 0;
}
