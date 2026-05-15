using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    #region variable
    //public bool doorHaveFuse;

    [Header("fuse")]
    public Fuse fuse;

    [Header("Door Open Waypoint")]
    [SerializeField] private Transform waypoint;
    [SerializeField] private float openSpeed;
    #endregion
    private void Update()
    {
        doorOpen();
    }

    private void doorOpen()
    {
        if (fuse.completeFuseDoor)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoint.position, openSpeed*Time.deltaTime);
        }
    }
}
