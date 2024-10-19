using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public InventoryModel inventory;
    public string objectName;
    public bool interactable = false;
    public EventSystem eventSystem;
    List<string> animals = new List<string> {"Horse","Cow", "Chicken"};
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (objects.Contains(objectName) && interactable){
            if(Input.GetKey(KeyCode.RightShift)){

                health--;
            }
        }*/
        if (interactable){
            if(Input.GetKey(KeyCode.RightShift)){
                inventory.AddItemByName(objectName, 1);
                if(objectName == "Barn" &&
                 inventory.itemQuantities["wood"] >= 1){
                    health--;
                    inventory.SetItemQuantity("wood", 
                    inventory.itemQuantities["wood"]-1);
                    eventSystem.AGHEvent = false;
                 }
                 if(animals.Contains(objectName)
                 &&inventory.itemQuantities["wheat"] >= 1){
                    health--;
                    inventory.SetItemQuantity("wheat", 
                    inventory.itemQuantities["wheat"]-1);
                    eventSystem.animalEvent = false;
                 }
            }  
        }
        Debug.Log(inventory.itemQuantities["wheat"]);
        if (health <= 0){
            if (transform.GetChild(0).gameObject!= null){
                Destroy(transform.GetChild(0).gameObject);
            }
            health = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        interactable = true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        interactable = false;
    }
}
