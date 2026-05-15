using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform checkpoint; // จุด Checkpoint ที่กำหนด
    [SerializeField] private GameObject respawnTrigger; // วัตถุที่ถ้าชนแล้วจะกลับไปที่ Checkpoint
    [SerializeField] private PlayerInventory playerInventory; // ช่องเก็บของผู้เล่น
    [SerializeField] private string currentScene;
    private GameObject player;

    private Vector3 checkpointPosition;

    private void Start()
    {
        checkpointPosition = checkpoint.position; // เริ่มต้นที่ Checkpoint ที่กำหนด
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่าชนกับวัตถุที่กำหนดหรือไม่
        if (other.tag == "Player")
        {
            player = other.gameObject;
            Invoke("Respawn", 1f); // To Do: Play Dead Anim first then respawn player
        }
    }

    private void Respawn()
    {
        SceneManager.LoadScene(currentScene);
        //player.transform.position = checkpointPosition; // ย้ายผู้เล่นกลับไปที่จุด Checkpoint
        //playerInventory.itemLists.Clear();
    }
}