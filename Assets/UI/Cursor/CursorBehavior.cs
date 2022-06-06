using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehavior : MonoBehaviour
{
    public Texture2D cursorTexture;

    // Start is called before the first frame update
        void OnApplicationFocus(bool focus)
        {
            Cursor.SetCursor(cursorTexture, new Vector2(0, 0), CursorMode.Auto);
        }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
