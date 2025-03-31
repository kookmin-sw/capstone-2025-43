using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.VFX;

public class PoolEffect : MonoBehaviour
{
    private string effectName;
    private VisualEffect visualEffect;
    private bool hasPlayed;
    
    public Transform parentBefore;
    public bool stickToGameObject = false;
    [HideInInspector] public GameObject stickGameObject;


    void Awake()
    {
        visualEffect = GetComponent<VisualEffect>();
    }

    public void Initialize(string effectName, Transform parentBefore)
    {
        this.effectName = effectName;
        this.parentBefore = parentBefore;
    }

    void Update()
    {
        //When Visual Effect Start
        if (visualEffect.aliveParticleCount > 0)
        {
            hasPlayed = true;
            if (stickToGameObject && stickGameObject)
            {
                transform.parent = stickGameObject.transform;
            }
        }
        //When Visual Effect End
        if (visualEffect.aliveParticleCount == 0 && hasPlayed)
        {
            transform.parent = parentBefore;
            ReturnToPool();
            hasPlayed = false;
            return;
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
