using UnityEngine;

static class ShaderID
{
    public static readonly int TextureY = Shader.PropertyToID("_textureY");
    public static readonly int TextureCbCr = Shader.PropertyToID("_textureCbCr");
    public static readonly int HumanStencil = Shader.PropertyToID("_HumanStencil");
    public static readonly int EnvironmentDepth = Shader.PropertyToID("_EnvironmentDepth");
    public static readonly int DepthRange = Shader.PropertyToID("_DepthRange");
    public static readonly int AspectFix = Shader.PropertyToID("_AspectFix");
}

static class Singletons
{
    static LidarDataProcessor _lidarDataProcessor;
    public static LidarDataProcessor LidarDataProcessor
        => _lidarDataProcessor != null ? _lidarDataProcessor :
        (_lidarDataProcessor = Object.FindObjectOfType<LidarDataProcessor>());
        
        
}

static class ProjectionUtil
{
    public static Vector4 GetVector(in Matrix4x4 m)
      => new Vector4(m[0, 2], m[1, 2], m[0, 0], m[1, 1]);

    public static Vector4 ProjectionVector
      => GetVector(Singletons.LidarDataProcessor.lidarData.projectionMatrix);
}
