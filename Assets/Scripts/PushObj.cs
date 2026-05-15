using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObj : MonoBehaviour
{
    [Header("Required Player Size Level")]
    [SerializeField] private int requiredSizeLevel = 2; // ระดับขนาดที่ต้องการในการดัน (1 = normalSize, 2 = mediumSize, 3 = largeSize, 4 = veryLargeSize)

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll; // ล็อควัตถุให้อยู่กับที่ในตอนเริ่มต้น
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // ตรวจสอบว่าผู้เล่นชนวัตถุนี้
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            // ตรวจสอบขนาดของผู้เล่นเทียบกับระดับขนาดที่กำหนด
            if (player.playerSize >= requiredSizeLevel)
            {
                // ถ้าขนาดตรงตามที่กำหนด ปลดล็อคการเคลื่อนที่ของวัตถุเพื่อให้ผู้เล่นดันได้
                rb.constraints = RigidbodyConstraints2D.FreezeRotation; // ให้วัตถุเคลื่อนที่ได้เฉพาะในแกน x และ y
            }
            else
            {
                // ถ้าขนาดไม่ตรงกับที่กำหนด ล็อควัตถุให้อยู่กับที่
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // เมื่อผู้เล่นออกจากการชนวัตถุ ให้ล็อควัตถุอีกครั้ง
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}