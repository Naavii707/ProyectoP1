using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 10;
    Rigidbody2D playerRB;

    GameObject inventarioCom;
    private bool inventoryVisible = false;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        inventarioCom = GameObject.FindGameObjectWithTag("Inventario-com");
        inventarioCom.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            playerRB.velocity = new Vector2(velocidad, playerRB.velocity.y);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            playerRB.velocity = new Vector2(-velocidad, playerRB.velocity.y);
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            if (!inventoryVisible)
            {
                inventoryVisible = true;
                inventarioCom.SetActive(inventoryVisible);
                GameObject.FindGameObjectWithTag("general-events").GetComponent<InventoryController>().showInventory();
            }


            else
            {
                inventoryVisible = false;
                inventarioCom.SetActive(inventoryVisible);
            }
        }


    }


}