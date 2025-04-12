using UnityEngine;
using UnityEngine.Serialization;

public class PlayerWeaponController : MonoBehaviour
{
    [FormerlySerializedAs("weapon")]
    [Header("[Weapon]")] 
    [SerializeField] private WeaponBase weaponBase;
    internal WeaponBase WeaponBase => weaponBase;

    [Header("[State]")]
    [SerializeField] public float speed  = 3;
    [SerializeField] public float shield = 5;
    [SerializeField] public float damage = 5;
    [SerializeField] public float attackDistance = 10;
}
