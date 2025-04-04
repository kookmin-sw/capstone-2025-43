
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Target/Enemy")]
public class TargetEnemy : TargetType
{
    public override List<Character> Filter(List<Character> allCharacters, Character self)
    {
        return allCharacters.FindAll(c => self.IsEnemy(c));
    }
}
