using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;

    // this will change dynamiclly
    Vector3 nextPos; 

    // Start is called before the first frame update
    void Start()
    {
        // set the initial position of the platform and its path
        pos1.position = new Vector3(pos1.position.x, pos1.position.y + 2); 
        pos2.position = new Vector3(pos2.position.x, pos2.position.y + 2); 
        nextPos = startPos.position; 
    }

    // Update is called once per frame
    void Update()
    {
        // move the platfrom with the speed "speed" each frame 
        // 
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        } 
        if(transform.position == pos2.position)
        {
            nextPos = pos1.position; 
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime); 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
