using UnityEngine;

public class GunHolder : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 offset = new Vector3(0.3f, 0.5f, 0.3f);// Set this to position the gun on the right side

    void Update()
    {
        // Position the GunHolder relative to the camera
        transform.position = cameraTransform.position + cameraTransform.TransformDirection(offset);
        transform.rotation = cameraTransform.rotation;
    }
}
