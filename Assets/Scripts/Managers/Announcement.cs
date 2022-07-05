using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Announcement : MonoBehaviour
{
    public Text _generalText;

    public Text _fundstext;

    [SerializeField]
    private AnimationCurve _alphaCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private float _remainingShowTime = 0;
    private float _totalShowTime = 0;

    public void Show(string text, float duration, Text _textObjectToShow)
    {
        _textObjectToShow.text = text;
        _remainingShowTime = duration;
        _totalShowTime = duration;
        _textObjectToShow.gameObject.SetActive(true);

        StartCoroutine(DisplayTextFade(_textObjectToShow));
    }

    public void UpdateText(string text, Text _textObjectToShow)
    {
        _textObjectToShow.gameObject.SetActive(true);
        _textObjectToShow.text = text;

        _textObjectToShow.color = new Color(_textObjectToShow.color.r, _textObjectToShow.color.g, _textObjectToShow.color.b, 1);
    }

    private IEnumerator DisplayTextFade(Text _textObjectToShow)
    {
        LeanTween.value(_textObjectToShow.gameObject, 1, 0, _totalShowTime).setEase(LeanTweenType.linear).setOnUpdate((float val) =>
        {
            Color newColor = _textObjectToShow.color;
            newColor.a = val;
            _textObjectToShow.color = newColor;
        });

        yield return new WaitForSeconds(_totalShowTime);

        _textObjectToShow.gameObject.SetActive(false);
        //ClassRefrencer.instance.UIManager.DeactivateSpecificScreens(new UIScreenTypes[] { UIScreenTypes.SystemMessages });
    }
}
