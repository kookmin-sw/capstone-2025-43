using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Target/Self")]
public class TargetSelf : TargetType
{
    public override List<Character> Filter(List<Character> allCharacters, Character self)
    {
        return new List<Character> { self }; // Tactic을 실행한 객체를 반환
    }
}