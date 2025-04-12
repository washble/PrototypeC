using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WeaponAttackType", menuName = "Scriptable Object/Weapon Attack Type", order = 0)]
public class WeaponAttackTypeSO : ScriptableObject
{
    [SerializeReference] public IAttackDetail[] attackDetails;
    
    public interface IAttackDetail
    {
        public AttackDetailType AttackDetailType { get; set; }
    }

    [Serializable]
    public struct DelayTime : IAttackDetail
    {
        [SerializeField, ReadOnlyInspector] private AttackDetailType attackDetailType;
        public AttackDetailType AttackDetailType
        {
            get => attackDetailType;
            set => attackDetailType = value;
        }
        
        [SerializeField] public int delayMilliSecond;
    }
    
    [Serializable]
    public struct AttackCount : IAttackDetail
    {
        [SerializeField, ReadOnlyInspector] private AttackDetailType attackDetailType;
        public AttackDetailType AttackDetailType
        {
            get => attackDetailType;
            set => attackDetailType = value;
        }
        
        [SerializeField] public int count;
    }
}
