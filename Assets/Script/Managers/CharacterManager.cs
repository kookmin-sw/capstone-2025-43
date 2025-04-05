using UnityEngine;
using System.Collections.Generic;

   public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    private List<Character> allCharacters = new List<Character>();
    private List<Character> allies = new List<Character>();
    private List<Character> monsters = new List<Character>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
   //Shallow Copy
    public List<Character> GetAllies() => new List<Character>(allies);
    public List<Character> GetEnemies() => new List<Character>(monsters);

    //RegisterCharacter is called by Character.cs Start Function
    public void RegisterCharacter(Character character, bool isMonster)
    {
        allCharacters.Add(character);
        if (isMonster)
            monsters.Add(character);
        else
            allies.Add(character);
    }

    public void UnregisterCharacter(Character character)
    {
        allCharacters.Remove(character);
        //TeamCode and Enemy boolean value can Change like mindControl Action.
        monsters.Remove(character);
        allies.Remove(character);
    }

    public List<Character> GetCharacters(Character self, TargetType targetType, ConditionType conditionType, ActionType actionType)
    {
        if (self == null || targetType == null) return new List<Character>();

        //TargetType Filtering
        List<Character> targets;
        if(BattleManager.Instance.battleCharacter.Count != 0) 
            targets= targetType.Filter(BattleManager.Instance.battleCharacter, self);
        else
            targets = targetType.Filter(allCharacters, self);

        //Dead target Filtering
        DeadTargetFilter(targets);

        //ConditionTypeFiltering
        if (conditionType != null)
        {
            targets = conditionType.Filter(targets, self);
        }

        if(actionType != null)
        {
            if(actionType.IsSingleTarget == true && targets.Count > 1)
            {
                targets = GetNearestTarget(self, targets);
            }
        }
        return targets;
    }

    private void DeadTargetFilter(List<Character> targets)
    {
        targets.RemoveAll(target => target.Hp <= 0);
    }
    //O(n) FInd Nearlest Target
    private List<Character> GetNearestTarget(Character self, List<Character> targets)
    {
        if (targets == null || targets.Count == 0) return new List<Character>();

        if (targets.Count == 1)
            return targets;

        Character nearest = null;
        float minDistance = float.MaxValue;

        foreach (Character target in targets)
        {
            float distance = Vector3.Distance(self.transform.position, target.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = target;
            }
        }
        return nearest != null ? new List<Character> { nearest } : new List<Character>();
    }
}


