using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public Button gameStartButton;
    public TMP_Text startText;

    // Start is called before the first frame update
    void Start()
    {
        gameStartButton.onClick.AddListener(delegate{
          SceneManager.LoadScene("CoffeeShop");
        });
    }

    // Update is called once per frame
    void Update()
    {
      startText.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Abs(Mathf.Sin(Time.time * 1.5f)));
    }
}
