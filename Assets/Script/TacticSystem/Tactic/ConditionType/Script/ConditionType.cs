using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionType : ScriptableObject
{
    public abstract List<Character> Filter(List<Character> targets, Character self);
}