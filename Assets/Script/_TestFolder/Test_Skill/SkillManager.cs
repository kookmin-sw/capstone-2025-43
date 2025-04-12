using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<SkillBase> skills;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && skills.Count > 0)
        {
            skills[0]?.Activate(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && skills.Count > 1)
        {
            skills[1]?.Activate(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && skills.Count > 2)
        {
            skills[2]?.Activate(gameObject);
        }
    }
}
