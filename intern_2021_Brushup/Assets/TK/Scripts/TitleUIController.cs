using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleUIController : MonoBehaviour
{
    private bool _isQuit=false;

    [SerializeField]
    private GameObject Fader;
    [SerializeField]
    private string _mainSceneName;
    [SerializeField]
    private GameObject _logo;

    [SerializeField]
    private AnimationCurve _logoFadeCurve;

    [SerializeField]
    private float _time=0.0f;
    private float _uiTime=0.0f;

    private float _timeScale=1.0f;

    [SerializeField]
    private bool _skyView = true;

    private bool _canInput = false;
    private int _viewCount = 0;

    [SerializeField]
    List<GameObject> _options = new List<GameObject>();

    private CanvasRenderer _rd;

    //選択しているオプション
    [SerializeField]
    private int OptionNum;

    private SE _se;

    [SerializeField]
    private TM.Discription _discription; // ゲーム説明コンポーネント

    // イントロ再生中か
    public bool IsSkyView { get => _skyView; private set => _skyView = value; }
    // 入力を受け付けるか
    public bool AcceptInput { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        _se = GameObject.Find("SEManager").GetComponent<SE>();
        _rd = _logo.GetComponent<CanvasRenderer>();
        _rd.SetAlpha(0.0f);

        AcceptInput = true;
    }


    // Update is called once per frame
    private void Update()
    {
        if (_isQuit)
        {
            Application.Quit();
        }
        if (!_skyView && _canInput && AcceptInput)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log(_options.Count);
                OptionNum = (OptionNum + 1) % (_options.Count-1);
                _se.Play(4);
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                OptionNum = (OptionNum + _options.Count - 2) % (_options.Count-1);
                _se.Play(4);
            }
            if (Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown(KeyCode.Space))
            {
                SelectOption();
                _se.Play(5);
            }
        }
        else if (_skyView)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                _timeScale = 5.0f;
            }
        }
    }
    private void FixedUpdate()
    {
        _time += Time.deltaTime * _timeScale;

        for (int i = 1; i < _options.Count; i++)
        {
            if (OptionNum + 1 == i)
            {
                _options[i].GetComponentInChildren<TK.MoveUi>().SetSelected();
            }
            else
            {
                _options[i].GetComponentInChildren<TK.MoveUi>().SetUnSelected();
            }
        }

        if (_skyView)
        {
            _rd.SetAlpha(_logoFadeCurve.Evaluate(_time / 8.0f));
            if (_time / 8.0f > 1.0f)
            {
                _skyView = false;
            }
        }
        else if (!_skyView)
        {
            _uiTime += Time.deltaTime;
            if (_uiTime > 0.5f)
            {
                _uiTime = 0;
                if (_viewCount < _options.Count)
                {
                    _options[_viewCount].GetComponentInChildren<TK.MoveUi>().ShowWindow();
                    _viewCount++;
                }
                if (_viewCount == _options.Count-1)
                {
                    _canInput = true;
                }
            }
        }

    }

    public void SelectOption()
    {
        if (!_skyView)
            switch (OptionNum)
            {
                case 0:
                    {
                        Fader.GetComponent<IrisIn>().StartFade(_mainSceneName);
                    }
                    break;
                case 1:
                    {
                        _discription.ShowDiscription = true;
                    }
                    break;
                case 2:
                    {
                        _isQuit = true;
                        Debug.Log("終了");
                    }
                    break;

            }
    }
    public float GetTime()
    {
        return _time;
    }
}
