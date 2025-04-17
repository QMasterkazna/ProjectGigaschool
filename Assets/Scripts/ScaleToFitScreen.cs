using UnityEngine;

[ExecuteAlways]
public class ScaleToFitScreen : MonoBehaviour {
    public float _targetWidth = 1170;
    public float _targetHeight = 2532;

    private RectTransform _rectTransform;
    private int _lastScreenWidth;
    private int _lastScreenHeight;
    private float _targetAspect;

    void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _lastScreenWidth = Screen.width;
        _lastScreenHeight = Screen.height;
        
        UpdateScale();
    }

    void LateUpdate() {
        if (Screen.width == _lastScreenWidth && Screen.height == _lastScreenHeight) return;
        _lastScreenWidth = Screen.width;
        _lastScreenHeight = Screen.height;
        UpdateScale();
    }

    void UpdateScale() {
        if (_rectTransform == null)
            return;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        _targetAspect = _targetWidth / _targetHeight;
        var screenAspect = screenWidth / screenHeight;

        var scaleFactor = 1f;

        if (screenAspect >= _targetAspect) {
            scaleFactor = screenHeight / _targetHeight;
        }
        else {
            scaleFactor = screenWidth / _targetWidth;
        }

        _rectTransform.localScale = new(scaleFactor, scaleFactor, 1f);
    }
}