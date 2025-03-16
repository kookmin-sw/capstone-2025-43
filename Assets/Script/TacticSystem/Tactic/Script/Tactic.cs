using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "TacticSystem/Tactic")]
public class Tactic : ScriptableObject
{
    public bool Enable = true;
    public int Priority = 0;
    [SerializeField] private TargetType targetType;  // Ÿ�� ���� (Self, Enemy, Ally ��)
    [SerializeField] private ConditionType conditionType;  // ���� ���� (HPBelow, Nearest ��)
    [SerializeField] private TacticAction action;  // ������ �׼� (����, �� ��)

    public void Execute(Character self)
    {
        if (self == null || targetType == null || action == null)
        {
            Debug.LogWarning("Tactic ���� ����: �ʼ� �� ����!");
            return;
        }

        // 1. Target�� Condition�� �����Ͽ� ��� ���͸�
        List<Character> targets = CharacterManager.Instance.GetCharacters(self, targetType, conditionType);

        // 2. ����� �����ϸ� �׼� ����
        foreach (Character target in targets)
        {
            action.Execute(self, target);
        }
    }
}
