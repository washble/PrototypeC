using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        
        Application.targetFrameRate = 60;
    }

    [SerializeField] private GameObject player;
    public GameObject Player => player;

    [SerializeField] private PlayerWeaponController playerWeaponController;
    public PlayerWeaponController PlayerWeaponController => playerWeaponController;
}

public enum GameObjectLayer
{
    Default = 0,
    Player = 3,
    Map = 6,
    Enemy = 7,
    Weapon = 8
}

public enum GameObjectTag
{
    Player,
    EGunslinger,
    CShielder
}

public enum WeaponType
{
    PlayerSword,
    PlayerRifleBullet,
    GunslingerBullet,
    CompanionShield,
    CompanionSword,
}