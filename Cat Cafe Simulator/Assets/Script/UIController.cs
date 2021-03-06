using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Button[] buyButton;
    public Button[] useToyButton;
    public Text[] itemCount;
    public Text money;
    public Text score;
    public GameObject character;

    public Image[] actionPointPics;

    public Image[] FlavPics;

    public Button bagButton;
    public GameObject bagLayout;
    public bool bagVisible = false;

    public Canvas battleLayout;
    public bool inBattle;
    public Text CatAttriText;

    public Image hintPic;
    public Text hintText;
    private float hintDuration;
    private float hintCurTime;
    private Color hintTargetColor;

    public TMP_Text[] catsNameText;

    public Button giveUpButton;

    public Image interactWithPic;
    public Text interactWithText;

    public Button bookButton;
    public Image bookLayout;
    private bool bookVisible;

    public Button instructionButton;
    public Image instructionLayout;

    public Text goalText;

    public Button leaveButton;
    public Image resultLayout;
    public Text[] resultText;
    public Button returnButton;

    private bool resultRunning = false;
    private float resultLayoutA;

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

      bagButton.onClick.AddListener(delegate{bagLayout.SetActive(!bagVisible);  bagVisible = !bagVisible;});

      giveUpButton.onClick.AddListener(
        delegate
        {

          GameObject curCat = GameObject.Find(character.GetComponent<Character>().data.curCatStatus.cat.name);

          StartCoroutine(curCat.GetComponent<catReact>().WaitForRotation());
          StartCoroutine(curCat.GetComponent<catReact>().WaitForLeave());

          curCat.GetComponent<catReact>().catLeave();
          inBattle = false;
        }
      );

      bookButton.onClick.AddListener(
        delegate
        {
          bookLayout.gameObject.SetActive(!bookVisible);
          bookVisible = !bookVisible;
        }
      );

      instructionButton.onClick.AddListener(
        delegate
        {
          character.GetComponent<Character>().gameStarted = true;
          Destroy(instructionLayout.gameObject);
        }
      );

      leaveButton.onClick.AddListener(
        delegate
        {
          if(resultRunning)
          {
            return;
          }

          resultLayout.gameObject.SetActive(true);

          resultLayout.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
          returnButton.GetComponent<Image>().color = new Color(1.0f, 1.0f, 0.0f, 0.0f);

          for (int i = 0; i < resultText.Length; i += 1)
          {
            resultText[i].color = new Color(1.0f, 1.0f, 0.0f, 0.0f);
          }

          int catSuccussCount = 0;

          for (int i = 0; i < 8; i += 1)
          {
            if(character.GetComponent<Character>().data.curCatResult[i] == 1)
            {
              catSuccussCount += 1;
            }
          }

          resultText[6].text = catSuccussCount + "/8";
          resultText[7].text = character.GetComponent<Character>().data.correctActionCount + "/" + character.GetComponent<Character>().data.wrongActionCount;
          resultText[8].text = character.GetComponent<Character>().data.correctToyCount + "/" + character.GetComponent<Character>().data.wrongToyCount;
          resultText[9].text = "$" + character.GetComponent<Character>().data.moneyUsedOnCat;
          resultText[10].text = "$" + character.GetComponent<Character>().data.moneyUsedOnToy;
          resultText[12].text = character.GetComponent<Character>().data.score + "";

          resultRunning = true;
        }
      );

      returnButton.onClick.AddListener(
        delegate
        {
          SceneManager.LoadScene("MainMenu");
        }
      );
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

      character.GetComponent<Character>().data.moneyUsedOnToy += character.GetComponent<Character>().toys[index].cost;
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

      processResult();
      bindVisibilityWithLeaveButton();

      if(character.GetComponent<Character>().data.quest1 == 0)
      {
        goalText.text = "* Interact with British Shorthhair, Lucy";
      }
      else if(character.GetComponent<Character>().data.quest1 == 1)
      {
        goalText.text = "* Interact with all other cats";
      }

      battleLayout.gameObject.SetActive(inBattle);

      if(hintCurTime < hintDuration)
      {
        processHint();
      }


      if(Mathf.Abs(character.GetComponent<Character>().data.UIMoney - character.GetComponent<Character>().data.money) < 0.2f)
      {
        character.GetComponent<Character>().data.UIMoney = character.GetComponent<Character>().data.money;
        money.color = new Color(1.0f, 1.0f, 0.0f);
      }
      else if(character.GetComponent<Character>().data.UIMoney < character.GetComponent<Character>().data.money)
      {
        character.GetComponent<Character>().data.UIMoney += 0.2f;

        Vector3 v1 = new Vector3(0.0f, 1.0f, 0.0f);
        Vector3 v2 = new Vector3(1.0f, 1.0f, 0.0f);
        float a = Mathf.Clamp(((float)character.GetComponent<Character>().data.money - character.GetComponent<Character>().data.UIMoney) / 30.0f, 0.0f, 1.0f);
        Vector3 color = Vector3.Lerp(v2, v1, a);

        money.color = new Color(color.x, color.y, color.z);
      }
      else if(character.GetComponent<Character>().data.UIMoney > character.GetComponent<Character>().data.money)
      {
        character.GetComponent<Character>().data.UIMoney -= 0.2f;

        Vector3 v1 = new Vector3(1.0f, 0.0f, 0.0f);
        Vector3 v2 = new Vector3(1.0f, 1.0f, 0.0f);
        float a = Mathf.Clamp((character.GetComponent<Character>().data.UIMoney - (float)character.GetComponent<Character>().data.money) / 30.0f, 0.0f, 1.0f);
        Vector3 color = Vector3.Lerp(v2, v1, a);

        money.color = new Color(color.x, color.y, color.z);
      }

      money.text = (int)character.GetComponent<Character>().data.UIMoney + "";

      if(Mathf.Abs(character.GetComponent<Character>().data.UIScore - character.GetComponent<Character>().data.score) < 0.2f)
      {
        character.GetComponent<Character>().data.UIScore = character.GetComponent<Character>().data.score;
        score.color = new Color(1.0f, 1.0f, 0.0f);
      }
      else if(character.GetComponent<Character>().data.UIScore < character.GetComponent<Character>().data.score)
      {
        character.GetComponent<Character>().data.UIScore += 0.5f;

        Vector3 v1 = new Vector3(0.0f, 1.0f, 0.0f);
        Vector3 v2 = new Vector3(1.0f, 1.0f, 0.0f);
        float a = Mathf.Clamp(((float)character.GetComponent<Character>().data.score - character.GetComponent<Character>().data.UIScore) / 100.0f, 0.0f, 1.0f);
        Vector3 color = Vector3.Lerp(v2, v1, a);

        score.color = new Color(color.x, color.y, color.z);
      }

      score.text = (int)character.GetComponent<Character>().data.UIScore + "";

      for(int i = 0; i < catsNameText.Length; i += 1)
      {
        catsNameText[i].transform.eulerAngles = new Vector3(
          character.transform.localEulerAngles.x,
          character.transform.localEulerAngles.y,
          character.transform.localEulerAngles.z
        );

        if(inBattle)
        {
            catsNameText[i].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        else if(character.GetComponent<Character>().data.curCatResult[i] == 0)
        {
          catsNameText[i].color = new Color(1.0f, 1.0f, 1.0f);
        }
        else if (character.GetComponent<Character>().data.curCatResult[i] == -1)
        {
          catsNameText[i].color = new Color(0.8f, 0.0f, 0.0f);
        }
        else
        {
          catsNameText[i].color = new Color(0.0f, 1.0f, 0.0f);
        }
      }

      if(!inBattle)
      {
        GameObject[] catsInScene = GameObject.FindGameObjectsWithTag("Cat");
        float dis = 999;
        int index = -1;

        for (int i = 0; i < catsInScene.Length; i += 1)
        {
          float curDis = Vector3.Distance(catsInScene[i].transform.position, character.transform.position);
          if(curDis < dis)
          {
            dis = curDis;
            index = i;
          }
        }

        if(dis < 1)
        {
          interactWithPic.gameObject.SetActive(true);

          for (int i = 0; i < character.GetComponent<Character>().cats.Length; i += 1)
          {
            if(character.GetComponent<Character>().cats[i].name == catsInScene[index].name)
            {
              if(character.GetComponent<Character>().data.curCatResult[i] == 0)
              {
                interactWithText.text = "$" + character.GetComponent<Character>().cats[i].cost + ": Interact with " + character.GetComponent<Character>().cats[i].name;
                interactWithText.color = new Color(0.94f, 0.84f, 0.0f);
              }
              else if(character.GetComponent<Character>().data.curCatResult[i] == -1)
              {
                interactWithText.text = character.GetComponent<Character>().cats[i].name + " has been interacted";
                interactWithText.color = new Color(0.8f, 0.0f, 0.0f);
              }
              else
              {
                interactWithText.text = character.GetComponent<Character>().cats[i].name + " has been interacted Successfully";
                interactWithText.color = new Color(0.0f, 0.8f, 0.0f);
              }

              break;
            }
          }
        }
        else
        {
          interactWithPic.gameObject.SetActive(false);
        }
      }
      else
      {
        interactWithPic.gameObject.SetActive(false);
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

      if(character.GetComponent<Character>().data.curCatStatus.favorability <= -1)
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

    private void bindVisibilityWithLeaveButton()
    {
      bool visible = true;

      for (int i = 0; i < 8; i += 1)
      {
        if(character.GetComponent<Character>().data.curCatResult[i] == 0 && character.GetComponent<Character>().cats[i].cost < character.GetComponent<Character>().data.money)
        {
          visible = false;

          break;
        }
      }

      leaveButton.gameObject.SetActive(visible);
    }

    private void processResult()
    {
      if(resultRunning)
      {
        if(resultLayoutA < 3.0f)
        {
          resultLayout.color = new Color(0.0f, 0.0f, 0.0f, resultLayoutA / 3.0f);
        }
        else if(resultLayoutA < 6.0f)
        {
          returnButton.GetComponent<Image>().color = new Color(0.0f, 1.0f, 0.0f, (resultLayoutA - 3.0f) / 3.0f);

          for (int i = 0; i < resultText.Length; i += 1)
          {
            resultText[i].color = new Color(1.0f, 1.0f, 0.0f, (resultLayoutA - 3.0f) / 3.0f);
          }
        }

        resultLayoutA += Time.deltaTime;
      }
    }
}
