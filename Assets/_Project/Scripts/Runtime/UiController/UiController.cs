using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    private GameManager gameManager;

    public TextMeshProUGUI lifesText;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowUi();
    }

    private void ShowUi()
    {
        lifesText.text = "x" + gameManager.lifes.ToString();
        scoreText.text = gameManager.score.ToString();
    }
}
