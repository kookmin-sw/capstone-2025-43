using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Condition/Random")]
public class RandomCondition : ConditionType
{
    [SerializeField, Range(1, 10)] private int targetCount = 1; // 최대 몇 명을 랜덤으로 선택할지 설정

    public override List<Character> Filter(List<Character> targets, Character self)
    {
        if (targets == null || targets.Count == 0)
        {
            return new List<Character>(); // 대상이 없으면 빈 리스트 반환
        }

        int count = Mathf.Min(targetCount, targets.Count); // 최대 targetCount명까지 선택
        List<Character> shuffledTargets = new List<Character>(targets); // 원본을 수정하지 않도록 복사
        List<Character> selectedTargets = new List<Character>();

        // Fisher-Yates Shuffle을 활용하여 리스트를 랜덤하게 섞고 앞에서 count명 선택
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(i, shuffledTargets.Count);
            (shuffledTargets[i], shuffledTargets[randomIndex]) = (shuffledTargets[randomIndex], shuffledTargets[i]);
            selectedTargets.Add(shuffledTargets[i]);
        }

        return selectedTargets;
    }
}