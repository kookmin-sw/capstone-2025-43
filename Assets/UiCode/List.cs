using System.Data.Common;
using UnityEngine;

public class List : MonoBehaviour
{
    public bool own;
    public GameObject listContent;
    public UnitData[] dataList;

    public void SetList(bool isOwned)
    {
        // 기존 리스트 정리 (중복 추가 방지)
        foreach (Transform child in listContent.transform)
        {
            Destroy(child.gameObject);
        }
        dataList = Managers.instance.dataManager.GetUnitDataset("Ally");
        foreach(UnitData data in dataList)
        {
            Debug.Log($"데이터 이름: {data.unitName}, Own 여부: {data.own}");
            if (data.own == isOwned)
            {
                Debug.Log("데이터 개수: " + dataList.Length);
                // listidx ����
                GameObject go = Managers.instance.resourceManager.Instantiate("ListIdx", listContent.transform);
                if (go == null)
                {
                    Debug.LogError("ListIdx 프리팹이 Instantiate되지 않았습니다!");
                }
                else
                {
                    Debug.Log("ListIdx 생성 성공: " + go.name);
                }
                if (listContent == null)
                {
                    Debug.LogError(gameObject.name + "의 listContent가 설정되지 않았습니다!");
                }
                else
                {
                    Debug.Log("listContent: " + listContent.name);
                }
                go.gameObject.GetComponent<ListIdx>().data = data;
                go.transform.position = listContent.transform.position;
            }
        }
        Debug.Log("SetList 실행됨: " + gameObject.name);
    }
}
