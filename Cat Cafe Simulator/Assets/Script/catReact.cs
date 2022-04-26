using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class catReact : MonoBehaviour
{   
    public Animator catAnim;
    Vector2 v;
    // cat statu
    bool isIdle;
    bool isInteract;

    float animTime;
    float curTime;
    
    int r;
    // Start is called before the first frame update
    void Start()
    {
        isIdle = true;
        isInteract = false;

        catAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        checkCat();
    }
    
    void checkCat()
    { 
        // if(isIdle && curTime < 0)
        // {
        //     r = Random.Range(1, 3);
        //     if(r == 1) catAnim.Play("idle1");
        //     if(r == 2) catAnim.SetBool();

        //     if(r == 3) 
        //     {
        //         catAnim.Play("idle3");

        //         v = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        //     }
        //     animTime = Random.Range(5, 10);
        //     curTime = animTime;
        // }
        // curTime -= Time.deltaTime;

        // if(r == 3)
        // {   
        //     Vector2 diff = Vector2.Lerp(new Vector2(transform.position.x, transform.position.z), new Vector2(transform.position.x, transform.position.z) + v, (animTime - curTime) / animTime);
        //     transform.position = new Vector3(diff.x, transform.position.y, diff.y);
        // }
    }
}
