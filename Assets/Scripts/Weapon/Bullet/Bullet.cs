using UnityEngine;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletAttackTypeSO attackTypeSo;

    private bool isUsed = false;
    
    private void OnEnable()
    {
        isUsed = true;
        Fire().Forget();

        if (attackTypeSo.useLifeTime)
        {
            LifeTime().Forget();
        }
    }

    private void OnDisable()
    {
        isUsed = false;
    }

    private async UniTaskVoid Fire()
    {
        while (isUsed)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * attackTypeSo.speed);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }

    private async UniTaskVoid LifeTime()
    {
        float duration = 0;
        
        while (isUsed)
        {
            duration += Time.deltaTime;
            if (duration > attackTypeSo.lifeTime)
            {
                WeaponSpwaner.Instance.RestoreWeapon(attackTypeSo.weaponType, gameObject);
            }

            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case (int)GameObjectLayer.Map:
                WeaponSpwaner.Instance.RestoreWeapon(attackTypeSo.weaponType, gameObject);
                break;
            case (int)GameObjectLayer.Player:
                WeaponSpwaner.Instance.RestoreWeapon(attackTypeSo.weaponType, gameObject);
                break;
        }
    }
}
