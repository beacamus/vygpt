using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Vector3 startPoint = new Vector3(0.0f,0.0f,0.0f);
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

//public static void DrawLine(Vector3 start, Vector3 end, Color color = Color.white, float duration = 0.0f, bool depthTest = true);
//(0.13, 1.05, 0.30) (0.23, 1.04, 0.30)

    // Update is called once per frame
    void Update() {

        if (Input.GetButtonDown("Fire1")) {
            Vector3 mousePos = Input.mousePosition;
            Vector3 endPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, (cam.nearClipPlane+5.0f)));
            Debug.DrawLine(new Vector3(-2.73f, 0.93f, 3.56f), new Vector3(0.23f, 0.93f, 3.56f), Color.green, 20.0f);
            Debug.DrawLine(startPoint,endPoint, Color.red, 2.5f, false);
            Debug.Log(mousePos.x);
            Debug.Log(mousePos.y);
            Debug.Log(startPoint+" "+endPoint);
            startPoint = endPoint;
        }
        
        
    }
}
