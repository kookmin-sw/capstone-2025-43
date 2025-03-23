using UnityEngine;
public abstract class ActionType : ScriptableObject
{
    public string displayName = "DisplayName";
    public float actionCooldown = 0f;
    public override string ToString() => displayName;

    //Can Edit isSingleTarget in Editor but Not in Other class
    [SerializeField]
    private bool isSingleTarget = true;
    public bool IsSingleTarget { get => isSingleTarget; protected set => isSingleTarget = value; }

    public abstract void Execute(Character user, Character target);
}