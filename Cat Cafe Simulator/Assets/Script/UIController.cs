using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button[] buyButton;
    public Text[] itemCount;
    public Text money;
    public GameObject character;

    public Button bagButton;
    public GameObject bagLayout;
    public bool bagisAble = true;
    // Start is called before the first frame update
    void Start()
    {

      for(int i = 0; i < 7; i += 1)
      {
        int j = i;
        buyButton[i].onClick.AddListener(delegate{buyItem(j);});
      }

      bagButton.onClick.AddListener(delegate{bagLayout.SetActive(!bagisAble);  bagisAble = !bagisAble;});
    }

    void buyItem(int index)
    {
      if(character.GetComponent<Character>().data.money - character.GetComponent<Character>().toys[index].cost < 0)
      {
        return;
      }

      character.GetComponent<Character>().data.curToys[index] += 1;
      character.GetComponent<Character>().data.money -= character.GetComponent<Character>().toys[index].cost;
      itemCount[index].text = character.GetComponent<Character>().data.curToys[index] + "";
      itemCount[index].color = Color.green;

      money.text = character.GetComponent<Character>().data.money + "";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
