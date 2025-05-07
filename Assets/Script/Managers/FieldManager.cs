using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MyProject.Utils;
using Unity.AI.Navigation;

public class FieldManager : MonoBehaviour
{
    public static FieldManager Instance { get; private set; }

    [Header("Field Data")]
    [SerializeField] private List<FieldSetData> fieldSetDataList;

    private GameObject currentEnvironment;
    private GameObject currentBattle;

    [Header("NavMesh Surface")]
    [SerializeField] private NavMeshSurface navMeshSurface;

    [SerializeField] private Material TransParentMaterial;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
    }
    public void InitializeRandomField(E_FieldType type)
    {
        ClearField();

        List<FieldSetData> matchingFields = fieldSetDataList.Where(f => f.fieldType == type).ToList();

        if (matchingFields.Count == 0)
        {
            Debug.LogWarning($"[FieldManager] ({type}) : Has No FieldSetData(Scriptable Object).");
            return;
        }

        int randomIndex = Random.Range(0, matchingFields.Count);
        FieldSetData selectedField = matchingFields[randomIndex];

        currentEnvironment = Instantiate(selectedField.environmentPrefab);
        ApplyTransform(currentEnvironment.transform, selectedField.environmentPosition, selectedField.environmentRotation, selectedField.environmentScale);

        currentBattle = Instantiate(selectedField.battleFieldPrefab);
        ApplyTransform(currentBattle.transform, selectedField.battlePosition, selectedField.battleRotation, selectedField.battleScale);

        BattleManager.Instance.InitializeFlag(currentBattle);

        ApplyNavigation();
        ApplyFieldTransParency();
    }
    public void InitializeRandomField(string envName)
    {
        E_FieldType type = E_FieldType.Empty;
        switch (envName)
        {
            case "Forest":
                type = E_FieldType.Forest;
                break;
            case "Mount":
                type = E_FieldType.Forest;
                break;
            case "Snow":
                type = E_FieldType.Snow;
                break;
            case "Desert":
                type = E_FieldType.Desert;
                break;
            default:
                type = E_FieldType.Test;
                break;
        }
        InitializeRandomField(type);
    }


    private void ApplyNavigation()
    {
        if (navMeshSurface != null)
        {
            navMeshSurface.transform.position = currentBattle.transform.position;
            navMeshSurface.BuildNavMesh();
            Debug.Log("[FieldManager] NavMeshSurface Bake");
        }
        else
        {
            Debug.LogError("[FieldManager] there is no NavMeshSurface!");
        }
    }
    private void ApplyTransform(Transform target, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        target.localPosition = pos;
        target.localRotation = rot;
        target.localScale = scale;
    }

    public void ClearField()
    {
        if (currentEnvironment != null) Destroy(currentEnvironment);
        if (currentBattle != null) Destroy(currentBattle);
        currentEnvironment = null;
        currentBattle = null;
    }
    public void ApplyFieldTransParency()
    {
        if (currentBattle == null || TransParentMaterial == null)
        {
            Debug.LogWarning("[FieldManager] Cannot apply transparency. currentBattle or TransParentMaterial is null.");
            return;
        }

        // 모든 자식 오브젝트의 MeshRenderer 찾기
        MeshRenderer[] meshRenderers = currentBattle.GetComponentsInChildren<MeshRenderer>(true);

        foreach (MeshRenderer renderer in meshRenderers)
        {
            // 기존 머테리얼 수만큼 투명 머테리얼 적용 (멀티 머테리얼 대응)
            Material[] transparentMats = Enumerable.Repeat(TransParentMaterial, renderer.sharedMaterials.Length).ToArray();
            renderer.materials = transparentMats;
        }

        Debug.Log("[FieldManager] Applied transparency to battlefield objects.");
    }
}
