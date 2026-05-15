using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadding : MonoBehaviour
{
    public Image fadeImage; // ใช้ Image หากเป็น UI หรือ SpriteRenderer หากเป็น 2D Sprite
    private float fadeDuration = 1.5f; // ระยะเวลาที่จะทำให้ความโปร่งแสงเปลี่ยน

    private void Start()
    {
        // เริ่มต้นที่ความโปร่งแสงเต็มที่ (Alpha = 1)
        Color tempColor = fadeImage.color;
        tempColor.a = 1f;
        fadeImage.color = tempColor;

        // เริ่มทำการลดค่าความโปร่งแสง (Alpha) จาก 1 ไป 0
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        // ลดค่าความโปร่งแสงจาก 1 ไป 0 ภายใน 1.5 วินาที
        float timeElapsed = 0f;
        Color tempColor = fadeImage.color;
        while (timeElapsed < fadeDuration)
        {
            tempColor.a = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);
            fadeImage.color = tempColor;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // ตั้งค่า Alpha ให้เป็น 0 หลังจากครบ 1.5 วินาที
        tempColor.a = 0f;
        fadeImage.color = tempColor;

        // ปิดการมองเห็นของภาพหากต้องการ (ถ้าไม่ต้องการให้ภาพหายไปก็ไม่ต้องปิด)
        fadeImage.gameObject.SetActive(false);
    }
}