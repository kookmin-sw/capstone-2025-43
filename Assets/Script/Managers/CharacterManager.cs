using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.TextCore.Text;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    private List<Character> allCharacters = new List<Character>();
    private List<Character> allies = new List<Character>();
    private List<Character> enemies = new List<Character>();

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            PrintAllCharacterNames();
        }
    }

    public void RegisterCharacter(Character character, bool isEnemy)
    {
        allCharacters.Add(character);
        if (isEnemy)
            enemies.Add(character);
        else
            allies.Add(character);
    }

    public void UnregisterCharacter(Character character)
    {
        allCharacters.Remove(character);
        allies.Remove(character);
        enemies.Remove(character);
    }


    public List<Character> GetAllies() => new List<Character>(allies);
    public List<Character> GetEnemies() => new List<Character>(enemies);

    /// <summary>
    /// 특정 TargetType과 ConditionType을 적용하여 적절한 캐릭터 리스트를 반환
    public List<Character> GetCharacters(Character self, TargetType targetType, ConditionType conditionType)
    {
        if (self == null || targetType == null) return new List<Character>();

        // 1. TargetType을 이용해 대상 캐릭터 목록 필터링
        List<Character> targets = targetType.Filter(allCharacters, self);

        // 2. ConditionType이 있으면 추가 필터링
        if (conditionType != null)
        {
            targets = conditionType.Filter(targets, self);
        }

        return targets;
    }

    public void PrintAllCharacterNames()
    {
        Debug.Log("All Characters:");
        foreach (var character in allCharacters)
        {
            Debug.Log(character.gameObject.name);
        }
    }

}
