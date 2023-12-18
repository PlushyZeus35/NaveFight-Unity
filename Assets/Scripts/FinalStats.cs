using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalStats : MonoBehaviour
{
    public Text pointText;
    public Button retryButton;
    // Start is called before the first frame update
    void Start()
    {
        int puntuacion = PlayerPrefs.GetInt("puntuacion");
        pointText.text = "Puntos: " + puntuacion;
        retryButton.onClick.AddListener(RetryGame);
    }

    void RetryGame(){
        SceneManager.LoadScene("MainScene");
    }
}
