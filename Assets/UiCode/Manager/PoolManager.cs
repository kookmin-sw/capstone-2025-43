using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using static UnityEngine.Analytics.IAnalytic;

public class PoolManager : MonoBehaviour
{
    [Header("#Enemy Pool")]
    public List<UnitData> bossData;
    public List<Dictionary<string, int>>[] creepComb;

    [Header("#Hero Pool")]
    public List<UnitData> ownHeroData;
    public List<UnitData> onSaleHeroData;


    public List<Dictionary<string,int>> GetCreepPool()
    {
        if (creepComb.Length == 0)
            return null;
        return creepComb[Random.Range(0, creepComb.Length - 1)];
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

    private void SetCreepComb()
    {

    }


}
