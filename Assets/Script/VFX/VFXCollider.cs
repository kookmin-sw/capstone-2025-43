using UnityEngine;

public class VFXCollider : MonoBehaviour
{
    [Header("Settings")]
    public float damage = 100f;
    public float delayTime = 0.0f;   // Collider 활성화까지 대기 시간
    public float activeTime = 1.0f;  // Collider가 활성화될 시간
    [HideInInspector] private Character caster;

    private float timer = 0f;
    private bool colliderActivated = false;

    private SphereCollider colliderComponent;
    private void OnEnable()
    {
        timer = 0f;
    }
    private void Start()
    {
        colliderComponent = GetComponent<SphereCollider>();
        colliderComponent.enabled = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!colliderActivated && timer >= delayTime)
        {
            colliderComponent.enabled = true;
            colliderActivated = true;
        }

        if (colliderActivated && timer >= delayTime + activeTime)
        {
            colliderComponent.enabled = false;
            colliderActivated = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Character otherCharacter = other.GetComponent<Character>();
        if (otherCharacter != null)
        {
            if(caster.IsMonster != otherCharacter.IsMonster)
                otherCharacter.ApplyDamage(damage);
        }
    }
    public void SetVFXOption(Character caster, float damage, float delayTime, float activeTime)
    {
        this.caster = caster;
        this.damage = damage;
        this.delayTime = delayTime;
        this.activeTime = activeTime;
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public void SetCaster(Character caster)
    {
        this.caster = caster;
    }
}
