using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Elevator : MonoBehaviour
{
    #region variable
    public bool canMove;
    public bool haveFuse;

    [Header("Elevator Weight")]
    [SerializeField] int elevatorWeight; // น้ำหนักผู้เล่นที่ลิฟต์สามารถลงได้เองโดยที่ไม่ต้องการฟิวส์

    [Header("Waypoints")]
    [SerializeField] float elevatorSpeed = 1; // ความเร็วลิฟต์
    [SerializeField] int wayPointStart; // จำนวนปลายทางของลิฟต์
    [SerializeField] Transform[] points;
    public PlayerMovement playerhello;
    public int waypointNum = 0; // จุดเริ่มต้น

    [Header("Fuse Box")]
    public Fuse fuse;
    bool playerOnElevator;
    bool reverse;
    #endregion

    #region start && update
    private void Start()
    {
        transform.position = points[wayPointStart].position;
        //currentPointIndex = wayPointStart;
    }

    private void Update()
    {
        elevatorMove();
        elevatorMove2();
    }
    #endregion

    #region elevator move
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(hello());
        if (collision.CompareTag("Player"))
        {
            playerOnElevator = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnElevator = false;
            fuse.overWeight = false;
        }
    }

    private void elevatorMove()
    {
        // น้ำหนัก
        if(canMove == true)
        {
            //waypointNum++;
            transform.position = Vector2.MoveTowards(transform.position, points[1].transform.position, elevatorSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, points[1].position) < 0.01f)
            {
                canMove = false; // หยุดลิฟต์เมื่อถึง points[1]
                waypointNum++;
            }
        }
    }
    private void elevatorMove2()
    {
        // ไฟฟ้า
        {
            if (fuse.completeFuse)
            {
                haveFuse = true;
            }
            else
            {
                haveFuse = false;
            }

            if (haveFuse)
            {
                if(reverse)
                {
                    transform.position = Vector2.MoveTowards(transform.position, points[2].position, elevatorSpeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, points[2].position) < 0.01f)
                    {
                        waypointNum = 0;
                        reverse = false;
                    }
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, points[1].position, elevatorSpeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, points[1].position) < 0.01f)
                    {
                        reverse = true;
                    }
                }
                if (playerhello.playerSize >= elevatorWeight && playerOnElevator)
                {
                    haveFuse = false;
                    fuse.overWeight = true;
                }
            }
        }
    }
    IEnumerator hello() //hello
    {
        if (playerhello.playerSize >= elevatorWeight && haveFuse == false)
        {
            yield return new WaitForSeconds(0.5f);
            canMove = true;
        }
    }
    #endregion 
}