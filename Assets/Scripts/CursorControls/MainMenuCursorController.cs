using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCursorController : MonoBehaviour
{
    public Texture2D menuCursor;


    private void Awake()
    {
        ChangeCursor(menuCursor);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        // Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);     //Changes the cursor pointer to the middle of the icon "hotspot"
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.ForceSoftware); //if there is a targeting icon instead of an arrow head -> change "Vector2.zero" to "hotspot"
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
