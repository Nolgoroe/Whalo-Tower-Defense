using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Announcement : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    [SerializeField]
    private CanvasGroup _canvasGroup = null;

    [SerializeField]
    private AnimationCurve _alphaCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private float _remainingShowTime = 0;
    private float _totalShowTime = 0;

    public void Show(string text, float duration)
    {
        _text.text = text;
        _remainingShowTime = duration;
        _totalShowTime = duration;

        StartCoroutine(DisplayTextFade());
    }

    public void UpdateText(string text)
    {
        _text.text = text;
        _canvasGroup.alpha = 1;
    }

    private IEnumerator DisplayTextFade()
    {
        while (_remainingShowTime >= 0)
        {
            _remainingShowTime -= Time.deltaTime;
            _canvasGroup.alpha = _alphaCurve.Evaluate(1 - (_remainingShowTime / _totalShowTime));
            yield return new WaitForSeconds(Time.deltaTime);
        }

        ClassRefrencer.instance.UIManager.DeactivateSpecificScreens(new UIScreenTypes[] { UIScreenTypes.SystemMessages });
    }
}
