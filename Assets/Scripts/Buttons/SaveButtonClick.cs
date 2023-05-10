using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Threading.Tasks;

public class SaveButtonClick : MonoBehaviour
{
	public Button saveButton;

	void Start()
	{
		Button btn = saveButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		// Read Editor.txt save
		// Put all data in new textfile
		// user can give name to this textfile
		Debug.Log("Saved!");
		// empty Editor.txt
	}

	public static async Task ExampleAsync(GameObject gameObject, Vector3 pos) // shit works damn
	{
		using StreamWriter file = new("Editor.txt", append: true);
		await file.WriteLineAsync(gameObject.GetComponent<Item>().PrefabID.ToString());
		await file.WriteLineAsync(pos.x.ToString());
		await file.WriteLineAsync(pos.y.ToString());
	}
}