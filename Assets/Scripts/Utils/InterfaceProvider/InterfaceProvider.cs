using System;
using UnityEngine;

[Serializable]
public class InterfaceProvider<T>
{
    [SerializeField] private GameObject interfaceObject;

    private T value;

    public T Value
    {
        get
        {
            if (value == null)
            {
                if (!interfaceObject.TryGetComponent(out value))
                {
                    Debug.Log($"{typeof(T)} component not found on {interfaceObject.name}.");
                }
            }

            return value;
        }
    }
}
