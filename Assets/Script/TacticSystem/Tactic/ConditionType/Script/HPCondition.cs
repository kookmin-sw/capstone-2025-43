using System.Collections.Generic;
using UnityEngine;

public enum HPConditionType
{
    Below,
    Above
}

[CreateAssetMenu(menuName = "TacticSystem/Condition/HP Condition")]
public class HPCondition: ConditionType
{
    [SerializeField, Range(0, 100)] private int hpPercent = 50; // 0~100% �� ����
    [SerializeField] private HPConditionType conditionType = HPConditionType.Below; // Below �Ǵ� Above ���� ����

    public override List<Character> Filter(List<Character> targets, Character self)
    {
        if (conditionType == HPConditionType.Below)
        {
            return targets.FindAll(c => c.stat.hp < c.stat.hp_max* (hpPercent / 100f));
        }
        else // Above
        {
            return targets.FindAll(c => c.stat.hp > c.stat.hp_max* (hpPercent / 100f));
        }
    }
}
