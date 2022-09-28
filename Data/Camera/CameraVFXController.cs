#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine.Rendering.PostProcessing;

public class CameraVFXController
{

    private static PostProcessVolume _postProcessVolume;

    public void SetSaturationValue(bool needChange)
    {
        var colorGrading = _postProcessVolume.profile.GetSetting<ColorGrading>();
        if (needChange)
        {
            colorGrading.saturation.value = -100f;
        }
        else
        {
            colorGrading.saturation.value = 0f;
        }
    }

    public void SetPostProcessVolume(PostProcessVolume postProcessVolume)
    {
        _postProcessVolume = postProcessVolume;
    }

}
#endif
