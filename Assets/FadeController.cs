using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image _blackFade = default;
    void Start()
    {
        _blackFade.canvasRenderer.SetAlpha(0.01f);
    }

    public void FadeOut()
    {
        _blackFade.canvasRenderer.SetAlpha(0.01f);
        _blackFade.CrossFadeAlpha(1f, 4.3f, false);
    }
}
