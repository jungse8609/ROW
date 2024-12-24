using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingControl : MonoBehaviour
{
    // Volume Component
    private Volume _volume;

    // hp option
    [SerializeField] public int _hp;
    [SerializeField] public int _hpBoundary = 50;

    // postprocessing variable
    private Vignette _vignette;
    private ChromaticAberration _chromaticAberration;
    private DepthOfField _depthOfField;


    private float _vignetteIntensity;

    private bool _lowHpFlag;

    


    void Start()
    {
        _volume = GetComponent<Volume>();

        _volume.profile.TryGet(out _vignette);
        _volume.profile.TryGet(out _chromaticAberration);
        _volume.profile.TryGet(out _depthOfField);

        _vignetteIntensity = _vignette.intensity.value;
    }

    // Update is called once per frame
    void Update()
    {
        LowHP(Time.time);
    }

    void LowHP(float time)
    {
        if (_hp < _hpBoundary)
        {
            // low hp action
            _vignette.intensity.value = Mathf.Sin(time * 2f) * 0.025f + _vignetteIntensity;
            _vignette.color.value = Color.Lerp(_vignette.color.value, Color.red, time * 0.001f);

            _chromaticAberration.intensity.value = Mathf.Lerp(_chromaticAberration.intensity.value, 1, time * 0.001f);

            _depthOfField.focalLength.value = Mathf.Lerp(_depthOfField.focalLength.value, 200, time * 0.2f);

            _lowHpFlag = true;
        } else if (_lowHpFlag)
        {
            // idle action
            _vignette.color.value = Color.black;
            _vignette.intensity.value = _vignetteIntensity;

            _chromaticAberration.intensity.value = 0f;

            _depthOfField.focalLength.value = 1f;

            _lowHpFlag = false;
        }
    }
}
