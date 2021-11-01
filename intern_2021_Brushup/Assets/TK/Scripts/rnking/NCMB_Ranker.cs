using UnityEngine;
using System.Collections;

public class NCMB_Ranker
{

    public int score { get; set; }
    public string name { get; private set; }

    // コンストラクタ -----------------------------------
    public NCMB_Ranker(int _score, string _name)
    {
        score = _score;
        name = _name;
    }

    // ランキングで表示するために文字列を整形 -----------
    public string print()
    {
        return name + ' ' + score;
    }
}