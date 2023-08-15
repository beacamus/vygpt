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
                    Debug.DrawLine(newShape[i-1],newShape[i], new Color(0.2f,0.6f,0.1f), iterations/100.0f, false);
                }
            }
            detectLine();
            detectCircle();
            iterations = 0.0f;

        }

    }

    void detectLine() {
     Vector3 startPt = newShape[0];
     Vector3 dir = newShape[newShape.Count - 1] - newShape[0];
     float len = dir.magnitude;
     Vector3 perp = Vector3.Cross(dir,Vector3.forward).normalized;
     float permit = len * 0.025f;
     matchLine(startPt,perp,permit);
    }

    void matchLine(Vector3 pt0,Vector3 dir,float permit) {
        float max = float.MinValue;
        float min = float.MaxValue;
	
        for (int i = 0; i < newShape.Count; i++) {
	    float dst = Vector3.Dot(dir,newShape[i] - pt0);
            if (dst > max) {
                max = dst;
            } else if (dst < min) {
                min = dst;
            }
        }

        float diff = Mathf.Abs(max - min);

        //Debug.Log("DIFFERENCE IN VALUES " + ydiff);

        if (diff < permit) {
            Debug.Log($"LINEEE in direction {dir} permitted {permit} deviation {diff}");
        }
    }

    void detectCircle() {
	// Get the sum of the vectors and divide by the average to get the origin
	Vector3 sum = Vector3.zero;
        for (int i = 0; i < newShape.Count; i++) {
            sum += newShape[i];
        }
        Vector3 origin = sum / newShape.Count;

//	Tracing to display the radius
//      Debug.DrawLine(origin, newShape[0], new Color(0.2f,0.1f,0.6f), iterations/100.0f, false);

	// Calculate the average radius length
	float totalRadius = 0;
        for (int i = 0; i < newShape.Count; i++) {
            totalRadius += (newShape[i] - origin).magnitude;
        }
        float radius = totalRadius / newShape.Count;

	// If the distance between the origin and the point on the shape is greater than the permitted deviated value - it is not a circle
        bool circle = true;
        int penalties = 0;
        float permit = radius * 0.3f;
        for (int i = 0; i < newShape.Count; i++) {
            float temp_dst = (newShape[i] - origin).magnitude;
            float diff = radius - temp_dst;
            if (Mathf.Abs(diff) > permit) {
                penalties += 1;
                circle = false;
            }
        }
        if (circle) {
            Debug.Log("CIRCLE");
        } else {
            Debug.Log("PENALTIES " + penalties + "/" + newShape.Count);
        }
    }
}
