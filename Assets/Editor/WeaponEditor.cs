using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponAttackTypeSO))]
public class WeaponEditor : Editor
{
    private WeaponAttackTypeSO weaponAttackTypeSO;
    private WeaponAttackTypeSO.IAttackDetail[] attackDetails;
    
    private void OnEnable()
    {
        weaponAttackTypeSO = target as WeaponAttackTypeSO;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        attackDetails = weaponAttackTypeSO.attackDetails;

        EditorGUILayout.HelpBox(
            "You need to press the button to add an element to get in properly",
             MessageType.Info    
        );
        
        DrawDefaultInspector();
        
        serializedObject.ApplyModifiedProperties();
        
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Add DelayTime"))
        {
            AddDelayTime();
        }
        
        if (GUILayout.Button("Add AttackCount"))
        {
            AddAttackCount();
        }
        
        GUILayout.EndHorizontal();
    }
    
    private void AddDelayTime()
    {
        WeaponAttackTypeSO.DelayTime delayTime = new WeaponAttackTypeSO.DelayTime();
        delayTime.AttackDetailType = AttackDetailType.Delay;
        
        weaponAttackTypeSO.attackDetails 
            = AddElementToArray(attackDetails, delayTime);
    }
    
    private void AddAttackCount()
    {
        WeaponAttackTypeSO.AttackCount attackCount = new WeaponAttackTypeSO.AttackCount();
        attackCount.AttackDetailType = AttackDetailType.AttackCount;
        
        weaponAttackTypeSO.attackDetails 
            = AddElementToArray(attackDetails, attackCount);
    }
    
    private T[] AddElementToArray<T>(T[] array, T element)
    {
        T[] newArray = new T[array.Length + 1];
        
        for (int i = 0; i < array.Length; i++)
        {
            newArray[i] = array[i];
        }

        newArray[^1] = element;
        
        return newArray;
    }

    private T[] RemoveElementToArray<T>(T[] array, int index)
    {
        T[] newArray = new T[array.Length - 1];

        for (int i = 0; i < array.Length; i++)
        {
            if(i == index) continue;
            newArray[i] = array[i];
        }
        
        return newArray;
    }
}
