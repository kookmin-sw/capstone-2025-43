using System.Collections.Generic;
using UnityEngine;

public abstract class TargetType : ScriptableObject
{
    public string DisplayName = "DisplayName";

    public abstract List<Character> Filter(List<Character> allCharacters, Character self);
}