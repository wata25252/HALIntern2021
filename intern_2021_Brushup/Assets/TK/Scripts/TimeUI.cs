using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private TK.GameManager _gameManager;
    
    private Text _timeText;
    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<TK.GameManager>();
        _timeText = this.gameObject.GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        _timeText.text = ((int)_gameManager._timeCount).ToString();
    }
}
