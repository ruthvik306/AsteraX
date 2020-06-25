using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public TMP_Text scoretext;
    void Awake()
    {
        scoretext.text = "SCORE :  " + score.ToString("0");
    }

        // Update is called once per frame
        void Update()
        {
        scoretext.text = "SCORE :  " + score.ToString("0");
        }
}
