using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PostProcessingControl : MonoBehaviour
{
    // Volume Component
    private Volume _volume;

    // hp option
    private float _hpBoundary = 50f;

    [SerializeField] private GameObject _player = default;
    [SerializeField] private GameObject _camera = default;

    [SerializeField] private PlayerStatSO _playerStat = default;

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

        _hpBoundary = _playerStat.MaxHp * 0.3f;
        Debug.Log($"Max HP : {_playerStat.MaxHp}, Hp Boundary : {_playerStat.MaxHp * 0.3f}");
    }

    // Update is called once per frame
    void Update()
    {
        LowHP(Time.time);
        if (_player != null && _camera != null)
        {
            Vector3 camera_to_player = _player.transform.position - _camera.transform.position;
            float dept_of_field = Vector3.Dot(camera_to_player, _camera.transform.forward);
            _depthOfField.focusDistance.value = dept_of_field;
            //Debug.Log($"Player Position: {_player.transform.position}, Camera Position: {_camera.transform.position}");
        }
    }

    void LowHP(float time)
    {
        if (_playerStat.CurrentHp < _hpBoundary)
        {
            // low hp action
            _vignette.intensity.value = Mathf.Sin(time * 2f) * 0.025f + 0.05f + _vignetteIntensity;
            _vignette.color.value = Color.Lerp(_vignette.color.value, Color.red, time * 0.001f);

            _chromaticAberration.intensity.value = Mathf.Lerp(_chromaticAberration.intensity.value, 0.5f, time * 0.001f);

            _depthOfField.focalLength.value = Mathf.Lerp(_depthOfField.focalLength.value, 300, time * 0.2f);

            _lowHpFlag = true;
        }
        else if (_lowHpFlag)
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
