using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BattleSystem/BattleWavePreset")]
public class BattleWavePreset : ScriptableObject
{
    [System.Serializable]
    public class MonsterInform
    {
        public Vector3 RelativeLocation = Vector3.zero;
        public Quaternion Rotation = Quaternion.identity;
        public Vector3 Scale = Vector3.one;
        public GameObject prefab;
        
        public MonsterInform(GameObject prefab,Transform transform)
        {
            this.prefab = prefab;
            RelativeLocation = transform.position;
            Rotation = transform.rotation;
            Scale = transform.localScale;
        }
    }

    // The monster is spawned at a relative position based on the flag.
    public List<MonsterInform> wave = new List<MonsterInform>();

    public void AddMonster(GameObject prefab, Transform transform)
    {
        wave.Add(new MonsterInform(prefab, transform));
    }
}
