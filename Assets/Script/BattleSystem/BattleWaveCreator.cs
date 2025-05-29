#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class BattleWaveCreator : MonoBehaviour
{
    [ContextMenu("CREATE BATTLE WAVE PRESET")]
    public void CreateBattleWavePreset()
    {
#if UNITY_EDITOR
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

        string path = "Assets/Script/BattleSystem/New_BattleWavePreset.asset"; // save path
        AssetDatabase.CreateAsset(newPreset, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newPreset;
#endif
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
