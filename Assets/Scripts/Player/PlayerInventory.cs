using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> itemLists; // Player eatable object lists
    public List<Image> inventorySlotLists; // Player inventory slot lists 
    public int foodCount = 0;
    private void Update()
    {
        foreach(GameObject item in itemLists)
        {
            if(item.GetComponent<EatableObject>().isDigestable == true && item.GetComponent<EatableObject>().digestTime <= 0)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}
