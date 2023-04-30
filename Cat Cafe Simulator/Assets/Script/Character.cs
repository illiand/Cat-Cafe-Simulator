using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public bool gameStarted;

    public GameData data;
    public Cat[] cats;
    public Toy[] toys;
    public Action[] actions;
    public Animator catAnim;

    private string action;

    public Button act11;
    public Button act12;
    public Button act13;

    public Button act21;
    public Button act22;
    public Button act23;

    public Button act31;
    public Button act32;
    public Button act33;

    public Button act34;
    public Button act35;
    public Button act36;

    public Canvas mainUI;

    public AudioClip blow;
    public AudioClip lick1;
    public AudioClip lick2;
    public AudioClip meow1;
    public AudioClip meow2;
    public AudioClip purr;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
      source = GetComponent<AudioSource>();

      initData();

      playerAction();
    }

    // Update is called once per frame
    void Update()
    {
      if(!gameStarted)
      {
        return;
      }

      handleUserInput();
    }

    void handleUserInput()
    {
      if(mainUI.GetComponent<UIController>().inBattle)
      {
        return;
      }

      float speed = 1.0f;

      if (Input.GetKey(KeyCode.S))
      {
        transform.Translate(0, 0, -Time.deltaTime * speed);
      }

      if (Input.GetKey(KeyCode.W))
      {
        transform.Translate(0, 0, Time.deltaTime * speed);
      }

      if (Input.GetKey(KeyCode.D))
      {
        transform.Translate(Time.deltaTime * speed, 0, 0);
      }

      if (Input.GetKey(KeyCode.A))
      {
        transform.Translate(-Time.deltaTime * speed, 0, 0);
      }

      if (Input.GetKey(KeyCode.E))
      {
        GameObject[] catsInScene = GameObject.FindGameObjectsWithTag("Cat");
        float dis = 999;
        int index = -1;

        for (int i = 0; i < catsInScene.Length; i += 1)
        {
          float curDis = Vector3.Distance(catsInScene[i].transform.position, transform.position);
          if(curDis < dis)
          {
            dis = curDis;
            index = i;
          }
        }

        if(index != -1 && dis < 1)
        {
          int catArrayIndex = -1;

          for (int i = 0; i < cats.Length; i += 1)
          {
            if(cats[i].name == catsInScene[index].name)
            {
              catArrayIndex = i;

              break;
            }
          }

          if(data.quest1 == -1)
          {
            if(catsInScene[index].name != "British Shorthhair")
            {
              mainUI.GetComponent<UIController>().showHint("Follow the quest to interact with Lucy.", new Color(0.8f, 0.0f, 0.0f), 1.5f);

              return;
            }
          }

          if(data.curCatResult[catArrayIndex] != 0)
          {
            return;
          }

          initRound(catsInScene[index]);

          catsInScene[index].GetComponent<Rigidbody>().detectCollisions = false;
          catsInScene[index].GetComponent<Rigidbody>().useGravity = false;

          catsInScene[index].transform.LookAt(transform.position);
          GameObject.Find("Main Camera").transform.LookAt(new Vector3(catsInScene[index].transform.position.x, transform.position.y, catsInScene[index].transform.position.z));
          catsInScene[index].GetComponent<catReact>().catAnim.Play("Base Layer.pa");
          catsInScene[index].GetComponent<catReact>().disableAnimation();
          catsInScene[index].GetComponent<catReact>().r = 1;
        }
      }

      transform.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X"), 0);
    }

    void playerAction()
    {
      act11.onClick.AddListener(delegate{useAction("Slowly Approach");});
      act12.onClick.AddListener(delegate{useAction("Beckon Approach");});
      act13.onClick.AddListener(delegate{useAction("Call Approach");});

      act21.onClick.AddListener(delegate{useAction("Lift up");});
      act22.onClick.AddListener(delegate{useAction("Holding its head and bottom with gentle hands");});
      act23.onClick.AddListener(delegate{useAction("Hold it on your shoulder");});

      act31.onClick.AddListener(delegate{useAction("Touch the back ear - Gently touch");});
      act32.onClick.AddListener(delegate{useAction("Touch the back ear - Quick Rub");});
      act33.onClick.AddListener(delegate{useAction("Touch the back ear - Harder Rub");});
      act34.onClick.AddListener(delegate{useAction("Touch the chin - Gently touch");});
      act35.onClick.AddListener(delegate{useAction("Touch the chin - Quick Rub");});
      act36.onClick.AddListener(delegate{useAction("Touch the chin - Harder Rub");});

      // if(data.curCatStatus != null)
      // {
      //   Debug.Log(data.curCatStatus.favorability);
      // }
    }

    public void initData()
    {
      data = new GameData();
      data.money = 100;
      data.UIMoney = 100;
      data.quest1 = -1;

      data.curToys = new int[]{0, 0, 0, 0, 0, 0, 0};
      data.curCatResult = new int[]{0, 0, 0, 0, 0, 0, 0, 0};

      cats = new Cat[8];

      Cat cat1 = new Cat();
      cat1.name = "British Shorthhair";
      cat1.cost = 0;
      cat1.yesToy = new string[]{"Teaser", "Snack", "Catnip", "Scratcher", "Wagging Fish", "Bell", "Laser Pointer"};
      cat1.noToy = new string[]{};
      cat1.yesAction = new string[]{"Slowly Approach", "Beckon Approach", "Call Approach", "Lift up", "Holding its head and bottom with gentle hands", "Hold it on your shoulder", "Touch the back ear - Gently touch", "Touch the chin - Gently touch", "Touch the back ear - Quick Rub", "Touch the chin - Quick Rub", "Touch the back ear - Harder Rub", "Touch the chin - Harder Rub"};
      cat1.noAction = new string[]{};
      cat1.characteristic = "Gentle";
      cat1.initFavorability = 1;
      cats[0] = cat1;

      Cat cat2 = new Cat();
      cat2.name = "Bombay";
      cat2.cost = 15;
      cat2.yesToy = new string[]{"Snack", "Catnip", "Scratcher", "Wagging Fish"};
      cat2.noToy = new string[]{"Bell", "Teaser", "Laser Pointer"};
      cat2.yesAction = new string[]{"Slowly Approach", "Holding its head and bottom with gentle hands"};
      cat2.noAction = new string[]{"Call Approach", "Lift up", "Touch the back ear - Harder Rub", "Touch the chin - Harder Rub"};
      cat2.characteristic = "Stable";
      cat2.initFavorability = 0;
      cats[1] = cat2;

      Cat cat3 = new Cat();
      cat3.name = "Ocicat";
      cat3.cost = 30;
      cat3.yesToy = new string[]{"Laser Pointer", "Bell", "Teaser", "Snack"};
      cat3.noToy = new string[]{"Catnip", "Wagging Fish", "Scratcher"};
      cat3.yesAction = new string[]{"Call Approach", "Hold it on your shoulder", "Touch the back ear - Gently touch", "Touch the chin - Gently touch"};
      cat3.noAction = new string[]{"Slowly Approach", "Beckon Approach", "Lift up", "Touch the back ear - Quick Rub", "Touch the chin - Quick Rub"};
      cat3.characteristic = "Cautious";
      cat3.initFavorability = -1;
      cats[2] = cat3;

      Cat cat4 = new Cat();
      cat4.name = "Siamese";
      cat4.cost = 15;
      cat4.yesToy = new string[]{"Teaser", "Bell", "Scratcher", "Wagging Fish", "Laser Pointer"};
      cat4.noToy = new string[]{"Snack", "Catnip"};
      cat4.yesAction = new string[]{"Call Approach", "Beckon Approach", "Lift up", "Touch the back ear - Quick Rub", "Touch the chin - Quick Rub", "Touch the back ear - Harder Rub", "Touch the chin - Harder Rub"};
      cat4.noAction = new string[]{"Slowly Approach", "Holding its head and bottom with gentle hands", "Touch the back ear - Gently touch", "Touch the chin - Gently touch"};
      cat4.characteristic = "Naughty";
      cat4.initFavorability = 2;
      cats[3] = cat4;

      Cat cat5 = new Cat();
      cat5.name = "Maine Coon";
      cat5.cost = 30;
      cat5.yesToy = new string[]{"Snack", "Catnip", "Scratcher"};
      cat5.noToy = new string[]{"Laser Pointer", "Teaser", "Bell", "Wagging Fish"};
      cat5.yesAction = new string[]{"Slowly Approach", "Hold it on your shoulder", "Touch the back ear - Gently touch", "Touch the chin - Gently touch"};
      cat5.noAction = new string[]{"Call Approach", "Holding its head and bottom with gentle hands", "Touch the back ear - Quick Rub", "Touch the chin - Quick Rub"};
      cat5.characteristic = "Independent";
      cat5.initFavorability = 0;
      cats[4] = cat5;

      Cat cat6 = new Cat();
      cat6.cost = 20;
      cat6.name = "American Bobtail";
      cat6.yesToy = new string[]{"Teaser", "Laser Pointer", "Wagging Fish", "Snack", "Catnip"};
      cat6.noToy = new string[]{"Scratcher", "Bell"};
      cat6.yesAction = new string[]{"Touch the chin - Gently touch", "Touch the chin - Quick Rub", "Touch the chin - Harder Rub", "Lift up", "Holding its head and bottom with gentle hands", "Hold it on your shoulder"};
      cat6.noAction = new string[]{"Touch the back ear - Gently touch", "Touch the chin - Gently touch"};
      cat6.characteristic = "Clever";
      cat6.initFavorability = 1;
      cats[5] = cat6;

      Cat cat7 = new Cat();
      cat7.name = "Turnish Angora";
      cat7.cost = 20;
      cat7.yesToy = new string[]{"Teaser", "Catnip", "Laser Pointer", "Bell", "Scratcher"};
      cat7.noToy = new string[]{"Wagging Fish", "Snack"};
      cat7.yesAction = new string[]{"Hold it on your shoulder", "Touch the back ear - Harder Rub", "Touch the chin - Harder Rub"};
      cat7.noAction = new string[]{"Touch the back ear - Gently touch", "Touch the back ear - Quick Rub", "Touch the back ear - Harder Rub"};
      cat7.characteristic = "Agile";
      cat7.initFavorability = 1;
      cats[6] = cat7;

      Cat cat8 = new Cat();
      cat8.name = "Russian Blue";
      cat8.cost = 20;
      cat8.yesToy = new string[]{"Scratcher", "Laser Pointer", "Catnip"};
      cat8.noToy = new string[]{"Teaser", "Snack", "Belly", "Wagging fish"};
      cat8.yesAction = new string[]{"Touch the chin - Gently touch", "Touch the chin - Quick Rub", "Touch the chin - Harder Rub", "Touch the back ear - Gently touch"};
      cat8.noAction = new string[]{"Lift up", "Holding its head and bottom with gentle hands", "Hold it on your shoulder", "Touch the back ear - Harder Rub", "Touch the chin - Harder Rub"};
      cat8.characteristic = "Quiet";
      cat8.initFavorability = -1;
      cats[7] = cat8;

      toys = new Toy[7];

      Toy toy1 = new Toy();
      toy1.name = "Teaser";
      toy1.path = "";
      toy1.cost = 10;
      toys[0] = toy1;

      Toy toy2 = new Toy();
      toy2.name = "Snack";
      toy2.path = "";
      toy2.cost = 10;
      toys[1] = toy2;

      Toy toy3 = new Toy();
      toy3.name = "Bell";
      toy3.path = "";
      toy3.cost = 10;
      toys[2] = toy3;

      Toy toy4 = new Toy();
      toy4.name = "Scratcher";
      toy4.path = "";
      toy4.cost = 20;
      toys[3] = toy4;

      Toy toy5 = new Toy();
      toy5.name = "Catnip";
      toy5.path = "";
      toy5.cost = 15;
      toys[4] = toy5;

      Toy toy6 = new Toy();
      toy6.name = "Wagging Fish";
      toy6.path = "";
      toy6.cost = 10;
      toys[5] = toy6;

      Toy toy7 = new Toy();
      toy7.name = "Laser Pointer";
      toy7.path = "";
      toy7.cost = 10;
      toys[6] = toy7;

      actions = new Action[12];

      Action action1 = new Action();
      action1.name = "Slowly Approach";
      action1.reqLevel = 1;
      actions[0] = action1;

      Action action2 = new Action();
      action2.name = "Beckon Approach";
      action2.reqLevel = 1;
      actions[1] = action2;

      Action action3 = new Action();
      action3.name = "Call Approach";
      action3.reqLevel = 1;
      actions[2] = action3;

      Action action4 = new Action();
      action4.name = "Lift up";
      action4.reqLevel = 2;
      actions[3] = action4;

      Action action5 = new Action();
      action5.name = "Holding its head and bottom with gentle hands";
      action5.reqLevel = 2;
      actions[4] = action5;

      Action action6 = new Action();
      action6.name = "Hold it on your shoulder";
      action6.reqLevel = 2;
      actions[5] = action6;

      Action action7 = new Action();
      action7.name = "Touch the back ear - Gently touch";
      action7.reqLevel = 3;
      actions[6] = action7;

      Action action8 = new Action();
      action8.name = "Touch the back ear - Quick Rub";
      action8.reqLevel = 3;
      actions[7] = action8;

      Action action9 = new Action();
      action9.name = "Touch the back ear - Harder Rub";
      action9.reqLevel = 3;
      actions[8] = action9;

      Action action10 = new Action();
      action10.name = "Touch the chin - Gently touch";
      action10.reqLevel = 3;
      actions[9] = action10;

      Action action11 = new Action();
      action11.name = "Touch the chin - Quick Rub";
      action11.reqLevel = 3;
      actions[10] = action11;

      Action action12 = new Action();
      action12.name = "Touch the chin - Harder Rub";
      action12.reqLevel = 3;
      actions[11] = action12;
    }

    public void initRound(GameObject cat)
    {
      for(int i = 0; i < cats.Length; i += 1)
      {
        if(cats[i].name == cat.name)
        {
          if(data.money < cats[i].cost)
          {
            return;
          }

          cat.GetComponent<catReact>().isIdle = false;
          catAnim = cat.GetComponent<Animator>();

          data.money -= cats[i].cost;
          data.moneyUsedOnCat += cats[i].cost;
          data.actionPoint = 3;
          data.preResult = 0;
          data.preUsedToy = "";

          data.curCatStatus = new CatStatus();
          data.curCatStatus.cat = cats[i];
          data.curCatStatus.favorability = cats[i].initFavorability;
          data.curCatStatus.resistAction = new ArrayList();

          mainUI.GetComponent<UIController>().inBattle = true;

          break;
        }
      }
    }

    public void useAction(string name)
    {
      if(data.actionPoint == 0)
      {
        mainUI.GetComponent<UIController>().showHint("No enough action point to interact with cat.", new Color(0.8f, 0.0f, 0.0f), 1.5f);

        return;
      }

      //cat's resistance
      for(int j = 0; j < data.curCatStatus.resistAction.Count; j += 1)
      {
        if((string)data.curCatStatus.resistAction[j] == name)
        {
          mainUI.GetComponent<UIController>().showHint("Action can not be used successively.", new Color(0.8f, 0.0f, 0.0f), 1.5f);

          return;
        }
      }

      data.actionPoint -= 1;

      int addedPoint = 0;

      for(int i = 0; i < data.curCatStatus.cat.yesAction.Length; i += 1)
      {
        if(name == data.curCatStatus.cat.yesAction[i])
        {
          addedPoint = 1;

          if(data.preResult == 1)
          {
            addedPoint += 1;
          }

          catReaction(true, addedPoint);

          data.curCatStatus.favorability += 1;
          data.curCatStatus.resistAction.Add(name);

          data.score += 45;

          data.correctActionCount += 1;

          //add successive bonus
          if(data.preResult == 1)
          {
            data.curCatStatus.favorability = Mathf.Min(data.curCatStatus.favorability + 1, 8);
            data.actionPoint = Mathf.Min(data.actionPoint + 1, 3);

            mainUI.GetComponent<UIController>().showHint("The cat likes you a lot, this action has no cost!", new Color(0.0f, 1.0f, 0.0f), 1.5f);
            data.preResult = 0;

            data.score += 62;
          }
          else
          {
            data.preResult = 1;
          }

          return;
        }
      }

      for(int i = 0; i < data.curCatStatus.cat.noAction.Length; i += 1)
      {
        if(name == data.curCatStatus.cat.noAction[i])
        {
          addedPoint = -1;

          if(data.preResult == 1)
          {
            addedPoint -= 1;
          }

          catReaction(true, addedPoint);

          data.curCatStatus.favorability -= 1;

          data.wrongActionCount += 1;

          if(data.preResult == -1)
          {
            data.curCatStatus.favorability = Mathf.Max(data.curCatStatus.favorability - 1, -1);
            mainUI.GetComponent<UIController>().showHint("The cat started loathing...", new Color(0.8f, 0.0f, 0.0f), 1.5f);
            data.preResult = 0;
          }
          else
          {
            data.preResult = -1;
          }

          return;
        }
      }
    }

    public void useToy(string name)
    {
      if(name == data.preUsedToy)
      {
        mainUI.GetComponent<UIController>().showHint("The toy can not be used successively", new Color(0.8f, 0.0f, 0.0f), 1.5f);

        return;
      }

      for(int i = 0; i < data.curToys.Length; i += 1)
      {
        if(name == toys[i].name)
        {
          if(data.curToys[i] == 0)
          {
            mainUI.GetComponent<UIController>().showHint("No available toy.", new Color(0.8f, 0.0f, 0.0f), 1.5f);

            return;
          }

          data.curToys[i] -= 1;

          break;
        }
      }

      data.preUsedToy = name;

      for(int i = 0; i < data.curCatStatus.cat.yesToy.Length; i += 1)
      {
        if(name == data.curCatStatus.cat.yesToy[i])
        {
          catReaction(false, 2);
          data.curCatStatus.favorability += 2;

          data.score += 238;

          data.correctToyCount += 1;

          return;
        }
      }

      for(int i = 0; i < data.curCatStatus.cat.noToy.Length; i += 1)
      {
        if(name == data.curCatStatus.cat.noToy[i])
        {
          catReaction(false, -2);
          data.curCatStatus.favorability -= 2;

          data.wrongToyCount += 1;

          return;
        }
      }

    }

    void catReaction(bool isAction, int addedPoint)
    {
      GameObject.Find(data.curCatStatus.cat.name + " - neko:body").GetComponent<Renderer>().materials[1].mainTexture = GameObject.Find(data.curCatStatus.cat.name).GetComponent<catReact>().Face1;

      if(data.curCatStatus.favorability + addedPoint <= -2)
      {
        //audio
        source.clip = blow;
        if(!source.isPlaying) source.Play();

        catAnim.SetTrigger("lost");
        GameObject.Find(data.curCatStatus.cat.name + " - neko:body").GetComponent<Renderer>().materials[1].mainTexture = GameObject.Find(data.curCatStatus.cat.name).GetComponent<catReact>().Face4;
        mainUI.GetComponent<UIController>().showHint(data.curCatStatus.cat.name + " feels angry, it leaves you", new Color(0.8f, 0.0f, 0.0f), 2.5f);
        //GameObject.Find(data.curCatStatus.cat.name).GetComponent<catReact>().catLeave();
      }
      else if(-1 <= data.curCatStatus.favorability + addedPoint && data.curCatStatus.favorability + addedPoint <= 2)
      {
        if(data.curCatStatus.favorability > 2)
        {
          source.clip = meow2;
          if(!source.isPlaying) source.Play();
          catAnim.SetTrigger("p2-p1");
        }

        int r = Random.Range(1, 3);

        if(r == 1) catAnim.SetTrigger("p1y1");
        if(r == 2)
        {
          source.clip = lick1;
          if(!source.isPlaying) source.Play();
          catAnim.SetTrigger("p1y2");
        }
        if(r == 3)
        {
          source.clip = lick2;
          if(!source.isPlaying) source.Play();
          catAnim.SetTrigger("p1y3");
        }

      }
      else if(3 <= data.curCatStatus.favorability + addedPoint && data.curCatStatus.favorability + addedPoint <= 5)
      {
        if(data.curCatStatus.favorability <= 2)
        {
          source.clip = meow1;
          if(!source.isPlaying) source.Play();

          data.score += isAction? 105 : 72;
          catAnim.SetTrigger("p1-p2");
        }

        if(!data.curCatStatus.p1)
        {
          data.money += 50;
          data.curCatStatus.p1 = true;
        }
      }
      else if(data.curCatStatus.favorability + addedPoint <= 7)
      {
        if(data.curCatStatus.favorability <= 5)
        {
          data.score += isAction? 208 : 143;
        }

        source.clip = meow1;
        if(!source.isPlaying) source.Play();

        int r = Random.Range(1, 3);

        if(r == 1) catAnim.SetTrigger("approach1");
        if(r == 2) catAnim.SetTrigger("approach2");
        if(r == 3) catAnim.SetTrigger("approach3");

        if(!data.curCatStatus.p2)
        {
          data.money += 10;
          data.curCatStatus.p2 = true;
        }
      }
      else if(data.curCatStatus.favorability + addedPoint > 7)
      {
        Debug.Log("happy");

        source.clip = purr;
        if(!source.isPlaying) source.Play();

        catAnim.SetTrigger("happy");

        data.score += 592;
        data.money += 20;

        GameObject.Find(data.curCatStatus.cat.name).GetComponent<catReact>().catHappy();
      }
    }

    public class GameData
    {
      public int money;
      public float UIMoney;

      public int score;
      public float UIScore;

      public int actionPoint;
      public int[] curToys;
      public CatStatus curCatStatus;

      public int quest1;

      public int preResult;
      public string preUsedToy;

      public int[] curCatResult;
      public int correctActionCount;
      public int wrongActionCount;
      public int correctToyCount;
      public int wrongToyCount;
      public int moneyUsedOnCat;
      public int moneyUsedOnToy;
    }

    public class Cat
    {
      public string name;
      public int cost;
      public string[] yesToy;
      public string[] noToy;
      public string[] yesAction;
      public string[] noAction;
      public string characteristic;
      public int initFavorability;
    }

    public class Action
    {
      public string name;
      public int reqLevel;
    }

    public class Toy
    {
      public string name;
      public string path;
      public int cost;
    }

    public class CatStatus
    {
      public Cat cat;
      public int favorability;
      public ArrayList resistAction;

      public bool p1;
      public bool p2;
    }

    public class ToyStatus
    {
      public Toy toy;
      public int remaining;
    }
}
