using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadScene : MonoBehaviour
{
    public Image fadeImage; // ใช้ Image หากเป็น UI หรือ SpriteRenderer หากเป็น 2D Sprite
    public string nextSceneName; // ชื่อ Scene ที่ต้องการโหลด
    private float fadeDuration = 1.5f; // ระยะเวลาที่จะทำให้ความโปร่งแสงเปลี่ยน
    [SerializeField] private bool isEnd;
    [SerializeField] private GameObject endText;

    private void Start()
    {
        // ตั้งค่าให้เริ่มต้นด้วยความโปร่งแสง 0 (Alpha = 0) และปิดการมองเห็น
        Color tempColor = fadeImage.color;
        tempColor.a = 0f;
        fadeImage.color = tempColor;
        //fadeImage.gameObject.SetActive(false); // ซ่อนวัตถุ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่า Object ที่ชนคือผู้เล่นหรือไม่ (ตรวจสอบโดย tag หรือ component)
        if (collision.gameObject.CompareTag("Player"))
        {
            // ปิดการเคลื่อนที่ของผู้เล่น (ปิดสคริปต์ที่ควบคุมการเคลื่อนที่)
            collision.gameObject.GetComponent<PlayerMovement>().enabled = false;

            // เริ่มฟังก์ชันการบังหน้าจอและโหลด Scene
            StartCoroutine(FadeAndLoadScene());
        }

        if (collision.gameObject.CompareTag("Player") && isEnd == true)
        {
            // ปิดการเคลื่อนที่ของผู้เล่น (ปิดสคริปต์ที่ควบคุมการเคลื่อนที่)
            collision.gameObject.GetComponent<PlayerMovement>().enabled = false;

            // เริ่มฟังก์ชันการบังหน้าจอและโหลด Scene
            StartCoroutine(FadeAndLoadScene());
            endText.SetActive(true);
        }
    }

    private IEnumerator FadeAndLoadScene()
    {
        // แสดงวัตถุบังหน้าจอ
        fadeImage.gameObject.SetActive(true);

        // เริ่มต้นที่ความโปร่งแสง 0 (Alpha = 0)
        Color tempColor = fadeImage.color;
        tempColor.a = 0f;
        fadeImage.color = tempColor;

        // เพิ่มค่าความโปร่งแสงจาก 0 ไป 1 (Alpha จาก 0 ไป 1) ภายใน 1.5 วินาที
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            tempColor.a = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);
            fadeImage.color = tempColor;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // ตั้งค่า Alpha ให้เป็น 1 หลังจากครบ 1.5 วินาที
        tempColor.a = 1f;
        fadeImage.color = tempColor;

        // โหลด Scene ใหม่
        SceneManager.LoadScene(nextSceneName);
    }
}