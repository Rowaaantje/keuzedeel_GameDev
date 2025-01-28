using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Gun : MonoBehaviour
{
    PlayerScript playerScript;

    [Header("Gun Stats")]
    public int damage;
    public int rpm;
    public float dmgMultiplier; // The lower, the more damage
    public int maxAmmo;
    public float reloadSpeed;
    private int _currentAmmo;

    [Header("VFX")]
    public GameObject hitVFX;

    [SerializeField] protected TextMeshProUGUI WeaponName;
    [SerializeField] protected TextMeshProUGUI AmmoText;
    [SerializeField] protected TextMeshProUGUI ReloadingText;
    [SerializeField] protected string CurrentName;

    public new Camera camera;

    private float fireRate; // Cooldown time between shots
    private float nextFireTime = 0f;


    void Start()
    {
        fireRate = 60f / rpm; // Calculate time between shots

        _currentAmmo = maxAmmo;

        WeaponName.fontSize = 4;
        WeaponName.color = Color.white;

        AmmoText.text = $"{_currentAmmo} / inf";
    }

    void Update()
    {
        WeaponName.text = CurrentName.ToString();
        AmmoText.text = $"{_currentAmmo} / inf";

        // Check for held mouse button (automatic fire)
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + fireRate; // Update cooldown
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Fire()
    {
        if (_currentAmmo == 0) { return; }

        _currentAmmo--;

        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000f))
        {
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

    IEnumerator Reload()
    {
        ReloadingText.text = "Reloading...";

        yield return new WaitForSeconds(reloadSpeed);
        _currentAmmo = maxAmmo;

        ReloadingText.text = "";
    }
}
