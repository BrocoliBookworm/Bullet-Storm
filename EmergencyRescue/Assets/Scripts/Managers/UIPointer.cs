using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPointer : MonoBehaviour
{
    public Image image;
    public Transform[] target;

    void Awake() 
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < target.Length; i++)
        {
            target[i] = WaveSpawner.Instance().survivorSpawnPoints[i];            
        }
    }

    // Update is called once per frame
    void Update()
    {
        float minX = image.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = image.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        if(!WaveSpawner.Instance().survivorSpawn1 && !WaveSpawner.Instance().survivorSpawn2 && !WaveSpawner.Instance().survivorSpawn3 && !WaveSpawner.Instance().survivorSpawn4)
        {
            Vector2 pos1 = gameObject.transform.position;
            pos1.y = 397f;
            image.transform.position = pos1;
        }

        if(WaveSpawner.Instance().survivorSpawn1)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(target[0].transform.position);

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            image.transform.position = pos;
        }

        if(WaveSpawner.Instance().survivorSpawn2)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(target[1].transform.position);
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            image.transform.position = pos;
        }

        if(WaveSpawner.Instance().survivorSpawn3)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(target[2].transform.position);

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            image.transform.position = pos;
        }

        if(WaveSpawner.Instance().survivorSpawn4)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(target[3].transform.position);

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            image.transform.position = pos;
        }
    }
}
