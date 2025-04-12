using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializeDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver
{
    [SerializeField]
    private DataType[] dicData;

    [Serializable]
    public struct DataType
    {
        public K key;
        public V value;
    }

    public void OnBeforeSerialize()
    {
        if (!Application.isPlaying) { return; }

        RefreshDataType();
    }

    public void OnAfterDeserialize()
    {
        Clear();
        for (int i = 0; i < dicData.Length; i++)
        {
            Add(dicData[i].key, dicData[i].value);
        }
    }

    public DataType[] GetDataType()
    {
        return dicData;
    }

    public void RefreshDataType()
    {
        if (this.Count != dicData.Length) { dicData = new DataType[this.Count]; }
        int num = 0;
        foreach (var keyValuePair in this)
        {
            dicData[num++] = new DataType
            {
                key = keyValuePair.Key,
                value = keyValuePair.Value
            };
        }
    }
}