using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class EnemyManager : Singleton<EnemyManager>
{
    public Dictionary<GameObjectTag, float> enemyDamage = new();
}
