using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroInventory", menuName = "Inventory/HeroInventory")]
public class HeroInventory : ScriptableObject
{
    public List<UnitData> ownedHeroes = new List<UnitData>();

    public bool HasHero(UnitData hero)
    {
        return ownedHeroes.Contains(hero);
    }

    public void AddHero(UnitData hero)
    {
        if (!HasHero(hero))
        {
            ownedHeroes.Add(hero);
        }
    }
}
