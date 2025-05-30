using UnityEngine;
using UnityEngine.UI;

public class VolumeUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // 초기값 세팅
        bgmSlider.value = SoundManager.Instance.BGMVolume;
        sfxSlider.value = SoundManager.Instance.SFXVolume;

        // 값 변경 리스너
        bgmSlider.onValueChanged.AddListener(OnBGMChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXChanged);
    }

    private void OnBGMChanged(float value)
    {
        SoundManager.Instance.BGMVolume = value;
    }

    private void OnSFXChanged(float value)
    {
        SoundManager.Instance.SFXVolume = value;
    }

    private void OnDestroy()
    {
        // 안전하게 리스너 해제
        bgmSlider.onValueChanged.RemoveListener(OnBGMChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXChanged);
    }
}
