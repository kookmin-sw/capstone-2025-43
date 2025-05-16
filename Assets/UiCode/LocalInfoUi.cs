using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LocalInfoUi : MonoBehaviour
{
    public Image image;
    public Text desc;
    public Transform creepList;
    public Transform boss;
    public LocalInfo info;
    public void SetLocalUi()
    {
        Vector2 local = Managers.Data.handOverData.openLocal;
        info = Managers.Data.handOverData.localInfos[local];

        desc.text = info.localData.desc;
        image.sprite = info.localData.image;
        setCreepList();
    }
    public void setCreepList()
    {
        foreach(BattleWavePreset creep in info.battleWaves)
        {
            foreach(var t in creep.wave)
            {
                GameObject go = Managers.Resource.Instantiate("ListIdx", creepList);
                go.GetComponent<ListIdx>().Init(t.prefab.GetComponent<CharacterStat>());
                go.GetComponent<Drag>().enabled = false;
            }
        }
    }

}
