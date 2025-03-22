using System.Collections.Generic;
using UnityEngine;
using MyProject.Utils;
using MyProject.TacticUtils;
using static UnityEditor.Experimental.GraphView.Port;
using System.ComponentModel;

[RequireComponent(typeof(Character))]
public class TacticSystem : MonoBehaviour
{ 
    private Boundary1D<int> capacityBoundary = new Boundary1D<int>(2, 6);
    private Character character;
    [SerializeField]
    public int TacticCapacity //you can get or set TacticCapacity in Boundary
    {
        get => character.tacticCapacity;
        set => character.tacticCapacity = Mathf.Clamp(value, capacityBoundary.min, capacityBoundary.max);
    }
    //public List<TacticType> tactics = new List<TacticType>();
    public List<Tactic> tactics = new List<Tactic>(); //Characters TacticList
    private float cooldownTimer = 0f; //Global Cooldown Timer
    [HideInInspector] public bool StopcoolDown = false;
  
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
            if (tactic.enable && tactic.Execute(character))
            {
                //TODO :: Apply Action Cooldown
                return;
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
