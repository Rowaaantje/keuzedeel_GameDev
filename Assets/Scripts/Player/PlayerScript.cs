using UnityEngine;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
    public Transform gunHolder;
    public List<Gun> inventory;
    public int itemHolding = 0;
    private int lastHeldIndex = -1; // Track the last held item

    void Start()
    {
        Debug.Log($"{inventory.Count} items in inventory");
        // Initialize the first gun
        if (inventory.Count > 0)
        {
            inventory[itemHolding].gameObject.SetActive(true);
            lastHeldIndex = itemHolding;
        }
    }

    void Update()
    {
        HandleScrollInput();
        RenderItemHeld();
    }

    private void HandleScrollInput()
    {
        float scroll = Input.GetAxis("Mouse Scroll");
        if (scroll != 0)
        {
            int scrollIncrement = (int)(10 * scroll);
            itemHolding += scrollIncrement;

            // Wrap around inventory indices
            if (itemHolding < 0)
                itemHolding = inventory.Count - 1;
            else
                itemHolding %= inventory.Count;
        }
    }

    private void RenderItemHeld()
    {
        if (inventory.Count < 1) return;

        // Only update if the held item has changed
        if (lastHeldIndex != itemHolding)
        {
            // Deactivate the previous gun
            if (lastHeldIndex >= 0 && lastHeldIndex < inventory.Count)
                inventory[lastHeldIndex].gameObject.SetActive(false);

            // Activate the new gun
            inventory[itemHolding].gameObject.SetActive(true);
            lastHeldIndex = itemHolding;
        }
    }
}
