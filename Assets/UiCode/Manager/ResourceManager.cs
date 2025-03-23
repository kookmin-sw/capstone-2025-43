using UnityEditor;
using UnityEngine;

public class ResourceManager
{
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath($"Prefabs/{path}", typeof(GameObject));

        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent); //Object�� �ִ� Instantiate()ȣ��
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            return;
        }

        Object.Destroy(go);
    }
}
