using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using static UnityEngine.Analytics.IAnalytic;

public class PoolManager : MonoBehaviour
{
    [Header("#Enemy Pool")]
    public List<List<Dictionary<UnitData, int>>> creepComb;
    public List<UnitData> bossData;

    [Header("#Hero Pool")]
    public List<UnitData> ownHeroData;
    public List<UnitData> onSaleHeroData;


    public List<Dictionary<UnitData,int>> GetCreepPool()
    {
        if (creepComb.Count == 0)
            return null;
        return creepComb[Random.Range(0, creepComb.Count - 1)];
    }

    public void SetHeroList()
    {
        UnitData[] dataList = Managers.instance.dataManager.GetUnitDataset("Ally");
        foreach (UnitData data in dataList)
        {
            if (data.own)
                ownHeroData.Add(data);
            else
                onSaleHeroData.Add(data);
        }
    }


}
