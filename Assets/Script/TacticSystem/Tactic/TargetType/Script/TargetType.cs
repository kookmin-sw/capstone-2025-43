using System.Collections.Generic;
using UnityEngine;

public abstract class TargetType : ScriptableObject
{
    public abstract List<Character> Filter(List<Character> allCharacters, Character self);
}