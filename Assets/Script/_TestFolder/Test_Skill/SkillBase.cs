using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public string skillName;
    public abstract void Activate(GameObject user);
}