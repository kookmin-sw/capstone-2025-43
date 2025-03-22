using System.Collections.Generic;
using UnityEngine;

public abstract class TargetType : ScriptableObject
{
    public string displayName = "DisplayName";
    public override string ToString() => displayName;


    public abstract List<Character> Filter(List<Character> allCharacters, Character self);
}