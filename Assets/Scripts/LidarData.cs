using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LidarData
{
    public RenderTexture DepthTexture;
    public RenderTexture ColorTexture; // color + human stencil
    public Matrix4x4 projectionMatrix;
    public Matrix4x4 CameraToWorldMatrix;
    public Vector2 DepthRange;
}
