using System.Collections.Generic;
using UnityEngine;
using MyProject.Utils;
using static UnityEditor.Experimental.GraphView.Port;
using System.ComponentModel;

[RequireComponent(typeof(Character))]
public class TacticSystem : MonoBehaviour
{ 
    private int tacticCapacity;
    private Boundary1D<int> capacityBoundary = new Boundary1D<int>(2, 6);
    [SerializeField]
    public int TacticCapacity //you can get or set TacticCapacity in Boundary
    {
        get => tacticCapacity;
        set => tacticCapacity = Mathf.Clamp(value, capacityBoundary.min, capacityBoundary.max);
    }
    private Character character;
    [SerializeField] private List<Tactic> tactics = new List<Tactic>(); //Characters TacticList
    private float cooldownTimer = 0f; //Global Cooldown Timer
    public bool StopcoolDown = false;
  
    private void Start()
    {
        character = GetComponent<Character>();
        InitializeTactic(character.tacticCapacity);
        //TODO :: LoadTactic 
    }

    private void Update()
    {
        GlobalCooldown();
    }

    private void GlobalCooldown()
    {
        if (character == null || tactics.Count == 0) return;

        if (!StopcoolDown)
            cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            cooldownTimer = 0f;
            //StopcoolDown = true;
            ExecuteTactic();
            cooldownTimer = character.GlobalCooldown;
        }
    }
    private void ExecuteTactic()
    {
        if (tactics.Count == 0) return;

        foreach (Tactic tactic in tactics)
        {
            //TODO :: Check tactic Character Enable
            if (tactic.Enable && tactic.Execute(character))
            {
                return;
            }
        }
        Debug.Log("No valid Tactic executed.");
    }
    private void InitializeTactic(int Count)
    {
        //tactics.Clear();
        for (int i = 0; i < Count; i++)
        {
            Tactic tactic = ScriptableObject.CreateInstance<Tactic>();
            tactic.Priority = i;
            tactic.Enable = true;
            tactics.Add(tactic);
        }
    }
    public void SortTactics()
    {
        tactics.Sort((a, b) => a.Priority.CompareTo(b.Priority));
    }
}
