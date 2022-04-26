using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button[] buyButton;
    public Button[] useToyButton;
    public Text[] itemCount;
    public Text money;
    public GameObject character;

    public Image[] actionPointPics;

    public Image[] FlavPics;

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

      for(int i = 0; i < 7; i += 1)
      {
        int j = i;
        useToyButton[i].onClick.AddListener(delegate{useItem(j);});
      }

      bagButton.onClick.AddListener(delegate{bagLayout.SetActive(!bagisAble);  bagisAble = !bagisAble;});
    }

    void buyItem(int index)
    {
      if(character.GetComponent<Character>().data.money - character.GetComponent<Character>().toys[index].cost < 0)
      {
        return;
      }

      character.GetComponent<Character>().data.curToys[index] += 3;
      character.GetComponent<Character>().data.money -= character.GetComponent<Character>().toys[index].cost;

      money.text = character.GetComponent<Character>().data.money + "";
    }

    void useItem(int index)
    {
      character.GetComponent<Character>().useToy(character.GetComponent<Character>().toys[index].name);
    }

    // Update is called once per frame
    void Update()
    {
      for(int i = 0; i < 3; i += 1)
      {
        actionPointPics[i].enabled = i < character.GetComponent<Character>().data.actionPoint;
      }

      for(int i = 0; i < 7; i += 1)
      {
        if(i < character.GetComponent<Character>().data.curCatStatus.favorability)
        {
          FlavPics[i].color = Color.green;
        }
        else
        {
          FlavPics[i].color = Color.white;
        }
      }

      for(int i = 0; i < 7; i += 1)
      {
        itemCount[i].text = character.GetComponent<Character>().data.curToys[i] + "";

        if(character.GetComponent<Character>().data.curToys[i] > 0)
        {
          itemCount[i].color = Color.green;
        }
        else
        {
          itemCount[i].color = Color.red;
        }
      }
    }
}
