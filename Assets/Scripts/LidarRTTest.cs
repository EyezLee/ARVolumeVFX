using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LidarRTTest : MonoBehaviour
{
    [SerializeField] LidarDataProcessor lidarProcessor;

    [SerializeField] Texture2D testTex;

    private void Update()
    {
        //GetComponent<Renderer>().material.SetTexture("_MainTex", lidarProcessor._outputTex);
        //GetComponent<Renderer>().material.SetTexture("_MainTex", testTex);
        //GetComponent<RawImage>().texture = lidarProcessor._outputTex;
    }
}
