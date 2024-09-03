using UnityEngine;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private WeaponType weaponType;

    [SerializeField] private float speed = 3.5f;

    private bool isUsed = false;
    
    private void OnEnable()
    {
        isUsed = true;
        Fire().Forget();
    }

    private void OnDisable()
    {
        isUsed = false;
    }

    private async UniTaskVoid Fire()
    {
        while (isUsed)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case (int)GameObjectLayer.Map:
                WeaponSpwaner.Instance.RestoreWeapon(weaponType, gameObject);
                break;
            case (int)GameObjectLayer.Player:
                WeaponSpwaner.Instance.RestoreWeapon(weaponType, gameObject);
                break;
        }
    }
}
