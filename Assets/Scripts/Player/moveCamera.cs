using UnityEngine;

public class moveCamera : MonoBehaviour
{
    [SerializeField] protected Transform cameraPosition;


    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
