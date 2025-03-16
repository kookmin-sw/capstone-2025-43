using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Condition/Random")]
public class RandomCondition : ConditionType
{
    [SerializeField, Range(1, 10)] private int targetCount = 1; // �ִ� �� ���� �������� �������� ����

    public override List<Character> Filter(List<Character> targets, Character self)
    {
        if (targets == null || targets.Count == 0)
        {
            return new List<Character>(); // ����� ������ �� ����Ʈ ��ȯ
        }

        int count = Mathf.Min(targetCount, targets.Count); // �ִ� targetCount����� ����
        List<Character> shuffledTargets = new List<Character>(targets); // ������ �������� �ʵ��� ����
        List<Character> selectedTargets = new List<Character>();

        // Fisher-Yates Shuffle�� Ȱ���Ͽ� ����Ʈ�� �����ϰ� ���� �տ��� count�� ����
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(i, shuffledTargets.Count);
            (shuffledTargets[i], shuffledTargets[randomIndex]) = (shuffledTargets[randomIndex], shuffledTargets[i]);
            selectedTargets.Add(shuffledTargets[i]);
        }

        return selectedTargets;
    }
}