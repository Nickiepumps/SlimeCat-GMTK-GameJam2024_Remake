using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puke : MonoBehaviour
{
    private PlayerMovement player;
    private PlayerInventory playerInventory;
    [SerializeField] private Transform pukeDirectionRight; // Player puke direction
    [SerializeField] private Transform pukeDirectionLeft; // Player puke direction
    [SerializeField] private int pukeforce = 1300;
    private void Start()
    {
        player = GetComponent<PlayerMovement>();
        playerInventory = GameObject.FindGameObjectWithTag("Manager").GetComponentInChildren<PlayerInventory>();
    }
    private void Update()
    {
        foreach(GameObject obj in playerInventory.itemLists)
        {
            if(obj.GetComponent<EatableObject>().isDigestable == true)
            {
                if(obj.GetComponent<EatableObject>().digestTime <= 0)
                {
                    Digest(obj);
                    return;
                }
                else
                {
                    obj.GetComponent<EatableObject>().digestTime -= Time.deltaTime;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && playerInventory.foodCount > 0)
        {
            player.animator.SetBool("IsPuke", true);
            PukeObject();
        }
    }
    private void PukeObject()
    {
        player.isPuke = true;

        GameObject pukeItem = playerInventory.itemLists[playerInventory.foodCount-1];
        Rigidbody2D pukeItemRB = playerInventory.itemLists[playerInventory.foodCount-1].GetComponent<Rigidbody2D>();
        pukeItem.SetActive(true);
        pukeItemRB.bodyType = RigidbodyType2D.Dynamic;
        if(transform.GetComponent<SpriteRenderer>().flipX == true)
        {
            pukeItem.transform.position = pukeDirectionLeft.position;
            pukeItemRB.AddForce(Vector2.left * pukeforce, ForceMode2D.Impulse);
            player.walkSpeed = player.walkSpeed + 0.8f;
            player.jumpForce = player.jumpForce - 15;
            //player.dashForce = player.dashForce + 10;
        }
        else
        {
            pukeItem.transform.position = pukeDirectionRight.position;
            pukeItemRB.AddForce(Vector2.right * pukeforce, ForceMode2D.Impulse);
            player.walkSpeed = player.walkSpeed + 0.8f;
            player.jumpForce = player.jumpForce - 3;
            //player.dashForce = player.dashForce + 10;
        }
        playerInventory.itemLists.Remove(pukeItem);
        player.playerSize--;
        if (playerInventory.foodCount == 3)
        {
            StartCoroutine(player.Dash());
            player.transform.localScale = player.largeSize;
        }
        else if (player.playerSize == 2)
        {
            StartCoroutine(player.Dash());
            player.transform.localScale = player.mediumSize;
        }
        else if (player.playerSize == 1)
        {
            StartCoroutine(player.Dash());
            player.transform.localScale = player.normalSize;
        }
        playerInventory.foodCount--;

        playerInventory.inventorySlotLists[playerInventory.foodCount].gameObject.SetActive(false);
    }
    private void CanclePuke()
    {
        player.animator.SetBool("IsPuke", false);
        player.isPuke = false;
    }
    private void Digest(GameObject digestedItem)
    {
        playerInventory.itemLists.Remove(digestedItem); // UI
        

        player.walkSpeed = player.walkSpeed + 0.8f;
        player.jumpForce = player.jumpForce + 10;

        player.playerSize--;

        if (playerInventory.foodCount == 3)
        {
            player.transform.localScale = player.largeSize;
        }
        else if (player.playerSize == 2)
        {
            player.transform.localScale = player.mediumSize;
        }
        else if (player.playerSize == 1)
        {
            player.transform.localScale = player.normalSize;
        }

        playerInventory.foodCount--;

        playerInventory.inventorySlotLists[playerInventory.foodCount].gameObject.SetActive(false);
    }
}
