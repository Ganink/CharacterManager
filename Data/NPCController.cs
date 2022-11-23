using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class NPCController
{
    private CinemachineVirtualCamera CVC;
    public void SetMainCamera(Transform playerPos)
    {
        CVC = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        CVC.Follow = playerPos;
    }
}
