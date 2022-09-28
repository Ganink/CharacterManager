using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine.Rendering.PostProcessing;
#endif

public class CharacterCamera : MonoBehaviour
{
    private GameObject target;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private GameObject thisCamera;

#if UNITY_POST_PROCESSING_STACK_V2
#endif

    private void Start()
    {
        target = GameObject.FindObjectOfType<NPCManager>().gameObject;
        thisCamera = this.gameObject;
        cinemachineVirtualCamera = thisCamera.GetComponentInChildren<CinemachineVirtualCamera>();
        if (cinemachineVirtualCamera == null)
        {
            Debug.LogError("Not Cinemachine Virtual Camera found in children");
            return;
        }
        else
        {
            cinemachineVirtualCamera.m_Follow = target.transform;
            cinemachineVirtualCamera.m_LookAt = target.transform;
        }

        PostProcessingInitialized();

    }

    private void PostProcessingInitialized()
    {
#if UNITY_POST_PROCESSING_STACK_V2
        SetPostProcessingInitialized();
#endif
    }

#if UNITY_POST_PROCESSING_STACK_V2

    private PostProcessVolume postProcessVolume;
    private static CameraVFXController cameraVFXController;

    private void SetPostProcessingInitialized()
    {
        cameraVFXController = new CameraVFXController();
        postProcessVolume = GetComponent<PostProcessVolume>();
        cameraVFXController.SetPostProcessVolume(postProcessVolume);
    }

    public void SetSaturationValue(bool updateValue)
    {
        cameraVFXController.SetSaturationValue(updateValue);
    }
#endif
}