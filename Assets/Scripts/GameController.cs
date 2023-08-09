using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //The field to hold the main camera
    public Camera cam;

    //How long the drawing should last for
    public float persistence = 0.0f;

    //The list to hold the points of each shape
    public List<Vector3> newShape = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update() {
        /**When the left mouse button is pressed
        - Increase the persistence
        - Calculate the position of the mouse in the world
        - Add it to the current list of shape points**/
        if (Input.GetButtonDown("Fire1")) {
            persistence += 1.0f;
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, (cam.nearClipPlane+5.0f)));
            newShape.Add(worldPoint);
        }
        /**When the right mouse button is pressed
        - Draw each line of the shape
        - Clear the list to signify the end of a shape
        - Reset the persistence**/
        if (Input.GetButtonDown("Fire2")) {
            for (var i = 0; i < newShape.Count; i++) {
                if (i != 0) {
                    Debug.DrawLine(newShape[i-1],newShape[i], Color.red, persistence, false);
                }
            }
            newShape.Clear();
            persistence = 0.0f;
        }

        
        
    }
}
