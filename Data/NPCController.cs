using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController 
{
    public GameObject GetMainCamera()
    {
        /*var newCamera  = GameObject.Instantiate(Resources.Load<GameObject>("Cameras/main-camera"));
        if (newCamera == null)
        {
            Debug.LogError("Not camera found");
            return null;
        }

        return newCamera;*/
        return Camera.main.gameObject;
    }
}
