using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;
using System;


[RequireComponent(typeof(VFXGraphPropertyMenu))]
[VFXBinder("LidarData")]
public class VFXLidarDataBinder : VFXBinderBase
{
    public string ColorMapProperty
    {
        get => (string)_colorMapProperty;
        set => _colorMapProperty = value;
    }

    public string DepthMapProperty
    {
        get => (string)_depthMapProperty;
        set => _depthMapProperty = value;
    }

    public string EnvironmentMeshProperty
    {
        get => (string)_environmentMeshProperty;
        set => _environmentMeshProperty = value;
    }
    public string ProjectionVectorProperty
    {
        get => (string)_projectionVectorProperty;
        set => _projectionVectorProperty = value;
    }

    public string InverseViewMatrixProperty
    {
        get => (string)_inverseViewMatrixProperty;
        set => _inverseViewMatrixProperty = value;
    }

    [VFXPropertyBinding("UnityEngine.Texture2D"), SerializeField]
    ExposedProperty _colorMapProperty = "ColorMap";

    [VFXPropertyBinding("UnityEngine.Texture2D"), SerializeField]
    ExposedProperty _depthMapProperty = "DepthMap";

    [VFXPropertyBinding("UnityEngine.Mesh"), SerializeField]
    ExposedProperty _environmentMeshProperty = "EnvironmentMesh";

    [VFXPropertyBinding("UnityEngine.Vector4"), SerializeField]
    ExposedProperty _projectionVectorProperty = "ProjectionVector";

    [VFXPropertyBinding("UnityEngine.Matrix4x4"), SerializeField]
    ExposedProperty _inverseViewMatrixProperty = "InverseViewMatrix";

    VFXGraphPropertyMenu vfxMenu;
    
    protected override void Awake()
    {
        vfxMenu = GetComponent<VFXGraphPropertyMenu>();
    }

    public override bool IsValid(VisualEffect component)
    {
        bool isValid = true;

        if (vfxMenu.UseColorMap) isValid &= component.HasTexture(_colorMapProperty);
        if (vfxMenu.UseDepthMap) isValid &= component.HasTexture(_depthMapProperty);
        if (vfxMenu.UseEnvironmentMesh) isValid &= component.HasMesh((_environmentMeshProperty));
        isValid &= component.HasVector4(_projectionVectorProperty);
        isValid &= component.HasMatrix4x4(_inverseViewMatrixProperty);

        return isValid;
    }

    public override void UpdateBinding(VisualEffect component)
    {
        var lidar = Singletons.LidarDataProcessor;
        var prj = ProjectionUtil.ProjectionVector;
        var v2w = lidar.lidarData.CameraToWorldMatrix;
        if(vfxMenu.UseColorMap) component.SetTexture(_colorMapProperty, lidar.lidarData.ColorTexture);
        if(vfxMenu.UseDepthMap) component.SetTexture(_depthMapProperty, lidar.lidarData.DepthTexture);
        if (vfxMenu.UseEnvironmentMesh) component.SetMesh(_environmentMeshProperty, lidar.lidarData.EnvironmentMesh);
        component.SetVector4(_projectionVectorProperty, prj);
        component.SetMatrix4x4(_inverseViewMatrixProperty, v2w);
    }

    public override string ToString()
      => $"Lidar Data : {_colorMapProperty}, {_depthMapProperty}";
}
