using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Move
    public void SetMoveState(bool isMoving, float moveSpeed = 0f)
    {
        animator.SetBool("IsMove", isMoving);
        animator.SetFloat("MoveSpeed", moveSpeed);
    }

    // Attack
    public void PlayAttack(int attackIndex)
    {
        animator.SetInteger("Attack_index", attackIndex);
        animator.SetTrigger("Attack");
    }

    // Skill
    public void PlaySkill(int skillIndex)
    {
        animator.SetInteger("Skill_index", skillIndex);
        animator.SetTrigger("Skill");
    }

    // Die
    public void PlayDying()
    {
        animator.SetTrigger("Dying");
    }
}
