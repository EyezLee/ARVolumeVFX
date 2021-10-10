using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

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

    [VFXPropertyBinding("UnityEngine.Vector4"), SerializeField]
    ExposedProperty _projectionVectorProperty = "ProjectionVector";

    [VFXPropertyBinding("UnityEngine.Matrix4x4"), SerializeField]
    ExposedProperty _inverseViewMatrixProperty = "InverseViewMatrix";

    public override bool IsValid(VisualEffect component)
      => component.HasTexture(_colorMapProperty) &&
         component.HasTexture(_depthMapProperty) &&
         component.HasVector4(_projectionVectorProperty) &&
         component.HasMatrix4x4(_inverseViewMatrixProperty);

    public override void UpdateBinding(VisualEffect component)
    {
        var lidar = Singletons.LidarDataProcessor;
        var prj = ProjectionUtil.ProjectionVector;
        var v2w = lidar.lidarData.CameraToWorldMatrix;
        component.SetTexture(_colorMapProperty, lidar.lidarData.ColorTexture);
        component.SetTexture(_depthMapProperty, lidar.lidarData.DepthTexture);
        component.SetVector4(_projectionVectorProperty, prj);
        component.SetMatrix4x4(_inverseViewMatrixProperty, v2w);
    }

    public override string ToString()
      => $"Lidar Data : {_colorMapProperty}, {_depthMapProperty}";
}
