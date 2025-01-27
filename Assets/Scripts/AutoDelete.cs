using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
