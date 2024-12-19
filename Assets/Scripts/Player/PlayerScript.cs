using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Transform gunHolder;
    public List<HoldableObject> inventory;      // inventory only holds gun
    public int itemHolding = 0;                 // first in index
    void Start()
    {
        inventory = new List<HoldableObject>(); // initialise the list
    }

    void Update()
    {
        RenderItemHeld();
    }

    public void AddToInventory(HoldableObject item)
    {
        inventory.Add(item);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("GunHolder")) { return; }      // inverse if-statement to save indentation

        PickupSphere pickupSphere = collider.GetComponent<PickupSphere>();

        if (pickupSphere != null && pickupSphere.itemInSphere != null)
        {
            HoldableObject item = pickupSphere.itemInSphere;
            AddToInventory(item);
            pickupSphere.itemInSphere = null;                       // Clear the item from the sphere after picking it up
            item.transform.SetParent(gunHolder);                 // Detach the item from the PickupSphere
        }

        pickupSphere.DestroySelf();
    }

    private void RenderItemHeld()
    {
        if (inventory.Count < 1) { return; }                                         // do nothing if inventory is empty

        for (int i = 0; i < inventory.Count; i++)                                    // set all items invisible by default
        {
            inventory[i].gameObject.SetActive(false);
        }

        int scrollIncrement = (int)(10 * Input.GetAxis("Mouse Scroll"));    // Input.GetAxis returns 0.1 / -0.1 by default
        itemHolding = itemHolding + scrollIncrement < 0f ? inventory.Count - 1 : itemHolding + scrollIncrement;
        itemHolding %= inventory.Count;

        inventory[itemHolding].gameObject.SetActive(true);              // render the item holding
    }
}
