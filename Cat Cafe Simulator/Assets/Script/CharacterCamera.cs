using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
  public GameObject character;
  private float cameraDistance = -3f;

  private float x = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
      if(character.GetComponent<Character>().mainUI.GetComponent<UIController>().inBattle) return;

      float curDegree = transform.localEulerAngles.x;

      transform.position = character.transform.position;
      transform.localEulerAngles = character.transform.localEulerAngles;
      x -= Input.GetAxis("Mouse Y");
      x = Mathf.Clamp(x, -20, 15);

      transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, 0);
      transform.Translate(0, 0.2f, 0);

      cameraDistance = Mathf.Clamp(cameraDistance + Input.GetAxis("Mouse ScrollWheel"), -3.5f, -1);

      // RaycastHit hit;
      //
      // if (Physics.Linecast(character.transform.position + new Vector3(0, 0.95f, 0), transform.position, out hit))
      // {
      //   Color color = new Color(1.0f, 0, 0);
      //   Debug.DrawLine(transform.position, hit.point, color);
      //
      //   if (!hit.collider.isTrigger && hit.rigidbody != character.GetComponents<Rigidbody>()[0])
      //   {
      //       transform.Translate(0, 0, Mathf.Min(1f, Vector3.Distance(hit.point, character.transform.position + new Vector3(0, 0.95f, 0))) + Vector3.Distance(hit.point, transform.position));
      //   }
      // }
    }
}
