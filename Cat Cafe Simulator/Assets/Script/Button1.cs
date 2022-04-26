using UnityEngine;
using UnityEngine.UI;
using System.Collections;

<<<<<<< HEAD:Cat Cafe Simulator/Assets/Script/Button3.cs
public class Button3 : MonoBehaviour
=======
public class Button1 : MonoBehaviour
>>>>>>> e6e0e75e60ba37cc6f78d4e5c423a225f3ab06e0:Cat Cafe Simulator/Assets/Script/Button1.cs
{
	public Button yourButton;
	void Start()
	{
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("You have clicked the button!");
	}
}
