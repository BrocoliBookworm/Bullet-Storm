using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private bool isScrolling;
    private float rotation;
    // Start is called before the first frame update
    void Start()
    {
        isScrolling = true;
        rotation = gameObject.GetComponent<Transform>().eulerAngles.x;
        Debug.Log("Rotation: " + rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(isScrolling)
        {
            Vector3 currentPosition = gameObject.transform.position;
            Debug.Log("Current Position: " + currentPosition);

            Vector3 incrementPosition = new Vector3(currentPosition.x, currentPosition.y + .01f * Mathf.Sin(Mathf.Deg2Rad * rotation), currentPosition.z);

            Debug.Log("New Position: " + incrementPosition);
            gameObject.transform.position = incrementPosition;
        }
    }
}
