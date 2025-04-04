using UnityEditor;
using UnityEngine;

public class BattleWaveCreator : MonoBehaviour
{
    [ContextMenu("CREATE BATTLE WAVE PRESET")]
    public void CreateBattleWavePreset()
    {
        BattleWavePreset newPreset = ScriptableObject.CreateInstance<BattleWavePreset>();

        foreach (Transform child in transform)
        {
            if (child.GetComponent<Character>() == null)
                continue;
            GameObject prefab = GetPrefab(child.gameObject);
            if (prefab != null)
            {
                newPreset.AddMonster(prefab, child);
            }
        }
        string path = "Assets/Script/BattleSystem/New_BattleWavePreset.asset"; // save Path
        AssetDatabase.CreateAsset(newPreset, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newPreset;
    }

    GameObject GetPrefab(GameObject obj)
    {
    #if UNITY_EDITOR
            return UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(obj);
    #else
            return null;
    #endif
    }
}
