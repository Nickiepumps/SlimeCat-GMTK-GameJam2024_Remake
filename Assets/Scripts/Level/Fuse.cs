using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    #region variable
    [SerializeField] private Sprite noFuseSprite;
    [SerializeField] private Sprite haveFuseSprite;
    public bool completeFuse;
    public bool completeFuseDoor;
    public bool overWeight;

    [Header("Fuse")]
    [SerializeField] List<bool> hasFuseList;
    [SerializeField] public bool hasFuse;
    public GameManager gameManager;
    public Elevator elevator;
    #endregion

    #region completeFuse
    private void Update()
    {
        if (hasFuseList.Count >= 1 && overWeight == false)
        {
            completeFuse = true;
            elevator.haveFuse = true;
        }
        else
        {
            completeFuse = false;
        }

        if (gameManager.completeFuses == true)
        {
            completeFuseDoor = true;
        }
        else
        {
            completeFuseDoor = false;
        }
    }
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fuse")
        {
            hasFuse = true;
            completeFuse = true;
            hasFuseList.Add(true);
            GameObject fuseItem = collision.GetComponent<GameObject>().gameObject;
            GetComponent<SpriteRenderer>().sprite = haveFuseSprite;
            collision.gameObject.SetActive(false);
            GetComponent<BoxCollider2D>().enabled = false;
            return;
        }
    }

}
