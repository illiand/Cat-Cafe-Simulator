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

        if(r == 3)
        {
            transform.GetComponent<Rigidbody>().velocity = new Vector3(v.x / animTime, 0,v.y / animTime);
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
    }

    public void catHappy()
    {
      isIdle = true;
      disableAnimation();
      catAnim.Play("Base Layer.idle1");
      r = 1;

      setCatStatus(true);
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
