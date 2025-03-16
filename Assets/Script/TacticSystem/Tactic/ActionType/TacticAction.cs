using UnityEngine;
public abstract class TacticAction : ScriptableObject
{
    public abstract void Execute(Character user, Character target);
}