using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameData data;
    public Cat[] cats;
    public Toy[] toys;
    public Action[] actions;
    public Animator catAnim;

    public Texture Face1;
    public Texture Face2;
    public Texture Face3;
    public Texture Face4;
    public Texture Face5;

    // Start is called before the first frame update
    void Start()
    { 
      initData();

      catAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      handleUserInput();

      if(data.money <= 0 && data.actionPoint == 0)
      {
        Debug.Log("GG");
      }
    }

    void handleUserInput()
    {
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

      transform.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X"), 0);
    }

    public void initData()
    {
      data = new GameData();
      data.money = 100;
      data.curToys = new int[]{0, 0, 0, 0, 0, 0, 0};

      cats = new Cat[8];

      Cat cat1 = new Cat();
      cat1.name = "British Shorthhair";
      cat1.cost = 0;
      cat1.yesToy = new string[]{"Teaser", "Snack", "Catnip", "Scratcher", "Wagging Fish", "Bell", "Laser Pointer"};
      cat1.noToy = new string[]{};
      cat1.yesAction = new string[]{"Slowly Approach", "Beckon Approach", "Call Approach", "Lift up", "Holding its head and bottom with gentle hands", "Hold it on your shoulder", "Gentle touch", "Quick Rub", "Harder Rub"};
      cat1.noAction = new string[]{};
      cat1.characteristic = "Gentle";
      cat1.initFavorability = 1;
      cats[0] = cat1;

      Cat cat2 = new Cat();
      cat2.name = "Bombay";
      cat1.cost = 15;
      cat2.yesToy = new string[]{"Snack", "Catnip", "Scratcher", "Wagging Fish"};
      cat2.noToy = new string[]{"Bell", "Teaser", "Laser Pointer"};
      cat2.yesAction = new string[]{"Slowly Approach", "Holding its head and bottom with gentle hands"};
      cat2.noAction = new string[]{"Call Approach", "Lift up", "Touch the back ear - Harder Rub", "Touch the chin - Harder Rub"};
      cat2.characteristic = "Stable";
      cat2.initFavorability = 0;
      cats[1] = cat2;

      Cat cat3 = new Cat();
      cat3.name = "Ocicat";
      cat1.cost = 30;
      cat3.yesToy = new string[]{"Laser Pointer", "Bell", "Teaser", "Snack"};
      cat3.noToy = new string[]{"Catnip", "Wagging Fish", "Scratcher"};
      cat3.yesAction = new string[]{"Call Approach", "Hold it on your shoulder", "Touch the back ear - Gently touch", "Touch the chin - Gently touch"};
      cat3.noAction = new string[]{"Slowly Approach", "Beckon Approach", "Lift up", "Touch the back ear - Quick Rub", "Touch the chin - Quick Rub"};
      cat3.characteristic = "Cautious";
      cat3.initFavorability = -2;
      cats[2] = cat3;

      Cat cat4 = new Cat();
      cat4.name = "Siamese";
      cat1.cost = 15;
      cat4.yesToy = new string[]{"Teaser", "Bell", "Scratcher", "Wagging Fish", "Laser Pointer"};
      cat4.noToy = new string[]{"Snack", "Catnip"};
      cat4.yesAction = new string[]{"Call Approach", "Beckon Approach", "Lift up", "Touch the back ear - Quick Rub", "Touch the chin - Quick Rub", "Touch the back ear - Harder Rub", "Touch the chin - Harder Rub"};
      cat4.noAction = new string[]{"Slowly Approach", "Holding its head and bottom with gentle hands", "Touch the back ear - Gently touch", "Touch the chin - Gently touch"};
      cat4.characteristic = "Naughty";
      cat4.initFavorability = 2;
      cats[3] = cat4;

      Cat cat5 = new Cat();
      cat5.name = "Maine Coon";
      cat1.cost = 30;
      cat5.yesToy = new string[]{"Snack", "Catnip", "Scratcher"};
      cat5.noToy = new string[]{"Laser Pointer", "Teaser", "Bell", "Wagging Fish"};
      cat5.yesAction = new string[]{"Slowly Approach", "Hold it on your shoulder", "Touch the back ear - Gently touch", "Touch the chin - Gently touch"};
      cat5.noAction = new string[]{"Call Approach", "Holding its head and bottom with gentle hands", "Touch the back ear - Quick Rub", "Touch the chin - Quick Rub"};
      cat5.characteristic = "Independent";
      cat5.initFavorability = 0;
      cats[4] = cat5;

      Cat cat6 = new Cat();
      cat1.cost = 20;
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
      cat1.cost = 20;
      cat7.yesToy = new string[]{"Teaser", "Catnip", "Laser pointer", "Bell", "Scratcher"};
      cat7.noToy = new string[]{"Wagging Fish", "Snack"};
      cat7.yesAction = new string[]{"Hold it on your shoulder", "Touch the back ear - Harder Rub", "Touch the chin - Harder Rub"};
      cat7.noAction = new string[]{"Touch the back ear - Gently touch", "Touch the back ear - Quick Rub", "Touch the back ear - Harder Rub"};
      cat7.characteristic = "Agile";
      cat7.initFavorability = 1;
      cats[6] = cat7;

      Cat cat8 = new Cat();
      cat8.name = "Russian Blue";
      cat1.cost = 20;
      cat8.yesToy = new string[]{"Scratcher", "Laser pointer", "Catnip"};
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

          data.money -= cats[i].cost;
          data.actionPoint = 3;
          data.preResult = 0;
          data.preUsedToy = "";

          data.curCatStatus = new CatStatus();
          data.curCatStatus.cat = cats[i];
          data.curCatStatus.favorability = cats[i].initFavorability;
          data.curCatStatus.resistAction = new ArrayList();

          break;
        }
      }
    }

    public void useAction(string name)
    {
      if(data.actionPoint == 0)
      {
        return;
      }

      data.actionPoint -= 1;

      //catReaction(name);

      for(int i = 0; i < data.curCatStatus.cat.yesAction.Length; i += 1)
      {
        if(name == data.curCatStatus.cat.yesAction[i])
        {
          //cat's resistance
          for(int j = 0; j < data.curCatStatus.resistAction.Count; i += 1)
          {
            if(data.curCatStatus.resistAction[j] == name)
            {
              return;
            }
          }

          data.curCatStatus.favorability += 1;
          data.curCatStatus.resistAction.Add(name);

          //add successive bonus
          if(data.preResult == 1)
          {
            data.curCatStatus.favorability = Mathf.Min(data.curCatStatus.favorability + 1, 3);
          }

          data.preResult = 1;

          return;
        }
      }

      for(int i = 0; i < data.curCatStatus.cat.noAction.Length; i += 1)
      {
        if(name == data.curCatStatus.cat.noAction[i])
        {
          data.curCatStatus.favorability -= 1;

          if(data.preResult == -1)
          {
            data.curCatStatus.favorability = Mathf.Max(data.curCatStatus.favorability - 1, 0);
          }

          data.preResult = -1;

          return;
        }
      }
    }

    public void useToy(string name)
    {
      if(name == data.preUsedToy)
      {
        return;
      }

      //catReaction(name);

      data.preUsedToy = name;

      for(int i = 0; i < data.curToys.Length; i += 1)
      {
        if(name == toys[i].name)
        {
          data.curToys[i] -= 1;

          break;
        }
      }

      for(int i = 0; i < data.curCatStatus.cat.yesToy.Length; i += 1)
      {
        if(name == data.curCatStatus.cat.yesToy[i])
        {
          data.curCatStatus.favorability += 2;

          return;
        }
      }

      for(int i = 0; i < data.curCatStatus.cat.noToy.Length; i += 1)
      {
        if(name == data.curCatStatus.cat.noToy[i])
        {
          data.curCatStatus.favorability -= 2;

          return;
        }
      }
    }

    void catReaction(string name)
    {   
        //preferred Action
        for(int i = 0; i < data.curCatStatus.cat.yesAction.Length; i += 1)
        {     
              if(name == data.curCatStatus.cat.yesAction[i])
              {
                if(data.curCatStatus.favorability < 3)
                {
                  int r = Random.Range(1, 3);
                  if(r == 1) catAnim.SetTrigger("p1");
                  if(r == 2) catAnim.SetTrigger("p2");
                  if(r == 3) catAnim.SetTrigger("p3");
                }

                if(data.curCatStatus.favorability == 4)
                { 
                  catAnim.SetTrigger("attract");
                }

                if(data.curCatStatus.favorability > 4 && data.curCatStatus.favorability < 6)
                { 
                  int r = Random.Range(1, 3);
                  if(r == 1) catAnim.SetTrigger("approach1");
                  if(r == 2) catAnim.SetTrigger("approach2");
                  if(r == 3) catAnim.SetTrigger("approach3");
                }

                if(data.curCatStatus.favorability > 6)
                {
                  catAnim.SetTrigger("happy");
                }
              }
        }

        //not preferred action
        for(int i = 0; i < data.curCatStatus.cat.noAction.Length; i += 1)
        {     
              if(name == data.curCatStatus.cat.noAction[i])
              {
                if(data.curCatStatus.favorability < 3 && data.curCatStatus.favorability > 0)
                { 
                  //swp face 
                  gameObject.GetComponent<Renderer>().materials[1].mainTexture = Face2;

                  int r = Random.Range(1, 3);
                  if(r == 1) catAnim.SetTrigger("np1");
                  if(r == 2) catAnim.SetTrigger("np2");
                  if(r == 3) catAnim.SetTrigger("np3");
                }

                if(data.curCatStatus.favorability > 3 && data.curCatStatus.favorability < 6)
                { 
                  //swp face 
                  gameObject.GetComponent<Renderer>().materials[1].mainTexture = Face3;

                  int r = Random.Range(1, 3);
                  if(r == 1) catAnim.SetTrigger("np4");
                  if(r == 2) catAnim.SetTrigger("np5");
                  if(r == 3) catAnim.SetTrigger("np6");
                }

                if(data.curCatStatus.favorability < 0)
                { 
                  //swp face 
                  gameObject.GetComponent<Renderer>().materials[1].mainTexture = Face4;
                  catAnim.SetTrigger("lost");
                }
              }else{
                gameObject.GetComponent<Renderer>().materials[1].mainTexture = Face1;        
              }  
        }

        //preferred toy
        for(int i = 0; i < data.curCatStatus.cat.yesToy.Length; i += 1)
        {     
              if(name == data.curCatStatus.cat.yesToy[i])
              {
                if(data.curCatStatus.favorability < 3)
                { 
                  int r = Random.Range(1, 3);
                  if(r == 1) catAnim.SetTrigger("p1");
                  if(r == 2) catAnim.SetTrigger("p2");
                  if(r == 3) catAnim.SetTrigger("p3");
                }

                if(data.curCatStatus.favorability == 4)
                { 
                  catAnim.SetTrigger("attract");
                }

                if(data.curCatStatus.favorability > 4 && data.curCatStatus.favorability < 6)
                { 
                  int r = Random.Range(1, 3);
                  if(r == 1) catAnim.SetTrigger("approach1");
                  if(r == 2) catAnim.SetTrigger("approach2");
                  if(r == 3) catAnim.SetTrigger("approach3");
                }

                if(data.curCatStatus.favorability > 6)
                {
                  catAnim.SetTrigger("happy");
                }
              } 
        }

        //not preferred toy
        for(int i = 0; i < data.curCatStatus.cat.noToy.Length; i += 1)
        {     
              if(name == data.curCatStatus.cat.noToy[i])
              {
                if(data.curCatStatus.favorability < 3 && data.curCatStatus.favorability > 0)
                { 
                  //swp face 
                  gameObject.GetComponent<Renderer>().materials[1].mainTexture = Face2;

                  int r = Random.Range(1, 3);
                  if(r == 1) catAnim.SetTrigger("np1");
                  if(r == 2) catAnim.SetTrigger("np2");
                  if(r == 3) catAnim.SetTrigger("np3");
                }

                if(data.curCatStatus.favorability > 3 && data.curCatStatus.favorability < 6)
                { 
                  //swp face
                  gameObject.GetComponent<Renderer>().materials[1].mainTexture = Face3;

                  int r = Random.Range(1, 3);
                  if(r == 1) catAnim.SetTrigger("np4");
                  if(r == 2) catAnim.SetTrigger("np5");
                  if(r == 3) catAnim.SetTrigger("np6");
                }

                if(data.curCatStatus.favorability < 0)
                { 
                  //swp face
                  gameObject.GetComponent<Renderer>().materials[1].mainTexture = Face4;
                  catAnim.SetTrigger("lost");
                }
              }else{
                gameObject.GetComponent<Renderer>().materials[1].mainTexture = Face1;        
              }
        }

    }

    public class GameData
    {
      public int money;

      public int actionPoint;
      public int[] curToys;
      public CatStatus curCatStatus;

      public int preResult;
      public string preUsedToy;
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
    }

    public class ToyStatus
    {
      public Toy toy;
      public int remaining;
    }
}
