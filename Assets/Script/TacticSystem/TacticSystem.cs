using System.Collections.Generic;
using UnityEngine;
using MyProject.Utils;
using static UnityEditor.Experimental.GraphView.Port;
using System.ComponentModel;

[RequireComponent(typeof(Character))]
public class TacticSystem : MonoBehaviour
{ 
    private Boundary1D<int> capacityBoundary = new Boundary1D<int>(2, 6);
    private Character character;
    public bool isActive
    {
        get => _isActive;
        set
        {
            if (_isActive && !value) // isActive Setting to true -> false, ResetCooldown Activate
            {
                ResetCooldown();
            }
            _isActive = value;
        }
    }
    private bool _isActive = true; [SerializeField]
    public Tactic currentTactic; 
    public int TacticCapacity //you can get or set TacticCapacity in Boundary
    {
        get => character.tacticCapacity;
        set => character.tacticCapacity = Mathf.Clamp(value, capacityBoundary.min, capacityBoundary.max);
    }
    public List<Tactic> tactics = new List<Tactic>(); //Characters TacticList
    [HideInInspector] public float cooldownTimer; //Global Cooldown Timer
    [HideInInspector] public bool stopCooldown = false;
    private void Start()
    {
        character = GetComponent<Character>();
        cooldownTimer = 3f;
        stopCooldown = false;
        InitializeTactic(character.tacticCapacity);
        //TODO :: LoadTactic 
    }

    private void Update()
    {
        if (isActive)
        {
            GlobalCooldown();
            TacticCoolDown();
        }
    }

    public void ResetCooldown()
    {
        stopCooldown = false;
        if(character)
            cooldownTimer = character.GlobalCooldown;
        foreach(Tactic tactic in tactics)
        {
            tactic.ApplyCoolDown();
        }
    }

    private void GlobalCooldown()
    {
        if (character == null || tactics.Count == 0) return;

        if (!stopCooldown)
            cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            cooldownTimer = 0f;
            //StopcoolDown = true;
            ExecuteTactic();
            cooldownTimer = character.GlobalCooldown;
        }
    }

    private void TacticCoolDown()
    {
        if (character == null || tactics.Count == 0) return;

        foreach (Tactic tactic in tactics)
        {
            if (!tactic.stopcoolDown)
                tactic.cooldownTimer -= Time.deltaTime;

            if (tactic.cooldownTimer <= 0f)
            {
                tactic.cooldownTimer = 0f;
                tactic.stopcoolDown = true;
            }
        }
    }
    private void ApplyTacticCooldown(Tactic executeTactic)
    {
        foreach (Tactic tactic in tactics)
        {
            if (tactic.actionType == executeTactic.actionType)
            {
                tactic.ApplyCoolDown();
            }
        }
    }
    private void ExecuteTactic()
    {
        if (tactics.Count == 0) return;

        foreach (Tactic tactic in tactics)
        {
            if (tactic.enable && tactic.stopcoolDown == true)
            {
                if (tactic.Execute(character))
                {
                    currentTactic = tactic;
                    ApplyTacticCooldown(tactic);
                    return;
                }
            }
        }
        //Debug.Log("No valid Tactic executed.");
    }
    private void InitializeTactic(int Capacity)
    {
        //tactics.Clear();
        int tacticCount = tactics.Count;
        for(int i = 0; i < tacticCount; ++i)
        {
            Tactic tactic = tactics[i];
            tactic.priority = i;
            tactic.enable = true;
            character.AddTacticComponent(tactic);
        }

        for (int i = tacticCount; i < Capacity; i++)
        {
            Tactic tactic = ScriptableObject.CreateInstance<Tactic>();
            tactic.priority = i;
            tactic.enable = true;
            //Last Priority is Always Nearlest Enemy Attack
            if (i == Capacity - 1)
            {
                tactic.targetType = character.Targets.Find(target => target is TargetEnemy);
                tactic.conditionType = character.Conditions.Find(condition => condition is NearestCondition);
                tactic.actionType = character.Actions.Find(action => action is MeleeAttackAction);
                if (tactic.targetType && tactic.conditionType && tactic.actionType)
                {
                    tactic.draggable = false;
                    tactic.editable = false;
                }
            }
            tactics.Add(tactic);
        }
    }
    public void SortTactics()
    {
        tactics.Sort((a, b) => a.priority.CompareTo(b.priority));
    }
}
