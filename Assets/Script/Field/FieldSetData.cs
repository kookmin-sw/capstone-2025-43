using UnityEngine;
using MyProject.Utils;

[CreateAssetMenu(menuName = "Field/FieldSetData")]
public class FieldSetData : ScriptableObject
{
    public E_FieldType fieldType;

    public GameObject environmentPrefab;
    public Vector3 environmentPosition = Vector3.zero;
    public Quaternion environmentRotation = Quaternion.identity;
    public Vector3 environmentScale = Vector3.one;

    public GameObject battleFieldPrefab;
    public Vector3 battlePosition = Vector3.zero;
    public Quaternion battleRotation = Quaternion.identity;
    public Vector3 battleScale = Vector3.one;
}