using UnityEngine;

public class GunHolder : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform gunTransform;

    void Start()
    {

    }

    void Update()
    {
        transform.position = gunTransform.position;
        transform.rotation = cameraTransform.rotation;
    }
}
