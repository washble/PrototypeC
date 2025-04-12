// Interface Indicators Collection
using System;
using UnityEngine;

// Example of using SampleInterfaceProvider
#if UNITY_EDITOR
public class InterfaceProviderGroup : MonoBehaviour
{
    [SerializeField] private SampleInterfaceProvider sampleInterface; // Sample using interface provider
}
#endif

// Sample Interface and InterfaceProvider
#if UNITY_EDITOR
public interface ISampleInterface { }
[Serializable] public class SampleInterfaceProvider : InterfaceProvider<ISampleInterface> { }
#endif

// Write additional interface providers below here
