using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //The field to hold the main camera
    public Camera cam;
    Vector3 previousFrame;
    public float persistence = 10.0f;
    public float iterations = 0.0f;


     // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update() {

        if (Input.GetButtonDown("Fire1")) {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, (cam.nearClipPlane+5.0f)));
            previousFrame = worldPoint;
            iterations = 100.0f;
        }

        if (Input.GetMouseButton(0)) {
            iterations += 1.0f;
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, (cam.nearClipPlane+5.0f)));
            Debug.DrawLine(previousFrame,worldPoint, Color.red, (iterations/100.0f), false);
            previousFrame = worldPoint;
        }

        if (Input.GetMouseButtonUp(0)) {
            iterations = 0.0f;
        }

        /**if (Input.GetAxis("Mouse X") < 0) {
            //Code for action on mouse moving left
            print("Mouse moved left");
        }

        if (Input.GetAxis("Mouse X") > 0) {
            //Code for action on mouse moving right
            print("Mouse moved right");
        }**/

    }
}
