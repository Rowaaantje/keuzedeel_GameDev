using UnityEngine;

public class PickupSphere : MonoBehaviour
{
    public HoldableObject itemInSphere;
    void Start()
    {

    }

    void Update()
    {

    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
