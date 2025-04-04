using UnityEngine;
using UnityEngine.VFX;

public class PoolEffect : MonoBehaviour
{
    private string effectName;
    private VisualEffect visualEffect;
    private new ParticleSystem particleSystem;
    private bool hasPlayed;

    public Transform parentBefore;
    public bool stickToGameObject = false;
    [HideInInspector] public GameObject stickGameObject;

    void Awake()
    {
        visualEffect = GetComponent<VisualEffect>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void Initialize(string effectName, Transform parentBefore)
    {
        this.effectName = effectName;
        this.parentBefore = parentBefore;
    }

    void Update()
    {
        bool isPlaying = false;

        if (visualEffect != null)
        {
            isPlaying |= visualEffect.aliveParticleCount > 0;
        }

        if (particleSystem != null)
        {
            isPlaying |= particleSystem.IsAlive();
        }

        if (isPlaying)
        {
            hasPlayed = true;
            if (stickToGameObject && stickGameObject)
            {
                transform.parent = stickGameObject.transform;
            }
        }
        else if (hasPlayed)
        {
            transform.parent = parentBefore;
            ReturnToPool();
            hasPlayed = false;
        }
    }

    public void SetStickGameObject(GameObject go)
    {
        stickToGameObject = true;
        stickGameObject = go;
    }

    public void ReturnToPool()
    {
        EffectPoolManager.Instance.ReturnEffect(effectName, gameObject);
    }
}