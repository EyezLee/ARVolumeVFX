using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LidarDataProcessor : MonoBehaviour
{
    #region External scene object references

    [Space]
    [SerializeField] Camera _camera = null;
    [SerializeField] ARCameraManager _cameraManager = null;
    [SerializeField] ARCameraBackground _cameraBackground = null;
    [SerializeField] AROcclusionManager _occlusionManager = null;
    [SerializeField] ARMeshManager _meshManager = null;
    [SerializeField] Shader _textureProcessShader = null;

    #endregion

    #region Editable parameters

    [Space]
    [SerializeField] float _minDepth = 0.2f;
    [SerializeField] float _maxDepth = 3.2f;

    #endregion


    #region Internal objects

    const int _width = 2048;
    const int _height = 1024;

    //  transformation info
    Matrix4x4 _projection;
    Vector3 _cameraPosition;
    Quaternion _cameraRotation;


    //Material _bgMaterial;
    Material _textureProcessMaterial;

    [HideInInspector] public RenderTexture _outputTex;
    RenderTexture _colorTexture;
    RenderTexture _depthTexture;
    [SerializeField] Mesh _enviromentMesh;

    public LidarData lidarData = new LidarData();

    #endregion

    // make camera to world matrix from camera postion and rotation
    Matrix4x4 CalculateCameraToWorldMatrix()
    {
        if (_cameraPosition == Vector3.zero) return Matrix4x4.identity;
        return Matrix4x4.TRS
          (_cameraPosition, _cameraRotation, new Vector3(1, 1, -1));
    }

    void OnCameraFrameReceived(ARCameraFrameEventArgs args)
    {
        // We expect there is at least one texture.
        if (args.textures.Count == 0) return;

        // Try receiving Y/CbCr textures.
        for (var i = 0; i < args.textures.Count; i++)
        {
            var id = args.propertyNameIds[i];
            var tex = args.textures[i];
            if (id == ShaderID.TextureY)
                _textureProcessMaterial.SetTexture(ShaderID.TextureY, tex);
            else if (id == ShaderID.TextureCbCr)
                _textureProcessMaterial.SetTexture(ShaderID.TextureCbCr, tex);
        }

        // Try receiving the projection matrix.
        if (args.projectionMatrix.HasValue)
        {
            _projection = args.projectionMatrix.Value;

            // Aspect ratio compensation (camera vs. 16:9)
            _projection[1, 1] *= (16.0f / 9) / _camera.aspect;
        }

        // receive camera position and rotation
        _cameraPosition = _camera.transform.position;
        _cameraRotation = _camera.transform.rotation;

        // Use the first texture to calculate the source texture aspect ratio.
        var tex1 = args.textures[0];
        var texAspect = (float)tex1.width / tex1.height;

        // Aspect ratio compensation factor for the multiplexer
        var aspectFix = texAspect / (16.0f / 9);
        //_bgMaterial.SetFloat(ShaderID.AspectFix, aspectFix);
        _textureProcessMaterial.SetFloat(ShaderID.AspectFix, aspectFix);
    }


    void OnOcclusionFrameReceived(AROcclusionFrameEventArgs args)
    {
        // Try receiving stencil/depth textures.
        for (var i = 0; i < args.textures.Count; i++)
        {
            var id = args.propertyNameIds[i];
            var tex = args.textures[i];
            if (id == ShaderID.HumanStencil)
                _textureProcessMaterial.SetTexture(ShaderID.HumanStencil, tex);
            else if (id == ShaderID.EnvironmentDepth)
                _textureProcessMaterial.SetTexture(ShaderID.EnvironmentDepth, tex);
        }
    }

    // set up event callback 
    private void OnEnable()
    {
        // Camera callback setup
        _cameraManager.frameReceived += OnCameraFrameReceived;
        _occlusionManager.frameReceived += OnOcclusionFrameReceived;
        _meshManager.meshesChanged += GetEnvironmentMesh;
    }

    void OnDisable()
    {
        // Camera callback termination
        _cameraManager.frameReceived -= OnCameraFrameReceived;
        _occlusionManager.frameReceived -= OnOcclusionFrameReceived;
        _meshManager.meshesChanged -= GetEnvironmentMesh;
    }

    private void Start()
    {
        // Shader setup
        //_bgMaterial = new Material(_textureProcessShader);
        //_bgMaterial.EnableKeyword("RCAM_MONITOR");

        _textureProcessMaterial = new Material(_textureProcessShader);
        //_textureProcessMaterial.EnableKeyword("RCAM_MULTIPLEXER");

        // Custom background material
        //_cameraBackground.customMaterial = _bgMaterial;
        //_cameraBackground.useCustomMaterial = true;

        _outputTex = new RenderTexture(_width, _height, 0);
        _outputTex.Create();
        _colorTexture = new RenderTexture(_width, _height, 0);
        _colorTexture.Create();
        _depthTexture = new RenderTexture(_width, _height, 0);
        _depthTexture.Create();
    }

    // get and process mesh from ARMeshManager
    private void GetEnvironmentMesh(ARMeshesChangedEventArgs args)
    {
        int count = _meshManager.meshes.Count;
        CombineInstance[] instances = new CombineInstance[count];

        for(int i = 0; i < _meshManager.meshes.Count; i++)
        {
            instances[i].mesh = _meshManager.meshes[i].sharedMesh;
            instances[i].transform = _meshManager.meshes[i].transform.localToWorldMatrix;
        }

        _enviromentMesh = new Mesh();
        _enviromentMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; // to get away with 65535 indices limit
        _enviromentMesh.name = "EnviromentMesh";
        _enviromentMesh.CombineMeshes(instances);
    }

    private void Update()
    {
        // Parameter update
        var range = new Vector2(_minDepth, _maxDepth);
        //_bgMaterial.SetVector(ShaderID.DepthRange, range);
        _textureProcessMaterial.SetVector(ShaderID.DepthRange, range);

        // lidar color/depth textures update
        Graphics.Blit(null, _colorTexture, _textureProcessMaterial, 0);
        Graphics.Blit(null, _depthTexture, _textureProcessMaterial, 1);
        // test only
        Graphics.Blit(null, _outputTex, _textureProcessMaterial, 1);

        //GetEnvironmentMesh();

    }

    private void OnRenderObject()
    {
        lidarData = new LidarData
        {
            CameraToWorldMatrix = CalculateCameraToWorldMatrix(),
            projectionMatrix = _projection,
            EnvironmentMesh = _enviromentMesh,

            DepthRange = new Vector2(_minDepth, _maxDepth),
            DepthTexture = _depthTexture,
            ColorTexture = _colorTexture
        };
    }
}
