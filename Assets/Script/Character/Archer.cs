using UnityEngine;

public class Archer : MonoBehaviour
{
    public GameObject arrowPrefab;  
    public Transform firePoint;     
    public float arrowSpeed = 10f;  

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        
        Projectile homing = arrow.GetComponent<Projectile>();
        if (homing != null)
        {
            GameObject targetEnemy = GameObject.FindWithTag("TestEnemy");  
            if (targetEnemy != null)
            {
                homing.target = targetEnemy.transform;
                homing.speed = arrowSpeed;
            }
        }
    }
}
