using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("[Weapon]")] 
    [SerializeField] private Weapon weapon;
    internal Weapon Weapon => weapon;

    [Header("[State]")]
    [SerializeField] public float speed  = 3;
    [SerializeField] public float shield = 5;
    [SerializeField] public float damage = 5;
    [SerializeField] public float attackDistance = 10;
}
