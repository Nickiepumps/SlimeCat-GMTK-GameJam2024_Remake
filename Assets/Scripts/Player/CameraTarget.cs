using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private GameObject camFollowTarget;
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 offset = new Vector2(-5.19f, -2.86f);
    private void Update()
    {
        camFollowTarget.transform.position = new Vector2(player.position.x - offset.x, player.position.y - offset.y);
    }
}
