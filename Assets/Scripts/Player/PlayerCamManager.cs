using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Transform camTarget;
    [SerializeField] private Vector2 playerSmallSizeCamOffset;
    private void Update()
    {
        if (player.playerSize == 1f)
        {
            GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 5f;
        }
        else if(player.playerSize == 2)
        {
            GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 5.5f;
        }
        else if(player.playerSize == 3)
        {
            GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 6.5f;
        }
        else if (player.playerSize == 4)
        {
            GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 7.5f;
        }
        else if (player.playerSize == 0)
        {
            camTarget.position = player.transform.position; // Set target position to the center of the screen
            GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 3f; // Set orthographic size of the camera to 3f
        }
    }
}
