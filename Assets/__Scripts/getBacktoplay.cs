using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class getBacktoplay : MonoBehaviour
{
    int score = ScoreManager.score;

    public TMP_Text scoreText;

    private void Awake()
    {
        scoreText.text ="FINAL SCORE : " +  score.ToString("0");
         Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Game_Over")
        {
            StartCoroutine(getback());
        }
    }
    IEnumerator getback()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("_Scene_0");
    }
}
