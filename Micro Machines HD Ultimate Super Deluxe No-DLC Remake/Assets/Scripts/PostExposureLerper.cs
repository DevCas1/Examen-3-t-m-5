using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class PostExposureLerper : MonoBehaviour
{
    public float Duration;
    public bool TriggerOnComplete;
    public UnityEvent OnComplete;
    public bool TriggerOnReset;
    public UnityEvent OnReset;

    private PostProcessProfile _sharedProfile;
    private ColorGrading _gradingEffect;
    private float _originalExposure;

    private void Start()
    {
        _sharedProfile = GetComponent<PostProcessVolume>().sharedProfile;
        if (!_sharedProfile.TryGetSettings(out _gradingEffect)) return;
        _originalExposure = _gradingEffect.postExposure;
    }

    public void SetTriggerOnComplete(bool value) => TriggerOnComplete = value;

    public void SetTriggerOnReset(bool value) => TriggerOnReset = value;

    public void SetPostExposure(float value)
    {
        if (TriggerOnComplete)
            DOTween.To(x => _gradingEffect.postExposure.value = x, _gradingEffect.postExposure.value, value, Duration).OnComplete(OnComplete.Invoke);
        else
            DOTween.To(x => _gradingEffect.postExposure.value = x, _gradingEffect.postExposure.value, value, Duration);
    }

    public void SetPostExposureImmediately(float value)
    {
        if (_gradingEffect == null) return;
        _gradingEffect.postExposure.value = value;
        if (TriggerOnComplete) OnComplete.Invoke();
    }

    public void ResetPostExposure()
    {
        if (TriggerOnReset)
            DOTween.To(x => _gradingEffect.postExposure.value = x, _gradingEffect.postExposure.value, _originalExposure, Duration).OnComplete(OnReset.Invoke);
        else
            DOTween.To(x => _gradingEffect.postExposure.value = x, _gradingEffect.postExposure.value, _originalExposure, Duration);
    }

    private void OnDisable() => _gradingEffect.postExposure.value = _originalExposure;
}