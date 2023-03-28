using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenuButtonClick : MonoBehaviour
{
	public Button backToMenuButton;
	const string MENU_SCENE = "Menu";

	void Start()
	{
		Button btn = backToMenuButton.GetComponent<Button>();
		btn.onClick.AddListener(ButtonClick);
	}

	void ButtonClick()
	{
		SceneManager.LoadScene(MENU_SCENE);
	}
}
