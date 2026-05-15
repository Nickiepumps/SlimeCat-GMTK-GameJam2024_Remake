using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Fuse> fuseBoxList = new List<Fuse>();
    [SerializeField] private Door door;
    public bool completeFuses;
    private void Update()
    {
        if(CheckFuseStatus() == true)
        {
            completeFuses = true;
        }
    }
    private bool CheckFuseStatus()
    {
        foreach (Fuse fuse in fuseBoxList)
        {
            if(fuse.hasFuse == false)
            {
                return false;
            }
        }
        return true;
    }
}
