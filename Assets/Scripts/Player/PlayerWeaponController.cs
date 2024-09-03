using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("[Weapon]")] 
    [SerializeField] private Weapon weapon;
    internal Weapon Weapon => weapon;

    [field:Header("[State]")]
    [field:SerializeField] public float speed { get; set; } = 3;
    [field:SerializeField] public float shield { get; set; } = 5;
    [field:SerializeField] public float damage { get; set; } = 5;
    [field:SerializeField] public float attackDistance { get; set; } = 10;
}
