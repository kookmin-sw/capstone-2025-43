using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectPoolManager : MonoBehaviour
{
    public static EffectPoolManager Instance { get; private set; }
    private GameObject rootObj;

    [System.Serializable]
    public class EffectPrefab
    {
        public string effectName;
        public GameObject prefab;
        public int initialPoolSize = 5;
    }
    //List For Edit Prefabs in the unity editor
    public List<EffectPrefab> effectPrefabs = new List<EffectPrefab>();

    private Dictionary<string, GameObject> effectPrefabsDict = new Dictionary<string, GameObject>();
    private Dictionary<string, Queue<GameObject>> effectPools = new Dictionary<string, Queue<GameObject>>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePools();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePools()
    {
        rootObj = new GameObject();
        rootObj.name = "VFXPools";

        foreach (EffectPrefab effect in effectPrefabs)
        {
            RegisterEffect(effect.effectName, effect.prefab, effect.initialPoolSize);
        }
    }

    public void RegisterEffect(string effectName, GameObject prefab, int initialPoolSize)
    {
        if (!effectPools.ContainsKey(effectName))
        {
            effectPools[effectName] = new Queue<GameObject>();
            effectPrefabsDict[effectName] = prefab;
        }

        GameObject go = new GameObject();
        go.name = effectName;
        go.transform.parent = rootObj.transform;

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject effect = Instantiate(prefab);
            effect.name = effectName;
            effect.SetActive(false);
            effect.transform.SetParent(go.transform);
            PoolEffect pool = effect.GetOrAddComponent<PoolEffect>();
            pool.Initialize(effectName, go.transform);
            effectPools[effectName].Enqueue(effect);
        }
    }

    public GameObject GetEffect(string effectName, Vector3 position)
    {
        if (!effectPools.ContainsKey(effectName))
        {
            Debug.LogWarning($"Effect {effectName} is not registered in the pool.");
            return null;
        }

        GameObject effect;
        if (effectPools[effectName].Count > 0)
        {
            effect = effectPools[effectName].Dequeue();
        }
        else
        {
            RegisterEffect(effectName, effectPrefabsDict[effectName], 5);
            effect = Instantiate(effectPrefabsDict[effectName]);
        }

        effect.transform.position = position;
        effect.SetActive(true);
        return effect;
    }

    public void ReturnEffect(string effectName, GameObject effect)
    {
        if (effectPools.ContainsKey(effectName))
        {
            effect.SetActive(false);
            effectPools[effectName].Enqueue(effect);
        }
    }
}