using UnityEngine;

public class GunslingerEnemy : Enemy
{
    [SerializeField] private Transform weaponGrabTransform;
    
    internal IEnemyMove curMove;
    internal IEnemyMove moveIdle;
    internal IEnemyMove moveRun;
    internal IEnemyMove moveDash;
    internal IEnemyMove moveAttack;
    internal IEnemyMove moveDamaged;
    internal IEnemyMove moveDie;

    protected override void Start()
    {
        base.Start();
        
        EnemyMoveSettings();
    }
    
    private void EnemyMoveSettings()
    {
        moveIdle = new GunslingerEnemyMoveIdle();
        moveIdle.SetEnemy(this);
        moveRun = new GunslingerEnemyMoveRun();
        moveRun.SetEnemy(this);
        moveDash = new GunslingerEnemyMoveDash();
        moveDash.SetEnemy(this);
        moveAttack = new GunslingerEnemyMoveAttack();
        moveAttack.SetEnemy(this);
        moveDamaged = new GunslingerEnemyMoveDamaged();
        moveDamaged.SetEnemy(this);
        moveDie = new GunslingerEnemyMoveDie();
        moveDie.SetEnemy(this);

        curMove = moveIdle;
    }

    private void Update()
    {
        curMove.Move();
    }

    internal void DamagedEnd(float health)
    {
        curMove = health > 0 ? moveIdle : moveDie;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        switch (otherGameObject.layer)
        {
            case (int)GameObjectLayer.Weapon:
                if (otherGameObject.CompareTag(GameObjectTag.Player.ToString()))
                {
                    curMove = moveDamaged;
                }
                break;
        }
    }
}
