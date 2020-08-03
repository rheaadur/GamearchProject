using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : Player
{
    private List<Item> itemList;
    public Text numberofhearts;
    public Text numberofthrow;
    public Text numberofkey;
    private int hearts = 0;
    private int throws= 0;
    private int keys = 0;
    private int usableitem;
    public GameObject throwing;



    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    
    }

    void Update()
    {
        int xDir = (int)(Input.GetAxisRaw("Horizontal"));

        int yDir = (int)(Input.GetAxisRaw("Vertical"));
        useItem(xDir, yDir);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //Check if the tag of the trigger collided with is Food.
        if (other.CompareTag("heal"))
        {
            AddItem(new Item { itemType = Item.ItemType.Heart, Amount =+ 1 });
            hearts = +1;
            //Add pointsPerFood to the players current food total.
            //currentHealth += healCrystal;

            //Disable the food object the player collided with.
            other.gameObject.SetActive(false);
            numberofhearts.text = hearts.ToString();
            //slider.value = currentHealth;
        }

        else if (other.CompareTag("throw"))
        {

            AddItem(new Item { itemType = Item.ItemType.Throwingitem, Amount =+ 1 });
            other.gameObject.SetActive(false);
            Debug.Log(itemList.Count);
            throws = +1;
            numberofthrow.text = throws.ToString();
            
        }

        else if (other.CompareTag("key"))
        {
            AddItem(new Item { itemType = Item.ItemType.Key, Amount =+ 1 });
            other.gameObject.SetActive(false);
            Debug.Log(itemList.Count);
            keys = +1;
            numberofkey.text = keys.ToString();
        }
        
        else if (other.CompareTag("door"))
        {

            if (keys >= 1)
            {
                other.gameObject.SetActive(false);
                Debug.Log(itemList.Count);
                keys = -1;
                numberofkey.text = keys.ToString();
            }
        }
    }

    void chooseItem()
    {
    
    }
    void useItem(int xDir, int yDir)
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Space1");
            if (hearts >= 1)
            {
                currentHealth += healCrystal;
                slider.value = currentHealth;
                hearts -= 1;
                numberofhearts.text = hearts.ToString();
                Debug.Log("Space1");
                
            }
            //itemMenu.SetActive((!itemMenu.gameObject.activeSelf));
        }
        else if (Input.GetKeyDown("k"))
        {
            //Store start position to move from, based on objects current transform position.
            Vector2 start = throwing.transform.position;
            boxCollider.enabled = false;
            if (Input.GetKeyDown("up"))
                xDir = 0;
            //switch()
            //{
            
            //}
                
            
            
            // Calculate end position based on the direction parameters passed in when calling Move.
                Vector2 end = start + new Vector2(xDir, yDir);
            //else if (Input.GetKeyDown("down"))
                //xDir = 0;
                // Calculate end position based on the direction parameters passed in when calling Move.
                //Vector2 end = start + new Vector2(xDir, yDir);
            //else if (Input.GetKeyDown("left"))
                // Calculate end position based on the direction parameters passed in when calling Move.
                //Vector2 end = start + new Vector2(xDir, yDir);
            //else if (Input.GetKeyDown("right"))
                // Calculate end position based on the direction parameters passed in when calling Move.
                //Vector2 end = start + new Vector2(xDir, yDir);

            boxCollider.enabled = true;
            if (throwing.CompareTag("wall"))
            {

            }
            else if (throwing.CompareTag("enemy"))
            {
            
            
            }
        }

    }


    // Start is called before the first frame update


    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
