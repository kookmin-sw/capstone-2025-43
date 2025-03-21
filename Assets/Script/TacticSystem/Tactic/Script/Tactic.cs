using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "TacticSystem/Tactic")]
public class Tactic : ScriptableObject
{
    public bool Enable = true;
    public int Priority = 0;
    [SerializeField] private TargetType targetType;  // 타겟 선택 (Self, Enemy, Ally 등)
    [SerializeField] private ConditionType conditionType;  // 조건 필터 (HPBelow, Nearest 등)
    [SerializeField] private ActionType actionType;  // 수행할 액션 (공격, 힐 등)

    public void Execute(Character self)
    {
        if (self == null || targetType == null || actionType == null)
        {
            Debug.LogWarning("Tactic 실행 실패: 필수 값 누락!");
            return;
        }

        // 1. Target과 Condition을 조합하여 대상 필터링
        List<Character> targets = CharacterManager.Instance.GetCharacters(self, targetType, conditionType, actionType);

        // 2. 대상이 존재하면 액션 수행
        foreach (Character target in targets)
        {
            actionType.Execute(self, target);
        }
    }
}
