using UnityEngine;

public class Gun : HoldableObject
{

    public int damage;
    public int fireRate;
    private float nextFire;
    public new Camera camera;

    [Header("VFX")]
    public GameObject hitVFX;

    void Update()
    {
        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0) && nextFire <= 0)
        {
            nextFire = 1 / fireRate;
            Fire();
        }
    }

    void Fire()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        RaycastHit  hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {
            Instantiate(hitVFX, hit.point, Quaternion.identity);
        }
    }
}
