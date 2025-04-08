
using UnityEngine;
using System.Collections;
public class SphereGizmo : MonoBehaviour
{
    void OnDrawGizmos()
    {
        // Draw a yellow wire sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1);

        // Draw a red line in the forward direction
        Gizmos.color = Color.red;
        Vector3 offset = new Vector3(0f, 0.5f, 0f);
        Vector3 start = transform.position + offset;
        Vector3 end = start + transform.forward * 2f ; 
        Gizmos.DrawLine(start, end);
    }


}
