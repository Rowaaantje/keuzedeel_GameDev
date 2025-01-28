using UnityEngine;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
    [Header("Unity Objects")]
    public Transform gunHolder;

    [Header("Inventory")]
    public List<Gun> inventory;
    public int itemHolding = 0;

    private int _lastHeldIndex = -1;

    void Start()
    {
        Debug.Log($"{inventory.Count} items in inventory");

        // Initialize the first gun
        if (inventory.Count > 0)
        {
            inventory[itemHolding].gameObject.SetActive(true);
            _lastHeldIndex = itemHolding;
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
            {
                itemHolding = inventory.Count - 1;
            }
            else
            {
                itemHolding %= inventory.Count;
            }
        }
    }

    private void RenderItemHeld()
    {
        if (inventory.Count < 1) return;

        // Only update if the held item has changed
        if (_lastHeldIndex != itemHolding)
        {
            // Deactivate the previous gun
            if (_lastHeldIndex >= 0 && _lastHeldIndex < inventory.Count)
            {
                inventory[_lastHeldIndex].gameObject.SetActive(false);
            }

            // Activate the new gun
            inventory[itemHolding].gameObject.SetActive(true);
            {
                _lastHeldIndex = itemHolding;
            }
        }
    }
}
