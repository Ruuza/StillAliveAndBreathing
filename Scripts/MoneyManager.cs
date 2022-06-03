using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

	public static int money;

	Text text; 


	void Awake ()
	{
		text = GetComponent <Text> ();

		money = 0;
	}


	void Update ()
	{
		text.text = "$: " + money;
	}
}
