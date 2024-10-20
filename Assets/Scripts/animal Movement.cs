using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalMovement : MonoBehaviour
{
    public Rigidbody2D animalRB;
    public SpriteRenderer animalSprite;
    public int speed;
    public int choice;
    public int frames =0;
    public int randomFrames=0; // the animal moves a dirction certain dirction for a random amount of frames 
    
    private InteractionObjectModel animalObjectModel = null;


    // Start is called before the first frame update
    void Start()
    {
        animalObjectModel = GetComponent<InteractionObjectModel>();
        //Debug.Log(animalObjectModel);
    
        Debug.Log(animalObjectModel.IsRunningAround);
    }

    // Update is called once per frame
    void Update()
    {
        if(animalObjectModel.IsRunningAround == true)
        {
        if(frames >randomFrames || randomFrames==0 )
        {
            choice =  Random.Range(0,9);
            randomFrames =Random.Range(30,100);
            frames = 0;
        }
        if(choice ==4)
        {
            move(Vector2.right,speed);
        }
         else if(choice ==5)
        {
            move(Vector2.left,speed);
        }
         else if(choice ==6)
        {
            move(Vector2.up,speed);
        }
         else if(choice ==7)
        {
            move(Vector2.down,speed);
        }
         else
       {
            move(Vector2.zero,speed);
       }
       frames++;
       }
       else
       {
        move(Vector2.zero,speed);
       }


        
    }
    private void move(Vector2 direction, int speed)
    {
        animalRB.velocity = direction.normalized * speed * Time.deltaTime * 100f;
        if (animalRB.velocity[0] > 0)
          {
               animalSprite.flipX = true;
          }
        else if (animalRB.velocity[0] < 0)
        {
          animalSprite.flipX = false;
        }
    }
}
