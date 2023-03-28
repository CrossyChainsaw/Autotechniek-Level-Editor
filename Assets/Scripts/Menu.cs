using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Text textObject;
    bool isHovering;
    const string PLAY_SCENE = "Play";
    const string LEVEL_EDITOR_SCENE = "Level Editor";

    void Start()
    {
        textObject = GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isHovering)
        {
            if (textObject.text == "Spelen")
            {
                SceneManager.LoadScene(PLAY_SCENE);
            }
            if (textObject.text == "Level Editor") 
            {
                SceneManager.LoadScene(LEVEL_EDITOR_SCENE);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        textObject.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        textObject.color = Color.black;
    }

}
