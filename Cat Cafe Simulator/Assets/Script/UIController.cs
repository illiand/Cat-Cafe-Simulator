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

    public Canvas battleLayout;
    public bool inBattle;
    public Text CatAttriText;

    public Image hintPic;
    public Text hintText;
    private float hintDuration;
    private float hintCurTime;
    private Color hintTargetColor;

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
        showHint("No more money lift to buy the toy.", new Color(0.8f, 0.0f, 0.0f), 1.5f);

        return;
      }

      showHint("Successfully bought " + character.GetComponent<Character>().toys[index].name + ".", new Color(0.0f, 0.8f, 0.0f), 1.5f);

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

      battleLayout.gameObject.SetActive(inBattle);

      if(hintCurTime < hintDuration)
      {
        processHint();
      }

      if(!inBattle) return;

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

      if(character.GetComponent<Character>().data.curCatStatus.favorability == -1)
      {
        FlavPics[7].color = Color.red;
      }
      else
      {
        FlavPics[7].color = Color.green;
      }

      CatAttriText.text = character.GetComponent<Character>().data.curCatStatus.cat.name + " ~ " + character.GetComponent<Character>().data.curCatStatus.cat.characteristic + " ~";
    }

    public void showHint(string content, Color rgb, float duration)
    {
      hintDuration = duration;
      hintCurTime = 0;
      hintTargetColor = rgb;

      hintText.text = content;
    }

    private void processHint()
    {
      if(hintCurTime < hintDuration * 0.2f)
      {
        hintPic.color = new Color(0.0f, 0.0f, 0.0f, Mathf.Lerp(0.0f, 0.5f, hintCurTime / (hintDuration * 0.2f)));
        hintText.color = new Color(hintTargetColor.r, hintTargetColor.g, hintTargetColor.b, Mathf.Lerp(0.0f, 1.0f, hintCurTime / (hintDuration * 0.2f)));
      }

      if(hintDuration * 0.6f < hintCurTime)
      {
        hintPic.color = new Color(0.0f, 0.0f, 0.0f, Mathf.Lerp(0.5f, 0.0f, (hintCurTime - hintDuration * 0.6f) / (hintDuration * 0.4f)));
        hintText.color = new Color(hintTargetColor.r, hintTargetColor.g, hintTargetColor.b, Mathf.Lerp(1.0f, 0.0f, (hintCurTime - hintDuration * 0.6f) / (hintDuration * 0.4f)));
      }

      hintCurTime += Time.deltaTime;
    }
}
