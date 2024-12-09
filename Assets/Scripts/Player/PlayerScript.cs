using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Transform gunHolder;
    public List<HoldableObject> inventory; // inventory only holds gun
    void Start()
    {
        inventory = new List<HoldableObject>(); // initialise the list
    }

    void Update()
    {
        if (inventory.Count < 1) { return; }

        inventory[0].transform.position = gunHolder.position;
    }

    public void MoveInventory(List<HoldableObject> list, int oldIndex, int newIndex)
    {
        HoldableObject item = list[oldIndex];

        list.RemoveAt(oldIndex);
        list.Insert(newIndex, item);
    }

    public void AddToInventory(HoldableObject item)
    {
        inventory.Add(item);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("GunHolder")) { return; } // inverse if-statement to save indentation

        PickupSphere pickupSphere = collider.GetComponent<PickupSphere>();

        if (pickupSphere != null && pickupSphere.itemInSphere != null)
        {
            HoldableObject item = pickupSphere.itemInSphere;
            AddToInventory(item);
            pickupSphere.itemInSphere = null; // Clear the item from the sphere after picking it up
            item.transform.SetParent(gunHolder); // Detach the item from the PickupSphere
        }

        pickupSphere.DestroySelf();
    }
}
