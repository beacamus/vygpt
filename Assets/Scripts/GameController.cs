using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //The field to hold the main camera
    public Camera cam;
    Vector3 previousFrame;
    public float iterations = 0.0f;
    //The list to hold the points of each shape
    public List<Vector3> newShape = new List<Vector3>();



     // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update() {

        if (Input.GetButtonDown("Fire1")) {
            newShape.Clear();
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, (cam.nearClipPlane+5.0f)));
            newShape.Add(worldPoint);
            previousFrame = worldPoint;
            iterations = 100.0f;
        }

        if (Input.GetMouseButton(0)) {
            iterations += 1.0f;
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, (cam.nearClipPlane+5.0f)));
            newShape.Add(worldPoint);
            Debug.DrawLine(previousFrame,worldPoint, Color.red, (iterations/100.0f), false);
            previousFrame = worldPoint;
        }

        if (Input.GetMouseButtonUp(0)) {
            for (var i = 0; i < newShape.Count; i++) {
                if (i != 0) {
                    //Debug.Log(newShape[i-1] + " " + newShape[i]);
                    Debug.DrawLine(newShape[i-1],newShape[i], Color.green, iterations/100.0f, false);
                }
            }
            detectLine();
            iterations = 0.0f;

        }

    }


    void detectLine() {
        float biggesty = 0.0f;
        float smallesty = 0.0f;
        for (var i = 0; i < newShape.Count; i++) {
            if (i == 0) {
                biggesty = newShape[i].y;
                smallesty = newShape[i].y;
            } else {
                if (newShape[i].y > biggesty) {
                    biggesty = newShape[i].y;
                } else if (newShape[i].y < smallesty) {
                    smallesty = newShape[i].y;
                }
            }
        }


        float biggestx = 0.0f;
        float smallestx = 0.0f;
        for (var i = 0; i < newShape.Count; i++) {
            if (i == 0) {
                biggestx = newShape[i].x;
                smallestx = newShape[i].x;
            } else {
                if (newShape[i].x > biggestx) {
                    biggestx = newShape[i].x;
                } else if (newShape[i].x < smallestx) {
                    smallestx = newShape[i].x;
                }
            }
        }

        //Debug.Log("BIGGEST X " + biggestx);
        //Debug.Log("BIGGEST Y " + biggesty);
        //Debug.Log("SMALLEST X " + smallestx);
        //Debug.Log("SMALLEST y " + smallesty);

        float ydiff = biggesty - smallesty;
        float xdiff = biggestx - smallestx;

        //Debug.Log("DIFFERENCE IN Y VALUES " + ydiff);
        //Debug.Log("DIFFERENCE IN X VALUES " + xdiff);

        if ((ydiff < 0.2) || (xdiff < 0.2)) {
            Debug.Log("LINEEE");
        }
        
    }

}
