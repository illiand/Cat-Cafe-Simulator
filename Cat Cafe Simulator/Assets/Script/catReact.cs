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

    int r = 1;
    // Start is called before the first frame update
    void Start()
    {
        isIdle = true;

        catAnim = GetComponent<Animator>();

        catAnim.Play("Base Layer.idle1");
    }

    // Update is called once per frame
    void Update()
    {
        checkCat();

        curTime -= Time.deltaTime;

        if(r == 3)
        {
            Vector2 diff = Vector2.Lerp(previousV, previousV + v, (animTime - curTime) / animTime);
            transform.position = new Vector3(diff.x, transform.position.y, diff.y);
            transform.LookAt(new Vector3((previousV + v).x, transform.position.y, (previousV + v).y));
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

            if(r == 3)
            {
                v = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
                previousV = new Vector2(transform.position.x, transform.position.z);
            }

            disableAnimation();
            catAnim.SetBool(preStatus + "-" + r, true);

            animTime = Random.Range(5, 10);
            curTime = animTime;
        }
    }

    void disableAnimation()
    {
      catAnim.SetBool("1-2", false);
      catAnim.SetBool("2-3", false);
      catAnim.SetBool("1-3", false);
      catAnim.SetBool("2-1", false);
      catAnim.SetBool("3-2", false);
      catAnim.SetBool("3-1", false);
    }
}
