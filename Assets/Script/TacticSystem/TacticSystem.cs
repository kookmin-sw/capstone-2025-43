using System.Collections.Generic;
using UnityEngine;

public class TacticSystem : MonoBehaviour
{
    [SerializeField] private List<Tactic> tactics = new List<Tactic>(); //Characters TacticList
    private Character character;
    private float cooldownTimer = 0f;
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

        if (!StopcoolDown)
            cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            ExecuteTactic();
            cooldownTimer = character.GlobalCooldown;
        }
    }

    private void ExecuteTactic()
    {
        if (tactics.Count == 0) return;

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
