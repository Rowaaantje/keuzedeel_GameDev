using UnityEngine;

public class Shotgun : Gun
{
    public override void Fire()
    {
        if (CurrentAmmo == 0 || Reloading) { return; }

        CurrentAmmo--;

        if (shotSound != null)
        {
            AudioSource.PlayClipAtPoint(shotSound, transform.position);
        }

        ShotgunShoot();
    }

    private void ShotgunShoot() // Shoots 6 pellets in a slightly random direction
    {
        if (CurrentAmmo == 0 || Reloading) { return; }

        CurrentAmmo--;

        if (shotSound != null)
        {
            AudioSource.PlayClipAtPoint(shotSound, transform.position);
        }

        for (int i = 0; i < 6; i++)
        {
            Vector3 randomDirection = camera.transform.forward;
            randomDirection.x += Random.Range(-0.1f, 0.1f);
            randomDirection.y += Random.Range(-0.1f, 0.1f);

            Ray ray = new Ray(camera.transform.position, randomDirection);
            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000f))
            {
                // Spawn a small red circle at the hit point for debugging
                GameObject debugCircle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                debugCircle.transform.position = hit.point;
                debugCircle.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                debugCircle.GetComponent<Renderer>().material.color = Color.red;
                Destroy(debugCircle, 1f);

                // Spawn particles at hit point
                if (hitVFX != null)
                {
                    Instantiate(
                        hitVFX,
                        hit.point,
                        Quaternion.LookRotation(hit.normal) // Align with surface
                    );
                }

                // Check if we hit a ball
                if (hit.collider.CompareTag("Ball"))
                {
                    Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        // Apply force in the bullet's direction
                        rb.AddForce(ray.direction * damage / dmgMultiplier, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
