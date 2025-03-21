using System.Collections.Generic;
using UnityEngine;

public class TacticSystem : MonoBehaviour
{
    [SerializeField] private List<Tactic> tactics = new List<Tactic>(); // ĳ���Ͱ� ������ Tactic ����Ʈ
    private Character character; // �� TacticSystem�� �����ϴ� ĳ����
    private float cooldownTimer = 0f; // Global Cooldown Ÿ�̸�
    public bool StopcoolDown = false;
    private void Start()
    {
        character = GetComponent<Character>(); // ���� ��ü�� Character ��������
        if (character == null)
        {
            Debug.LogError("TacticSystem�� Character ������Ʈ�� ����!");
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
