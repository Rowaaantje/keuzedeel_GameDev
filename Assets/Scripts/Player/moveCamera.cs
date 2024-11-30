using UnityEngine;

public class moveCamera : MonoBehaviour
{
    [SerializeField] Transform  cameraPosition;


    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
