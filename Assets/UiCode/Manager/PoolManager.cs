using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using static UnityEngine.Analytics.IAnalytic;
using NUnit.Framework.Constraints;
using System.Linq;

public class PoolManager : MonoBehaviour
{
    [Header("#Enemy Pool")]
    public const int wavesCount = 10;
    public List<UnitData> bossData;
    public List<BattleWavePreset>[] waves = new List<BattleWavePreset>[wavesCount];

    [Header("#Hero Pool")]
    public List<UnitData> ownHeroData;
    public List<UnitData> onSaleHeroData;


    public List<BattleWavePreset> GetCreepPool()
    {
        return waves[Random.Range(0, 10)];
    }

    public void SetHeroList()
    {
        ownHeroData.Clear();
        onSaleHeroData.Clear();
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
