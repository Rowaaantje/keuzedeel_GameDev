using System.Collections;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public GameObject pelletPrefab; // Prefab for the shotgun pellet
    public Transform barrelEnd; // The end of the shotgun barrel
    public int pelletCount = 10; // Number of pellets per shot
    public float spreadAngle = 10f; // Spread angle for the pellets
    public float pelletSpeed = 20f; // Speed of the pellets
    public float fireRate = 1f; // Shots per second
    private float nextFireTime = 0f; // Time until next shot

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            // Calculate spread direction
            Vector3 spread = barrelEnd.forward;
            spread += new Vector3(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle)
            ).normalized * Mathf.Tan(spreadAngle * Mathf.Deg2Rad);
            
            // Instantiate and shoot the pellet
            GameObject pellet = Instantiate(pelletPrefab, barrelEnd.position, Quaternion.LookRotation(spread));
            Rigidbody rb = pellet.GetComponent<Rigidbody>();
            rb.AddForce(spread * pelletSpeed, ForceMode.VelocityChange);
        }
    }
}
