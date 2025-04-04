using UnityEngine;
namespace MyProject.TacticUtils
{
    public struct TacticType
    {
        public Tactic tactic;
        public float cooldownTimer;

        public TacticType(Tactic tactic)
        {
            this.tactic = tactic;
            this.cooldownTimer = tactic.actionType.actionCooldown;
        }
    }
}