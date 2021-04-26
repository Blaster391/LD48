using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraScaler : MonoBehaviour
{
    [SerializeField]
    private float m_targetHeight = 32;
    [SerializeField]
    private float m_targetWidth = 61;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if(Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
            {
                Screen.SetResolution(Mathf.RoundToInt(Screen.currentResolution.width * 0.75f), Mathf.RoundToInt(Screen.currentResolution.height * 0.75f), FullScreenMode.Windowed);
            }
            else
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
            }
        }

        Camera camera = GetComponent<Camera>();
        float halfHeightTarget = m_targetHeight / 2;
        float halfWidthTarget = m_targetWidth / 2;

        float screenRatio = (float)Screen.height / Screen.width;
#if UNITY_EDITOR
        string[] res = UnityStats.screenRes.Split('x');
        screenRatio = (float)int.Parse(res[1]) / int.Parse(res[0]);
#endif

        float requiredSizeHeight = halfHeightTarget;
        float requiredSizeWidth = halfWidthTarget * screenRatio;


        camera.orthographicSize = Mathf.Max(requiredSizeHeight, requiredSizeWidth);
    }
}
