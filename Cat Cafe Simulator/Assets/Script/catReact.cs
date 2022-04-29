using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class catReact : MonoBehaviour
{
    public Animator catAnim;

    public Texture Face1;
    public Texture Face2;
    public Texture Face3;
    public Texture Face4;
    public Texture Face5;

    Vector2 v;
    Vector2 previousV;
    // cat statu
    public bool isIdle;

    float animTime;
    float curTime;

    public int r = 1;

    GameObject ui;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        isIdle = true;

        catAnim = GetComponent<Animator>();

        catAnim.Play("Base Layer.idle1");

        ui = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        checkCat();
        curTime -= Time.deltaTime;
        timer += Time.deltaTime;

        if(r == 3)
        {
            transform.GetComponent<Rigidbody>().velocity = new Vector3(v.x / animTime, 0,v.y / animTime);

            // StartCoroutine(WaitForRotation());

            transform.LookAt(new Vector3((previousV + v).x, transform.position.y, (previousV + v).y));

        }
        else
        {
          transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }

    void checkCat()
    {
        if(isIdle && curTime < 0)
        {
            int preStatus = r;

            while(r == preStatus)
            {
              r = Random.Range(1, 4);
            }

            if(r == 1 || r == 2)
            {
              animTime = Random.Range(3, 6);
              curTime = animTime;
            }
            else if(r == 3)
            {
                float xMovement = Random.Range(0, 2) == 0 ? Random.Range(-4.0f, -1.5f) : Random.Range(1.5f, 4.0f);
                float yMovement = Random.Range(0, 2) == 0 ? Random.Range(-4.0f, -1.5f) : Random.Range(1.5f, 4.0f);
                moveForward(new Vector2(xMovement, yMovement), Random.Range(4.0f, 7.0f));
            }

            disableAnimation();
            catAnim.SetBool(preStatus + "-" + r, true);
        }

        if(!isIdle)
        {
          GameObject character = GameObject.Find("Character");
          int curPoint = character.GetComponent<Character>().data.curCatStatus.favorability;
          //catAnim.Play("leave");
          if(curPoint <= -2)
          {
            catAnim.Play("Base Layer.standUp");
            GameObject.Find("Canvas").GetComponent<UIController>().inBattle = false;

            StartCoroutine(WaitForRotation());
            StartCoroutine(WaitForLeave());
          }
        }
    }

    IEnumerator WaitForRotation()
    {
      float time = 0;

      Quaternion start = transform.rotation;
      Quaternion target = transform.rotation * Quaternion.Euler(0, 180, 0);
      while(time < 0.5f)
      {
        transform.rotation = Quaternion.Slerp(start, target, time / 0.5f);
        time += Time.deltaTime;
        yield return null;
      }
      transform.rotation = target;
    }

    IEnumerator WaitForLeave()
    {
      catLeave();

      yield return new WaitForSeconds(2f);
    }

    public void moveForward(Vector2 pos, float duration)
    {
      v = new Vector2(pos.x, pos.y);
      previousV = new Vector2(transform.position.x, transform.position.z);
      animTime = duration;
      curTime = duration;
    }

    public void catLeave()
    {
      isIdle = true;
      disableAnimation();
      catAnim.Play("Base Layer.idle3");
      r = 3;

      GameObject camera = GameObject.Find("Main Camera");

      //hide action ui
      ui.GetComponent<buttonManager>().action1.SetActive(false);
      ui.GetComponent<buttonManager>().action2.SetActive(false);
      ui.GetComponent<buttonManager>().action3.SetActive(false);

      float dirFactor = 1.0f;

      while(Vector2.Distance(transform.position, new Vector3(camera.transform.position.x, 0, camera.transform.position.z)) * dirFactor < 1.5f)
      {
        dirFactor *= 1.25f;
      }

      Vector2 moveDis = new Vector2(
        ((transform.position - new Vector3(camera.transform.position.x, 0, camera.transform.position.z)) * dirFactor).x,
        ((transform.position - new Vector3(camera.transform.position.x, 0, camera.transform.position.z)) * dirFactor).z
      );

      moveForward(moveDis, Random.Range(2.0f, 3.0f));
      setCatStatus(false);

      GetComponent<Rigidbody>().detectCollisions = true;
      GetComponent<Rigidbody>().useGravity = true;
    }

    public void catHappy()
    {
      isIdle = true;
      disableAnimation();
      r = 1;
      animTime = 6;
      curTime = 6;

      setCatStatus(true);
      GetComponent<Rigidbody>().detectCollisions = true;
      GetComponent<Rigidbody>().useGravity = true;

      ui.GetComponent<UIController>().inBattle = false;

      if(this.name == "British Shorthhair")
      {
        GameObject.Find("Character").GetComponent<Character>().data.quest1 = 1;
      }

      ui.GetComponent<UIController>().showHint(this.name + " feels happy, great job!", new Color(0.0f, 1.0f, 0.0f), 2.5f);
    }

    public void setCatStatus(bool success)
    {
      GameObject character = GameObject.Find("Character");

      for(int i = 0; i < character.GetComponent<Character>().cats.Length; i += 1)
      {
        if(this.name == character.GetComponent<Character>().cats[i].name)
        {
          character.GetComponent<Character>().data.curCatResult[i] = success ? 1 : -1;

          return;
        }
      }
    }

    public void disableAnimation()
    {
      catAnim.SetBool("1-2", false);
      catAnim.SetBool("2-3", false);
      catAnim.SetBool("1-3", false);
      catAnim.SetBool("2-1", false);
      catAnim.SetBool("3-2", false);
      catAnim.SetBool("3-1", false);
    }
}
