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
    [SerializeField] Shader _shader = null;

    #endregion

    #region Editable parameters

    [Space]
    [SerializeField] float _minDepth = 0.2f;
    [SerializeField] float _maxDepth = 3.2f;

    #endregion


    #region Internal objects

    const int _width = 2048;
    const int _height = 1024;


    Matrix4x4 _projection;

    Material _bgMaterial;
    Material _muxMaterial;

    RenderTexture _outputTex;

    #endregion


    void OnCameraFrameReceived(ARCameraFrameEventArgs args)
    {

    }


    void OnOcclusionFrameReceived(AROcclusionFrameEventArgs args)
    {

    }

    // set up event callback 
    private void OnEnable()
    {
        // Camera callback setup
        _cameraManager.frameReceived += OnCameraFrameReceived;
        _occlusionManager.frameReceived += OnOcclusionFrameReceived;
    }

    void OnDisable()
    {
        // Camera callback termination
        _cameraManager.frameReceived -= OnCameraFrameReceived;
        _occlusionManager.frameReceived -= OnOcclusionFrameReceived;
    }

    private void Start()
    {
        // Shader setup
        _bgMaterial = new Material(_shader);
        _bgMaterial.EnableKeyword("RCAM_MONITOR");

        _muxMaterial = new Material(_shader);
        _muxMaterial.EnableKeyword("RCAM_MULTIPLEXER");

        // Custom background material
        _cameraBackground.customMaterial = _bgMaterial;
        _cameraBackground.useCustomMaterial = true;

        _outputTex = new RenderTexture(_width, _height, 0);
        _outputTex.Create();
    }

    private void Update()
    {
        // Parameter update
        var range = new Vector2(_minDepth, _maxDepth);
        //_bgMaterial.SetVector(ShaderID.DepthRange, range);
        //_muxMaterial.SetVector(ShaderID.DepthRange, range);

        // NDI sender RT update
        Graphics.Blit(null, _outputTex, _muxMaterial, 0);
    }

}
