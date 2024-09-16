using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class EnemySpawner : Singleton<EnemySpawner>
{
    // Temp Simple Settings
    [SerializeField] private Enemy[] enemies;
    public Enemy[] Enemies => enemies;
}
