using UnityEngine;
using System.Collections.Generic;

public class PoolManager
{
    [Header("#Enemy Pool")]
    public const int wavesCount = 10;
    public List<UnitData> bossData;
    public List<BattleWavePreset>[] waves = new List<BattleWavePreset>[wavesCount];

    [Header("#Hero Pool")]
    public List<UnitData> ownHeroData = new List<UnitData>();
    public List<UnitData> onSaleHeroData = new List<UnitData>();


    public void Init()
    {
        SetHeroList();
    }

    public List<BattleWavePreset> GetCreepPool()
    {
        return waves[Random.Range(0, 10)];
    }

    public void SetHeroList()
    {
        ownHeroData.Clear();
        onSaleHeroData.Clear();
        UnitData[] dataList = Managers.Data.GetUnitDataset("Ally");
        foreach (UnitData data in dataList)
        {
            if (data.own)
                ownHeroData.Add(data);
            else
                onSaleHeroData.Add(data);
        }
    }
}
