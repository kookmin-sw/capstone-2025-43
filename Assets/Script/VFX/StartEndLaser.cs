using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndLaser : MonoBehaviour
{
    public int damageOverTime = 30;

    public GameObject HitEffect;
    public float HitOffset = 0;
    public bool useLaserRotation = false;

    public float MaxLength;
    private LineRenderer Laser;

    public float MainTextureLength = 1f;
    public float NoiseTextureLength = 1f;
    private Vector4 Length = new Vector4(1, 1, 1, 1);

    private bool LaserSaver = false;
    private bool UpdateSaver = false;

    private ParticleSystem[] Effects;
    private ParticleSystem[] Hit;

    void Start()
    {
        // Get LineRender and ParticleSystem components from current prefab;  
        Laser = GetComponent<LineRenderer>();
        Effects = GetComponentsInChildren<ParticleSystem>();
        Hit = HitEffect.GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        // Laser material texture scaling
        Laser.material.SetTextureScale("_MainTex", new Vector2(Length[0], Length[1]));
        Laser.material.SetTextureScale("_Noise", new Vector2(Length[2], Length[3]));

        // To set LineRender position
        if (Laser != null && UpdateSaver == false)
        {
            Laser.SetPosition(0, transform.position);

            // Calculate end point based on character's forward direction and max length
            Vector3 endPos = transform.position + transform.forward * MaxLength;
            Laser.SetPosition(1, endPos);

            HitEffect.transform.position = endPos;
            if (useLaserRotation)
                HitEffect.transform.rotation = transform.rotation;
            else
                HitEffect.transform.LookAt(endPos + Vector3.forward * HitOffset);

            // Play effects
            foreach (var AllPs in Effects)
            {
                if (!AllPs.isPlaying) AllPs.Play();
            }

            // Texture tiling
            Length[0] = MainTextureLength * (Vector3.Distance(transform.position, endPos));
            Length[2] = NoiseTextureLength * (Vector3.Distance(transform.position, endPos));

            // Ensure the laser is enabled if not already
            if (Laser.enabled == false && LaserSaver == false)
            {
                LaserSaver = true;
                Laser.enabled = true;
            }
        }
    }

    public void DisablePrepare()
    {
        if (Laser != null)
        {
            Laser.enabled = false;
        }
        UpdateSaver = true;
        // Effects can = null in multiply shooting
        if (Effects != null)
        {
            foreach (var AllPs in Effects)
            {
                if (AllPs.isPlaying) AllPs.Stop();
            }
        }
    }
}
