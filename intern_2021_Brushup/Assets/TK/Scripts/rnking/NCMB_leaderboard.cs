using NCMB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NCMB_leaderboard : MonoBehaviour
{
    private NCMB_Ranker _currentRecord; //今回の記録

    [SerializeField] private bool _isOnline = false;//デバッグ用
    [SerializeField] private GameObject _InputObject; //入力欄
    [SerializeField] private GameObject _resultBGM; //BGM
    [SerializeField] private GameObject _gameBGM; //BGM

    //シーン遷移先
    [SerializeField] private string _mainSceneName;
    [SerializeField] private string _titleSceneName;

    //フェーダー
    [SerializeField] private GameObject Fader;

    //表示する文字列
    [SerializeField] private Text _header;
    [SerializeField] private Text _failedHeader;
    [SerializeField] private Text[] _top = new Text[5];
    [SerializeField] private Text[] _top_score = new Text[5];

    //選択肢
    [SerializeField] private List<Text> _options;
    [SerializeField] private List<Text> _failedOptions;

    [SerializeField] private int OptionNum;

    public List<NCMB_Ranker> _topRankers = null;

    private bool _isRankIn = false;

    private int _rankingNum = 999;
    private bool _endInput;
    private SE _se;

    enum RankingState
    {
        BEGIN,
        FETCH,
        VIEW,
        FAILED,
        INPUT,
        SELECT,
    }
    [SerializeField] private RankingState _state = RankingState.BEGIN;

    private void Start()
    {
        _InputObject.SetActive(false);
        _se = GameObject.Find("SEManager").GetComponent<SE>();
    }

    void Update()
    {
        switch (_state)
        {
            case RankingState.BEGIN:
                {

                }
                break;
            case RankingState.INPUT:
                {


                }
                break;
            case RankingState.FAILED:
                {
                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Debug.Log(_failedOptions.Count);
                        OptionNum = (OptionNum + 1) % (_failedOptions.Count);
                        _se.Play(4);
                    }
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        OptionNum = (OptionNum + _failedOptions.Count - 1) % (_failedOptions.Count);
                        _se.Play(4);
                    }
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                    {
                        SelectFailedOption();
                        _se.Play(5);
                    }
                }
                break;
            case RankingState.SELECT:
                {

                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Debug.Log(_options.Count);
                        OptionNum = (OptionNum + 1) % (_options.Count);
                        _se.Play(4);
                    }
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        OptionNum = (OptionNum + _options.Count - 1) % (_options.Count);
                        _se.Play(4);
                    }
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                    {
                        SelectOption();
                        _se.Play(5);
                    }

                }
                break;
            default:
                { }
                break;
        }




    }

    private void FixedUpdate()
    {

        switch (_state)
        {
            case RankingState.BEGIN:
                {

                }
                break;
            case RankingState.FETCH:
                {
                    if (fetchTopRankers()&&_isOnline)
                    {
                        for (int i = 0; i < _failedOptions.Count; i++)
                        {
                            _failedHeader.GetComponent<TK.ViewUi>().HideMessage();

                            if (_failedOptions[i].text.Length > 0)
                            {
                                _failedOptions[i].GetComponent<TK.ViewUi>().HideMessage();
                            }
                        }
                        _state = RankingState.VIEW;
                    }
                    else
                    {
                        _failedHeader.GetComponent<TK.ViewUi>().ShowWindow();

                        for (int i = 0; i < _failedOptions.Count; i++)
                        {
                            _failedOptions[i].GetComponent<TK.ViewUi>().ShowWindow();
                        }
                        _state = RankingState.FAILED;
                    }
                }
                break;
            case RankingState.FAILED:
                {
                    //選択された項目の演出
                    for (int i = 0; i < _failedOptions.Count; i++)
                    {
                        if (OptionNum == i)
                        {
                            _failedOptions[i].GetComponentInChildren<TK.SelectUi>().SetSelected();
                        }
                        else
                        {
                            _failedOptions[i].GetComponentInChildren<TK.SelectUi>().SetUnSelected();
                        }

                    }

                }
                break;
            case RankingState.VIEW:
                {
                    // ランキングの取得が完了したら1度だけ実行
                    if (_topRankers != null)
                    {
                        _topRankers.Add(_currentRecord);
                        _topRankers.Sort((a, b) => b.score - a.score);
                        bool first = true;

                        _header.GetComponent<TK.ViewUi>().ShowWindow();
                        for (int i = 0; i < 5; i++)
                        {
                            if (_topRankers[i].name == _currentRecord.name && _topRankers[i].score == _currentRecord.score && first)
                            {
                                _top[i].DOColor(Color.yellow, 1.0f);
                                _rankingNum = i;
                                first = false;
                                _isRankIn = true;
                            }

                            //_top[i].GetComponent<TK.ViewUi>().SetMessage(i + 1 + ". " + _topRankers[i].score.ToString() + "人");
                            _top[i].GetComponent<TK.ViewUi>().SetMessage(i + 1 + ". " + _topRankers[i].name);
                            _top_score[i].GetComponent<TK.ViewUi>().SetMessage(_topRankers[i].score.ToString());
                            _top[i].GetComponent<TK.ViewUi>().ShowWindow();
                            _top_score[i].GetComponent<TK.ViewUi>().ShowWindow();
                        }

                        if (_isRankIn)
                        {
                            _InputObject.SetActive(true);
                            _state = RankingState.INPUT;
                        }
                        else
                        {
                            for (int i = 0; i < _options.Count; i++)
                            {
                                _options[i].GetComponent<TK.ViewUi>().ShowWindow();
                            }
                            _state = RankingState.SELECT;
                        }
                    }
                }
                break;
            case RankingState.INPUT:
                {
                    if (!_endInput)
                    {

                    }
                    else
                    {
                        _top[_rankingNum].GetComponent<TK.ViewUi>().SetMessage(_rankingNum + 1 + ". " + _currentRecord.name);
                        _top[_rankingNum].GetComponent<TK.ViewUi>().ShowWindow();
                        for (int i = 0; i < 2; i++)
                        {
                            _options[i].GetComponent<TK.ViewUi>().ShowWindow();
                        }
                        _state = RankingState.SELECT;
                    }
                }
                break;
            case RankingState.SELECT:
                {
                    //選択された項目の演出
                    for (int i = 0; i < _options.Count; i++)
                    {
                        if (OptionNum == i)
                        {
                            _options[i].GetComponentInChildren<TK.SelectUi>().SetSelected();
                        }
                        else
                        {
                            _options[i].GetComponentInChildren<TK.SelectUi>().SetUnSelected();
                        }

                    }
                }
                break;
            default:
                { }
                break;
        }
    }
    public void SelectOption()
    {
        switch (OptionNum)
        {
            case 0:
                {
                    Fader.GetComponent<IrisIn>().StartFade(_titleSceneName);
                }
                break;
            case 1:
                {
                    Fader.GetComponent<IrisIn>().StartFade(_mainSceneName);
                }
                break;
        }
    }
    public void SelectFailedOption()
    {
        switch (OptionNum)
        {
            case 0:
                {
                    _state = RankingState.FETCH;
                }
                break;
            case 1:
                {
                    Fader.GetComponent<IrisIn>().StartFade(_titleSceneName);

                }
                break;
            case 2:
                {
                    Fader.GetComponent<IrisIn>().StartFade(_mainSceneName);

                }
                break;
        }
    }

    public void SaveScore(string name, int score)
    {
        _currentRecord = new NCMB_Ranker(score, name);
    }

    private void UploadRecord()
    {
        NCMBObject obj = new NCMBObject("GameScore");
        obj["name"] = _currentRecord.name;
        obj["score"] = _currentRecord.score;
        obj.SaveAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("保存に失敗しました。エラーコード:" + e.ErrorCode);
                //エラー処理
            }
            else
            {
                Debug.Log("保存に成功しました。ObjectId:" + obj.ObjectId);
                //成功時の処理
            }
        });
    }
    public void ViewRanking()
    {
        _gameBGM.GetComponent<AudioSource>().Stop();
        _resultBGM.GetComponent<AudioSource>().Play();

        _state = RankingState.FETCH;
    }
    public void InputPlayerName()
    {
        _endInput = true;

        _currentRecord = new NCMB_Ranker(_currentRecord.score, _InputObject.GetComponent<InputField>().text);
        _InputObject.SetActive(false);

        UploadRecord();
    }

    private bool fetchTopRankers()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("GameScore");

        query.OrderByDescending("score");

        query.Limit = 5;

        bool isConnected = true;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("検索に失敗しました。エラーコード：" + e.ErrorCode);
                isConnected = false;
            }
            else
            {
                Debug.Log("検索に成功しました。");

                List<NCMB_Ranker> list = new List<NCMB_Ranker>();
                foreach (NCMBObject obj in objList)
                {
                    int s = System.Convert.ToInt32(obj["score"]);
                    string n = System.Convert.ToString(obj["name"]);
                    list.Add(new NCMB_Ranker(s, n));
                }
                _topRankers = list;
            }
        });

        return isConnected;
    }

}

