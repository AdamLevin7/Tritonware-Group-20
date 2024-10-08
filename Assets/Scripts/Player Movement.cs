using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody2D PlayerRB;
    private int speed;
    // Start is called before the first frame update
    void Start()
    {
    speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        // All the movement direction if statements(Adjacently and diagonally)
        if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
       {
            Move(new Vector2(-1,1),speed);
       }
        else if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
       {
            Move(new Vector2(1,1),speed);
       }
        else if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
       {
            Move(new Vector2(-1,-1),speed);
       }
        else if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
       {
            Move(new Vector2(1,-1),speed);
       }
        else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
       {
            Move(Vector2.right,speed);
       }
        else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
       {
            Move(Vector2.left,speed);
       }
        else if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
       {
            Move(Vector2.up,speed);
       }
        else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
       {
            Move(Vector2.down,speed);
       }
        else
       {
            Move(Vector2.zero,speed);
       }
    }
    private void Move(Vector2 direction, int speed)
    {
        PlayerRB.velocity = new Vector2(speed*direction.x, speed*direction.y);
    }
}
