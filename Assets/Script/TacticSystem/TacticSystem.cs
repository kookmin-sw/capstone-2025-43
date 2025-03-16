using System.Collections.Generic;
using UnityEngine;

public class TacticSystem : MonoBehaviour
{
    [SerializeField] private List<Tactic> tactics = new List<Tactic>(); // 캐릭터가 보유한 Tactic 리스트
    private Character character; // 이 TacticSystem을 실행하는 캐릭터
    private float cooldownTimer = 0f; // Global Cooldown 타이머
    public bool StopcoolDown = false;
    private void Start()
    {
        character = GetComponent<Character>(); // 같은 객체의 Character 가져오기
        if (character == null)
        {
            Debug.LogError("TacticSystem에 Character 컴포넌트가 없음!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (character == null || tactics.Count == 0) return;

        // 타이머 감소
        if (!StopcoolDown)
            cooldownTimer -= Time.deltaTime;

        // Global Cooldown이 끝났다면 Tactic 실행
        if (cooldownTimer <= 0f)
        {
            ExecuteTactic();
            cooldownTimer = character.GlobalCooldown; // 쿨다운 초기화
        }
    }

    private void ExecuteTactic()
    {
        if (tactics.Count == 0) return;

        // 1. 우선순위가 높은 Tactic부터 실행
        foreach (Tactic tactic in tactics)
        {
            tactic.Execute(character);
        }
    }


    public void AddTactic(Tactic tactic)
    {
        if (!tactics.Contains(tactic))
        {
            Debug.Log("AddTactic!");
            tactics.Add(tactic);
        }
    }
    public void RemoveTactic(Tactic tactic)
    {
        Debug.Log("RemoveTactic!");
        tactics.Remove(tactic);
    }
}
