using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    public Texture2D cursor;
    public Texture2D cursorClicked;
    public Texture2D menuCursor;

    private void Awake()
    {
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);     //Changes the cursor pointer to the middle of the icon "hotspot"
        Cursor.SetCursor(cursorType, hotspot, CursorMode.ForceSoftware); //if there is a targeting icon instead of an arrow head -> change "Vector2.zero" to "hotspot"
    }

    void CursorChangeLogistics()
    {
        if (Time.timeScale == 0f)    
        {
            ChangeCursor(menuCursor);            
        }
        else
        {
            ChangeCursor(cursor);
        }


    }

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }


    void Update()
    {
        CursorChangeLogistics();
    }
}
