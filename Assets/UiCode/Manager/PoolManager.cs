using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

public class PoolManager
{
    GameObject root;

    [Header("#Enemy Pool")]
    public const int wavesCount = 10;
    public List<UnitData> bossData;
    public List<BattleWavePreset>[] waves = new List<BattleWavePreset>[wavesCount];

    [Header("#Hero Pool")]
    [SerializeField]
    List<string> heros = new List<string>();
    public Dictionary<string, GameObject> heroPool = new Dictionary<string, GameObject>();

    public void Init()
    {
        root = new GameObject() { name = "@Pool_Root" };
        Object.DontDestroyOnLoad(root);
        SetHeroList();
    }

    public List<BattleWavePreset> GetCreepPool()
    {
        return waves[Random.Range(0, 10)];
    }

    public void SetHeroList()
    {
        heroPool.Clear();
        foreach (string name in heros)
        {
            Push(name);
        }
    }

    public void Push(string name)
    {
        if (!heroPool.ContainsKey(name))
            heroPool.Add(name, Managers.Resource.Instantiate(name));
        heroPool[name].transform.parent = root.transform;
        heroPool[name].SetActive(false);
    }
    public GameObject Pop(Transform user, string name)
    {
        if (!heroPool.ContainsKey(name))
            Push(name);
        heroPool[name].transform.parent = user;
        heroPool[name].SetActive(true);
        return heroPool[name];
    }
}
