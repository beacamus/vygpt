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
            int lines = newShape.Count - 1;
            Debug.Log(lines);
            for (var i = 0; i < newShape.Count; i++) {
                if (i != 0) {
                    //Debug.Log(newShape[i-1] + " " + newShape[i]);
                    Debug.DrawLine(newShape[i-1],newShape[i], Color.red, persistence, false);
                }
            }
            switch (lines) {
                case 1:
                    Debug.Log("Line");
                    break;
                case 2:
                    Debug.Log("Angle");
                    break;
                case 3:
                    Debug.Log("Triangle");
                    break;
                case 4:
                    Vector3[] exampleSquare = { new Vector3 (2,0,0), new Vector3 (0,-2,0), new Vector3 (-2,0,0), new Vector3 (0,2,0)};
                    //Vector3[] positions = { new Vector3 { x = 0, y = 0, z = 0 }, new Vector3 { x = 1, y = 1, z = 1} }; 
                    Debug.Log("Square");
                    Vector3 v_one= newShape[1] - newShape[0];
                    Vector3 v_two= newShape[2] - newShape[1];
                    Vector3 v_three= newShape[3] - newShape[2];
                    Vector3 v_four= newShape[0] - newShape[3];
                    Debug.Log(v_one + " " + v_two + " " + v_three + " " + v_four);
                    Vector3 sim1 = v_one - exampleSquare[0];
                    Vector3 sim2 = v_two - exampleSquare[1];
                    Vector3 sim3 = v_three - exampleSquare[2];
                    Vector3 sim4 = v_four - exampleSquare[3];
                    Debug.Log(sim1 + " " + sim2 + " " + sim3 + " " + sim4);
                    bool angle1 = false;
                    bool angle2 = false;
                    bool angle3 = false;
                    bool angle4 = false;
                    if ((sim1.x < 0.4) && (sim1.y < 0.4)) {
                        angle1 = true;
                    }
                    if ((sim2.x < 0.4) && (sim2.y < 0.4)) {
                        angle2 = true;
                    }
                    if ((sim3.x < 0.4) && (sim3.y < 0.4)) {
                        angle3 = true;
                    }
                    if ((sim4.x < 0.4) && (sim4.y < 0.4)) {
                        angle4 = true;
                    }
                    Debug.Log(sim1 + " " + angle1);
                    Debug.Log(sim2 + " " + angle2);
                    Debug.Log(sim3 + " " + angle3);
                    Debug.Log(sim4 + " " + angle4);
                    if (angle1 && angle2 && angle3 && angle4) {
                        Debug.Log("AHHHH");
                    }
                    //xyxy < 0.2
                    break;
            }


            newShape.Clear();
            persistence = 0.0f;
        }

        
        
    }
}
