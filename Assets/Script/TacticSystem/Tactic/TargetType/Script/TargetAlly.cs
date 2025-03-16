using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Target/Ally")]
public class TargetAlly : TargetType
{
    public override List<Character> Filter(List<Character> allCharacters, Character self)
    {
        return allCharacters.FindAll(c => self.IsAlly(c) && c != self);
    }
}