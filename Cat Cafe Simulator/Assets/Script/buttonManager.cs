using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonManager : MonoBehaviour
{
    public GameObject player;
    public GameObject action1;
    public GameObject action2;
    public GameObject action3;

    int p;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Character>().data.curCatStatus == null)return;

        if(!player.GetComponent<Character>().mainUI.GetComponent<UIController>().inBattle)
        {
          action1.SetActive(false);
          action2.SetActive(false);
          action3.SetActive(false);

          return;
        }

        p = player.GetComponent<Character>().data.curCatStatus.favorability;

        if(p < 3)
        {
            action1.SetActive(true);
            action2.SetActive(false);
            action3.SetActive(false);
        }

        if(p >= 3 && p <= 5)
        {
            action1.SetActive(false);
            action2.SetActive(true);
            action3.SetActive(false);
        }

        if(p >= 6)
        {
            action1.SetActive(false);
            action2.SetActive(false);
            action3.SetActive(true);
        }

    }
}
