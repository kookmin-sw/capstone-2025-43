using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PoolManager
{
    GameObject root;

    [Header("#Enemy Pool")]
    public const int wavesCount = 10;
    public List<UnitData> bossData;
    public BattleWavePreset[] waves = null;

    [Header("#Hero Pool")]
    string[] heros = { "Paladin", "Wizard" ,"HeroMage", "Werewolf", "NagaWizard", "BlackKnight", "FishMan"};
    public Dictionary<string, GameObject> heroPool = new Dictionary<string, GameObject>();

    public void Init()
    {
        root = new GameObject() { name = "@Pool_Root" };
        Object.DontDestroyOnLoad(root);
        SetHeroList();
        CreateBattleWave();
    }

    public void CreateBattleWave()
    {
        waves = Managers.Data.GetBattleWaveDataset("BattleWave");
    }


    public BattleWavePreset GetCreepPool()
    {
        if(waves == null)
            return null;
        return waves[Random.Range(0, waves.Length)];
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
        Debug.Log(name);
        if (!heroPool.ContainsKey(name))
        {
            AsyncOperationHandle handle = Addressables.LoadAssetAsync<GameObject>(name);
            handle.WaitForCompletion();

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"[Hero Init] Failed to load hero prefab: {name}");
                return;
            }
            else
            {
                heroPool.Add(name, Object.Instantiate((GameObject)handle.Result));
            }
        }
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
