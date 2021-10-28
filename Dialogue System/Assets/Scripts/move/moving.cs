using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{

    public float moveSpeed;
    // Update is called once per frame
    void Update()
    {
        
        
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector2 (1f, 0f) * moveSpeed *Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector2 (-1f, 0f) * moveSpeed *Time.deltaTime);
            }
        
        
    }
}
