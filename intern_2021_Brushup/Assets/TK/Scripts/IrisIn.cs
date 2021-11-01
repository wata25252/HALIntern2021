using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IrisIn : MonoBehaviour
{
    Material _mt;
    float _value = 0.0f;
    const float _maxValue = 2.0f;
    [SerializeField]
    string _nextScenename;

    bool _isFading = false;
    private void Start()
    {
        _mt =GetComponent<Image>().material;
        _mt.SetFloat("_FadeValue", 0.0f);
    }

    private void FixedUpdate()
    {
        var screen = new Vector2(Screen.width, Screen.height);
        _mt.SetVector("_Aspect", screen);

        if (_isFading)
        {
            if (_value < 0.0f)
            {
                _value = 0.0f;
                SceneManager.LoadScene(_nextScenename);
            }
            else
            {
                _mt.SetFloat("_FadeValue", _value);
                _value -= 0.04f;
            }
        }
        else
        {
            _mt.SetFloat("_FadeValue", _value);
            _value += 0.04f;
            if(_maxValue< _value)
            {
                _value = _maxValue;
            }
        }
    }

    public void StartFade(string nextscene)
    {
        _nextScenename = nextscene;
        _isFading = true;
    }

}