using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isMonster = false;
    public int team_id = 1;
    public float GlobalCooldown;
    public float hp_max;
    public float hp;
    public float mp_max;
    public float mp;
    public float damage_max;
    public float damage;
    public float attackRange;
    public float moveSpeed;
    public float rotationSpeed;
    // Ư�� ĳ����(other)�� ������ �Ǵ� 
    public bool IsEnemy(Character other)
    {
        return this.isMonster != other.isMonster && this.team_id != other.team_id;
    }

    // Ư�� ĳ����(other)�� �Ʊ����� �Ǵ� ���� �÷��̾� ĳ���Ͱ� �ƴ� �ٸ� ĳ���Ϳ��� ���� ���� �����ϱ� ����
    //������ ���ٰ� �Ʊ��� �ƴ϶�� �Ǵ���.
    public bool IsAlly(Character other)
    {
        return this.team_id == other.team_id;
    }


    public void AddHP(float amount)
    {
        // HP�� ������Ų ��, MaxHP�� �ʰ����� �ʵ��� ����
        hp += amount;

        // HP�� MaxHP�� ���� �ʵ��� ó��
        if (hp > hp_max)
        {
            hp = hp_max;
        }
       if(hp <= 0)
        {
            hp = 0;
            // Die
            if (TryGetComponent(out Animator animator))
            {
            animator.SetTrigger("Die");
            }
            Destroy(gameObject, 5f);

        }
    }

    public void ApplyDamage(float amount)
    {
        hp -= amount;

        if (hp > hp_max)
        {
            hp = hp_max;
        }
        if (hp <= 0)
        {
            hp = 0;

            // Die
            if (TryGetComponent(out Animator animator))
            {
            animator.SetTrigger("Die");
            }
            Destroy(gameObject, 5f);

        }
    }

    private void Start()
    {
        CharacterManager.Instance.RegisterCharacter(this, isMonster);
    }

    private void OnDestroy()
    {
        CharacterManager.Instance.UnregisterCharacter(this);
    }
}
