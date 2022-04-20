using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class catReact : MonoBehaviour
{   
    // cat animation controller
    Animator animator;
    
    // cat statu
    bool isIdle;
    bool isAttracted;
    bool isPreferred;

    // cat face textures
    public Texture happyFace_1;
    public Texture sadFace_1;
    public Texture happyFace_2;
    public Texture sadFace_2;

    // actions --> ui button
    

    // points
    public int points;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        isIdle = true;
        isAttracted = false;
        isPreferred = false;
    }

    // Update is called once per frame
    void Update()
    {
        // //check player's action first
        // checkPlayerActions();

        // //check cat's characteristic
        // catReaction();

    }

//     //check player actions
//     void checkPlayerActions()
//     {   
//         if()
//         {
//             point += 
//         }
//     }

//     //cat reactions
//     void catReaction()
//     {
//         if()
//     }

//     //swap cat face
//     void swapFaces()
//     {   
//         if()
//         {
//             gameObject.GetComponent<Renderer>().materials[1].mainTexture = happyFace_1;
//         }
//         if()
//         {
//             gameObject.GetComponent<Renderer>().materials[1].mainTexture = happyFace_2;
//         }

//         if()
//         {
//             gameObject.GetComponent<Renderer>().materials[1].mainTexture = sadFace_1;
//         }
//         if()
//         {
//             gameObject.GetComponent<Renderer>().materials[1].mainTexture = sadFace_2;
//         }
//     }
}
