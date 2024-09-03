using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class WeaponSpwaner : Singleton<WeaponSpwaner>
{
    [SerializeField] private int initCrateCount = 5;
    
    [SerializeField] private SpawnPrefab[] spawnPrefabArray;
    [Serializable]
    private struct SpawnPrefab
    {
        [SerializeField] internal WeaponType weaponType;
        [SerializeField] internal GameObject prefab;
    }
    
    private Dictionary<WeaponType, Queue<GameObject>> weaponQueueDictionary = new();

    void Start()
    {
        InitCreateInstantiate();
    }

    private void InitCreateInstantiate()
    {
        for (int i = 0; i < spawnPrefabArray.Length; i++)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int j = 0; j < initCrateCount; j++)
            {
                AddInstantiateToQueue(queue, spawnPrefabArray[i].prefab);
            }
            weaponQueueDictionary.Add(spawnPrefabArray[i].weaponType, queue);
        }
    }

    private void AddInstantiateToQueue(Queue<GameObject> queue, GameObject weapon, 
        Transform parent = null, Vector3 position = default, Quaternion rotation = default)
    { 
        GameObject instantiate = Instantiate(weapon, position, rotation, parent);
        instantiate.name = weapon.name;
        instantiate.SetActive(false);
        queue.Enqueue(instantiate);
    }

    public GameObject GetWeapon(WeaponType weaponType)
    {
        Queue<GameObject> queue = weaponQueueDictionary[weaponType];
        GameObject weapon;
        if (queue.Count <= 1)
        {
            weapon = queue.Dequeue();
            AddInstantiateToQueue(queue, weapon);
        }
        else
        {
            weapon = queue.Dequeue();
        }
        weapon.SetActive(true);
        
        return weapon;
    }

    public void RestoreWeapon(WeaponType weaponType, GameObject weapon)
    {
        Queue<GameObject> queue = weaponQueueDictionary[weaponType];
        weapon.SetActive(false);
        queue.Enqueue(weapon);
    }
}
