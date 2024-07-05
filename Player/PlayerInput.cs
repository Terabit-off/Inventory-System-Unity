using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject inventory;
    public PlayerMovement playerMovement;
    public PlayerCameraControll playerCamera;
    public Inventory inventoryS;
    public Recipe recipe;

    bool inInentory = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inInentory = !inInentory;
            inventoryS.SplitPanelDisable();
            inventory.SetActive(inInentory);

            playerCamera.enabled = !inInentory;
            playerMovement.enabled = !inInentory;

            if (inInentory)
                Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }

        if(Input.GetKeyDown(KeyCode.F)) 
        {
            if (Physics.Raycast(playerCamera.ray, out RaycastHit hit, 2f))
            {
                if (hit.collider.GetComponent<Item>())
                {
                    inventoryS.PickUp(hit.collider.GetComponent<Item>().item, hit.collider.gameObject);
                }
            }
        }
    }
}