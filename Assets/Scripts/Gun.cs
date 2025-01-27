using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Stats")]
    public int damage;
    public int rpm;

    public new Camera camera;

    [Header("VFX")]
    public GameObject hitVFX;

    private float fireRate; // Cooldown time between shots
    private float nextFireTime = 0f;

    void Start()
    {
        fireRate = 60f / rpm; // Calculate time between shots
    }

    void Update()
    {
        // Check for held mouse button (automatic fire)
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + fireRate; // Update cooldown
            }
        }
    }

    void Fire()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {
            Instantiate(hitVFX, hit.point, Quaternion.identity);
        }
    }
}
