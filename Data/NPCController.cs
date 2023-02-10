using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class NPCController
{
    private CinemachineVirtualCamera CVC;
    private NPCAttributesSO npcAttributes;

    public NPCController() { }

    public NPCController(NPCAttributesSO npcAttributes)
    {
        this.npcAttributes = npcAttributes;
    }

    public void SetMainCamera(Transform playerPos)
    {
        CVC = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        CVC.Follow = playerPos;
    }

    public bool CanJump(InputPlayer inputPlayer, bool isGrounded)
    {
        if (inputPlayer != null)
        {
            if (inputPlayer.isJump && isGrounded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public NPCAttributesSO GetCharacterAttributes()
    {
        return npcAttributes;
    }

    public void GetCharacterAttributesInfo()
    {
        string CAInfo = $"nickname: maxlifepoints: {npcAttributes.MaxLifesPoints} maxmanaPoints: {npcAttributes.MaxManaPoints} speedUser: {npcAttributes.Speed}";
        Debug.Log(CAInfo);
    }
}
