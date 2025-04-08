using UnityEngine;
using System.Collections.Generic;
using MyProject.Utils;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> BattleFieldPrefabs;

    [System.Serializable]
    public class FieldPrefabGroup
    {
        public List<GameObject> prefabs = new List<GameObject>();
    }

    [SerializeField] private List<FieldPrefabGroup> EnvironmentFieldPrefabs = new List<FieldPrefabGroup>();

    private void Awake()
    {
        int enumCount = (int)E_FieldType.Max;
        while (EnvironmentFieldPrefabs.Count < enumCount)
        {
            EnvironmentFieldPrefabs.Add(new FieldPrefabGroup());
        }
    }

    // 사용 예시
    public List<GameObject> GetPrefabs(E_FieldType type)
    {
        return EnvironmentFieldPrefabs[(int)type].prefabs;
    }

    void Start()
    {
        // TODO :: Get MapNode's FieldType and Apply it
        var testPrefab = GetPrefabs(E_FieldType.Desert);
        if (testPrefab.Count > 0)
        {
            Instantiate(testPrefab[0]);
        }
    }

    void Update()
    {

    }
}
