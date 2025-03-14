using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor.EventSystems;
using Unity.VisualScripting;

public class Icon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public IconData data;
    GameObject desc;
    GameObject image;

    private void Awake() // 변수 초기화
    {
        image = transform.GetChild(0).gameObject;
        desc = transform.GetChild(1).gameObject;

        Init();
    }

    private void Init() // 내용 + 이미지 초기화
    {
        image.GetComponent<Image>().sprite = data.iconImg;
        desc.GetComponentInChildren<Image>().sprite = data.descImg;
        desc.GetComponentInChildren<Text>().text = data.descText;
    }



    public void OnPointerEnter(PointerEventData data) // 마우스가 img에 올라왔을때 1회 실행
    {
        desc.SetActive(true);
    }
    public void OnPointerExit(PointerEventData data) // 마우스가 img에서 벗어날때 1회 실행
    {
        desc.SetActive(false);
    }
}