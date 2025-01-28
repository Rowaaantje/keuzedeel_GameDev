using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float _lifetime = 0.5f;

    void Start()
    {
        Destroy(gameObject, _lifetime);
    }
}
