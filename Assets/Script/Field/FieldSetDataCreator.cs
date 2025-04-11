using UnityEngine;

using UnityEditor;
using MyProject.Utils;

public class FieldSetDataCreator : MonoBehaviour
{
    [ContextMenu("CREATE FIELD SET DATA")]
    public void CreateFieldSetData()
    {
        FieldSetData newFieldSet = ScriptableObject.CreateInstance<FieldSetData>();

        Transform environment = transform.Find("Environment");
        Transform battleField = transform.Find("BattleField");

        if (environment == null || battleField == null)
        {
            Debug.LogError("[FieldSetDataCreator] There is No 'Environment' or 'BattleField' in the  child");
            return;
        }

        // Prefab ¿¬°á
        GameObject environmentPrefab = GetPrefab(environment.gameObject);
        GameObject battlePrefab = GetPrefab(battleField.gameObject);

        if (environmentPrefab == null || battlePrefab == null)
        {
            Debug.LogError("[FieldSetDataCreator] Cant Find Prefabs");
            return;
        }

        // Set Data
        newFieldSet.environmentPrefab = environmentPrefab;
        newFieldSet.environmentPosition = environment.localPosition;
        newFieldSet.environmentRotation = environment.localRotation;
        newFieldSet.environmentScale = environment.localScale;

        newFieldSet.battleFieldPrefab = battlePrefab;
        newFieldSet.battlePosition = battleField.localPosition;
        newFieldSet.battleRotation = battleField.localRotation;
        newFieldSet.battleScale = battleField.localScale;

        // Set FieldType Empty
        newFieldSet.fieldType = E_FieldType.Empty; 

        // Save
        string path = $"Assets/Script/Field/New_FieldSet_{System.DateTime.Now:yyyyMMdd_HHmmss}.asset";
        AssetDatabase.CreateAsset(newFieldSet, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newFieldSet;
    }

    GameObject GetPrefab(GameObject obj)
    {
#if UNITY_EDITOR
        return PrefabUtility.GetCorrespondingObjectFromSource(obj);
#else
        return null;
#endif
    }
}

